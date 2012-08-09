using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OtpSharp
{
    /// <summary>
    /// Calculate Timd One Time Passwords from a secret key
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
        public Totp(byte[] secretKey)
        {
            this.secretKey = secretKey;
        }

        /// <summary>
        /// Takes a timestamp and computes a TOTP value
        /// </summary>
        /// <param name="timestamp">The timestamp to use for the TOTP calculation</param>
        /// <returns>a TOTP value</returns>
        public int ComputeTotp(DateTime timestamp)
        {
            var unixTimestamp = timestamp.Ticks - unixEpochTicks / ticksToSeconds;
            var thirtySecondTimestamp = unixTimestamp / 30L;
            var data = OtpUtility.GetBigEndianBytes(thirtySecondTimestamp);

            var otp = OtpUtility.CalculateOtp(this.secretKey, data);
            return OtpUtility.Digits(otp, 6);
        }
    }
}
