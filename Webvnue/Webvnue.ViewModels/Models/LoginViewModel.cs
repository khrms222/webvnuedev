using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Webvnue.ViewModels.Models
{
    public class LoginViewModel
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        public bool rememberme { get; set; }

        public void HandleRequest()
        {

        }

        public bool loginVerified()
        {
            string hashedPassword = sha256_hash(password);

            bool result = DatabaseLayer.DatabaseLayer.DatabaseUtility.verifyAccount(email, hashedPassword);

            return result;
        }

        public void clearAllFields()
        {
            email = "";
            password = "";
            rememberme = false;
        }

        public static String sha256_hash(String value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
}