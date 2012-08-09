using System;
using System.Linq;
using System.Security.Cryptography;

namespace OtpSharp
{
    /// <summary>
    /// Calculate Timed-One-Time-Passwords (TOTP) from a secret key
    /// </summary>
    /// <remarks>
    /// The specifications for this are found in RFC 6238
    /// http://tools.ietf.org/html/rfc6238
    /// </remarks>
    public class Totp : Otp
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
        private readonly int totpSize;
        public Totp(byte[] secretKey, int step = 30, OtpHashMode mode = OtpHashMode.Sha1, int totpSize = 6)
        {
            this.secretKey = secretKey;
            this.step = step;
            this.hashMode = mode;
            this.totpSize = totpSize;
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
            var data = this.GetBigEndianBytes(timeSteps);

            var otp = this.CalculateOtp(this.secretKey, data, this.hashMode);
            return this.Digits(otp, this.totpSize);
        }
    }
}