using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OtpSharp
{
    /// <summary>
    /// Calculate HMAC-One-Time-Passwords (HOTP) from a secret key
    /// </summary>
    public class Hotp
    {
        private readonly byte[] secretKey;
        public Hotp(byte[] secretKey)
        {
            this.secretKey = secretKey;
        }

        /// <summary>
        /// Takes a counter and produces an HOTP value
        /// </summary>
        /// <param name="counter">the counter to be incremented each time this method is called</param>
        /// <returns>Hotp</returns>
        public int ComputeHotp(long counter)
        {
            return OtpUtility.Digits(this.ComputeHotpDecimal(counter), 6); // all of the HOTP values are six digits long
        }

        /// <remarks>
        /// This method mainly exists for unit tests.
        /// The RFC defines a decimal value in the test table that is an
        /// intermediate step to a final HOTP value
        /// </remarks>
        internal long ComputeHotpDecimal(long counter)
        {
            var hashData = OtpUtility.GetBigEndianBytes(counter);
            return OtpUtility.CalculateOtp(this.secretKey, hashData, OtpHashMode.Sha1);
        }
    }
}
