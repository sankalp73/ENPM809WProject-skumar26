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

        public string Password { get; set; }

        public Admin(string email, string pass)
        {
            this.Email = email;
            this.Password = pass;
        }

       // public string Salt { get; set; }

        //public DateTime PassSetDate { get; set; }
        
    }
}
