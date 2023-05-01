using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebAPI.Models;
using NetCoreWebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Controllers
{
   
    [Authorize]
    [ApiController]
    public class JWTAuthenticateController : ControllerBase
    {
        private readonly IJWTAuthenticate _jWTAuthenticate;
        public JWTAuthenticateController(IJWTAuthenticate jWTAuthenticate)
        {
            this._jWTAuthenticate = jWTAuthenticate;
        }
        [HttpGet]
        [Route("api/User/login")]
        public IActionResult GetToken()
        {

            JWTAuthToken Token = _jWTAuthenticate.Token();
            if (Token.Token == null)
                return Unauthorized();
            return Ok(Token);
        }

        [HttpGet]
        [Route("api/GetData")]
        public IActionResult GetData()
        {
            
            return Ok();
        }

    }
}
