using System;
using System.IO.Ports;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ComXYZ
{
    [ToolboxBitmap(typeof(ComX), "ComX.bmp")]
    public partial class ComX : UserControl
    {
        #region///字段定义
        DataFormatProcess ComDataProcess = new DataFormatProcess();//
        private  bool ComPortIsOpen;                            //定义串口打开状态
        private bool isHexMode=true;                            //定义串口通信模式 二者之一
        int MaxComNum = 50;
        //------------------------------------------------------组件接收事件使用的字段
        int ReceiveByteCnt;
        byte[] ComRxdBuffTemp = new byte[4096];                //临时接收缓存
        //-----------------------------------------------------环型缓存 接收+发送
        private const UInt32 mRxBuffSize = 4096;               //缓存大小定义为4KB
        byte[] ComRxdBuff = new byte[mRxBuffSize];             //接收缓存
        int RxHexByteCnt;
        UInt32 RxBuffInIndex = 0;
        UInt32 RxBuffOutIndex = 0;
        //------------------------------------------------------纯向外输出字段
        UInt32 TxCounter = 0;                                  //发送计数器
        UInt32 RxCounter = 0;                                  //接收计数器 
        string ComRxString = "";                                //串口收到的字符串
        string OutputAsciiString = "";                          //输出(单纯的字符模式输出)
        string OutputHexString = "";                            //输出（HEX值转换为字符串用于显示）
        string OutputHexWithBlankString = "";                   //输出（HEX值转换为字符串每两个字符插入一个空格用于显示）
        #endregion

        #region///属性定义
        public int MaxComAmount                               //最大端口数量  
        {
            set { MaxComNum = value; }
            get { return MaxComNum; }
        }

        public bool ComOpen
        {
            //set { ComPortIsOpen = value; }
            get { return ComPortIsOpen;} 
        }
        public bool IsHexMode
        {
            set { isHexMode = value; }
            get { return isHexMode; }
        }

        public int ReceiveHexByteCounter
        {
            //set { mRxBuffSize = value; }
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

        public UInt32 RxBuffSize
        {
            //set { mRxBuffSize = value; }
            get { return mRxBuffSize; }
        }

        public UInt32 RxCounterVal
        {
            set { RxCounter = value; }          //可用于外部清零
            get { return RxCounter; }
        }

        public UInt32 TxCounterVal
        {
            set { TxCounter = value; }          //可用于外部清零
            get { return TxCounter; }           //
        }
        #endregion

        #region///复合控件界面控件设置
        /// <summary>
        /// 查询本机串口编号，串口编号，波特兰，位数量，
        /// </summary>
        private void CheckLocalPCcomNum()
        {
            string[] ComName;
            if (checkBoxComNum.Checked == true)
            {
                comboBoxComNum.Text = "";
                comboBoxComNum.Items.Clear();
                comboBoxComNum.BeginUpdate();
                //string[] ports = SerialPort.GetPortNames();
                ComName = SerialPort.GetPortNames();
                foreach (string port in ComName)
                {
                    comboBoxComNum.Items.Add(port);
                }
                comboBoxComNum.SelectedIndex = 0;
                comboBoxComNum.EndUpdate();
            }
            else
            {
                InitComNum();
            }
        }

        private void checkBoxComNum_CheckedChanged(object sender, EventArgs e)
        {
            CheckLocalPCcomNum();
        }

        private void InitComNum()
        {
            comboBoxComNum.Items.Clear();
            for (int i = 1; i < MaxComNum+1; i++)
                comboBoxComNum.Items.Add("COM" + i.ToString());
            comboBoxComNum.Text = "COM1";                           //每次启动完显示COM1
        }
        
        private void InitComBps()
        {
            comboBoxBps.Items.Add("300");
            comboBoxBps.Items.Add("600");
            comboBoxBps.Items.Add("1200");
            comboBoxBps.Items.Add("2400");
            comboBoxBps.Items.Add("4800");
            comboBoxBps.Items.Add("9600");
            comboBoxBps.Items.Add("14400");
            comboBoxBps.Items.Add("19200");
            comboBoxBps.Items.Add("28000");
            comboBoxBps.Items.Add("38400");
            comboBoxBps.Items.Add("57600");
            comboBoxBps.Items.Add("115200");
            comboBoxBps.Items.Add("128000");
            comboBoxBps.Items.Add("256000");
        }
        
        private void InitComDataBits()
        {
            comboBoxDataBits.Items.Add("5");
            comboBoxDataBits.Items.Add("6");
            comboBoxDataBits.Items.Add("7");
            comboBoxDataBits.Items.Add("8");
        }
        
        private void InitComParity()
        {
            comboBoxParity.Items.Add("无");
            comboBoxParity.Items.Add("奇校验");
            comboBoxParity.Items.Add("偶校验");
        }
        
        private void InitComStopBits()
        {
            comboBoxStopBits.Items.Add("1");
            comboBoxStopBits.Items.Add("1.5");
            comboBoxStopBits.Items.Add("2");
        }
        
        private void InitComDataMode()
        {
            radioBtnModeHex.Checked = true;
        }
        #endregion

        #region///串口初始化
        private void InitComDispText()
        {
            comboBoxComNum.Text = "COM1";
            comboBoxBps.Text = "9600";
            comboBoxDataBits.Text = "8";
            comboBoxParity.SelectedIndex = 0;
            comboBoxStopBits.SelectedIndex = 0;

        }
       
        private void InitComButton()
        {
            CloseCom.Enabled = false;
            checkBoxComNum.Checked = true;
        }

        private void EnableComButton()                  //关闭串口时调用
        {
            CloseCom.Enabled = false;
            OpenCom.Enabled = true;
            checkBoxComNum.Enabled = true;
            comboBoxComNum.Enabled = true;
            comboBoxBps.Enabled = true;
            comboBoxDataBits.Enabled = true;
            comboBoxParity.Enabled = true;
            comboBoxStopBits.Enabled = true;
        }
       
        private void DisableComButton()                 //打开串口时调用
        {
            CloseCom.Enabled = true;
            OpenCom.Enabled = false;
            checkBoxComNum.Enabled = false;
            comboBoxComNum.Enabled = false;
            comboBoxBps.Enabled = false;
            comboBoxDataBits.Enabled = false;
            comboBoxParity.Enabled = false;
            comboBoxStopBits.Enabled = false;
        }
      
        private void InitCom()
        {
            InitComNum();
            InitComBps();
            InitComDataBits();
            InitComParity();
            InitComStopBits();
            InitComDispText();
            InitComDataMode();
            InitComButton();
            CheckLocalPCcomNum();
            //comboBoxComNum.Text = "COM5";//临时调试语句
        }
        public ComX()
        {
            InitializeComponent();
            InitCom();
        }
        #endregion


        #region///串口参数配置 外部实现串口参数界面设计时使用（复合控件不可见时使用）
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

        public void SetPortOpen()
        {
            if (ComPortIsOpen == false)
            {
                try
                {
                    ComPort.Encoding = System.Text.Encoding.GetEncoding("GB2312");//此行非常重要 可解决接收中文乱码问题
                    ComPort.Open();
                    ComPortIsOpen = true;
                    //DisableComButton();
                }
                catch (Exception)
                {
                    MessageBox.Show("该串口编号非法或者已经被其他程序使用！", "提示");
                }
            }
        }

        public void SetPortClose()
        {
            if (ComPortIsOpen == true)
            {
                ComPort.Close();
                ComPortIsOpen = false;
                //EnableComButton();
            }
        }

        #endregion

        #region///基于组合控件可视为真时的串口配置及打开关闭
        public void PortSet()                           //由组合控件设置端口参数
        {
            //ComPort = new SerialPort();
            ComPort.Encoding = System.Text.Encoding.GetEncoding("GB2312");//此行非常重要 可解决接收中文乱码问题
            ComPort.PortName = comboBoxComNum.Text.Trim();
            ComPort.BaudRate = Convert.ToInt32(comboBoxBps.Text.Trim());
            ComPort.DataBits = Convert.ToInt16(comboBoxDataBits.Text.Trim());
            string s = comboBoxParity.Text.Trim();
            if (s.Equals("无") == true) ComPort.Parity = Parity.None;
            else if (s.Equals("偶校验") == true) ComPort.Parity = Parity.Even;
            else if (s.Equals("奇校验") == true) ComPort.Parity = Parity.Odd;
            else ComPort.Parity = Parity.None;
            //ComPort.Parity = Parity.Space;
            //ComPort.Parity = Parity.Mark;
            float f = Convert.ToSingle(comboBoxStopBits.Text.Trim());
            if (f == 0) ComPort.StopBits = StopBits.None;
            else if (f == 1) ComPort.StopBits = StopBits.One;
            else if (f == 1.5) ComPort.StopBits = StopBits.OnePointFive;
            else if (f == 2) ComPort.StopBits = StopBits.Two;
            else ComPort.StopBits = StopBits.One;
        
        }

        public void PortOpen()
        { 
            if (ComPortIsOpen == false)
            {
                try
                {
                    ComPort.Open();
                    ComPortIsOpen = true;
                    DisableComButton();
                }
                catch (Exception)
                {
                    MessageBox.Show("该串口编号非法或者已经被其他程序使用！", "提示");
                }
            }
        }

        private void OpenCom_Click(object sender, EventArgs e)
        {
            PortSet();
            PortOpen();
        }

        public void PortClose()
        {
            if (ComPortIsOpen == true)
            {
                ComPort.Close();
                ComPortIsOpen = false;
                EnableComButton();
            }
        }

        private void CloseCom_Click(object sender, EventArgs e)
        {
            PortClose();
        }

        private void radioBtnModeHex_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtnModeHex.Checked == true) isHexMode = true;
        }

        private void radioBtnModeAscii_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtnModeAscii.Checked == true) isHexMode = false;
        }

        #endregion

        #region///发送接收计数器增加
        private void RxCounterAdd(UInt32 AddVal)
        {
             RxCounter += AddVal;
        }
        private void TxCounterAdd(UInt32 AddVal)
        {
            TxCounter += AddVal;
        }
        #endregion

        #region///定义串口接收数据事件部分

        //public event EventHandler<ComEventArgs> ReceiveDataEvent;

        //protected virtual void OnReceiveDataEvent(ComEventArgs e)
        //{
        //    EventHandler<ComEventArgs> handler = ReceiveDataEvent;
        //    if (handler != null)
        //    {
        //        handler(this, e);
        //    }
        //}


        protected virtual void ComPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            ReceiveByteCnt = ComPort.BytesToRead;
            if (isHexMode == false)
                ComRxString = ComPort.ReadExisting();
            else
            {
                ComPort.Read(ComRxdBuffTemp, 0, ReceiveByteCnt);
            }
            this.Invoke(new EventHandler(GetComPortData));
        }

        private void GetComPortData(object sender, EventArgs e)
        {
            //ComEventArgs RxData = new ComEventArgs();
            if (isHexMode == true)
            {

                //RxData.RxString = ComRxStr;
                byte[] bBuff = new byte[ReceiveByteCnt];
                for (Int32 i = 0; i < ReceiveByteCnt; i++) bBuff[i] = ComRxdBuffTemp[i];
                RxCounterAdd((UInt32)bBuff.Length);
                PutByteArrToRxBuff(bBuff, ReceiveByteCnt);
                //有接收数据帧中断调用正式使用时改为私有
                OutputHexString += ComDataProcess.ConvertHexToString(bBuff);
                //OutputHexWithBlankString = ComDataProcess.HexStringInsertBlank(OutputHexString) + " ";
                //OnReceiveDataEvent(new ComEventArgs(ComRxString));
                //发起事件的函数必须在准备好所有向外输出的参数之后再引发事件
            }
            else
            {
                OutputAsciiString += ComRxString;
                RxCounterAdd((UInt32)ComRxString.Length);           
                //输出缓存会因为前面数据未来得及取走而被后面数据覆盖
                //OnReceiveDataEvent(new ComEventArgs(ComRxString));
                //发起事件的函数必须在准备好所有向外输出的参数之后再引发事件
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
            return temp;
        }
        public void ClearRxAsciiStringBuff()
        {
            OutputAsciiString = "";
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
            return temp;
        }
        public void ClearRxHexStringBuff()
        {
            OutputHexString = "";
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
            return temp;
        }
        public void ClearRxHexWithBlankStringBuff()
        {
            OutputHexWithBlankString = "";
        }
        #endregion

        #region///通用发送函数

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


        private void ComSendHexString(string SendText)
        {
            byte[] buffer = ComDataProcess.ConvertStringToHex(SendText);     //转换字符串为HEX的数组
            ComPort.Write(buffer, 0, buffer.Length);
            TxCounterAdd((UInt32)buffer.Length);
        }
        /// <summary>
        /// 通过串口发送字符串
        /// </summary>
        /// <param name="SendText">要发送的字符串</param>
        /// <returns></returns>
        public bool Send(string SendText)
        {
            if (isHexMode == true)
            {
                ComSendHexString(SendText.Trim());
            }
            else
            {
                TxCounterAdd((UInt32)SendText.Length);
                //ComPort.Write(SendText);
                ComSendAsciiString(SendText);
            }
            return true;
        }

        public bool Send(bool HexMode,string SendText)
        {
            if (HexMode == true)
            {
                ComSendHexString(SendText.Trim());
            }
            else
            {
                TxCounterAdd((UInt32)SendText.Length);
                //ComPort.Write(SendText);
                ComSendAsciiString(SendText);
            }
            return true;
        }
        #endregion




        #region        // 提供给外部用于HEX数据分析

        /// <summary>
        /// 有接收数据帧中断调用正式使用时改为私有
        /// </summary>
        /// <param name="buff"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private bool PutByteArrToRxBuff(byte[] buff, int count)
        {
            UInt32 i = 0;
            if (count > (mRxBuffSize - RxHexByteCnt)) 
                return false;
            else
            {
                for (i = 0; i < count; i++) ComRxdBuff[RxBuffInIndex++] = buff[i];
                RxHexByteCnt += count;
                if (RxBuffInIndex >= mRxBuffSize) RxBuffInIndex = 0;
                return true;
            }
        }
        
        /// <summary>
        /// 提供给外部用于HEX数据分析
        /// 数据帧提取
        /// </summary>
        /// <param name="buff">读取数据缓存</param>
        /// <param name="count">要读取的字节个数,返回时表示读到的个数</param>
        /// <returns></returns>
        public bool GetHexArrayFromRxBuff(byte[] buff, ref int count)
        {
            Int32 i = 0;
            byte[] bArr = new byte[count];
            if (count > RxHexByteCnt) count = RxHexByteCnt;
            if (RxHexByteCnt > 0)
            {
                for (i = 0; i < count; i++)buff[i] = ComRxdBuff[RxBuffOutIndex++];
                RxHexByteCnt -= count;
                if (RxBuffOutIndex >= mRxBuffSize) RxBuffOutIndex = 0;
                return true;
            }
            else 
                return false;
        }
        
        #endregion



        #region //以下重写serialPort属性和方法

        private int ReceivedBytesThreshold 
        {
            get { return ComPort.ReceivedBytesThreshold; }
            set { ComPort.ReceivedBytesThreshold = value; } 
        }
        /// <summary>
        /// ReadLine()/WriteLine()方法使用这个属性
        /// </summary>
        private string NewLineWord
        {
            get { return ComPort.NewLine; }
            set { ComPort.NewLine = value; }
        }

        private int BytesToRead
        {
            get { return ComPort.BytesToRead; } 
        }

        private int BytesToWrite 
        { 
            get{return ComPort.BytesToWrite;} 
        }

        #region///串口读数据
        //public int Read(byte[] buffer, int offset, int count)
        //{
        //    return ComPort.Read(buffer, offset,count);
        //}

        //public int Read(char[] buffer, int offset, int count)
        //{
        //    return ComPort.Read(buffer, offset,count);
        //}

        //public string ReadTo(string value)
        //{
        //    return ComPort.ReadTo(value);
        //}

        //public int ReadByte()
        //{
        //    return ComPort.ReadByte();
        //}

        //public int ReadChar()
        //{
        //    return ComPort.ReadChar();
        //}

        //public string ReadLine()
        //{
        //    return ComPort.ReadLine();        
        //}

        //public string ReadExisting()
        //{
        //    return ComPort.ReadExisting();
        //}
        #endregion

        /// <summary>
        /// 重写serialPort发送数据函数
        /// </summary>
        private void Write(string text)
        {
            ComPort.Write(text);
        }
        private void WriteLine(string text)
        {
            ComPort.WriteLine(text);
        }
        private void Write(byte[] buffer, int offset, int count)
        {
            ComPort.Write(buffer,offset,count);
        }
        private void Write(char[] buffer, int offset, int count)
        {
            ComPort.Write(buffer, offset, count);
        }
        #endregion
        /*******************************************************************/
        #region///定义数据处理***以下定义数据处理功能方法****
        /*
        private byte HexToAsciiHigh(byte BVal)
        {
            byte h;
            h = (byte)(BVal & (byte)0xf0);
            h >>= 4;
            if (h >= 0 && h <= 9) h = (byte)((byte)0x30 + h);
            if (h >= 10 && h <= 15)
            {
                h = (byte)(h - (byte)0x0A);
                h = (byte)((byte)0x41 + h);
            }
            return h;
        }

        private byte HexToAsciiLow(byte BVal)
        {
            byte l;
            l = (byte)(BVal & (byte)0x0f);
            if (l >= 0 && l <= 9) l = (byte)((byte)0x30 + l);
            if (l >= 10 && l <= 15)
            {
                l = (byte)(l - (byte)0x0A);
                l = (byte)((byte)0x41 + l);
            }
            return l;
        }

        private byte AsciiToHex(byte Val)
        {
            byte temp = 0;
            temp = Val;
            if (temp >= 0x30 && temp <= 0x39) temp &= (byte)0x0f;
            else if (temp >= 0x41 && temp <= 0x46) temp -= (byte)0x37;//大写ABCDEF转换成16进制数值
            else if (temp >= 0x61 && temp <= 0x66) temp -= (byte)0x57;//小写abcdef转换成16进制数值
            else temp = 0;
            return temp;
        }

        private String GetHexString(string InputStr)                //从输入字符串中提取仅包含HEX数据的字符串(字母转换为大写)
        {
            string strOnlyHexChar = "";
            for (Int32 i = 0; i < InputStr.Length; i++)
            {
                if (Uri.IsHexDigit(InputStr[i]))
                {
                    strOnlyHexChar += InputStr[i];
                }
            }
            return strOnlyHexChar.ToUpper();
        }

        public String HexStringInsertBlank(string OnlyHexCharInputStr) //先输入的HEX字符串字符之间插入空格
        {
            string strHexWithBlank = "";
            for (Int32 i = 0; i < OnlyHexCharInputStr.Length; i++)
            {
                if (i % 2 == 0)
                {
                    strHexWithBlank += ' ';
                }
                strHexWithBlank += OnlyHexCharInputStr[i];
            }
            strHexWithBlank = strHexWithBlank.Trim();
            return strHexWithBlank;
        }

        private byte GetHexValFromChar(char InputChar)
        {
            byte ByteVal = 0;
            string temp = "";
            string str1 = "";
            byte CharVal = 0;
            temp = Uri.HexEscape(InputChar);
            for (int j = 1; j < 3; j++) str1 += temp[j];
            CharVal = Convert.ToByte(str1);
            if (CharVal >= 30 && CharVal <= 39) ByteVal = (byte)(CharVal - 30);
            if (CharVal >= 41 && CharVal <= 46) ByteVal = (byte)(CharVal - 31);
            return ByteVal;
        }

        private byte ConverByteToChar(byte dat)
        {
            byte bTemp = 0;
            if (dat >= 0 && dat <= 9) bTemp = (byte)(30 + dat);
            if (dat >= 10 && dat <= 15) bTemp = (byte)(41 + dat - 10);
            return bTemp;
        }

        public string ConvertHexToString(byte[] InputDataArr)
        {
            string OutputStr = "";
            Int32 i = 0;
            byte CharVal1 = 0;
            byte bTemp1 = 0;
            string StrTemp = "%";
            int index = 0;
            for (i = 0; i < InputDataArr.Length; i++)
            {
                CharVal1 = InputDataArr[i];
                CharVal1 >>= 4;
                bTemp1 = ConverByteToChar(CharVal1);
                StrTemp = "%";
                StrTemp += bTemp1.ToString();
                index = 0;
                OutputStr += Uri.HexUnescape(StrTemp, ref index);

                CharVal1 = InputDataArr[i];
                CharVal1 &= (byte)0x0F;
                bTemp1 = ConverByteToChar(CharVal1);
                StrTemp = "%";
                StrTemp += bTemp1.ToString();
                index = 0;
                OutputStr += Uri.HexUnescape(StrTemp, ref index);
            }
            return OutputStr;
        }

        public byte[] ConvertStringToHex(string InputStr)     //转换字符串为HEX的数组
        {
            string strHexchar = GetHexString(InputStr);
            if (strHexchar.Length % 2 != 0) strHexchar += "0";//单数个的字符时最后增加一个0
            byte[] bArr = new byte[strHexchar.Length / 2];
            Int32 ByteCnt = 0;
            byte ByteVal = 0;
            for (Int32 i = 0; i < strHexchar.Length; i++)
            {
                ByteVal = GetHexValFromChar(strHexchar[i++]);
                ByteVal <<= 4;
                bArr[ByteCnt++] = (byte)(ByteVal + GetHexValFromChar(strHexchar[i]));
            }
            return bArr;
        }
        
        public byte[] ConvertStringToHex(string SourceStr, ref int ByteNumber)//输入为16进制数据字符形式字符串
        {
            byte[] byteBuff = new byte[1024];
            byte[] byteTemp = new byte[2];
            char temp = ' ';
            int j = 0, k = 0;
            for (int i = 0; i < SourceStr.Length; i++)
            {
                temp = SourceStr.ElementAtOrDefault<char>(i);
                if (temp != ' ')
                {
                    byteTemp[k++] = Convert.ToByte(temp);
                    if (k >= 2)
                    {
                        byteBuff[j++] = (byte)((AsciiToHex(byteTemp[0]) << (byte)4) + AsciiToHex(byteTemp[1]));
                        k = 0;
                        ByteNumber++;
                    }
                    else if (i == SourceStr.Length - 1)               //单数字符个数的时候处理最后一个并且作为第四位的值
                    {
                        byteBuff[j++] = AsciiToHex(byteTemp[0]);
                        k = 0;
                        ByteNumber++;
                    }
                }
            }
            return byteBuff;
        }
        */
        #endregion

        #region///提取有效字符串
        /********************************************************************************
         * 名称：GetEffectString()
         * 功能：提取有效字符串，即从输入字符串中去除指定无效字符串开始的部分，返回前面部分
         * 输入:InputString输入字符串，InvalidStartString无效开始表示字符
         * 输出:有效部分
         * 备份：把包含InvalidStartString字符串开始的字符串全部去掉
        ********************************************************************************/
        //先去掉所有“/**/”包围的所有字符串，再去掉双“//”后面所有字符串
        public string GetEffectString(string InputString)
        {
            string OutString = "";
            string InvalidStartString1 = "//";
            string InvalidStartString2 = "/*";
            string InvalidEndString2 = "*/";
            while (InputString.Contains(InvalidStartString2) && InputString.Contains(InvalidEndString2))
            {
                OutString = GetEffectString(InputString, InvalidStartString2, InvalidEndString2);
                InputString = OutString;
            }
            if (InputString.Contains(InvalidStartString1))
                OutString = GetEffectString(InputString, InvalidStartString1);      //去掉双“//”后面所有字符
            else
                OutString = InputString;
            return OutString;
        }
        //去掉“InvalidStartString”指定的后面的所有字符
        public string GetEffectString(string InputString, string InvalidStartString)
        {
            string OutString = "";
            int InvalidStrStartIndex = InputString.IndexOf(InvalidStartString);
            if (InvalidStrStartIndex != -1)
                OutString = InputString.Remove(InvalidStrStartIndex);
            else
                OutString = InputString;
            return OutString;
        }
        //去掉从InvalidStartString开始到InvalidEndString之间的所有字符
        public string GetEffectString(string InputString, string InvalidStartString, string InvalidEndString)
        {
            string OutString = "";
            int InvalidStrStartIndex = InputString.IndexOf(InvalidStartString);
            int InvalidStrEndIndex = InputString.IndexOf(InvalidEndString);
            if (InvalidStrStartIndex != -1 && InvalidStrEndIndex != -1)
                OutString = InputString.Remove(InvalidStrStartIndex, InvalidStrEndIndex - InvalidStrStartIndex + InvalidEndString.Length);
            else
                OutString = InputString;
            return OutString;
        }
        #endregion

        private void comboBoxComNum_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



    }
    /**************************************************************************************/
    #region///出口接收数据事件数据
    //public class ComEventArgs : EventArgs
    //{
    //    byte[] ReceiveByteArray;
    //    string ReceiveByteString;
    //    public ComEventArgs() { }
    //    public ComEventArgs(byte[] ReceiveByteArray)
    //    {
    //        this.ReceiveByteArray = ReceiveByteArray;
    //    }
    //    public ComEventArgs(string ReceiveByteString)
    //    {
    //        this.ReceiveByteString = ReceiveByteString;
    //    }
    //    public ComEventArgs(byte[] ReceiveByteArray, string ReceiveByteString)
    //    {
    //        this.ReceiveByteArray = ReceiveByteArray;
    //        this.ReceiveByteString = ReceiveByteString;
    //    }
    //    public string RxString
    //    {
    //        get { return ReceiveByteString; }
    //        set { ReceiveByteString = value; }
    //    }
    //    public byte[] RxByteArray
    //    {
    //        get { return ReceiveByteArray; }
    //        set { ReceiveByteArray = value; }
    //    }
    //}
    #endregion
}
