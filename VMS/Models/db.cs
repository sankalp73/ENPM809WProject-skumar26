using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
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
}


