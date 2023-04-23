using MobilePhoneDistributor_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Web;

namespace MobilePhoneDistributor_Web.Controllers
{
    internal class PasswordHasher
    {
        public static bool ValidatePassword(string Loginpassword, string StoredPassword, string StoredSalt)
        {
            byte[] salts = Convert.FromBase64String(StoredSalt);

            byte[] hash = GenerateHash(Loginpassword, salts);
            string hasedPassword = Convert.ToBase64String(hash);
            if (hasedPassword.Equals(StoredPassword))
            {
                return true;
            }
            return false;
        }

        public static string[] CreatePassword(string password)
        {
            byte[] salts = GenerateSalt();
            string StoredSalt =Convert.ToBase64String(salts);
            string StoredPassword=Convert.ToBase64String(GenerateHash(password,salts));

            return new string[]{
                StoredPassword, StoredSalt
            };
        }


        private static byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }

        private static byte[] GenerateHash(string password, byte[] salt)
        {
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

            Array.Copy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
            Array.Copy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

            using (var hashAlgorithm = new SHA256Managed())
            {
                return hashAlgorithm.ComputeHash(saltedPassword);
            }
        }
    }
    public class General
    {
        public static string GenerateStaffID(string LastesId)
        {
            if (LastesId  == null)
            {
                return "S0001";
            }
            int order = Int32.Parse(LastesId.Substring(1));
            if(order <10 ) {
                return "S000" + (order + 1);
            }else if(order <100) {
                return "S00" + (order + 1);
            }
            else if (order < 1000)
            {
                return "S0" + (order + 1);
            }
            else if (order < 10000)
            {
                return "S" + (order + 1);
            }
            return "S10001";
        }
        public static string GenerateReceiptId(Receipt lastestReceipt)
        {
            string reVal = "";
            if (lastestReceipt == null) {
                string Order = "R000";

                reVal += DateTime.Now.Date.ToString("MMddyy");
                reVal += Order;
                return reVal;
            }
            DateTime lastest = lastestReceipt.ReceiptDate;
            string ID = lastestReceipt.ReceiptId;
            if (lastest.Date < DateTime.Now.Date)
            {
                string Order = "R000";

                reVal += DateTime.Now.Date.ToString("MMddyy");
                reVal += Order;
                return reVal;

            }
            else if (lastest.Date == DateTime.Now.Date)
            {
                int order = Convert.ToInt32(ID.Substring(7, ID.Length - 7));
                order += 1;
                if (order < 10)
                {
                    return String.Format("{0}R{1}", DateTime.Now.Date.ToString("MMddyy"), "00" + order);
                }
                else if (order < 100)
                {
                    return String.Format("{0}R{1}", DateTime.Now.Date.ToString("MMddyy"), "0" + order);
                }
                else
                {
                    return String.Format("{0}R{1}", DateTime.Now.Date.ToString("MMddyy"), order);
                }
            }
            return null;
        }
    }
}