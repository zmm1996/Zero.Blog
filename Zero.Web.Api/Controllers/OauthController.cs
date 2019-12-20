using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Zero.Web.Api.Auth;

namespace Zero.Web.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OauthController : ControllerBase
    {
        private readonly AppAuthenticationSettings _appSettings;

        public OauthController(IOptions<AppAuthenticationSettings> appSettings)
        {
            this._appSettings = appSettings.Value;
        }
        [HttpGet]
        public IActionResult Auth(string username, string password)
        {
            var claimsIdentity = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim("guid","8EB66204-8023-C1EB-6162-39F22EF35F5F"),
                    new Claim("avatar",""),
                    new Claim("displayName","超级管理员")

                });
            var token = JwtBearerAuthenticationExtension.GetJwtAccessToken(_appSettings, claimsIdentity);


            return Ok(token);
        }

        [HttpGet]
        public IActionResult ValidatorAuth(string token)
        {
            var result = JwtBearerAuthenticationExtension.GetPrincipalFromAccessToken(token, _appSettings);

            return Ok(result);
        }




    }
}
