using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Zero.Core.Entities;
using Zero.Core.Intefaces;
using Zero.Core.Intefaces.Sys;
using Zero.Infrastructure.Resources.ViewModels;
using Zero.Util.Helpers;
using Zero.Util.Web;
using Zero.Web.Api.Auth;
using Zero.Web.Api.Extensions;

namespace Zero.Web.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OauthController : ControllerBase
    {
        private readonly AppAuthenticationSettings _appSettings;
        private readonly ISysUserRepo _sysUserRepo;
        private readonly ISysUserActionLogRepo _sysUserActionLogRepo;
        private readonly IUnitOfWork _unitOfWork;

        public OauthController(IOptions<AppAuthenticationSettings> appSettings,
            ISysUserRepo sysUserRepo,
            ISysUserActionLogRepo sysUserActionLogRepo,
              IUnitOfWork unitOfWork
            )
        {
            this._appSettings = appSettings.Value;
            this._sysUserRepo = sysUserRepo;
            this._sysUserActionLogRepo = sysUserActionLogRepo;
            this._unitOfWork = unitOfWork;
        }
        [HttpPost]
        public IActionResult Auth([FromBody]OauthViewModel viewModel)
        {
            var response = ResponseModelFactory.CreateInstance;

            var user = _sysUserRepo.FindEntity(x=>x.LoginName== viewModel .UserName&& x.Password== viewModel.Password
            );
            if(user == null)
            {
                response.SetNotFound("用户名密码不正确");
                return Ok(response);
            }
            var claimsIdentity = new ClaimsIdentity(new Claim[]
                {
                    
                    new Claim(ClaimTypes.NameIdentifier, viewModel.UserName),
                    new Claim("id",user.Id.ToString()),
                    new Claim("avatar",""),
                    new Claim("LoginName",user.LoginName),
                    new Claim("displayName",user.DisplayName),
                    new Claim("userType",((int)user.UserType).ToString())

                });
            var token = JwtBearerAuthenticationExtension.GetJwtAccessToken(_appSettings, claimsIdentity);
            Log(user.Id, user.LoginName + "_" + user.DisplayName);

            var result = new
            {
                token,
                user.DisplayName
            };
            response.SetData(result);

            return Ok(response);
        }

        [HttpGet]
        public IActionResult ValidatorAuth(string token)
        {
            var result = JwtBearerAuthenticationExtension.GetPrincipalFromAccessToken(token, _appSettings);

            return Ok(result);
        }



        private void Log(Guid userid,string userName)
        {
            var log = new Sys_UserActionLog()
            {
                Action = "Auth",
                Controller = "Oauth",
                ActionTime = DateTime.Now,
                ActionUserId = userid,
                ActionUserName = userName,
                ApiUrl = Request.GetAbsoluteUri(),
                Description = "登录",
                Id = NumberNo.SequentialGuid(),
                IsDeleted = (int)CommonEnum.IsDeleted.No,
                IP =HttpContext.GetClientUserIp()
            };
            _sysUserActionLogRepo.Insert(log);
            _unitOfWork.Save();
        }




    }
}
