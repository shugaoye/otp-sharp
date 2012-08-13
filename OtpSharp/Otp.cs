﻿using System;
using System.Linq;
using System.Security.Cryptography;

namespace OtpSharp
{
    /// <summary>
    /// An abstract class that contains common OTP calculations
    /// </summary>
    /// <remarks>
    /// https://tools.ietf.org/html/rfc4226
    /// </remarks>
    public abstract class Otp
    {
        /// <summary>
        /// An abstract definition of a compute method.  Takes a counter and runs it through the derived algorithm.
        /// </summary>
        /// <param name="counter">Counter or step</param>
        /// <returns>OTP calculated code</returns>
        protected abstract long Compute(long counter);

        /// <summary>
        /// Helper method that calculates OTPs
        /// </summary>
        protected internal long CalculateOtp(byte[] secretKey, byte[] data, OtpHashMode mode)
        {
            var hmacHasher = CreateHmacHasher(secretKey, mode);
            byte[] hmacComputedHash = hmacHasher.ComputeHash(data);
            // The RFC has a hard coded index 19 in this value.  Last is the same thing but also accomodates SHA256 and SHA512
            // hmacComputedHash[19] => hmacComputedHash.Last()
            int offset = hmacComputedHash.Last() & 0x0F;
            return (hmacComputedHash[offset] & 0x7f) << 24
                | (hmacComputedHash[offset + 1] & 0xff) << 16
                | (hmacComputedHash[offset + 2] & 0xff) << 8
                | (hmacComputedHash[offset + 3] & 0xff) % 1000000;
        }

        /// <summary>
        /// Create an HMAC object for the specified algorithm
        /// </summary>
        private HMAC CreateHmacHasher(byte[] secretKey, OtpHashMode mode)
        {
            switch (mode)
            {
                case OtpHashMode.Sha256:
                    return new HMACSHA256(secretKey);
                case OtpHashMode.Sha512:
                    return new HMACSHA512(secretKey);
                default:
                case OtpHashMode.Sha1:
                    return new HMACSHA1(secretKey);
            }
        }

        /// <summary>
        /// converts a long into a big endian byte array.
        /// </summary>
        /// <remarks>
        /// RFC 4226 specifies big endian as the method for converting the counter to data to hash.
        /// </remarks>
        protected internal byte[] GetBigEndianBytes(long input)
        {
            // Since .net uses little endian numbers, we need to reverse the byte order to get big endian.
            return BitConverter.GetBytes(input).Reverse().ToArray();
        }

        /// <summary>
        /// truncates a number down to the specified number of digits
        /// </summary>
        protected internal int Digits(long input, int digits)
        {
            return ((int)input % (int)Math.Pow(10, digits));
        }

        /// <summary>
        /// Verify an OTP value
        /// </summary>
        /// <param name="initialStep">The initial step to try</param>
        /// <param name="valueToVerify">The value to verify</param>
        /// <param name="matchedStep">Thed step where the match was found.  If no match was found it will be 0</param>
        /// <param name="window">The window to verify</param>
        /// <returns>True if a match is found</returns>
        protected bool Verify(long initialStep, int valueToVerify, out long matchedStep, VerificationWindow window)
        {
            if (window == null)
                window = new VerificationWindow();
            foreach (var frame in window.ValidationCandidates(initialStep))
            {
                var comparisonValue = this.Compute(frame);
                if (comparisonValue == valueToVerify)
                {
                    matchedStep = frame;
                    return true;
                }
            }

            matchedStep = 0;
            return false;
        }
    }
}