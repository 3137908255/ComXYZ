namespace ComXYZ
{
    partial class ComZ
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioBtnModeHex = new System.Windows.Forms.RadioButton();
            this.radioBtnModeAscii = new System.Windows.Forms.RadioButton();
            this.checkBoxComNum = new System.Windows.Forms.CheckBox();
            this.comboBoxStopBits = new System.Windows.Forms.ComboBox();
            this.comboBoxParity = new System.Windows.Forms.ComboBox();
            this.comboBoxDataBits = new System.Windows.Forms.ComboBox();
            this.comboBoxBps = new System.Windows.Forms.ComboBox();
            this.comboBoxComNum = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CloseCom = new System.Windows.Forms.Button();
            this.OpenCom = new System.Windows.Forms.Button();
            this.comY1 = new ComXYZ.ComY(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioBtnModeHex);
            this.groupBox1.Controls.Add(this.radioBtnModeAscii);
            this.groupBox1.Controls.Add(this.checkBoxComNum);
            this.groupBox1.Controls.Add(this.comboBoxStopBits);
            this.groupBox1.Controls.Add(this.comboBoxParity);
            this.groupBox1.Controls.Add(this.comboBoxDataBits);
            this.groupBox1.Controls.Add(this.comboBoxBps);
            this.groupBox1.Controls.Add(this.comboBoxComNum);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.CloseCom);
            this.groupBox1.Controls.Add(this.OpenCom);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(147, 224);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "通信设置";
            // 
            // radioBtnModeHex
            // 
            this.radioBtnModeHex.AutoSize = true;
            this.radioBtnModeHex.Checked = true;
            this.radioBtnModeHex.Location = new System.Drawing.Point(82, 168);
            this.radioBtnModeHex.Name = "radioBtnModeHex";
            this.radioBtnModeHex.Size = new System.Drawing.Size(65, 16);
            this.radioBtnModeHex.TabIndex = 49;
            this.radioBtnModeHex.TabStop = true;
            this.radioBtnModeHex.Text = "Hex模式";
            this.radioBtnModeHex.UseVisualStyleBackColor = true;
            this.radioBtnModeHex.CheckedChanged += new System.EventHandler(this.radioBtnModeHex_CheckedChanged_1);
            // 
            // radioBtnModeAscii
            // 
            this.radioBtnModeAscii.AutoSize = true;
            this.radioBtnModeAscii.Location = new System.Drawing.Point(6, 168);
            this.radioBtnModeAscii.Name = "radioBtnModeAscii";
            this.radioBtnModeAscii.Size = new System.Drawing.Size(77, 16);
            this.radioBtnModeAscii.TabIndex = 48;
            this.radioBtnModeAscii.Text = "Ascii模式";
            this.radioBtnModeAscii.UseVisualStyleBackColor = true;
            this.radioBtnModeAscii.CheckedChanged += new System.EventHandler(this.radioBtnModeAscii_CheckedChanged_1);
            // 
            // checkBoxComNum
            // 
            this.checkBoxComNum.AutoSize = true;
            this.checkBoxComNum.Location = new System.Drawing.Point(10, 21);
            this.checkBoxComNum.Name = "checkBoxComNum";
            this.checkBoxComNum.Size = new System.Drawing.Size(120, 16);
            this.checkBoxComNum.TabIndex = 47;
            this.checkBoxComNum.Text = "显示本机可用串口";
            this.checkBoxComNum.UseVisualStyleBackColor = true;
            this.checkBoxComNum.CheckedChanged += new System.EventHandler(this.checkBoxComNum_CheckedChanged);
            // 
            // comboBoxStopBits
            // 
            this.comboBoxStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStopBits.FormattingEnabled = true;
            this.comboBoxStopBits.Location = new System.Drawing.Point(60, 142);
            this.comboBoxStopBits.Name = "comboBoxStopBits";
            this.comboBoxStopBits.Size = new System.Drawing.Size(76, 20);
            this.comboBoxStopBits.TabIndex = 46;
            // 
            // comboBoxParity
            // 
            this.comboBoxParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxParity.FormattingEnabled = true;
            this.comboBoxParity.Location = new System.Drawing.Point(60, 117);
            this.comboBoxParity.Name = "comboBoxParity";
            this.comboBoxParity.Size = new System.Drawing.Size(76, 20);
            this.comboBoxParity.TabIndex = 45;
            // 
            // comboBoxDataBits
            // 
            this.comboBoxDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDataBits.FormattingEnabled = true;
            this.comboBoxDataBits.Location = new System.Drawing.Point(60, 89);
            this.comboBoxDataBits.Name = "comboBoxDataBits";
            this.comboBoxDataBits.Size = new System.Drawing.Size(76, 20);
            this.comboBoxDataBits.TabIndex = 44;
            // 
            // comboBoxBps
            // 
            this.comboBoxBps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBps.FormattingEnabled = true;
            this.comboBoxBps.Location = new System.Drawing.Point(60, 64);
            this.comboBoxBps.Name = "comboBoxBps";
            this.comboBoxBps.Size = new System.Drawing.Size(76, 20);
            this.comboBoxBps.TabIndex = 43;
            // 
            // comboBoxComNum
            // 
            this.comboBoxComNum.FormattingEnabled = true;
            this.comboBoxComNum.Location = new System.Drawing.Point(60, 40);
            this.comboBoxComNum.Name = "comboBoxComNum";
            this.comboBoxComNum.Size = new System.Drawing.Size(76, 20);
            this.comboBoxComNum.TabIndex = 42;
            this.comboBoxComNum.SelectedIndexChanged += new System.EventHandler(this.comboBoxComNum_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 41;
            this.label5.Text = "数据位数";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 40;
            this.label4.Text = "停止位";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 39;
            this.label3.Text = "校验方式";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 38;
            this.label2.Text = "波特率";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 37;
            this.label1.Text = "串口编号";
            // 
            // CloseCom
            // 
            this.CloseCom.Location = new System.Drawing.Point(74, 190);
            this.CloseCom.Name = "CloseCom";
            this.CloseCom.Size = new System.Drawing.Size(61, 23);
            this.CloseCom.TabIndex = 36;
            this.CloseCom.Text = "关闭串口";
            this.CloseCom.UseVisualStyleBackColor = true;
            this.CloseCom.Click += new System.EventHandler(this.CloseCom_Click);
            // 
            // OpenCom
            // 
            this.OpenCom.Location = new System.Drawing.Point(7, 190);
            this.OpenCom.Name = "OpenCom";
            this.OpenCom.Size = new System.Drawing.Size(61, 23);
            this.OpenCom.TabIndex = 35;
            this.OpenCom.Text = "打开串口";
            this.OpenCom.UseVisualStyleBackColor = true;
            this.OpenCom.Click += new System.EventHandler(this.OpenCom_Click);
            // 
            // comY1
            // 
            this.comY1.IsHexMode = true;
            this.comY1.MaxComAmount = 50;
            this.comY1.NewLineWord = "\n";
            this.comY1.ReceivedBytesThreshold = 1;
            this.comY1.RxCounterVal = ((uint)(0u));
            this.comY1.TxCounterVal = ((uint)(0u));
            // 
            // ComZ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ComZ";
            this.Size = new System.Drawing.Size(155, 226);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioBtnModeHex;
        private System.Windows.Forms.RadioButton radioBtnModeAscii;
        private System.Windows.Forms.CheckBox checkBoxComNum;
        private System.Windows.Forms.ComboBox comboBoxStopBits;
        private System.Windows.Forms.ComboBox comboBoxParity;
        private System.Windows.Forms.ComboBox comboBoxDataBits;
        private System.Windows.Forms.ComboBox comboBoxBps;
        private System.Windows.Forms.ComboBox comboBoxComNum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CloseCom;
        private System.Windows.Forms.Button OpenCom;
        private ComY comY1;
    }
}
