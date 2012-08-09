using System;
using System.Linq;
using System.Security.Cryptography;

namespace OtpSharp
{
    /// <remarks>
    /// // https://tools.ietf.org/html/rfc4226
    /// </remarks>
    public class Otp
    {
        public static long CalculateOtp(byte[] secretKey, long seed)
        {
            var hmacsha1 = new HMACSHA1(secretKey);
            var counterBytes = GetBigEndianBytes(seed);
            byte[] hmacComputedHash = hmacsha1.ComputeHash(counterBytes);
            int offset = hmacComputedHash.Last() & 0x0F;
            return (hmacComputedHash[offset] & 0x7f) << 24
                | (hmacComputedHash[offset + 1] & 0xff) << 16
                | (hmacComputedHash[offset + 2] & 0xff) << 8
                | (hmacComputedHash[offset + 3] & 0xff) % 1000000;
        }

        private static byte[] GetBigEndianBytes(long input)
        {
            return BitConverter.GetBytes(input).Reverse().ToArray();
        }
    }
}