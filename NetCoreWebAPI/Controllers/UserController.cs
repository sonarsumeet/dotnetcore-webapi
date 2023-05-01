using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebAPI.Models;
using NetCoreWebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Controllers
{
    
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsers _users;
        private readonly IJWTAuthenticate _jwtAuthenticate;
        public UserController(IUsers users, IJWTAuthenticate jWTAuthenticate)
        {
            this._users = users;
            this._jwtAuthenticate = jWTAuthenticate;
        }

        [Route("api/[controller]/CreateUsers")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(Users objUsers)
        {
            try 
            {
                var userData = new Users
                {
                    UserName = objUsers.UserName,
                    MailAddress = objUsers.MailAddress,
                    MobileAddress = objUsers.MobileAddress,
                    CreateTime = DateTime.Now
                };

               bool result =  await _users.CreateUsers(userData);
            }
            catch(Exception ex)
            {
                return NotFound();
            }
            return Ok();
        }

        [Route("api/[controller]/UserLogin")]
        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            try
            {
                string loginUserName = _users.LoginUser(loginModel);
                if (loginUserName == null || loginUserName == "")
                    return Unauthorized();

                JWTAuthToken Token = _jwtAuthenticate.Token(); //Creating the Access Token

                //Creating The Refresh Token
                var refreshToken = GenerateRefreshToken();

                //Setting the Refresh Token
                RefreshToken(refreshToken.Token);


                return Ok(new { 
                    message = "Success",
                    token = Token,
                    refreshToken = refreshToken
                });

            }
            catch(Exception ex)
            {
                return NotFound();
            }
            
        }

        private RefreshToken GenerateRefreshToken()
        {
            try
            {
                //var randomBytes = new byte[32];

                var randomNumber = new byte[32];
                var rng = RandomNumberGenerator.Create();
                rng.GetBytes(randomNumber);

                var RefreshToken = new RefreshToken()
                {
                    
                    Token = Convert.ToBase64String(randomNumber),
                    RefreshTokenExpires = DateTime.Now.AddMinutes(5),
                    RefreshTokenCreate = DateTime.Now

                };
                return RefreshToken;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        private void RefreshToken(string refreshToken)
        {
            try
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.Now.AddMinutes(2)
                };
                Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
            }
            catch(Exception ex)
            {

            }
        }

    }
}
