using System;
using System.Security.Cryptography;
using System.Text;

namespace BgetCore
{
    public class Utils
    {
        public static string GetMD5(string fingerPrint)
        {
            var md5 = MD5.Create();
            
            // Get string byte and then return
            byte[] textToHash = Encoding.UTF8.GetBytes(fingerPrint);
            byte[] md5Byte = md5.ComputeHash(textToHash);

            // Convert back to string
            return BitConverter.ToString(md5Byte).Replace("-", "").ToLower();
        }
    }
}