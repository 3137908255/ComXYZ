namespace TestComY
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.BtnComTx = new System.Windows.Forms.Button();
            this.BtnComRx = new System.Windows.Forms.Button();
            this.RxRichTextBox = new System.Windows.Forms.RichTextBox();
            this.TxRichTextBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.RxTextBox = new System.Windows.Forms.TextBox();
            this.TxTextBox = new System.Windows.Forms.TextBox();
            this.BtnComOpen = new System.Windows.Forms.Button();
            this.BtnComClose = new System.Windows.Forms.Button();
            this.BtnClearCounter = new System.Windows.Forms.Button();
            this.comboBoxComName = new System.Windows.Forms.ComboBox();
            this.comboBoxBps = new System.Windows.Forms.ComboBox();
            this.BtnRenewComNameList = new System.Windows.Forms.Button();
            this.checkBoxHexMode = new System.Windows.Forms.CheckBox();
            this.checkBoxThreadRx = new System.Windows.Forms.CheckBox();
            this.comY1 = new ComXYZ.ComY(this.components);
            this.SuspendLayout();
            // 
            // BtnComTx
            // 
            this.BtnComTx.Location = new System.Drawing.Point(174, 353);
            this.BtnComTx.Name = "BtnComTx";
            this.BtnComTx.Size = new System.Drawing.Size(75, 23);
            this.BtnComTx.TabIndex = 0;
            this.BtnComTx.Text = "发送";
            this.BtnComTx.UseVisualStyleBackColor = true;
            this.BtnComTx.Click += new System.EventHandler(this.BtnComTx_Click);
            // 
            // BtnComRx
            // 
            this.BtnComRx.Location = new System.Drawing.Point(255, 353);
            this.BtnComRx.Name = "BtnComRx";
            this.BtnComRx.Size = new System.Drawing.Size(75, 23);
            this.BtnComRx.TabIndex = 1;
            this.BtnComRx.Text = "接收";
            this.BtnComRx.UseVisualStyleBackColor = true;
            this.BtnComRx.Click += new System.EventHandler(this.BtnComRx_Click);
            // 
            // RxRichTextBox
            // 
            this.RxRichTextBox.Location = new System.Drawing.Point(25, 54);
            this.RxRichTextBox.Name = "RxRichTextBox";
            this.RxRichTextBox.Size = new System.Drawing.Size(356, 136);
            this.RxRichTextBox.TabIndex = 2;
            this.RxRichTextBox.Text = "";
            // 
            // TxRichTextBox
            // 
            this.TxRichTextBox.Location = new System.Drawing.Point(25, 232);
            this.TxRichTextBox.Name = "TxRichTextBox";
            this.TxRichTextBox.Size = new System.Drawing.Size(358, 103);
            this.TxRichTextBox.TabIndex = 3;
            this.TxRichTextBox.Text = "12 34 56 78 90";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(106, 208);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "R:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(243, 208);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "T:";
            // 
            // RxTextBox
            // 
            this.RxTextBox.Location = new System.Drawing.Point(137, 205);
            this.RxTextBox.Name = "RxTextBox";
            this.RxTextBox.Size = new System.Drawing.Size(100, 21);
            this.RxTextBox.TabIndex = 6;
            // 
            // TxTextBox
            // 
            this.TxTextBox.Location = new System.Drawing.Point(266, 205);
            this.TxTextBox.Name = "TxTextBox";
            this.TxTextBox.Size = new System.Drawing.Size(100, 21);
            this.TxTextBox.TabIndex = 7;
            // 
            // BtnComOpen
            // 
            this.BtnComOpen.Location = new System.Drawing.Point(2, 353);
            this.BtnComOpen.Name = "BtnComOpen";
            this.BtnComOpen.Size = new System.Drawing.Size(75, 23);
            this.BtnComOpen.TabIndex = 8;
            this.BtnComOpen.Text = "打开";
            this.BtnComOpen.UseVisualStyleBackColor = true;
            this.BtnComOpen.Click += new System.EventHandler(this.BtnComOpen_Click);
            // 
            // BtnComClose
            // 
            this.BtnComClose.Location = new System.Drawing.Point(83, 353);
            this.BtnComClose.Name = "BtnComClose";
            this.BtnComClose.Size = new System.Drawing.Size(75, 23);
            this.BtnComClose.TabIndex = 9;
            this.BtnComClose.Text = "关闭";
            this.BtnComClose.UseVisualStyleBackColor = true;
            this.BtnComClose.Click += new System.EventHandler(this.BtnComClose_Click);
            // 
            // BtnClearCounter
            // 
            this.BtnClearCounter.Location = new System.Drawing.Point(25, 203);
            this.BtnClearCounter.Name = "BtnClearCounter";
            this.BtnClearCounter.Size = new System.Drawing.Size(75, 23);
            this.BtnClearCounter.TabIndex = 10;
            this.BtnClearCounter.Text = "清零";
            this.BtnClearCounter.UseVisualStyleBackColor = true;
            this.BtnClearCounter.Click += new System.EventHandler(this.BtnClearCounter_Click);
            // 
            // comboBoxComName
            // 
            this.comboBoxComName.FormattingEnabled = true;
            this.comboBoxComName.Location = new System.Drawing.Point(25, 12);
            this.comboBoxComName.Name = "comboBoxComName";
            this.comboBoxComName.Size = new System.Drawing.Size(111, 20);
            this.comboBoxComName.TabIndex = 11;
            this.comboBoxComName.Text = "请输入串口号";
            // 
            // comboBoxBps
            // 
            this.comboBoxBps.FormattingEnabled = true;
            this.comboBoxBps.Items.AddRange(new object[] {
            "9600",
            "57600",
            "115200"});
            this.comboBoxBps.Location = new System.Drawing.Point(197, 12);
            this.comboBoxBps.Name = "comboBoxBps";
            this.comboBoxBps.Size = new System.Drawing.Size(109, 20);
            this.comboBoxBps.TabIndex = 12;
            this.comboBoxBps.Text = "9600";
            // 
            // BtnRenewComNameList
            // 
            this.BtnRenewComNameList.Location = new System.Drawing.Point(142, 12);
            this.BtnRenewComNameList.Name = "BtnRenewComNameList";
            this.BtnRenewComNameList.Size = new System.Drawing.Size(39, 23);
            this.BtnRenewComNameList.TabIndex = 13;
            this.BtnRenewComNameList.Text = "刷新";
            this.BtnRenewComNameList.UseVisualStyleBackColor = true;
            this.BtnRenewComNameList.Click += new System.EventHandler(this.BtnRenewComNameList_Click);
            // 
            // checkBoxHexMode
            // 
            this.checkBoxHexMode.AutoSize = true;
            this.checkBoxHexMode.Location = new System.Drawing.Point(316, 16);
            this.checkBoxHexMode.Name = "checkBoxHexMode";
            this.checkBoxHexMode.Size = new System.Drawing.Size(42, 16);
            this.checkBoxHexMode.TabIndex = 14;
            this.checkBoxHexMode.Text = "Hex";
            this.checkBoxHexMode.UseVisualStyleBackColor = true;
            this.checkBoxHexMode.CheckedChanged += new System.EventHandler(this.checkBoxHexMode_CheckedChanged);
            // 
            // checkBoxThreadRx
            // 
            this.checkBoxThreadRx.AutoSize = true;
            this.checkBoxThreadRx.Location = new System.Drawing.Point(336, 357);
            this.checkBoxThreadRx.Name = "checkBoxThreadRx";
            this.checkBoxThreadRx.Size = new System.Drawing.Size(72, 16);
            this.checkBoxThreadRx.TabIndex = 15;
            this.checkBoxThreadRx.Text = "自动接收";
            this.checkBoxThreadRx.UseVisualStyleBackColor = true;
            this.checkBoxThreadRx.CheckedChanged += new System.EventHandler(this.checkBoxThreadRx_CheckedChanged);
            // 
            // comY1
            // 
            this.comY1.IsHexMode = true;
            this.comY1.MaxComAmount = 50;
            this.comY1.NewLineWord = "\n";
            this.comY1.ReadBuffSize = 8192;
            this.comY1.ReceivedBytesThreshold = 1;
            this.comY1.RxCounterVal = ((uint)(0u));
            this.comY1.TxCounterVal = ((uint)(0u));
            this.comY1.WriteBuffSize = 8192;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 393);
            this.Controls.Add(this.checkBoxThreadRx);
            this.Controls.Add(this.checkBoxHexMode);
            this.Controls.Add(this.BtnRenewComNameList);
            this.Controls.Add(this.comboBoxBps);
            this.Controls.Add(this.comboBoxComName);
            this.Controls.Add(this.BtnClearCounter);
            this.Controls.Add(this.BtnComClose);
            this.Controls.Add(this.BtnComOpen);
            this.Controls.Add(this.TxTextBox);
            this.Controls.Add(this.RxTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxRichTextBox);
            this.Controls.Add(this.RxRichTextBox);
            this.Controls.Add(this.BtnComRx);
            this.Controls.Add(this.BtnComTx);
            this.Name = "Form1";
            this.Text = "TestComY";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnComTx;
        private System.Windows.Forms.Button BtnComRx;
        private System.Windows.Forms.RichTextBox RxRichTextBox;
        private System.Windows.Forms.RichTextBox TxRichTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox RxTextBox;
        private System.Windows.Forms.TextBox TxTextBox;
        private System.Windows.Forms.Button BtnComOpen;
        private System.Windows.Forms.Button BtnComClose;
        private System.Windows.Forms.Button BtnClearCounter;
        private System.Windows.Forms.ComboBox comboBoxComName;
        private System.Windows.Forms.ComboBox comboBoxBps;
        private System.Windows.Forms.Button BtnRenewComNameList;
        private System.Windows.Forms.CheckBox checkBoxHexMode;
        private System.Windows.Forms.CheckBox checkBoxThreadRx;
        private ComXYZ.ComY comY1;
    }
}

