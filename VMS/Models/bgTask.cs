using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quartz;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz.Spi;

namespace VMS.Models
{
    [DisallowConcurrentExecution]
    public class sendAppointmentVerificationOTP : IJob
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<sendAppointmentVerificationOTP> logger;
        private readonly AppointmentService _appointmentService;
      

        public sendAppointmentVerificationOTP(IConfiguration configuration, ILogger<sendAppointmentVerificationOTP> logger, AppointmentService appointmentService, CampaignService cservice)
        {
            this.logger = (ILogger<sendAppointmentVerificationOTP>)logger;
            this.configuration = configuration;
            _appointmentService = appointmentService;
        }

        public void Run()
        {
            List<Appointment> appointments = new List<Appointment>(); 
            appointments = _appointmentService.Get(); 
           
            logger.LogInformation("Checking appointments to send otp!");
            foreach (Appointment appointment in appointments)
            {
                if ((appointment.appointmentTime - DateTime.Now).TotalSeconds <= 300 && (appointment.appointmentTime - DateTime.Now).TotalSeconds >= -300)
                {
                    if (appointment.appUser.Email != null)
                    {
                        var token = EmailOtp.sendOTP(appointment.appUser.Email, 1, "");
                        Appointment a = _appointmentService.Get(token);
                        _appointmentService.Update(a);
                    }
                }
            }
        }

        public async Task Execute(IJobExecutionContext context)
        {
            // check if any appointment is close to the current time:
           
            this.Run();

            await Task.CompletedTask;
        }

    }

    public class sendAppointmentVerificationOTPFactory : IJobFactory
    {
        private readonly IServiceProvider serviceProvider;

        public sendAppointmentVerificationOTPFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return serviceProvider.GetService(typeof(IJob)) as IJob;
        }

        public void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }

    }
}
