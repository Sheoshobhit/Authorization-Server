using ASPNetIdentity.Demo;
using ASPNetIdentity.Models;
using IdentityServer3.Core.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using IdentityServer3.EntityFramework;
using AspNetIdentity.Demo.Providers;
using AspNetIdentity.Demo.Interfaces;
using AspNetIdentity.Demo.EmailComposers;

namespace ASPNetIdentity.Controllers
{
    public class PasswordController : ApiController
    {
        public PasswordController()
        {
            UserManager.PasswordValidator = new MinimumLengthValidator(1);
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        /// <summary>
        /// Change currently logged in user's password.
        /// </summary>
        /// <param name="changePassword"></param>
        /// <returns></returns>
        [Route("ChangePassword")]
        [HttpPost]
        [Authorize]
        public Task<HttpResponseMessage> ChangePassword([FromBody]ChangePassword changePassword)
        {
            
            string emailAddress = ClaimsPrincipal.Current.Claims.Single(claim => claim.Type == "email").Value;
            var user = UserManager.FindByEmail(emailAddress);

            if (user == null)
                return Task.FromResult(Request.CreateResponse(HttpStatusCode.NotFound, "Email Address is not present"));
            if (!new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d\W]{6,}$").IsMatch(changePassword.NewPassword))
            {
                var response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Format of the password is not valid");
                return Task.FromResult(response);
            }
            var result = UserManager.ChangePassword(user.Id, changePassword.CurrentPassword, changePassword.NewPassword);
            if (!result.Succeeded)
            {
                return Task.FromResult(Request.CreateResponse(HttpStatusCode.Forbidden, result.Errors));
            }

            
            //IEmailComposer emailComposer = new PasswordChangedEmailComposer(emailAddress);
            //IEmailProvider emailProvider = new EmailProvider();
            //emailProvider.Send(emailComposer.Compose());
            return Task.FromResult(Request.CreateResponse(HttpStatusCode.Created));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forgotPassword"></param>
        /// <returns></returns>
        [Route("ForgottenPassword")]
        [HttpPost]
        public Task<HttpResponseMessage> ForgotPassword([FromBody] ForgotPassword forgotPassword)
        {
            var user = UserManager.FindByEmail(forgotPassword.Email);
            if (user == null)
                return
                    Task.FromResult(Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Email Address not found"));

            if (UserManager.IsLockedOut(user.Id))
                return Task.FromResult(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Account Locked out"));
            //var client = Request.Headers.Contains("ClientId")
            //    ? GetClient(Request.Headers.GetValues("ClientId").FirstOrDefault())
            //    : null;

            //if (forgotPassword.ClientId != null)
            //    client = GetClient(forgotPassword.ClientId);

            var resetToken = UserManager.GeneratePasswordResetToken(user.Id);
            var encodedToken = HttpUtility.UrlEncode(resetToken);
            IEmailComposer composer =
                 new ForgottenPasswordEmailComposer(
                     Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.AbsolutePath, ""),
                     forgotPassword.Email,
                     encodedToken);
            IEmailProvider email = new EmailProvider();
            email.Send(composer.Compose());

            return Task.FromResult(Request.CreateResponse(HttpStatusCode.Created));
        }

        [Route("ResetForgottenPassword")]
        [HttpPost]
        public Task<HttpResponseMessage> ForgotPasswordConfirm([FromBody]ResetForgotPassword resetPassword)
        {
            //IValidator<ForgotPasswordResetPassword> validator = new ForgotPasswordResetPasswordValidator();
            //validator.Validate(resetPassword);
            var decodedCode = HttpUtility.UrlDecode(resetPassword.ConfirmationCode);
            var user = UserManager.FindByEmail(resetPassword.Email);
            if (user == null)
                return Task.FromResult(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Email Address not found"));

            var result = UserManager.ResetPassword(user.Id, decodedCode, resetPassword.NewPassword);
            if (!result.Succeeded)
                return Task.FromResult(Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Invalid Token"));

            IEmailComposer composer = new PasswordChangedEmailComposer(resetPassword.Email);
            IEmailProvider emailProvider = new EmailProvider();
            emailProvider.Send(composer.Compose());

            UserManager.SetLockoutEndDate(user.Id, DateTime.UtcNow.AddDays(-1));
            return Task.FromResult(Request.CreateResponse(HttpStatusCode.Created));
        }
        private Client GetClient(string clientId)

        {
            var efConfig = new EntityFrameworkServiceOptions
            {
                ConnectionString = "IdentityServer3Data",
            };
            using (var db = new ClientConfigurationDbContext(efConfig.ConnectionString))
            {
                return db.Clients.Where(x => x.ClientId == clientId).Select(x => new Client
                {
                    ClientName = x.ClientName,
                    RedirectUris = x.RedirectUris.Select(uri => uri.Uri).ToList()
                }).FirstOrDefault();
            }
        }
    }
}
