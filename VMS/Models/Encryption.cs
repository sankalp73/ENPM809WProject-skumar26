using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VMS.Models
{
    public class Encryption
    {
        static public byte[] ComputeHash(string password, byte[] salt)
        {
            var argon2 = new Konscious.Security.Cryptography.Argon2id(Encoding.UTF8.GetBytes(password));

            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 8; // four cores
            argon2.Iterations = 4;

            argon2.MemorySize = 1024 * 1024; // 1 GB

            return argon2.GetBytes(16);
        }

        static public byte[] CreateSalt()
        {
            var buffer = new byte[16];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buffer);
            return buffer;
        }

        static public Tuple<string, string> HashWithSalt(string password)
        {
            byte[] saltBytes = CreateSalt();
            byte[] digestBytes = ComputeHash(password, saltBytes);
            string salt = Convert.ToBase64String(saltBytes);
            string digest = Convert.ToBase64String(digestBytes);
            return Tuple.Create(salt, digest);

        }

        static public bool VerifyHashWithSalt(string password, string saltBytes, string storedPassword)
        {
            if (saltBytes[saltBytes.Length - 1] == '\r')
                saltBytes = saltBytes.Remove(saltBytes.Length - 1, 1);
            byte[] saltByte = Convert.FromBase64String(saltBytes);
            byte[] storedPass = Convert.FromBase64String(storedPassword);
            byte[] digestBytes = ComputeHash(password, saltByte);
            return digestBytes.SequenceEqual(storedPass);
        }
        static public byte[] Hash(string password)
        {
            byte[] saltBytes = CreateSalt();
            byte[] digestBytes = ComputeHash(password, saltBytes);
            return digestBytes;

        }
    }
}
