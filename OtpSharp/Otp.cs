using System;
using System.Linq;
using System.Security.Cryptography;

namespace OtpSharp
{
    internal class Otp
    {
        public static int CalculateHotp(byte[] key, long counter)
        {
            var hmacsha1 = new HMACSHA1(key);
            var counterBytes = GetBigEndianBytes(counter);
            byte[] hmacComputedHash = hmacsha1.ComputeHash(counterBytes);
            int offset = hmacComputedHash[19] & 0x0f;
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
