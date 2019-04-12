namespace ComXYZ
{
    partial class ComY
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
            this.ComPort = new System.IO.Ports.SerialPort(this.components);
            // 
            // ComPort
            // 
            this.ComPort.ReadBufferSize = 8192;
            this.ComPort.ReadTimeout = 1000;
            this.ComPort.WriteBufferSize = 8192;
            this.ComPort.WriteTimeout = 1000;
            this.ComPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.ComPort_DataReceived);

        }

        #endregion

        private System.IO.Ports.SerialPort ComPort;
    }
}
