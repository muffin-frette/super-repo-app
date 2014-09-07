using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    static class SecurityUtils
    {
        /// <summary>
        /// Customs the xertificate validation.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="cert">The cert.</param>
        /// <param name="chain">The chain.</param>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        public static bool CustomXertificateValidation(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            var certificate = (X509Certificate2)cert;
            return true;
        }

    }
}
