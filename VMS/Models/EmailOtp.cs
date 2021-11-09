using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OtpNet;

namespace VMS.Models
{
    public class OTP
    {
        public Totp totp;
        public string token;

    }

    public class TokenHashMap
    {
        public static Dictionary<string, OTP> HashMap = new Dictionary<string, OTP>();
    }

    public class EmailOtp
    {
        protected Tuple<string, string> getConfig()
        {
            string text = System.IO.File.ReadAllText(@"C:\Users\student\Workspace\Config.txt");

            // Display the file contents to the console. Variable text is a string.
            System.Console.WriteLine("Contents of WriteText.txt = {0}", text);

            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\student\Workspace\MigrateDatabase\customerlogin.txt");

            foreach (string line in lines)
            {
                string[] values = line.Split('|');
                return Tuple.Create(values[0], values[1]);
            }
            return Tuple.Create("", "");
        }

        public void sendOTP(string mail, int cmd, string details)
        {
            string email = mail;
            OTP Otp = new OTP();
            byte[] secretKey = Encryption.Hash(email);
            Otp.totp = new Totp(secretKey, totpSize: 8, step: 30, mode: OtpHashMode.Sha512);
            Otp.token = Otp.totp.ComputeTotp(DateTime.UtcNow);

            TokenHashMap.HashMap["email"] = Otp;
            Tuple<string, string> t;
            t = getConfig();
            string to = t.Item1; //To address    
            string from = t.Item1; //From address    
            MailMessage message = new MailMessage(from, to);
            string Url = "";
            string mailbody = "";
            switch (cmd)
            {
                case 0:
                    Url = "http://localhost:52251/Content/VerifyAccount.aspx?token=" + Otp.token;
                    mailbody = "Click on the link below to verify your account:" + Url;
                    message.Subject = "Please verify your account";
                    break;
                case 1:
                    Url = "http://localhost:52251/Content/AppointmentObliged.aspx?token=" + Otp.token;
                    mailbody = "Click on the link to check in to your appointment:" + Url;
                    message.Subject = "OTP for appointment";
                    break;
                case 2:
                    mailbody = "OTP for appointment your appointment scheduled for today. Below are the details: \n" + details;
                    message.Subject = "Appointment Reminder"; 
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
        }
    }
}
