using AspNetIdentity.Demo.Entities;
using AspNetIdentity.Demo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AspNetIdentity.Demo.Providers
{
    public class EmailProvider : IEmailProvider
    {
        public void Send(EmailMessage message)
        {
            var mail = new MailMessage(message.From, message.To)
            {

                Subject = message.Subject,
                Body = message.Body
            };
            mail.IsBodyHtml = true;
            var smtpClient = new SmtpClient();
            smtpClient.Send(mail);
        }
    }
}
