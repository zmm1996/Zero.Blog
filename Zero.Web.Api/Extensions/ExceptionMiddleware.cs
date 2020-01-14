using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Zero.Web.Api.Extensions
{
    public  class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            this._logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
               await  _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"错误中间件——————————————————");
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
