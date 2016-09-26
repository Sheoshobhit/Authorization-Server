using AspNetIdentity.Demo.Entities;
using AspNetIdentity.Demo.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetIdentity.Demo.EmailComposers
{
    public class AccountConfirmationEmailComposer : IEmailComposer
    {
        private string url;
        private string emailAddress;
        private string confirmationToken;

        /// <summary>
        /// Temporary class. THis should be removed once the universal notification solution is up and running
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="confirmationToken"></param>
        public AccountConfirmationEmailComposer(string emailAddress, string confirmationToken)
        {
            this.emailAddress = emailAddress;
            this.confirmationToken = confirmationToken;
        }

        public EmailMessage Compose()
        {
            var tokenUrl = string.Format(ConfigurationManager.AppSettings["AccountConfirmTokenUrl"], emailAddress, confirmationToken);
            var emailBody = string.Format("click the link to confirm your account <a target='_blank' href={0}>{0}</a>", tokenUrl);
            EmailMessage email = new EmailMessage()
            {
                To = emailAddress,
                From = "noreply@noreply.com",
                Subject = "Account Confirmation",
                Body = emailBody
            };
            return email;
        }
    }
}
