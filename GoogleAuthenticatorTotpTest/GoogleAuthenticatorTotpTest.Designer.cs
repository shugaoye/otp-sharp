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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.buttonVerify = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
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
            this.labelTotp.Location = new System.Drawing.Point(32, 19);
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
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 316);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(204, 100);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.labelTotp);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(196, 74);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Display";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.buttonVerify);
            this.tabPage2.Controls.Add(this.textBoxValue);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(196, 74);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "Verify";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBoxValue
            // 
            this.textBoxValue.Location = new System.Drawing.Point(16, 23);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size(100, 20);
            this.textBoxValue.TabIndex = 0;
            this.textBoxValue.TextChanged += new System.EventHandler(this.textBoxValue_TextChanged);
            this.textBoxValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxValue_KeyDown);
            // 
            // buttonVerify
            // 
            this.buttonVerify.Location = new System.Drawing.Point(122, 21);
            this.buttonVerify.Name = "buttonVerify";
            this.buttonVerify.Size = new System.Drawing.Size(48, 23);
            this.buttonVerify.TabIndex = 1;
            this.buttonVerify.Text = "Verify";
            this.buttonVerify.UseVisualStyleBackColor = true;
            this.buttonVerify.Click += new System.EventHandler(this.buttonVerify_Click);
            // 
            // GoogleAuthenticatorTotpTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 426);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.pictureBox1);
            this.Name = "GoogleAuthenticatorTotpTest";
            this.Text = "Google Authenticator Totp Test";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelTotp;
        private System.Windows.Forms.Timer timerUpdate;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button buttonVerify;
        private System.Windows.Forms.TextBox textBoxValue;
    }
}

