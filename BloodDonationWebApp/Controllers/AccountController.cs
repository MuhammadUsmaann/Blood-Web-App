using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BloodDonationWebApp.Entities;

namespace BloodDonationWebApp.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(Users userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            return Ok();
        }
    }
}
