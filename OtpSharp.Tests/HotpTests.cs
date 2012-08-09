using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

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

        /// <summary>
        /// The test context
        /// </summary>
        public TestContext TestContext { get; set; }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
            "|DataDirectory|\\Rfc4226AppendixD.xml",
            "Row",
            DataAccessMethod.Sequential)]
        public void OtpAppendixDTests()
        {
            // test values from RFC - Appendix D
            long counter = Convert.ToInt64(this.TestContext.DataRow["counter"]);
            long expectedResult = Convert.ToInt64(this.TestContext.DataRow["decimal"]);

            var i = Otp.CalculateOtp(rfcTestKey, counter);
            Assert.AreEqual(expectedResult, i);
        }
    }
}
