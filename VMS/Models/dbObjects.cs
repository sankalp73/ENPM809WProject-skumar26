using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VMS.Models
{
    public class Admin
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public string Hash { get; set; }
        public string IdProof { get; set; }
        public int Verified { get; set; }
        public DateTime PassSetDate { get; set; }

        public Admin(string email, string salt, string hash, string idproof, DateTime passsetdate)
        {
            this.Email = email;
            this.Salt = salt;
            this.Hash = hash;
            this.PassSetDate = passsetdate;
            this.IdProof = idproof;
            this.Verified = 0;
        }
        
    }
}
