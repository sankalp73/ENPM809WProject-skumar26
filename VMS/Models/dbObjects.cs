using System;
using System.ComponentModel.DataAnnotations;
using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace VMS.Models
{
    [CollectionName("application")]
    public class ApplicationUser : MongoIdentityUser<Guid>
    {
    }

    [CollectionName("applicationRoles")]
    public class ApplicationRoles : MongoIdentityRole<Guid>
    {
    }

    public class Admin
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        public string Salt { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string IdProof { get; set; }
    }

    public class Campaign
    {
       
        [BsonId]
        public ObjectId id { get; set; }

        [Required]
        [StringLength(32)]
        [BsonElement("CampaignName")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [BsonElement("EndDate")]
        public DateTime EndDate { get; set; }
    }

    public class Center
    {
        [BsonId]
        public ObjectId id { get; set; }

        [Required]
        [BsonElement("CenterName")]
        public string Name { get; set; }

        [Required]
        [BsonElement("VaccineName")]
        public string vname { get; set; }

        [Range(1, 1000000)]
        [Required]
        [BsonElement("Qty")]
        public int quantity { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [BsonElement("startTime")]
        public DateTime startTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [BsonElement("EndTime")]
        public DateTime EndTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [BsonElement("EndDate")]
        public DateTime EndDate { get; set; }

        [Range(1, 1000000)]
        [Required]
        [BsonElement("Zip")]
        public int zip { get; set; }

        [Required]
        [BsonElement("Campaign")]
        public Campaign campaign { get; set; }


    }
}
