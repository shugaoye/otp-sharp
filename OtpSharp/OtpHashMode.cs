using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OtpSharp
{
    /// <summary>
    /// Indicates which HMAC hash should be used
    /// </summary>
    public enum OtpHashMode
    {
        /// <summary>
        /// Sha1 is used as the HMAC hasing algorithm
        /// </summary>
        Sha1,
        /// Sha256 is used as the HMAC hasing algorithm
        Sha256,
        /// <summary>
        /// Sha512 is used as the HMAC hasing algorithm
        /// </summary>
        Sha512
    }
}
