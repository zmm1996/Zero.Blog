using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zero.Web.Api.Extensions
{
    public static class ExceptionMiddlewareExtension
    {

        /// <summary>
        /// 扩展异常类
        /// </summary>
        /// <param name="app"></param>
        public static void UseExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
