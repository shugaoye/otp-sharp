using System;
using System.Linq;
using System.Security.Cryptography;

namespace OtpSharp
{
    /// <remarks>
    /// // https://tools.ietf.org/html/rfc4226
    /// </remarks>
    internal static class OtpUtility
    {
        /// <summary>
        /// Helper method that calculates OTPs
        /// </summary>
        internal static long CalculateOtp(byte[] secretKey, byte[] data)
        {
            var hmacsha1 = new HMACSHA1(secretKey);
            byte[] hmacComputedHash = hmacsha1.ComputeHash(data);
            int offset = hmacComputedHash.Last() & 0x0F;
            return (hmacComputedHash[offset] & 0x7f) << 24
                | (hmacComputedHash[offset + 1] & 0xff) << 16
                | (hmacComputedHash[offset + 2] & 0xff) << 8
                | (hmacComputedHash[offset + 3] & 0xff) % 1000000;
        }

        /// <summary>
        /// converts a long into a big endian byte array
        /// </summary>
        internal static byte[] GetBigEndianBytes(long input)
        {
            return BitConverter.GetBytes(input).Reverse().ToArray();
        }

        /// <summary>
        /// truncates a number down to the specified number of digits
        /// </summary>
        internal static int Digits(long input, int digits)
        {
            return ((int)input % (int)Math.Pow(10, digits));
        }
    }
}