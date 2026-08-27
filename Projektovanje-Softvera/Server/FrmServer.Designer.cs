namespace Server
{
    partial class FrmServer
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnStart = new Button();
            btnStop = new Button();
            txtStatus = new TextBox();
            SuspendLayout();
            // 
            // btnStart
            // 
            btnStart.Anchor = AnchorStyles.None;
            btnStart.Location = new Point(54, 52);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(122, 50);
            btnStart.TabIndex = 0;
            btnStart.Text = "POKRENI";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnStop
            // 
            btnStop.Anchor = AnchorStyles.None;
            btnStop.Location = new Point(54, 131);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(122, 50);
            btnStop.TabIndex = 1;
            btnStop.Text = "ZAUSTAVI";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // txtStatus
            // 
            txtStatus.Anchor = AnchorStyles.None;
            txtStatus.Location = new Point(218, 102);
            txtStatus.Name = "txtStatus";
            txtStatus.Size = new Size(196, 27);
            txtStatus.TabIndex = 3;
            txtStatus.TextAlign = HorizontalAlignment.Center;
            // 
            // FrmServer
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(461, 241);
            Controls.Add(txtStatus);
            Controls.Add(btnStop);
            Controls.Add(btnStart);
            Name = "FrmServer";
            Text = "Server";
            FormClosing += FrmServer_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnStart;
        private Button btnStop;
        private TextBox txtStatus;
    }
}
