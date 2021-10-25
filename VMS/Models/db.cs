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

    public class dbService {
        public IMongoClient client;
        public IMongoDatabase database;

        public dbService(IVMSDatabaseSettings settings)
        {
            client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName); 
        }
    }

    public class AdminService
    {
        private readonly IMongoCollection<Admin> _admins;

        public AdminService(dbService db,IVMSDatabaseSettings settings)
        { 
            _admins = db.database.GetCollection<Admin>(settings.AdminCollectionName);
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


