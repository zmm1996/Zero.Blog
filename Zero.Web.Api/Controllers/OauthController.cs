using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Zero.Core.Intefaces.Sys;
using Zero.Util.Helpers;
using Zero.Web.Api.Auth;

namespace Zero.Web.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OauthController : ControllerBase
    {
        private readonly AppAuthenticationSettings _appSettings;
        private readonly ISysUserRepo _sysUserRepo;

        public OauthController(IOptions<AppAuthenticationSettings> appSettings,
            ISysUserRepo sysUserRepo
            )
        {
            this._appSettings = appSettings.Value;
            this._sysUserRepo = sysUserRepo;
        }
        [HttpGet]
        public IActionResult Auth(string username, string password)
        {

            var user = _sysUserRepo.FindEntity(x=>x.LoginName==username&&x.Password==password);
            if(user == null)
            {
                return BadRequest();
            }
            var claimsIdentity = new ClaimsIdentity(new Claim[]
                {
                    
                    new Claim(ClaimTypes.Name, username),
                    new Claim("id",user.Id.ToString()),
                    new Claim("avatar",""),
                    new Claim("LoginName",user.LoginName),
                    new Claim("displayName",user.DisplayName),
                    new Claim("userType",((int)user.UserType).ToString())

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
