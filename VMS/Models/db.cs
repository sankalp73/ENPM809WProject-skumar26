using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using MongoDB.Driver;

namespace VMS.Models
{
    public class VMSDatabaseSettings : IVMSDatabaseSettings
    {
        public string AdminCollectionName { get; set; }
        public string VaccineCampaignCollectionName { get; set; }
        public string VaccineCenterCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

    }
    public interface IVMSDatabaseSettings
    {
        string AdminCollectionName { get; set; }
        string VaccineCampaignCollectionName { get; set; }
        string VaccineCenterCollectionName { get; set; }
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

    public class CampaignService
    {
        public dbService service;
        private readonly IMongoCollection<Campaign> _campaign;
        public CampaignService(IVMSDatabaseSettings settings)
        {
            service = new dbService(settings);

            _campaign = service.database.GetCollection<Campaign>(settings.VaccineCampaignCollectionName);
        }

        public Campaign Create(Campaign c)
        {
            _campaign.InsertOne(c);
            return c;
        }

        public List<Campaign> Get() =>
            _campaign.Find(campaign => true).ToList();

        public Campaign Get(string name) =>
            _campaign.Find<Campaign>(campaign => campaign.Name == name).FirstOrDefault();
    }

    public class CenterService
    {
        public dbService service;
        private readonly IMongoCollection<Center> _center;
        public CenterService(IVMSDatabaseSettings settings)
        {
            service = new dbService(settings);

            _center = service.database.GetCollection<Center>(settings.VaccineCenterCollectionName);
        }

        public Center Create(Center c)
        {
            _center.InsertOne(c);
            return c;
        }

        public List<Center> Get() =>
            _center.Find(center => true).ToList();
    }
}


