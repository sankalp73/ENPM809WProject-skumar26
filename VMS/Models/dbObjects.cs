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

        public decimal Password { get; set; }

        public string Salt { get; set; }

        public DateTime PassSetDate { get; set; }
        
    }
}
