using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace testComZ
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnTx_Click(object sender, EventArgs e)
        {
            UInt32 txcounter;
            comZ1.Send(TxBox.Text);
            txcounter=comZ1.TxCounterVal;
            TxTextBox.Text = txcounter.ToString();
        }

        private void BtnRx_Click(object sender, EventArgs e)
        {
            if (comZ1.IsGotNewData() == true)
            {
                if (comZ1.IsHexMode == true)
                    RxBox.Text += comZ1.GetRxStringAndHexWithBlank();
                else
                    RxBox.Text += comZ1.GetRxAsciiString();
                    RxTextBox.Text = comZ1.RxCounterVal.ToString();
            }
            comZ1.ForceClearHexBuffer();

            


        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            comZ1.TxCounterVal = 0;
            comZ1.RxCounterVal = 0;
            TxTextBox.Text = comZ1.TxCounterVal.ToString();
            RxTextBox.Text = comZ1.RxCounterVal.ToString();
        }

        private void comZ1_Load(object sender, EventArgs e)
        {

        }
    }
}







