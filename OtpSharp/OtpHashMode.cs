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
        Sha1,
        Sha256,
        Sha512
    }
}
