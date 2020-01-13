
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zero.Core.Intefaces.Sys;
using Zero.Infrastructure.DataBase;
using Zero.Infrastructure.Repository.Sys;
using Zero.Web.Api.Auth;
using Zero.Web.Api.Extensions;
using Zero.Web.Util.Extensions.AuthContext;

namespace Zero.Web.Api.Filters
{
    public class CustomAuthorizationAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        //private readonly ISysUserRepo _sysUserRepo;

        //private readonly AppAuthenticationSettings _appSettings;
        //public CustomAuthorizationAttribute(ISysUserRepo sysUserRepo, IOptions<AppAuthenticationSettings> appSettings)
        //{
        //    this._sysUserRepo = sysUserRepo;
        //    this._appSettings = appSettings.Value;
        //}


        public void OnAuthorization(AuthorizationFilterContext context)
        {

            if (!AuthContextService.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            string methodType = context.HttpContext.Request.Method.ToLower();


            var _configuration = context.HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;
            var appSettings = _configuration.GetSection("AppSettings").Get<AppAuthenticationSettings>();


            var resonse = ResponseModelFactory.CreateInstance;
            if (!AuthContextService.IsAdministrator && methodType != "get"&&appSettings.AdministratorAction)
            {
                resonse.SetNoPermission();
                context.Result = new JsonResult(resonse);
                return;
            }
            //  var sysUserRepo = context.HttpContext.RequestServices.GetService(typeof(ISysUserRepo)) as ISysUserRepo;


            // ContentResult contentResult = new ContentResult
            // {

            //     StatusCode = 401,

            // };
            // var AuthorizationStr= context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            // var controllerName = context.ActionDescriptor.RouteValues["controller"]?.ToString();
            // var actionName = context.ActionDescriptor.RouteValues["action"]?.ToString();


            //string[] TokenArr= AuthorizationStr.Split(" ");
            //var userClaims= JwtBearerAuthenticationExtension.GetPrincipalFromAccessToken(TokenArr[1], _appSettings);

            // if (userClaims == null)
            // {
            //     contentResult.Content = "token错误";
            //     context.Result = contentResult;
            //     return;
            // }


            //var userGuid= userClaims.Identities.FirstOrDefault()?.Claims.Where(x=>x.Type=="guid").FirstOrDefault()?.Value;

            //var user=  _sysUserRepo.FindEntity(new Guid(userGuid));
            // if(user==null)
            // {
            //     contentResult.Content = "用户不存在";
            //     context.Result = contentResult;
            // }

        }
    }
}
