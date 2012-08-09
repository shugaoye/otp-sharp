using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OtpSharp
{
    /// <summary>
    /// Calculate Timed-One-Time-Passwords (TOTP) from a secret key
    /// </summary>
    public class Totp
    {
        /// <summary>
        /// The number of ticks as Measured at Midnight Jan 1st 1970;
        /// </summary>
        const long unixEpochTicks = 621355968000000000L;
        /// <summary>
        /// A divisor for converting ticks to seconds
        /// </summary>
        const long ticksToSeconds = 10000000L;

        private readonly byte[] secretKey;
        private readonly int step;
        private readonly OtpHashMode hashMode;
        public Totp(byte[] secretKey, int step = 30, OtpHashMode mode = OtpHashMode.Sha1)
        {
            this.secretKey = secretKey;
            this.step = step;
            this.hashMode = mode;
        }

        /// <summary>
        /// Takes a timestamp and computes a TOTP value
        /// </summary>
        /// <param name="timestamp">The timestamp to use for the TOTP calculation</param>
        /// <returns>a TOTP value</returns>
        public int ComputeTotp(DateTime timestamp)
        {
            var unixTimestamp = (timestamp.Ticks - unixEpochTicks) / ticksToSeconds;
            var timeSteps = unixTimestamp / (long)this.step;
            var data = OtpUtility.GetBigEndianBytes(timeSteps);

            var otp = OtpUtility.CalculateOtp(this.secretKey, data, this.hashMode);
            return OtpUtility.Digits(otp, 8);
        }
    }
}