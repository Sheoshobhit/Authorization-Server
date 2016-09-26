using AspNetIdentity.Demo.Entities;
using AspNetIdentity.Demo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetIdentity.Demo.EmailComposers
{
    public class PasswordChangedEmailComposer:IEmailComposer
    {
        public string emailAddress;

        public PasswordChangedEmailComposer(string emailAddress)
        {
            this.emailAddress = emailAddress;
        }

        public EmailMessage Compose()
        {
            return new EmailMessage
            {
                Body = String.Format(@" your Password has been changed"),
                From = "noreply@noreply.com",
                Subject = "Password Changed",
                To = emailAddress
            };
        }
    }
}
