﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OtpSharp.Tests
{
    [TestClass]
    public class ProtectedKeyTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ProtectedKey_Empty()
        {
            var key = new ProtectedKey(new byte[] { });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ProtectedKey_Null()
        {
            ProtectedKey key = new ProtectedKey(null);
        }

        [TestMethod]
        public void ProtectedKey_Basic()
        {
            var pk = new ProtectedKey(OtpCalculationTests.RfcTestKey);
            pk.UsePlainKey(key => CollectionAssert.AreEqual(OtpCalculationTests.RfcTestKey, key));
        }

        [TestMethod]
        public void ProtectedKey_WipeReference()
        {
            var key = OtpCalculationTests.RfcTestKey;
            var pk = ProtectedKey.CreateProtectedKeyAndDestroyPlaintextKey(key);
            CollectionAssert.AreNotEqual(OtpCalculationTests.RfcTestKey, key);
        }

        /// <remarks>
        /// This test exists because the original API would overwrite the plaintext key passed
        /// into the constructor with random garbage.  This test is to ensure that behavior
        /// isn't present anymore.
        /// </remarks>
        [TestMethod]
        public void ProtectedKey_EnsureOriginalkeyIntegrity()
        {
            var key = OtpCalculationTests.RfcTestKey;
            var pk = new ProtectedKey(key);
            CollectionAssert.AreEqual(OtpCalculationTests.RfcTestKey, key);
        }

        [TestMethod]
        public void ProtectedKey_ProtectKey()
        {
            var originalKey = KeyGeneration.GenerateRandomKey(16);
            var originalCopy = new byte[16];
            Array.Copy(originalKey, originalCopy, 16);
            CollectionAssert.AreEqual(originalKey, originalCopy);

            ProtectedMemory.Protect(originalCopy, MemoryProtectionScope.SameProcess);
            CollectionAssert.AreNotEqual(originalKey, originalCopy);

            var pk = ProtectedKey.CreateProtectedKeyFromPreProtectedMemory(originalCopy, 16, MemoryProtectionScope.SameProcess);

            pk.UsePlainKey(key => CollectionAssert.AreEqual(originalKey, key));
        }

        [TestMethod]
        public void ProtectedKey_ProtectKey_CrossProcess()
        {
            var originalKey = KeyGeneration.GenerateRandomKey(16);
            var originalCopy = new byte[16];
            Array.Copy(originalKey, originalCopy, 16);
            CollectionAssert.AreEqual(originalKey, originalCopy);

            ProtectedMemory.Protect(originalCopy, MemoryProtectionScope.CrossProcess);
            CollectionAssert.AreNotEqual(originalKey, originalCopy);

            var pk = ProtectedKey.CreateProtectedKeyFromPreProtectedMemory(originalCopy, 16, MemoryProtectionScope.CrossProcess);

            pk.UsePlainKey(key => CollectionAssert.AreEqual(originalKey, key));
        }

        [TestMethod]
        public void ProtectedKey_ProtectKey_SameLogon()
        {
            var originalKey = KeyGeneration.GenerateRandomKey(16);
            var originalCopy = new byte[16];
            Array.Copy(originalKey, originalCopy, 16);
            CollectionAssert.AreEqual(originalKey, originalCopy);

            ProtectedMemory.Protect(originalCopy, MemoryProtectionScope.SameLogon);
            CollectionAssert.AreNotEqual(originalKey, originalCopy);

            var pk = ProtectedKey.CreateProtectedKeyFromPreProtectedMemory(originalCopy, 16, MemoryProtectionScope.SameLogon);

            pk.UsePlainKey(key => CollectionAssert.AreEqual(originalKey, key));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ProtectedKey_ProtectKeyEmpty()
        {
            var pk = ProtectedKey.CreateProtectedKeyFromPreProtectedMemory(new byte[] { }, 16, MemoryProtectionScope.SameProcess);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ProtectedKey_ProtectKeyZeroLength()
        {
            var pk = ProtectedKey.CreateProtectedKeyFromPreProtectedMemory(OtpCalculationTests.RfcTestKey, 0, MemoryProtectionScope.SameProcess);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ProtectedKey_ProtectKeyNull()
        {
            var pk = ProtectedKey.CreateProtectedKeyFromPreProtectedMemory(null, 16, MemoryProtectionScope.SameProcess);
        }

        [TestMethod]
        public void ProtectedKey_MultipleUse()
        {
            var originalKey = KeyGeneration.GenerateRandomKey(16);
            var originalCopy = new byte[16];
            Array.Copy(originalKey, originalCopy, 16);
            CollectionAssert.AreEqual(originalKey, originalCopy);

            ProtectedMemory.Protect(originalCopy, MemoryProtectionScope.SameProcess);
            CollectionAssert.AreNotEqual(originalKey, originalCopy);

            var pk = ProtectedKey.CreateProtectedKeyFromPreProtectedMemory(originalCopy, 16, MemoryProtectionScope.SameProcess);

            // The key is protected and un-protected several times.
            // Make sure that the key can be used multiple times.
            for (int i = 0; i < 10; i++)
                pk.UsePlainKey(key => CollectionAssert.AreEqual(originalKey, key));
        }

        [TestMethod]
        public void ProtectedKey_ProtectKeyWithSpecificLength()
        {
            var originalKey = KeyGeneration.GenerateRandomKey(20);
            var originalCopy = new byte[32];
            Array.Copy(originalKey, originalCopy, 20);

            ProtectedMemory.Protect(originalCopy, MemoryProtectionScope.SameProcess);
            CollectionAssert.AreNotEqual(originalKey, originalCopy);

            var pk = ProtectedKey.CreateProtectedKeyFromPreProtectedMemory(originalCopy, 20, MemoryProtectionScope.SameProcess);

            pk.UsePlainKey(key => CollectionAssert.AreEqual(originalKey, key, "The unprotected plain key and the original key don't match"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ProtectedKey_UseKeyWithNullDelegate()
        {
            var pk = new ProtectedKey(OtpCalculationTests.RfcTestKey);
            pk.UsePlainKey(null);
        }

        [TestMethod]
        public void ProtectedKey_UseKeyWipeTempKey()
        {
            var pk = new ProtectedKey(OtpCalculationTests.RfcTestKey);
            byte[] tempKey = null;
            pk.UsePlainKey(key =>
            {
                tempKey = key;
                CollectionAssert.AreEqual(OtpCalculationTests.RfcTestKey, tempKey);
            });

            CollectionAssert.AreNotEqual(OtpCalculationTests.RfcTestKey, tempKey);
        }

        [TestMethod]
        public void ProtectedKey_UseKeyThrowExceptionWipeKey()
        {
            var pk = new ProtectedKey(OtpCalculationTests.RfcTestKey);
            byte[] tempKey = null;

            try
            {
                pk.UsePlainKey(key =>
                {
                    tempKey = key;
                    CollectionAssert.AreEqual(OtpCalculationTests.RfcTestKey, tempKey);
                    throw new ArgumentNullException(); // Throw a specific exception type of argument null as this is what is caught below
                });

                Assert.Fail("The exception should have diverted control away from here.");
            }
            catch (ArgumentNullException) // Catch a specific argument null exception so as not to catch assert exceptions if thrown
            {
                CollectionAssert.AreNotEqual(OtpCalculationTests.RfcTestKey, tempKey);
            }
        }
    }
}
