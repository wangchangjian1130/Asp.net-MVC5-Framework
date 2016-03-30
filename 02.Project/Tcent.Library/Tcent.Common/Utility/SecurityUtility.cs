using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Tcent.Common
{
    /// <summary>
    /// SafetyUtility
    /// </summary>
    /// { Created At Time:[ 2016/3/16 18:54 ], By User:wcj21259, On Machine:WCJ }
    public sealed class SecurityUtility
    {
        /// <summary>
        /// MD5加密.
        /// </summary>
        /// <param name="encryptStr">The encrypt string.</param>
        /// <returns></returns>
        /// { Created At Time:[ 2015/12/31 9:51], By User:Jake Wang, On Machine:APP-DEV-JAKE}
        public static string Md5Encrypt(string encryptStr)
        {
            try
            {
                var md5Csp = new MD5CryptoServiceProvider();
                var md5Source = Encoding.Default.GetBytes(encryptStr);
                var md5Out = md5Csp.ComputeHash(md5Source);
                return Convert.ToBase64String(md5Out);
            }
            catch (Exception)
            {
                return encryptStr;
            }
        }
    }
}
