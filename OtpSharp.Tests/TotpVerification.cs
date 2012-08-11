using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

        private void AssertWindow(VerificationWindow verificationWindow, int i = 0, bool shouldMatch = true)
        {
            var time = testTime.AddSeconds(30 * i);

            var totp = new Totp(OtpCalculationTests.rfcTestKey);
            var expected = totp.ComputeTotp(time);
            long timeStepUsed;
            var success = totp.VerifyTotp(testTime, expected, out timeStepUsed, verificationWindow);
            if (shouldMatch)
                Assert.IsTrue(success);
            else
                Assert.IsFalse(success);
        }
    }
}
