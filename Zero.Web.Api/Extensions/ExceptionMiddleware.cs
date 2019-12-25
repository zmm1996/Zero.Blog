using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zero.Web.Api.Extensions
{
    public  class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
               await  _next(context);
            }
            catch (Exception ex)
            {
               await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context,Exception exception)
        {
            var error = new ErrorDetails
            {
                StatusCode = 500,
                Message =  $"资源服务器忙,请稍候再试,原因:{exception.Message}"
            };
            context.Response.StatusCode = error.StatusCode;
            context.Response.ContentType = "application/json";
            return  context.Response.WriteAsync(error.ToString());
           
        }


    }
}
