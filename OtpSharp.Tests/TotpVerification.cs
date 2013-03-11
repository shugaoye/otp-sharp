﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace OtpSharp.Tests
{
    [TestClass]
    public class TotpVerification
    {
        DateTime testTime = new DateTime(2009, 2, 13, 23, 31, 30);

        [TestMethod]
        public void ExactMatch_SpecificVerificationWindow()
        {
            var verificationWindow = new VerificationWindow();

            AssertWindow(verificationWindow);
        }

        [TestMethod]
        public void ExactMatch_SpecificVerificationWindowWithPriors()
        {
            var verificationWindow = new VerificationWindow(previous: 1);

            AssertWindow(verificationWindow);
        }

        [TestMethod]
        public void ExactMatch_NullVerificationWindow()
        {
            VerificationWindow verificationWindow = null;

            AssertWindow(verificationWindow);
        }

        [TestMethod]
        public void PreviousMatch_SpecificVerificationWindow()
        {
            var verificationWindow = new VerificationWindow(previous: 1);

            AssertWindow(verificationWindow, -1);
        }

        [TestMethod]
        public void PreviousNonMatch_SpecificVerificationWindow()
        {
            var verificationWindow = new VerificationWindow();

            AssertWindow(verificationWindow, -1, false);
        }

        [TestMethod]
        public void FutureMatch_SpecificVerificationWindow()
        {
            var verificationWindow = new VerificationWindow(future: 1);

            AssertWindow(verificationWindow, 1);
        }

        [TestMethod]
        public void FutureNonMatch_SpecificVerificationWindow()
        {
            var verificationWindow = new VerificationWindow();

            AssertWindow(verificationWindow, 1, false);
        }

        [TestMethod]
        public void ExactMatch_SpecificVerificationWindowUtcTime()
        {
            var totp = new Totp(OtpCalculationTests.RfcTestKey);

            // we don't want this to cross over the time step window mid test.
            // if there is at least a second remaining we should be just fine
            while (totp.RemainingSeconds() == 0)
                Thread.Sleep(100);

            var code = totp.ComputeTotp();
            long window;
            Assert.IsTrue(totp.VerifyTotp(code, out window));
            Assert.IsTrue(window > 0);
        }

        private void AssertWindow(VerificationWindow verificationWindow, int i = 0, bool shouldMatch = true)
        {
            var time = testTime.AddSeconds(30 * i);

            var totp = new Totp(OtpCalculationTests.RfcTestKey);
            var expected = totp.ComputeTotp(time);
            long timeStepUsed;
            var success = totp.VerifyTotp(testTime, expected, out timeStepUsed, verificationWindow);
            if (shouldMatch)
                Assert.IsTrue(success);
            else
                Assert.IsFalse(success);
        }

        [TestMethod]
        public void Totp_EnsureKeyIntegrity()
        {
            var key = OtpCalculationTests.RfcTestKey;
            var totp = new Totp(key);
            CollectionAssert.AreEqual(OtpCalculationTests.RfcTestKey, key);
        }

        [TestMethod]
        public void TotpUrl()
        {
            var url = KeyUrl.GetTotpUrl(OtpCalculationTests.RfcTestKey, "user");
            Assert.AreEqual(string.Format("otpauth://totp/user?secret={0}", Base32.Encode(OtpCalculationTests.RfcTestKey)), url);
        }

        [TestMethod]
        public void TotpUrl_Sha256()
        {
            var url = KeyUrl.GetTotpUrl(OtpCalculationTests.RfcTestKey, "user", mode: OtpHashMode.Sha256);
            Assert.AreEqual(string.Format("otpauth://totp/user?secret={0}&algorithm=Sha256", Base32.Encode(OtpCalculationTests.RfcTestKey)), url);
        }

        [TestMethod]
        public void TotpUrl_Sha512()
        {
            var url = KeyUrl.GetTotpUrl(OtpCalculationTests.RfcTestKey, "user", mode: OtpHashMode.Sha512);
            Assert.AreEqual(string.Format("otpauth://totp/user?secret={0}&algorithm=Sha512", Base32.Encode(OtpCalculationTests.RfcTestKey)), url);
        }

        [TestMethod]
        public void TotpUrl_StepSizeFifteen()
        {
            var url = KeyUrl.GetTotpUrl(OtpCalculationTests.RfcTestKey, "user", step: 15);
            Assert.AreEqual(string.Format("otpauth://totp/user?secret={0}&period=15", Base32.Encode(OtpCalculationTests.RfcTestKey)), url);
        }

        [TestMethod]
        public void TotpUrl_DigitsEight()
        {
            var url = KeyUrl.GetTotpUrl(OtpCalculationTests.RfcTestKey, "user", totpSize: 8);
            Assert.AreEqual(string.Format("otpauth://totp/user?secret={0}&digits=8", Base32.Encode(OtpCalculationTests.RfcTestKey)), url);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TotpUrl_DigitsTen()
        {
            var url = KeyUrl.GetTotpUrl(OtpCalculationTests.RfcTestKey, "user", totpSize: 10);
        }

        [TestMethod]
        public void TotpUrl_Sha512AndStepSizeFifteen()
        {
            var url = KeyUrl.GetTotpUrl(OtpCalculationTests.RfcTestKey, "user", step: 15, mode: OtpHashMode.Sha512);
            Assert.AreEqual(string.Format("otpauth://totp/user?secret={0}&algorithm=Sha512&period=15", Base32.Encode(OtpCalculationTests.RfcTestKey)), url);
        }

        [TestMethod]
        public void TotpUrl_Sha512AndStepSizeFifteenDigitsEight()
        {
            var url = KeyUrl.GetTotpUrl(OtpCalculationTests.RfcTestKey, "user", step: 15, mode: OtpHashMode.Sha512, totpSize: 8);
            Assert.AreEqual(string.Format("otpauth://totp/user?secret={0}&algorithm=Sha512&period=15&digits=8", Base32.Encode(OtpCalculationTests.RfcTestKey)), url);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TotpUrl_EmptyUser()
        {
            var url = KeyUrl.GetTotpUrl(OtpCalculationTests.RfcTestKey, string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TotpUrl_NullUser()
        {
            var url = KeyUrl.GetTotpUrl(OtpCalculationTests.RfcTestKey, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TotpUrl_NullKey()
        {
            var url = KeyUrl.GetTotpUrl(null, "user");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TotpUrl_EmptyKey()
        {
            var url = KeyUrl.GetTotpUrl(new byte[] { }, "user");
        }

        [TestMethod]
        public void TotpUtcOverload()
        {
            var totp = new Totp(OtpCalculationTests.RfcTestKey);

            // we don't want this test to barely hit the time window crossover.
            // If there is at least 1 remaining second we should be OK
            while (totp.RemainingSeconds() == 0)
                Thread.Sleep(100);

            var code1 = totp.ComputeTotp(DateTime.UtcNow);
            var code2 = totp.ComputeTotp();

            Assert.AreEqual(code1, code2);
        }

        [TestMethod]
        public void TotpRemainingTimeUtcOverload()
        {
            var totp = new Totp(OtpCalculationTests.RfcTestKey);

            var remaining1 = totp.RemainingSeconds();
            var remaining2 = totp.RemainingSeconds(DateTime.UtcNow);

            Assert.AreEqual(remaining1, remaining2);
        }
    }
}
