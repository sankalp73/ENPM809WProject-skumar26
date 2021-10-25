using System;
using System.Collections.Generic;
using MongoDB.Driver;

namespace VMS.Models
{
    public class VMSDatabaseSettings : IVMSDatabaseSettings
    {
        public string AdminCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

    }
    public interface IVMSDatabaseSettings
    {
        string AdminCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }

    public class AdminService
    {
        private readonly IMongoCollection<Admin> _admins;

        public AdminService(IVMSDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _admins = database.GetCollection<Admin>(settings.AdminCollectionName);
        }

        public List<Admin> Get() =>
            _admins.Find(Admin => true).ToList();

        public Admin Get(string id) =>
            _admins.Find<Admin>(admin => admin.Id == id).FirstOrDefault();

        public Admin Create(Admin admin)
        {
            _admins.InsertOne(admin);
            return admin;
        }

        public void Update(string id, Admin adminIn) =>
            _admins.ReplaceOne(admin => admin.Id == id, adminIn);

        public void Remove(Admin adminIn) =>
            _admins.DeleteOne(admin => admin.Id == adminIn.Id);

        public void Remove(string id) =>
            _admins.DeleteOne(admin => admin.Id == id);
    }
}


