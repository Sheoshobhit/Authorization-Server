using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace Demo.Api.Controllers
{
    [RoutePrefix("api/Home")]
    public class HomeController : ApiController
    {
        public HomeController() { }

        [HttpGet]
        public IHttpActionResult Get() {
            return Ok("Unauthorised");
        }

        [HttpGet]
        [Authorize]
        [Route("GetAuthorize")]
        public IHttpActionResult GetAuthorize()
        {
            var user = User as ClaimsPrincipal;
            var email = user?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
            return Ok(email);
        }
    }
}
