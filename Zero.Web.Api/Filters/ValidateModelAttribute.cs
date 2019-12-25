using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zero.Web.Api.Filters
{
    /// <summary>
    /// model验证过滤器
    /// </summary>
    public class ValidateModelAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(!context.ModelState.IsValid)
            {
                 context.Result = new ObjectResult(
                    context.ModelState.Values.SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage)
                    );
            }

            base.OnActionExecuting(context);
        }

    }
}
