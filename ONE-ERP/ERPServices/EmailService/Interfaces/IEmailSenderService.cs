using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.EmailService.Interfaces
{
   public interface IEmailSenderService
    {
        Task SendEmail(string mailTo, string subject, string message);
        Task SendEmailWithFrom(string mailTo, string name, string subject, string message);
        Task SendEmailViaAppPass(string mailTo, string subject, string message);
        Task SendEmailNew();
    }
}
