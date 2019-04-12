using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;


namespace TestComY
{
    public partial class Form1 : Form
    {
        bool UseThreadProcessRxDataFlag = false;

        public Form1()
        {
            InitializeComponent();
            BtnComClose.Enabled = false;

            ComThreadStart();
        }

        private void BtnComTx_Click(object sender, EventArgs e)
        {
            comY1.Send(TxRichTextBox.Text);
            TxTextBox.Text = comY1.TxCounterVal.ToString();


            //TxRichTextBox.Text += "WriteBuffSize:";
            //TxRichTextBox.Text += comY1.WriteBuffSize.ToString();
            //TxRichTextBox.Text += "\r\n";
            //TxRichTextBox.Text += "ReadBuffSize:";
            //TxRichTextBox.Text += comY1.ReadBuffSize.ToString();
            //TxRichTextBox.Text += "\r\n";

            //comY1.WriteBuffSize = 1000;
            //comY1.ReadBuffSize = 2000;

            //TxRichTextBox.Text += "WriteBuffSize:";
            //TxRichTextBox.Text += comY1.WriteBuffSize.ToString();
            //TxRichTextBox.Text += "\r\n";
            //TxRichTextBox.Text += "ReadBuffSize:";
            //TxRichTextBox.Text += comY1.ReadBuffSize.ToString();
            //TxRichTextBox.Text += "\r\n";

            
        }

        private void BtnComRx_Click(object sender, EventArgs e)
        {
            if (comY1.IsGotNewData() == true)
            {
                if (comY1.IsHexMode==true)
                {
                    int count = comY1.ReceiveHexByteCounter;
                    int count2 = comY1.GetRxByteCount();
                    byte[] temp = new byte[count];
                    //count = 100;

                    comY1.GetHexArrayFromRxBuff(temp,ref count);

                    RxRichTextBox.Text += comY1.GetRxHexWithBlankString();
                }
                else
                    RxRichTextBox.Text += comY1.GetRxAsciiString();
            }
                
            RxTextBox.Text = comY1.RxCounterVal.ToString();
        }

        private void BtnComOpen_Click(object sender, EventArgs e)
        {
            string ComName = comboBoxComName.Text.Trim();
            int bps = Convert.ToInt32(comboBoxBps.Text.Trim());
            comY1.SetPortPara(ComName, bps);
            //comY1.IsHexMode = false;
            comY1.Open();
            if (comY1.IsOpen == true)
            { 
                BtnComOpen.Enabled = false;
                BtnComClose.Enabled = true;
            }
            
        }

        private void BtnComClose_Click(object sender, EventArgs e)
        {
            comY1.Close();
            if (comY1.IsOpen == false)
            {
                BtnComClose.Enabled = false;
                BtnComOpen.Enabled = true;
            }
        }

        private void BtnClearCounter_Click(object sender, EventArgs e)
        {
            RxTextBox.Text = "";
            TxTextBox.Text = "";
            comY1.RxCounterVal = 0;
            comY1.TxCounterVal = 0;
        }

        private void BtnRenewComNameList_Click(object sender, EventArgs e)
        {
            string[] comNameList = comY1.GetComNameList();
            if (comNameList.Length > 0)
            {
                foreach (string ComName in comNameList)
                {
                    comboBoxComName.Items.Add(ComName);
                }
                comboBoxComName.SelectedIndex = 0;
            }
        }

        private void checkBoxHexMode_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBoxHexMode.Checked == true) comY1.IsHexMode = true;
            else comY1.IsHexMode = false;
        }


        #region///

        delegate void ComTextBoxAddText(string text);

        private void DispComTextBoxAddText(string text)
        {
            if (this.RxRichTextBox.InvokeRequired)
            {
                ComTextBoxAddText d = new ComTextBoxAddText(DispComTextBoxAddText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.RxRichTextBox.Text += text;
                RxRichTextBox.SelectionStart = RxRichTextBox.Text.Length;
                RxRichTextBox.ScrollToCaret();

                RxTextBox.Text = comY1.RxCounterVal.ToString();
            }
        }

        private void ComThreadStart()
        {

            Thread ComRxProcessThread = new Thread(ComRxDataProcessThread);
            ComRxProcessThread.IsBackground = true;
            ComRxProcessThread.Name = "com rx data process";
            ComRxProcessThread.Start();
        }


        private void ComRxDataProcessThread()
        {
            string text="";
            TimeSpan interval = new TimeSpan(0, 0, 2);
            Thread.Sleep(200);
            Console.WriteLine("Name:" + Thread.CurrentThread.Name);
            Console.WriteLine("ManagedThreadId:" + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Priority:" + Thread.CurrentThread.Priority);
            while (true)
            {
                Thread.Sleep(100);
                if (UseThreadProcessRxDataFlag == true)
                {
                    if (comY1.IsGotNewData() == true)
                    {
                        if (comY1.IsHexMode == true)
                            text = comY1.GetRxHexWithBlankString();
                        else
                            text = comY1.GetRxAsciiString();

                        DispComTextBoxAddText(text);
                    }
                }
                else
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Thread running....");
                }
                
            }
        }
        #endregion

        private void checkBoxThreadRx_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxThreadRx.Checked == true)
            {
                UseThreadProcessRxDataFlag = true;
                Console.WriteLine("线程自动收数据....");
            }
            else
            {
                UseThreadProcessRxDataFlag = false;
                Console.WriteLine("手动按钮收数据....");
            }
        }

    }
}
