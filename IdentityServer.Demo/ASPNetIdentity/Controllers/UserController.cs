using ASPNetIdentity.Demo;
using ASPNetIdentity.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ASPNetIdentity.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private ApplicationUserManager _userManager;
        //private RoleManager<IdentityRole> roleManager;


        //public UserController()
        //{

        //    UserManager.PasswordValidator = new MinimumLengthValidator(1);
        //    roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(ApplicationDbContext.Create()));

        //}

        //public UserController(ApplicationUserManager userManager)
        //{
        //    UserManager = userManager;
        //    UserManager.PasswordValidator = new MinimumLengthValidator(1);

        //}

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [Route("GetUserByName/{username}")]
        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> GetUserByName(string username)
        {
            var user = await this.UserManager.FindByNameAsync(username);

            if (user != null)
            {
                return Ok(user);
            }

            return NotFound();

        }
        /// <summary>
        /// Create a user
        /// </summary>
        /// <param name="user">user object</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [Route("CreateUser")]
        [HttpPost]
        public Task<HttpResponseMessage> Post([FromBody]User user)
        {
            try
            {
                
                if (UserManager.FindByEmailAsync(user.Email).Result != null)
                    return Task.FromResult(Request.CreateErrorResponse(HttpStatusCode.Conflict, "Email address already exists"));

                if (UserManager.FindByNameAsync(user.UserName).Result != null)
                    return Task.FromResult(Request.CreateErrorResponse(HttpStatusCode.Conflict, "Username already exists"));

                var userToCreate = new ApplicationUser(user.UserName) { Email = user.Email };
                IdentityResult result = UserManager.Create(userToCreate, user.Password);
                
                

                return Task.FromResult(Request.CreateResponse(HttpStatusCode.Created, user));
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }
    }
}