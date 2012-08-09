using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OtpSharp.Tests
{
    /// <summary>
    /// the RFC documentation provides test values
    /// </summary>
    /// <remarks>
    /// http://tools.ietf.org/html/rfc4226#appendix-D
    /// </remarks>
    [TestClass]
    public class HotpTests
    {
        readonly byte[] rfcTestKey = new byte[] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30 };

        [TestMethod]
        public void CounterZeroDecimalTest()
        {
            var i = Otp.CalculateHotp(rfcTestKey, 0);
            Assert.AreEqual(1284755224L, i); // value from RFC - Appendix D
        }

        [TestMethod]
        public void CounterOneDecimalTest()
        {
            var i = Otp.CalculateHotp(rfcTestKey, 1);
            Assert.AreEqual(1094287082L, i); // value from RFC - Appendix D
        }

        [TestMethod]
        public void CounterFiveDecimalTest()
        {
            var i = Otp.CalculateHotp(rfcTestKey, 5);
            Assert.AreEqual(868254676L, i); // value from RFC - Appendix D
        }
    }
}
