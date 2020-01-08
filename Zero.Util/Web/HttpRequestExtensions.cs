using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zero.Util.Web
{
    public static class HttpRequestExtensions
    {

        /// <summary>
        /// 获取url
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        public static string GetAbsoluteUri(this HttpRequest httpRequest)
        {
            return new StringBuilder()
                 .Append(httpRequest.Scheme)
                 .Append("://")
                 .Append(httpRequest.Host)
                 .Append(httpRequest.PathBase)
                 .Append(httpRequest.Path)
                 .Append(httpRequest.QueryString)
                 .ToString();
        }

        /// <summary>
        /// 获取客户Ip
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetClientUserIp(this HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection.RemoteIpAddress.ToString();//.LocalIpAddress.ToString();
            }
            return ip;
        }

    }
}
