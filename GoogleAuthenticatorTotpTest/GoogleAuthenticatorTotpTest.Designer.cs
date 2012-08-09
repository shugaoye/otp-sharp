namespace GoogleAuthenticatorTotpTest
{
    partial class GoogleAuthenticatorTotpTest
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
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelTotp = new System.Windows.Forms.Label();
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.labelDescription = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::GoogleAuthenticatorTotpTest.Properties.Resources.QR;
            this.pictureBox1.Location = new System.Drawing.Point(12, 92);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(204, 207);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // labelTotp
            // 
            this.labelTotp.AutoSize = true;
            this.labelTotp.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotp.Location = new System.Drawing.Point(52, 317);
            this.labelTotp.Name = "labelTotp";
            this.labelTotp.Size = new System.Drawing.Size(93, 36);
            this.labelTotp.TabIndex = 1;
            this.labelTotp.Text = "TOTP";
            // 
            // timerUpdate
            // 
            this.timerUpdate.Enabled = true;
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
            // 
            // labelDescription
            // 
            this.labelDescription.Location = new System.Drawing.Point(12, 13);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(204, 76);
            this.labelDescription.TabIndex = 2;
            this.labelDescription.Text = "Download the Google Authenticator app to your smartphone and scan the QR code to " +
    "demonstrate the OtpSharp TOTP implementation.";
            // 
            // GoogleAuthenticatorTotpTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 364);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.labelTotp);
            this.Controls.Add(this.pictureBox1);
            this.Name = "GoogleAuthenticatorTotpTest";
            this.Text = "Google Authenticator Totp Test";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelTotp;
        private System.Windows.Forms.Timer timerUpdate;
        private System.Windows.Forms.Label labelDescription;
    }
}

