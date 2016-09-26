using AspNetIdentity.Demo.Entities;
using AspNetIdentity.Demo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetIdentity.Demo.EmailComposers
{
    public class ForgottenPasswordEmailComposer : IEmailComposer
    {
        readonly string _emailAddress, _userManagementUrl, _resetPasswordToken;

        public ForgottenPasswordEmailComposer(string userManagementUrl, string emailAddress, string resetPasswordToken)
        {
            _emailAddress = emailAddress;
            _resetPasswordToken = resetPasswordToken;
            _userManagementUrl = userManagementUrl;
        }

        public EmailMessage Compose()
        {
            string resetTokenbase64 = "_";
            
                var resetTokenBytes = Encoding.UTF8.GetBytes(_resetPasswordToken);
                resetTokenbase64 = Convert.ToBase64String(resetTokenBytes);
            
            var emailBytes = Encoding.UTF8.GetBytes(_emailAddress);
            var emailEncoded = Convert.ToBase64String(emailBytes);

            string link = $"{_userManagementUrl}/#/Account/ResetPassword/{resetTokenbase64}/{emailEncoded}";
            //var tokenText = _isShortTokenImplementation ? $"Token: {_resetPasswordToken}" : string.Empty;
            var tokenText = string.Empty;
            var body =
                string.Format(
                    "Thanks for letting us know you've forgotten the password for your account. <b>{1}</b>  Please click on the link to change your password  </br> <a target='_blank' href={0}>{0}</a>",
                    link, tokenText);
            return new EmailMessage
            {
                From = "noreply@noreply.com",
                To = _emailAddress,
                Subject = "Forgotten password",
                Body = body
            };
        }
    }
}
