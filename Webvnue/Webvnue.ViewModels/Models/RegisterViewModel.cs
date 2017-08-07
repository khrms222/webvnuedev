using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Webvnue.DatabaseLayer.DatabaseLayer;

namespace Webvnue.ViewModels.Models
{
    public class RegisterViewModel
    {
        public string id { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string firstname { get; set; }
        [Required]
        public string lastname { get; set; }
        [Required]
        public DateTime? dob { get; set; }
        public void HandleRequest()
        {
            id = Guid.NewGuid().ToString();

            string hashedPassword = sha256_hash(password);

            DatabaseUtility.insertAccount(id, email, hashedPassword, firstname, lastname, dob);
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