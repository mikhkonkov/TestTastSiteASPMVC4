using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TestTask.Domain.Concrete {
    public static class SecurityHelper {
        public static string getHash(string s) {
            byte[] bytes = Encoding.Unicode.GetBytes(s);
            var csp = new MD5CryptoServiceProvider();
            byte[] byteHash = csp.ComputeHash(bytes);
            string hash = string.Empty;
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);
            return hash;
        }
    }
}