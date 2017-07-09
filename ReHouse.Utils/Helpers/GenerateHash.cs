using System;
using System.Security.Cryptography;
using System.Text;

namespace ReHouse.Utils.Helpers
{
    public class GenerateHash
    {
        public static string GetMd5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                //return data;//Encoding.UTF8.GetString(data);
                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                var sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                foreach (byte t in data)
                    sBuilder.Append(t.ToString("x2"));

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }
        public static string GetSha1Hash(string input)
        {
            using (var sha1Hash = SHA1.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = sha1Hash.ComputeHash(Encoding.UTF8.GetBytes(input + Guid.NewGuid().ToString()));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                var sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                foreach (byte t in data)
                    sBuilder.Append(t.ToString("x2"));

                // Return the hexadecimal string.
                return sBuilder.ToString();// + Guid.NewGuid().ToString();
            }
        }        
        public static string FixBase64ForImage(string Image)
        {
            string base64 = Image.Substring(Image.IndexOf(',') + 1);
            base64 = base64.Trim('\0');

            System.Text.StringBuilder sbText = new System.Text.StringBuilder(base64, base64.Length);
            sbText.Replace("\r\n", String.Empty); sbText.Replace(" ", String.Empty);
            return sbText.ToString();
        }
    }
}