using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OtpSharp;

namespace GoogleAuthenticatorTotpTest
{
    public partial class GoogleAuthenticatorTotpTest : Form
    {
        /// <summary>
        /// The test key provided in the RFC
        /// </summary>
        static readonly Totp totp = new Totp(new byte[] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30 });
        public GoogleAuthenticatorTotpTest()
        {
            InitializeComponent();
            this.UpdateCode();
        }

        private void UpdateCode()
        {
            this.labelTotp.Text = totp.ComputeTotp(DateTime.UtcNow).ToString().PadLeft(6, '0');
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            this.UpdateCode();
        }
    }
}
