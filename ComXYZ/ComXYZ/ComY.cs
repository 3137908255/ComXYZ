//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.IO.Ports;
//using System.Windows.Forms;


using System;
using System.IO.Ports;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace ComXYZ
{
    [ToolboxBitmap(typeof(ComY),"ComY.bmp")]
    public partial class ComY : Component
    {
        #region///字段定义
        bool RxNewDataFlag = false;
        DataFormatProcess ComDataProcess = new DataFormatProcess();//
        private bool ComPortIsOpen;                                 //定义串口打开状态
        private bool isHexMode = true;                              //定义串口通信模式 二者之一
        int MaxComNum = 50;
        int writeBufferSize = 8192, readBuffSize = 8192;
        //------------------------------------------------------组件接收事件使用的字段
        int ReceiveByteCnt;                                         //本次收到的字节数量
        byte[] ComRxdBuffTemp = new byte[4096];              //临时接收缓存（本次）
        //--------------启用更加安全可靠的队列模式，这里的缓存不再使用-RxHexByteCnt（例外） 环型缓存 接收+发送
        private const UInt32 mRxBuffSize = 10;                    //缓存大小定义为4KB
        byte[] ComRxdBuff = new byte[mRxBuffSize];                //接收缓存
        int RxHexByteCnt;                                           
        UInt32 RxBuffInIndex = 0;
        UInt32 RxBuffOutIndex = 0;
        //--------------------
        Queue ComRxQueue=new Queue();                                           //接收字节数据的队列
        //------------------------------------------------------纯向外输出字段
        UInt32 TxCounter = 0;                                       //发送计数器
        UInt32 RxCounter = 0;                                       //接收计数器 
        string ComRxStringTemp = "";                                //串口收到的字符串
        string OutputAsciiString = "";                              //输出(单纯的字符模式输出)
        string OutputHexString = "";                                //输出（HEX值转换为字符串用于显示）
        string OutputHexWithBlankString = "";                       //输出（HEX值转换为字符串每两个字符插入一个空格用于显示）
        #endregion

        #region///属性定义
        public int MaxComAmount                               //最大端口数量  
        {
            set { MaxComNum = value; }
            get { return MaxComNum; }
        }

        public bool IsOpen
        {
            //set { ComPortIsOpen = value; }
            get { return ComPortIsOpen; }
        }
        /// <summary>
        /// 设置/查询是否为16进制模式
        /// </summary>
        public bool IsHexMode
        {
            set { isHexMode = value; }
            get { return isHexMode; }
        }
        /// <summary>
        /// 获取串口接收到的字节个数
        /// </summary>
        public int ReceiveHexByteCounter
        {
            get
            {
                if (RxHexByteCnt == 0)
                {
                    RxBuffInIndex = 0;
                    RxBuffOutIndex = 0;
                }
                return RxHexByteCnt;
            }
        }


        /// <summary>
        /// 已经过时（不再使用）
        /// </summary>
        public UInt32 RxBuffSize
        {
            //set { mRxBuffSize = value; }
            get { return mRxBuffSize; }
        }
        /// <summary>
        /// 接收计数器
        /// </summary>
        public UInt32 RxCounterVal
        {
            set { RxCounter = value; }          //可用于外部清零
            get { return RxCounter; }
        }
        /// <summary>
        /// 发送计数器
        /// </summary>
        public UInt32 TxCounterVal
        {
            set { TxCounter = value; }          //可用于外部清零
            get { return TxCounter; }           //
        }
        /// <summary>
        /// 原始控件写缓存大小
        /// </summary>
        public int WriteBuffSize
        {
            set 
            { 
                writeBufferSize=value;
                ComPort.WriteBufferSize = writeBufferSize; 
            }
            get { return writeBufferSize; }
        }  
        /// <summary>
        /// 原始控件读缓存大小
        /// </summary>
        public int ReadBuffSize
        {
            set 
            { 
                readBuffSize=value;
                ComPort.ReadBufferSize = readBuffSize; 
            }          
            get { return readBuffSize; }           
        }


        #endregion

        #region///串口参数配置                //供外部调用的配置函数
        /// <summary>
        /// 串口名称，波特率，数据位数量，停止位，校验方式
        /// 备注：此组函数提供外部本符合控件Visible=false时
        /// 由外部控件/组合框或者固定的参数配置串口使用
        /// 比如：一个使用本组合控件的应用仅仅需要修改串口编号
        /// 和波特率即可使用这组函数实现其功能
        /// </summary>
       
        public void SetPortName(string Name)
        {
            ComPort.PortName = Name;
        }
        /// <summary>
        /// 获取串口名称列表（返回串口名称数组）
        /// </summary>
        /// <returns></returns>
        public string[] GetComNameList()
        {
            string[] ComName;
            ComName = SerialPort.GetPortNames();
            return ComName;
        }
        
        
        public void SetPortBaudRate(int BaudRate)
        {
            ComPort.BaudRate = BaudRate;
        }
        public void SetPortDataBits(int Databits)
        {
            ComPort.DataBits = Databits;
        }
        public void SetPortParityBits(Parity Paritybits)
        {
            ComPort.Parity = Paritybits;
        }
        public void SetPortStopBits(StopBits Stopbits)
        {
            ComPort.StopBits = Stopbits;
        }
        /// <summary>
        /// 配置串口名，波特率即可
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="BaudRate"></param>
        public void SetPortPara(string portName, int BaudRate)
        {
            ComPort.PortName = portName;
            ComPort.BaudRate = BaudRate;
        }
        /// <summary>
        /// 配置串口：数据位，校验，停止位
        /// </summary>
        /// <param name="Databits"></param>
        /// <param name="Paritybits"></param>
        /// <param name="Stopbits"></param>
        public void SetPortPara(int Databits, Parity Paritybits, StopBits Stopbits)
        {
            ComPort.DataBits = Databits;
            ComPort.Parity = Paritybits;
            ComPort.StopBits = Stopbits;
        }
        /// <summary>
        /// 配置串口全部参数
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="BaudRate"></param>
        /// <param name="Databits"></param>
        /// <param name="Paritybits"></param>
        /// <param name="Stopbits"></param>
        public void SetPortPara(string portName, int BaudRate, int Databits, Parity Paritybits, StopBits Stopbits)
        {
            ComPort.PortName = portName;
            ComPort.BaudRate = BaudRate;
            ComPort.DataBits = Databits;
            ComPort.Parity = Paritybits;
            ComPort.StopBits = Stopbits;
        }
        /// <summary>
        /// 设置串口参数
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="BaudRate"></param>
        /// <param name="Databits"></param>
        /// <param name="Paritybits">0 无校验 1 奇校验 2偶校验</param>
        /// <param name="Stopbits"></param>
        public void SetPortPara(string portName, int BaudRate, int Databits, int Paritybits, int Stopbits)
        {
            Parity comParity = Parity.None; 
            StopBits ComStopbit = StopBits.One; 
            switch(Paritybits)
            {
                case 0:comParity = Parity.None;//无校验
                    break;
                case 1: comParity = Parity.Odd;//奇校验
                    break;
                case 2: comParity = Parity.Even;//偶校验
                    break;
                default :comParity = Parity.None; 
                    break;
            }
            switch(Paritybits)
            {
                case 0: ComStopbit = StopBits.None; 
                    break;
                case 1:ComStopbit = StopBits.One; 
                    break;
                case 2: ComStopbit = StopBits.Two; 
                    break;
                default :ComStopbit = StopBits.One; 
                    break;
            }
            ComPort.PortName = portName;
            ComPort.BaudRate = BaudRate;
            ComPort.DataBits = Databits;
            ComPort.Parity = comParity;
            ComPort.StopBits = ComStopbit;
        }
        public void Open()
        {
            if (ComPortIsOpen == false)
            {
                try
                {
                    ComPort.Encoding = System.Text.Encoding.GetEncoding("GB2312");//此行非常重要 可解决接收中文乱码问题
                    ComPort.Open();
                    ComPortIsOpen = true;
                }
                catch (Exception)
                {
                    MessageBox.Show("该串口编号非法或者已经被其他程序使用！", "提示");
                }
            }
        }

        public void Close()
        {
            if (ComPortIsOpen == true)
            {
                ComPort.Close();
                ComPortIsOpen = false;
            }
        }

        #endregion

        #region///组件自动添加的方法
        public ComY()
        {
            InitializeComponent();
            //ComRxQueue.Clear();
        }

        public ComY(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        #endregion

        #region///发送接收计数器增加
        public void RxCounterAdd(UInt32 AddVal)
        {
            RxCounter += AddVal;
        }
        public void TxCounterAdd(UInt32 AddVal)
        {
            TxCounter += AddVal;
        }
        #endregion

        #region///定义串口接收数据事件部分


        private void ComPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            ReceiveByteCnt = ComPort.BytesToRead;
            if (isHexMode == false)
            {
                ComRxStringTemp = ComPort.ReadExisting();
                OutputAsciiString += ComRxStringTemp;
                RxCounterAdd((UInt32)ComRxStringTemp.Length);
                RxNewDataFlag = true;
            } 
            else
            {
                /* 20180127注释掉
                ComPort.Read(ComRxdBuffTemp, 0, ReceiveByteCnt);
                byte[] bBuff = new byte[ReceiveByteCnt];
                for (Int32 i = 0; i < ReceiveByteCnt; i++) bBuff[i] = ComRxdBuffTemp[i];
                RxCounterAdd((UInt32)bBuff.Length);
                PutByteArrToRxBuff(bBuff, ReceiveByteCnt);
                OutputHexString += ComDataProcess.ConvertHexToString(bBuff);
                RxNewDataFlag = true;
                */
                byte[] RxBuffTemp = new byte[ReceiveByteCnt];
                ComPort.Read(RxBuffTemp, 0, ReceiveByteCnt);
                RxCounterAdd((UInt32)ReceiveByteCnt);
                PutByteArrToRxBuff(RxBuffTemp, ReceiveByteCnt);
                OutputHexString += ComDataProcess.ConvertHexToString(RxBuffTemp);
                RxNewDataFlag = true;
            }
        }
        #endregion

        #region //获取数据更新标志外部使用查询模式接收数据
        /// <summary>
        /// 串口控件收到新数据设置为TRUE
        /// </summary>
        /// <returns></returns>
        public bool IsGotNewData()
        {
            return RxNewDataFlag;
        }
        /// <summary>
        /// 强制清空接收16进制数据缓存
        /// </summary>
        public void ForceClearHexBuffer()
        {
            RxHexByteCnt = 0;
            RxBuffInIndex = 0;
            RxBuffOutIndex = 0;
            //for (int i = 0; i < mRxBuffSize; i++) ComRxdBuff[i]=0;
            ComRxQueue.Clear();
            RxNewDataFlag = false;                  //数据提取完毕之后清零这个标志
        }
        #endregion

        #region///通用发送函数
        /// <summary>
        /// 发送中文与字符
        /// </summary>
        /// <param name="cmdLine"></param>
        private void ComSendAsciiString(string cmdLine)//发送ASCII代码专用函数 包括中文
        {
            if (cmdLine == null)
                return;
            if (cmdLine.Length > 0)
            {
                if (ComPort.IsOpen == true)
                {
                    byte[] SendBytes = null;
                    System.Text.Encoding chs = System.Text.Encoding.GetEncoding("gb2312");
                    byte[] bytes = chs.GetBytes(cmdLine);
                    string str = "";
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        str += string.Format("{0:X2}", bytes[i]);
                    }
                    List<string> SendDataList = new List<string>();
                    for (int i = 0; i < str.Length; i = i + 2)
                    {
                        SendDataList.Add(str.Substring(i, 2));
                    }
                    SendBytes = new byte[SendDataList.Count];
                    for (int j = 0; j < SendBytes.Length; j++)
                    {
                        SendBytes[j] = (byte)(Convert.ToInt32(SendDataList[j], 16));
                    }

                    ComPort.Write(SendBytes, 0, SendBytes.Length);//发送数据
                }
                else
                {
                    MessageBox.Show("请打开串口！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// 发送HEX数据
        /// </summary>
        /// <param name="SendText"></param>
        private void ComSendHexString(string SendText)
        {
            byte[] buffer = ComDataProcess.ConvertStringToHex(SendText);     //转换字符串为HEX的数组
            ComPort.Write(buffer, 0, buffer.Length);
            TxCounterAdd((UInt32)buffer.Length);
        }
        /// <summary>
        /// 通过串口发送字符串(由属性控制模式：16进制还是字符)
        /// </summary>
        /// <param name="SendText">要发送的字符串</param>
        /// <returns></returns>
        public bool Send(string SendText)
        {
            if (ComPortIsOpen == true)
            {
                if (isHexMode == true)
                {
                    ComSendHexString(SendText.Trim());
                }
                else
                {
                    TxCounterAdd((UInt32)SendText.Length);
                    ComSendAsciiString(SendText);
                }
            }
            else
            {
                MessageBox.Show("请打开串口","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            
            return true;
        }
        /// <summary>
        /// 直接指定发送模式（16进制或者字符）
        /// </summary>
        /// <param name="HexMode"></param>
        /// <param name="SendText"></param>
        /// <returns></returns>
        public bool Send(bool HexMode, string SendText)
        {
            if (ComPortIsOpen == true)
            {
                if (HexMode == true)
                {
                    ComSendHexString(SendText.Trim());
                }
                else
                {
                    TxCounterAdd((UInt32)SendText.Length);
                    ComSendAsciiString(SendText);
                }
            }
            else
            {
                MessageBox.Show("请打开串口", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
            return true;
        }
        /// <summary>
        /// 直接发送16进制字节，HexMode=true
        /// </summary>
        /// <param name="HexMode"></param>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        public void SendHex(bool HexMode, byte[] buffer, int offset, int count)
        {
            if (ComPortIsOpen == true)
            {
                if(HexMode == true)
                {
                    ComPort.Write(buffer,offset,count);
                    TxCounterAdd((UInt32)count); 
                }
                else
                    MessageBox.Show("HexMode必须为true", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("请打开串口", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region///复合控件 将接收的数据向外部提供数据接口
        //（无论是HEX模式还是ASCII模式均有效）
        //在HEX模式是会将收到的数据保存到RX缓冲中同时将数据转换为ASCII码，向外输出
        /// <summary>
        /// 向外输出字符串,数据提取后缓存被清零
        /// 使用于ASCII模式，用于外部显示所接收到的数据
        /// </summary>
        /// <returns></returns>
        public string GetRxAsciiString()
        {
            string temp = OutputAsciiString;
            OutputAsciiString = "";
            RxNewDataFlag = false;                  //数据提取完毕之后清零这个标志
            return temp;
        }
        /// <summary>
        /// 向外输出字符串,数据提取后缓存被清零
        /// 使用HEX模式，用于外部显示所接收到的数据
        /// </summary>
        /// <returns></returns>
        public string GetRxHexString()
        {
            string temp = OutputHexString;
            OutputHexString = "";
            RxNewDataFlag = false;                  //数据提取完毕之后清零这个标志
            return temp;
        }
        /// <summary>
        /// 向外输出字符串,数据提取后缓存被清零
        /// 使用HEX模式，每两个个HEX数据插入一个空格，用于外部显示所接收到的数据
        /// 备注：与GetRxHexString()同时有效
        /// </summary>
        /// <returns></returns>
        public string GetRxHexWithBlankString()
        {
            string temp;
            OutputHexWithBlankString = ComDataProcess.HexStringInsertBlank(OutputHexString) + " ";
            temp = OutputHexWithBlankString;
            OutputHexWithBlankString = "";
            OutputHexString = "";
            RxNewDataFlag = false;                  //数据提取完毕之后清零这个标志
            return temp;
        }
        /// <summary>
        /// 不区分ASCII还是HEX字符串（属性控制模式）
        /// </summary>
        /// <returns></returns>
        public string GetRxString()
        {
            if (isHexMode == true)
            {
                return GetRxHexString();
            }
            else
            {
                return GetRxAsciiString();
            }
        }
        /// <summary>
        /// 不区分ASCII还是HEX字符串(HEX插入空格)（属性控制模式） 
        /// </summary>
        /// <returns></returns>
        public string GetRxStringAndHexWithBlank()
        {
            if (isHexMode == true)
            {
                return GetRxHexWithBlankString();
            }
            else
            {
                return GetRxAsciiString();
            }
        }
        /// <summary>
        /// 清空四缓存之一     ASCII字符缓存
        /// </summary>
        public void ClearRxAsciiStringBuff()
        {
            OutputAsciiString = "";
            RxNewDataFlag = false;                  //数据提取完毕之后清零这个标志
        }
        /// <summary>
        /// 清空四缓存之一     HEX字符串缓存
        /// </summary>
        public void ClearRxHexStringBuff()
        {
            OutputHexString = "";
            RxNewDataFlag = false;                  //数据提取完毕之后清零这个标志
        }
        /// <summary>
        /// 清空四缓存之一    HEX字符+空格缓存
        /// </summary>
        public void ClearRxHexWithBlankStringBuff()
        {
            OutputHexWithBlankString = "";
            RxNewDataFlag = false;                  //数据提取完毕之后清零这个标志
        }
        /// <summary>
        /// 清空四缓存之一     接收数据缓存
        /// </summary>
        public void ClearHexDataBuff()
        {
            ComRxQueue.Clear();
            RxNewDataFlag = false;                  //数据提取完毕之后清零这个标志
        }
        /// <summary>
        /// 清空四缓存
        /// </summary>
        public void ClearAllBuff()
        {
            OutputAsciiString = "";
            OutputHexString = "";
            OutputHexWithBlankString = "";
            ComRxQueue.Clear();
            RxNewDataFlag = false;                  //数据提取完毕之后清零这个标志
        }

        #endregion


        #region         // 提供给外部用于HEX数据分析
        /// <summary>
        /// 获取串口接收缓存中有多少个数据
        /// </summary>
        /// <returns></returns>
        public int GetRxByteCount()
        {
            return ComRxQueue.Count;
        }

        /// <summary>
        /// 有接收数据帧中断调用正式使用时改为私有
        /// </summary>
        /// <param name="buff"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private bool PutByteArrToRxBuff(byte[] buff, int count)
        {

            UInt32 i = 0;
            /*
            if (count > (mRxBuffSize - RxHexByteCnt))
                return false;
            else
            {
                for (i = 0; i < count; i++) ComRxdBuff[RxBuffInIndex++] = buff[i];
                RxHexByteCnt += count;
                if (RxBuffInIndex >= mRxBuffSize) RxBuffInIndex = 0;
                return true;
            }
            */
            
            for(i=0;i<count;i++)
            {
                ComRxQueue.Enqueue((byte)buff[i]);
            }
            RxHexByteCnt += count;
            return true;


        }

        /// <summary>
        /// 提供给外部用于HEX数据分析
        /// 数据帧提取
        /// </summary>
        /// <param name="buff">读取数据缓存</param>
        /// <param name="count">要读取的字节个数,返回时表示读到的个数</param>
        /// <returns>true 数据可用，false 无可用数据</returns>
        public bool GetHexArrayFromRxBuff(byte[] buff, ref int count)
        {
            Int32 i = 0;
            /*
            if (count > RxHexByteCnt) count = RxHexByteCnt;
            if (RxHexByteCnt > 0)
            {
                for (i = 0; i < count; i++) buff[i] = ComRxdBuff[RxBuffOutIndex++];
                RxHexByteCnt -= count;
                if (RxBuffOutIndex >= mRxBuffSize) RxBuffOutIndex = 0;
                if (RxHexByteCnt==0) RxNewDataFlag = false;                  //数据提取完毕之后清零这个标志
                return true;
            }
            else
                return false;
            */

            if (count > RxHexByteCnt) count = RxHexByteCnt;
            for (i = 0; i < count; i++)
            {
                buff[i] = (byte)ComRxQueue.Dequeue();
            }
            RxHexByteCnt -= count;
            if(RxHexByteCnt==0)
                RxNewDataFlag = false;                  //数据提取完毕之后清零这个标志
            return true;

        }

        #endregion


        #region //以下重写serialPort属性和方法

        public int ReceivedBytesThreshold
        {
            get { return ComPort.ReceivedBytesThreshold; }
            set { ComPort.ReceivedBytesThreshold = value; }
        }
        /// <summary>
        /// ReadLine()/WriteLine()方法使用这个属性
        /// </summary>
        public string NewLineWord
        {
            get { return ComPort.NewLine; }
            set { ComPort.NewLine = value; }
        }
        #endregion




    }
}
