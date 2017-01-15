using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ReHouse.Utils.BusinessOperations
{
    public class HashHelper
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
        public static byte[] GetMd5FromFile(string filename)
        {
            try
            {
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(filename))
                    {
                        return md5.ComputeHash(stream);
                    }
                }
            }
            catch (Exception)
            {
            }
            return null;
        }
        public static bool ByteArrayCompareWithSimplest(byte[] pBytesLeft, byte[] pBytesRight)
        {
            if (pBytesLeft.Length != pBytesRight.Length)
                return false;

            var length = pBytesLeft.Length;

            for (var i = 0; i < length; i++)
            {
                if (pBytesLeft[i] != pBytesRight[i])
                    return false;
            }

            return true;
        }
    }

}