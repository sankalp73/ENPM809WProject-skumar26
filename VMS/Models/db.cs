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
        public string CertificateCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string AppointmentCollectionName { get; set; }
        public string DatabaseName { get; set; }

    }
    public interface IVMSDatabaseSettings
    {
        string AdminCollectionName { get; set; }
        string VaccineCampaignCollectionName { get; set; }
        string VaccineCenterCollectionName { get; set; }
        string CertificateCollectionName { get; set; }
        string AppointmentCollectionName { get; set; }
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

        public List<Center> Get(string cname, int zip, DateTime now) =>
          _center.Find(center => (center.campaign.Name.Equals(cname) && center.zip == zip && center.campaign.EndDate > now && center.quantity > 0)).ToList();

        public void Update(string name, string vname, int zip, Center newc) =>
            _center.ReplaceOne(center => center.Name.Equals(name) && center.vname.Equals(vname) && center.zip == zip, newc);
    }

    public class CertificateService
    {
        public dbService service;
        private readonly IMongoCollection<Certificate> _cert;
        public CertificateService(IVMSDatabaseSettings settings)
        {
            service = new dbService(settings);

            _cert = service.database.GetCollection<Certificate>(settings.CertificateCollectionName);
        }

        public Certificate Create(Certificate c)
        {
            _cert.InsertOne(c);
            return c;
        }

        public List<Certificate> Get(ApplicationUser user, Campaign camp) =>
            _cert.Find(certificate => certificate.applicationUser.Email.Equals(user.Email) && certificate.center.campaign.Name.Equals(camp.Name) ).ToList();
    }

    public class AppointmentService
    {
        public dbService service;
        private readonly IMongoCollection<Appointment> _app;
        public AppointmentService(IVMSDatabaseSettings settings)
        {
            service = new dbService(settings);

            _app = service.database.GetCollection<Appointment>(settings.AppointmentCollectionName);
        }

        public Appointment Create(Appointment c)
        {
            _app.InsertOne(c);
            return c;
        }

        public List<Appointment> Get() =>
            _app.Find(appointment => true).ToList();

        public Appointment Get(string email, string centerName, string vname, string campname) =>
            _app.Find<Appointment>(app => app.center.Name == centerName && app.appUser.Email.Equals(email) && app.center.vname.Equals(vname) && app.center.campaign.Name.Equals(campname)).FirstOrDefault();
    }
}


