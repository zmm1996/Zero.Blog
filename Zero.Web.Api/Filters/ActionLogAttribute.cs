
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using Zero.Core.Entities;
using Zero.Infrastructure.DataBase;
using Zero.Util.Helpers;
using Zero.Web.Util.Extensions.AuthContext;
using Zero.Util.Web;
namespace Zero.Web.Api.Filters
{
    public class ActionLogAttribute : ActionFilterAttribute
    {
        public string Title { get; set; }

        public ActionLogAttribute(string title)
        {
            Title = title;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            base.OnActionExecuting(context);

        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {

            var myContext = context.HttpContext.RequestServices.GetService(typeof(MyContext)) as MyContext;


            var controllerName = context.RouteData.Values["controller"].ToString();
            var actionName = context.RouteData.Values["action"].ToString();
            var uri = context.HttpContext.Request.GetAbsoluteUri();

            
            

            var log = new Sys_UserActionLog()
            {
                Action = actionName,
                Controller = controllerName,
                ActionTime = DateTime.Now,
                ActionUserId = AuthContextService.CurrentUser.Guid,
                ActionUserName = AuthContextService.CurrentUser.DisplayName,
                ApiUrl = uri,
                Description = Title,
                Id = Guid.NewGuid(),
                IsDeleted = (int)CommonEnum.IsDeleted.No,
                IP = AuthContextService.CurrentUser.IpAddress
            };
            myContext.Sys_UserActionLog.Add(log);
            myContext.SaveChanges();

            base.OnResultExecuted(context);
        }
    }
}
