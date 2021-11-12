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
}
