using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aspweb.DTO
{
    public class PasswordEncode
    {
        public static string Encode(string password)
        {
            try
            {
                byte[] encDataByte = new byte[password.Length];
                encDataByte = System.Text.Encoding.UTF8.GetBytes(password);
                string EncryptedData = Convert.ToBase64String(encDataByte);
                return EncryptedData;
            } catch (Exception ex)
            {
                throw new Exception("Error is " + ex.Message);
            }
        }
    }
}