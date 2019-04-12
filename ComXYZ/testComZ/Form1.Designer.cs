namespace testComZ
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
            this.btnTx = new System.Windows.Forms.Button();
            this.RxBox = new System.Windows.Forms.RichTextBox();
            this.TxBox = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.RxTextBox = new System.Windows.Forms.TextBox();
            this.TxTextBox = new System.Windows.Forms.TextBox();
            this.BtnRx = new System.Windows.Forms.Button();
            this.btnClean = new System.Windows.Forms.Button();
            this.comZ1 = new ComXYZ.ComZ();
            this.SuspendLayout();
            // 
            // btnTx
            // 
            this.btnTx.Location = new System.Drawing.Point(235, 381);
            this.btnTx.Name = "btnTx";
            this.btnTx.Size = new System.Drawing.Size(75, 23);
            this.btnTx.TabIndex = 1;
            this.btnTx.Text = "发送";
            this.btnTx.UseVisualStyleBackColor = true;
            this.btnTx.Click += new System.EventHandler(this.btnTx_Click);
            // 
            // RxBox
            // 
            this.RxBox.Location = new System.Drawing.Point(212, 49);
            this.RxBox.Name = "RxBox";
            this.RxBox.Size = new System.Drawing.Size(371, 142);
            this.RxBox.TabIndex = 2;
            this.RxBox.Text = "";
            // 
            // TxBox
            // 
            this.TxBox.Location = new System.Drawing.Point(212, 209);
            this.TxBox.Name = "TxBox";
            this.TxBox.Size = new System.Drawing.Size(371, 150);
            this.TxBox.TabIndex = 3;
            this.TxBox.Text = "12345678";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(210, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "发送：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(363, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "接收：";
            // 
            // RxTextBox
            // 
            this.RxTextBox.Location = new System.Drawing.Point(410, 12);
            this.RxTextBox.Name = "RxTextBox";
            this.RxTextBox.Size = new System.Drawing.Size(100, 21);
            this.RxTextBox.TabIndex = 7;
            // 
            // TxTextBox
            // 
            this.TxTextBox.Location = new System.Drawing.Point(257, 12);
            this.TxTextBox.Name = "TxTextBox";
            this.TxTextBox.Size = new System.Drawing.Size(100, 21);
            this.TxTextBox.TabIndex = 8;
            // 
            // BtnRx
            // 
            this.BtnRx.Location = new System.Drawing.Point(329, 381);
            this.BtnRx.Name = "BtnRx";
            this.BtnRx.Size = new System.Drawing.Size(75, 23);
            this.BtnRx.TabIndex = 9;
            this.BtnRx.Text = "接收";
            this.BtnRx.UseVisualStyleBackColor = true;
            this.BtnRx.Click += new System.EventHandler(this.BtnRx_Click);
            // 
            // btnClean
            // 
            this.btnClean.Location = new System.Drawing.Point(516, 10);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(75, 23);
            this.btnClean.TabIndex = 10;
            this.btnClean.Text = "清零";
            this.btnClean.UseVisualStyleBackColor = true;
            this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // comZ1
            // 
            this.comZ1.IsHexMode = true;
            this.comZ1.Location = new System.Drawing.Point(31, 76);
            this.comZ1.MaxComAmount = 50;
            this.comZ1.Name = "comZ1";
            this.comZ1.ReadBuffSize = 4096;
            this.comZ1.RxCounterVal = ((uint)(0u));
            this.comZ1.Size = new System.Drawing.Size(155, 226);
            this.comZ1.TabIndex = 11;
            this.comZ1.TxCounterVal = ((uint)(0u));
            this.comZ1.WriteBuffSize = 4096;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 444);
            this.Controls.Add(this.comZ1);
            this.Controls.Add(this.btnClean);
            this.Controls.Add(this.BtnRx);
            this.Controls.Add(this.TxTextBox);
            this.Controls.Add(this.RxTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TxBox);
            this.Controls.Add(this.RxBox);
            this.Controls.Add(this.btnTx);
            this.Name = "Form1";
            this.Text = "TestComZ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTx;
        private System.Windows.Forms.RichTextBox RxBox;
        private System.Windows.Forms.RichTextBox TxBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox RxTextBox;
        private System.Windows.Forms.TextBox TxTextBox;
        private System.Windows.Forms.Button BtnRx;
        private System.Windows.Forms.Button btnClean;
        private ComXYZ.ComZ comZ1;
    }
}

