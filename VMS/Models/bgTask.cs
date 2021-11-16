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
    public class sendAppointmentVerificationOTP : IJob
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<sendAppointmentVerificationOTP> logger;
        private readonly AppointmentService _appointmentService;


        public sendAppointmentVerificationOTP(IConfiguration configuration, ILogger<sendAppointmentVerificationOTP> logger, AppointmentService appointmentService)
        {
            this.logger = (ILogger<sendAppointmentVerificationOTP>)logger;
            this.configuration = configuration;
            _appointmentService = appointmentService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            // check if any appointment is close to the current time:
            List<Appointment> appointments = new List<Appointment>();
            appointments = _appointmentService.Get();

            foreach(Appointment appointment in appointments)
            {
                if ((appointment.appointmentTime - DateTime.Now).TotalSeconds <= 300)
                {
                    if(appointment.appUser.Email != null)
                        EmailOtp.sendOTP(appointment.appUser.Email, 1, "");
                }
            }

            await Task.CompletedTask;
        }
    }

    public class ScheduledJobFactory : IJobFactory
    {
        private readonly IServiceProvider serviceProvider;

        public ScheduledJobFactory(IServiceProvider serviceProvider)
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
