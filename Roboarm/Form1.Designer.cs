namespace Roboarm
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtLog = new System.Windows.Forms.TextBox();
            this.UdpServer = new System.ComponentModel.BackgroundWorker();
            this.Arduino = new System.ComponentModel.BackgroundWorker();
            this.ProcessData = new System.ComponentModel.BackgroundWorker();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnTgl = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.SystemColors.Window;
            this.txtLog.Location = new System.Drawing.Point(13, 40);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(533, 228);
            this.txtLog.TabIndex = 1;
            // 
            // UdpServer
            // 
            this.UdpServer.WorkerReportsProgress = true;
            this.UdpServer.WorkerSupportsCancellation = true;
            this.UdpServer.DoWork += new System.ComponentModel.DoWorkEventHandler(this.UdpServer_DoWork);
            // 
            // Arduino
            // 
            this.Arduino.WorkerReportsProgress = true;
            this.Arduino.WorkerSupportsCancellation = true;
            this.Arduino.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Arduino_DoWork);
            // 
            // ProcessData
            // 
            this.ProcessData.WorkerReportsProgress = true;
            this.ProcessData.WorkerSupportsCancellation = true;
            this.ProcessData.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ProcessData_DoWork);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(175, 274);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 35);
            this.btnReset.TabIndex = 14;
            this.btnReset.Text = "Reset Servo";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnTgl
            // 
            this.btnTgl.Location = new System.Drawing.Point(94, 274);
            this.btnTgl.Name = "btnTgl";
            this.btnTgl.Size = new System.Drawing.Size(75, 35);
            this.btnTgl.TabIndex = 17;
            this.btnTgl.Text = "Toogle Arm";
            this.btnTgl.UseVisualStyleBackColor = true;
            this.btnTgl.Click += new System.EventHandler(this.btnTgl_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(13, 274);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 35);
            this.btnClear.TabIndex = 20;
            this.btnClear.Text = "Clear Log";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 311);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnTgl);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.txtLog);
            this.Name = "Form1";
            this.Text = "RoboArm";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLog;
        private System.ComponentModel.BackgroundWorker UdpServer;
        private System.ComponentModel.BackgroundWorker Arduino;
        private System.ComponentModel.BackgroundWorker ProcessData;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnTgl;
        private System.Windows.Forms.Button btnClear;

    }
}

