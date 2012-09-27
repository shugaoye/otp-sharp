﻿using System;
using System.Security.Cryptography;
#if NO_WEB
#else
using System.Web;
#endif

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
        /// Secret key
        /// </summary>
        protected readonly Key secretKey;

        /// <summary>
        /// Constructor for the abstract class.  This is to guarantee that all implementations have a secret key
        /// </summary>
        /// <param name="secretKey"></param>
        public Otp(byte[] secretKey)
        {
            if (!(secretKey != null))
                throw new ArgumentNullException("secretKey");
            if (!(secretKey.Length > 0))
                throw new ArgumentException("secretKey empty");

            // when passing a key into the constructor the caller may depend on the reference to the key remaining intact.
            this.secretKey = new ProtectedKey(secretKey);
        }

        /// <summary>
        /// Constrocutor for the abstract class.  This is to guarantee that all implementations have a secret key
        /// </summary>
        /// <param name="secretKey"></param>
        public Otp(Key secretKey)
        {
            if (!(secretKey != null))
                throw new ArgumentNullException("A secret key must be provided");

            this.secretKey = secretKey;
        }

        /// <summary>
        /// An abstract definition of a compute method.  Takes a counter and runs it through the derived algorithm.
        /// </summary>
        /// <param name="counter">Counter or step</param>
        /// <returns>OTP calculated code</returns>
        protected abstract long Compute(long counter);

        /// <summary>
        /// Helper method that calculates OTPs
        /// </summary>
        protected internal long CalculateOtp(byte[] data, OtpHashMode mode)
        {
            byte[] hmacComputedHash = this.ComputeHmacHash(data, mode);

            // The RFC has a hard coded index 19 in this value.  Last is the same thing but also accomodates SHA256 and SHA512
            // hmacComputedHash[19] => hmacComputedHash[hmacComputedHash.Length - 1]

            int offset = hmacComputedHash[hmacComputedHash.Length - 1] & 0x0F;
            return (hmacComputedHash[offset] & 0x7f) << 24
                | (hmacComputedHash[offset + 1] & 0xff) << 16
                | (hmacComputedHash[offset + 2] & 0xff) << 8
                | (hmacComputedHash[offset + 3] & 0xff) % 1000000;
        }

        /// <summary>
        /// Uses the protected key to compute an HMAC Hash
        /// </summary>
        /// <param name="input">The data to use as the HMAC input</param>
        /// <param name="mode">The Hash algorithm to use</param>
        /// <returns>The hashed data</returns>
        public byte[] ComputeHmacHash(byte[] input, OtpHashMode mode)
        {
            byte[] hashedValue = null;

            using (HMAC hmac = CreateHmacHash(mode))
            {
                this.secretKey.UsePlainKey(key =>
                {
                    hmac.Key = key;
                    hashedValue = hmac.ComputeHash(input);
                });
            }

            return hashedValue;
        }

        /// <summary>
        /// Create an HMAC object for the specified algorithm
        /// </summary>
        private static HMAC CreateHmacHash(OtpHashMode otpHashMode)
        {
            HMAC hmacAlgorithm = null;
            switch (otpHashMode)
            {
                case OtpHashMode.Sha256:
                    hmacAlgorithm = new HMACSHA256();
                    break;
                case OtpHashMode.Sha512:
                    hmacAlgorithm = new HMACSHA512();
                    break;
                default: //case OtpHashMode.Sha1:
                    hmacAlgorithm = new HMACSHA1();
                    break;
            }
            return hmacAlgorithm;
        }

        /// <summary>
        /// converts a long into a big endian byte array.
        /// </summary>
        /// <remarks>
        /// RFC 4226 specifies big endian as the method for converting the counter to data to hash.
        /// </remarks>
        protected static internal byte[] GetBigEndianBytes(long input)
        {
            // Since .net uses little endian numbers, we need to reverse the byte order to get big endian.
            var data = BitConverter.GetBytes(input);
            Array.Reverse(data);
            return data;
        }

        /// <summary>
        /// truncates a number down to the specified number of digits
        /// </summary>
        protected internal static int Digits(long input, int digitCount)
        {
            return ((int)input % (int)Math.Pow(10, digitCount));
        }

        /// <summary>
        /// Verify an OTP value
        /// </summary>
        /// <param name="initialStep">The initial step to try</param>
        /// <param name="valueToVerify">The value to verify</param>
        /// <param name="matchedStep">Output parameter that provides the step where the match was found.  If no match was found it will be 0</param>
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

#if NO_WEB
#else

        /// <summary>
        /// Gets a URL that conforms to the de-facto standard
        /// created and used by Google
        /// </summary>
        protected string GetBaseKeyUrl(string user)
        {
            string url = null;
            this.secretKey.UsePlainKey(key =>
            {
                url = string.Format("otpauth://{0}/{1}?secret={2}", this.OtpType, HttpUtility.UrlEncode(user), Base32.Encode(key));
            });

            return url;
        }
#endif
        /// <summary>
        /// Used in generating URLs
        /// </summary>
        protected abstract string OtpType { get; }

        /// <remarks>
        /// This method simply exists for tests
        /// </remarks>
        internal string GetOtpType()
        {
            return this.OtpType;
        }
    }
}