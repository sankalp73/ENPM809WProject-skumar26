using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OtpNet;

namespace VMS.Models
{
    public class OTP
    {
        public Totp totp;
        public string token;
        public DateTime created;

    }

    public class TokenHashMap
    {
        public static Dictionary<string, OTP> HashMap = new Dictionary<string, OTP>();
    }   

    public class EmailOtp
    {

        public Tuple<string, string> getConfig(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(path);

            foreach (string line in lines)
            {
                string[] values = line.Split(',');
                return Tuple.Create(values[0], values[1]);
            }
            return Tuple.Create("", "");
        }

        public static void sendOTP(string mail, int cmd, string details)
        {
            string email = mail;
            var emailotp = new EmailOtp();
            OTP Otp = new OTP();
            byte[] secretKey = Encryption.Hash(email);
            Otp.totp = new Totp(secretKey, totpSize: 8, step: 5*60, mode: OtpHashMode.Sha512);
            Otp.token = Otp.totp.ComputeTotp(DateTime.UtcNow);
            Otp.created = DateTime.Now;

            TokenHashMap.HashMap["email"] = Otp;
            Tuple<string, string> t;
            t = emailotp.getConfig("C:\\Users\\student\\Workspace\\config.txt");
            string to = email; //To address    
            string from = t.Item1; //From address    
            MailMessage message = new MailMessage(from, to);
            string Url = "";
            string mailbody = "";
            switch (cmd)
            {
                case 0:
                    Url = "https://localhost:44337/VerifyAccount?token=" + details +"&email=" + email;
                    mailbody = "Click on the link below to verify your account:" + Url;
                    message.Subject = "Please verify your account";
                    break;
                case 1:
                    mailbody = "Please provide this to the Admin at the center:" + Otp.token;
                    message.Subject = "OTP for appointment";
                    break;
                case 2:
                    mailbody = "OTP for 2FA: \n" + details;
                    message.Subject = "VMS OTP";
                    break;
            }
           
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential(t.Item1, t.Item2);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            client.Send(message);
        }
    }
}
