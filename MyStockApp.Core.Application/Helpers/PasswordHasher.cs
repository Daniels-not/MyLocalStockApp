    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;

namespace MyStockApp.Core.Application.Helpers
{
    public class PasswordHasher
    {
        public static string ComputeSha256Hash(string password)
        {
            if (String.IsNullOrEmpty(password))
            {
                return String.Empty;
            }

            using (SHA256 sha256Hash = SHA256.Create())
            {
                // return byte array to use in our hash pass
                byte[] hash = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // conver our hash array into string 
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("x2").Replace("-", String.Empty));
                }
                return builder.ToString();
            }
        }
    }
}
