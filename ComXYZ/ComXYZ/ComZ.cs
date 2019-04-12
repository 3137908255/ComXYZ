//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Drawing;
//using System.Data;
//using System.Linq;
//using System.Text;
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
namespace ComXYZ
{
    [ToolboxBitmap(typeof(ComZ), "ComZ.bmp")]
    public partial class ComZ : UserControl
    {
        int MaxComNum = 50;
        private bool ComPortIsOpen;                                 //定义串口打开状态
        private bool isHexMode = true;                              //定义串口通信模式 二者之一
        UInt32 TxCounter = 0;                                       //发送计数器
        UInt32 RxCounter = 0;                                       //接收计数器 
        int writeBufferSize = 4096, readBuffSize = 4096;

        #region///属性实现
        public int MaxComAmount                               //最大端口数量  
        {
            set { MaxComNum = value; }
            get { return MaxComNum; }
        }
        public bool IsOpen
        {
            //set { ComPortIsOpen = value; }
            get 
            {
                ComPortIsOpen = comY1.IsOpen;
                return ComPortIsOpen; 
            }
        }

        /// <summary>
        /// 设置/查询是否为16进制模式
        /// </summary>
        public bool IsHexMode
        {
            set 
            { 
                isHexMode = value;
                comY1.IsHexMode = isHexMode;
            }
            get 
            {
                isHexMode=comY1.IsHexMode; 
                return isHexMode; 
            }
        }
        /// <summary>
        /// 获取串口接收到的字节个数
        /// </summary>
        public int ReceiveHexByteCounter
        {
            get
            {
                return comY1.ReceiveHexByteCounter;
            }
        }
        /// <summary>
        /// 接收计数器
        /// </summary>
        public UInt32 RxCounterVal
        {
            set                              //可用于外部清零
            { 
                RxCounter = value;
                comY1.RxCounterVal = RxCounter;
            } 
            get 
            {
                RxCounter=comY1.RxCounterVal ;
                return RxCounter; 
            }
        }
        /// <summary>
        /// 发送计数器
        /// </summary>
        public UInt32 TxCounterVal                              //可用于外部清零
        {
            set 
            { 
                TxCounter = value;
                comY1.TxCounterVal = TxCounter;
            }  
            get 
            {
                TxCounter = comY1.TxCounterVal;
                return TxCounter;
            }          
        }
        /// <summary>
        /// 原始控件写缓存大小
        /// </summary>
        public int WriteBuffSize
        {
            set
            {
                writeBufferSize = value;
                comY1.WriteBuffSize = writeBufferSize;
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
                readBuffSize = value;
                comY1.ReadBuffSize = readBuffSize;
            }
            get { return readBuffSize; }
        }
        
        #endregion

        #region //控件初始化
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
        }
        public ComZ()
        {
            InitializeComponent();
            InitCom();
        }
        #endregion

        #region  ///复合控件操作函数

        private void InitComNum()
        {
            comboBoxComNum.Items.Clear();
            for (int i = 1; i < MaxComNum + 1; i++)
                comboBoxComNum.Items.Add("COM" + i.ToString());
            comboBoxComNum.Text = "COM1";                           //每次启动完显示COM1
        }
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
                ComName = comY1.GetComNameList();
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

        private void comboBoxComNum_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioBtnModeAscii_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioBtnModeAscii.Checked == true)
            {
                isHexMode = false;
                comY1.IsHexMode = false;
            }
            else
            {
                isHexMode = true;
                comY1.IsHexMode = true;
            }
        }

        private void radioBtnModeHex_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioBtnModeHex.Checked == true) 
            {
                isHexMode = true;
                comY1.IsHexMode = true;
            }
            else
            {
                isHexMode = false;
                comY1.IsHexMode = false;
            }
            
        }

        public void PortSet()                           //由组合控件设置端口参数
        {
            Parity comParity;
            StopBits ComStopbit;
            string PortName = comboBoxComNum.Text.Trim();
            int BaudRate = Convert.ToInt32(comboBoxBps.Text.Trim());
            int DataBits = Convert.ToInt16(comboBoxDataBits.Text.Trim());
            string s = comboBoxParity.Text.Trim();
            if (s.Equals("无") == true) comParity = Parity.None;
            else if (s.Equals("偶校验") == true) comParity = Parity.Even;
            else if (s.Equals("奇校验") == true) comParity = Parity.Odd;
            else comParity = Parity.None;

            float f = Convert.ToSingle(comboBoxStopBits.Text.Trim());
            if (f == 0) ComStopbit = StopBits.None;
            else if (f == 1) ComStopbit = StopBits.One;
            else if (f == 1.5) ComStopbit = StopBits.OnePointFive;
            else if (f == 2) ComStopbit = StopBits.Two;
            else ComStopbit = StopBits.One;
            comY1.SetPortPara(PortName, BaudRate, DataBits, comParity, ComStopbit);
        }

        private void OpenCom_Click(object sender, EventArgs e)
        {
            comY1.Open();
            DisableComButton();
        }

        private void CloseCom_Click(object sender, EventArgs e)
        {
            comY1.Close();
            EnableComButton();
        }

        #endregion

        #region///对外开放的接口函数
        /// <summary>
        /// 发送字符串，不区分HEX,ASCII
        /// </summary>
        /// <param name="SendText"></param>
        public void Send(string SendText)
        {
            comY1.Send(SendText);
        }
        /// <summary>
        /// 发送字符串，不区分HEX,ASCII但要指明模式
        /// </summary>
        /// <param name="HexMode"></param>
        /// <param name="SendText"></param>
        public void Send(bool HexMode, string SendText)
        {
            comY1.Send(HexMode, SendText);
        }
        /// <summary>
        /// 直接发送16进制字节，HexMode=true
        /// </summary>
        /// <param name="HexMode"></param>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        private void SendHex(bool HexMode, byte[] buffer, int offset, int count)
        {
            if (comY1.IsOpen == true)
            {
                if (HexMode == true)
                {
                    comY1.SendHex(true,buffer, offset, count);
                    comY1.TxCounterAdd((UInt32)count);
                }
                else
                    MessageBox.Show("HexMode必须为true", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("请打开串口", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        /// <summary>
        /// 查询是否接收到新的数据
        /// </summary>
        /// <returns></returns>
        public bool IsGotNewData()
        {
            return comY1.IsGotNewData();
        }
        /// <summary>
        /// 提取HEX数据 BYTE类型
        /// </summary>
        /// <param name="buff"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool GetHexArrayFromRxBuff(byte[] buff, ref int count)
        {
            return comY1.GetHexArrayFromRxBuff(buff, ref count);
        }
        /// <summary>
        /// 获取串口接收缓存中有多少个数据
        /// </summary>
        /// <returns></returns>
        public int GetRxByteCount()
        {
            return comY1.GetRxByteCount();
        }
        /// <summary>
        /// 获取字符串 包括中文
        /// </summary>
        /// <returns></returns>
        public string GetRxAsciiString()
        {
            return comY1.GetRxAsciiString();
        }
        /// <summary>
        /// 获取HEX字符串
        /// </summary>
        /// <returns></returns>
        public string GetRxHexString()
        {
            return comY1.GetRxHexString();
        }
        /// <summary>
        /// 返回字符串表示的HEX串，每两个字节插入一个空格
        /// </summary>
        /// <returns></returns>
        public string GetRxHexWithBlankString()
        {
            return comY1.GetRxHexWithBlankString();
        }
        /// <summary>
        /// 不区分ASCII还是HEX字符串
        /// </summary>
        /// <returns></returns>
        public string GetRxString()
        {
            return comY1.GetRxString();
        }
        /// <summary>
        /// 不区分ASCII还是HEX字符串(HEX插入空格)
        /// </summary>
        /// <returns></returns>
        public string GetRxStringAndHexWithBlank()
        {
            return comY1.GetRxStringAndHexWithBlank();
        }
        /// <summary>
        /// 强制清空16进制数据接收缓存
        /// </summary>
        public void ForceClearHexBuffer()
        {
            comY1.ForceClearHexBuffer();
        }

        /// <summary>
        /// 清空四缓存之一     ASCII字符缓存
        /// </summary>
        public void ClearRxAsciiStringBuff()
        {
            comY1.ClearRxAsciiStringBuff();
        }
        /// <summary>
        /// 清空四缓存之一     HEX字符串缓存
        /// </summary>
        public void ClearRxHexStringBuff()
        {
            comY1.ClearRxHexStringBuff();
        }
        /// <summary>
        /// 清空四缓存之一    HEX字符+空格缓存
        /// </summary>
        public void ClearRxHexWithBlankStringBuff()
        {
            comY1.ClearRxHexWithBlankStringBuff();
        }
        /// <summary>
        /// 清空四缓存之一     接收数据缓存
        /// </summary>
        public void ClearHexDataBuff()
        {
            comY1.ClearHexDataBuff();
        }
        /// <summary>
        /// 清空四缓存
        /// </summary>
        public void ClearAllBuff()
        {
            comY1.ClearAllBuff();
        }

        #endregion




    }




}
