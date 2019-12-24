using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Zero.Core.Entities;

namespace Zero.Web.Api.Extensions.AuthContext
{
    public static class AuthContextService
    {
        private static IHttpContextAccessor _context;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _context = httpContextAccessor;
        }

        public static HttpContext Current => _context.HttpContext;

        /// <summary>
        /// 当前登录用户
        /// </summary>
        public static AuthContextUser CurrentUser
        {

            get
            {

                var user = new AuthContextUser()
                {
                    LoginName = Current.User.FindFirstValue(ClaimTypes.NameIdentifier),
                    DisplayName = Current.User.FindFirstValue("displayName"),
                    Avator = Current.User.FindFirstValue("avatar"),
                    Guid = new Guid(Current.User.FindFirstValue("id")),
                    UserType = (UserType)Convert.ToInt32(Current.User.FindFirstValue("userType"))
                };

                return user;
            }
        }

        /// <summary>
        /// 是否授权
        /// </summary>
        public static bool IsAuthenticated
        {
            get
            {

                return Current.User.Identity.IsAuthenticated;
            }
        }

        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        public static bool IsAdministrator
        {
            get
            {
                return (UserType)Convert.ToInt32(Current.User.FindFirstValue("userType")) == UserType.SuperAdministrator;
            }
        }




    }
}
