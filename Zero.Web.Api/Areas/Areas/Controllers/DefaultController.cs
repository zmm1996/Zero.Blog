using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Zero.Infrastructure.DataBase;
using Zero.Web.Api.Filters;

namespace Zero.Web.Api.Areas.Areas.Controllers
{
    [Route("api/Admin/[controller]")]
    [ApiController]
   // [TypeFilter(typeof(CustomAuthorizationAttribute))]
    public class DefaultController : ControllerBase
    {
        private readonly MyContext _myContext;
     
        private readonly ILogger<DefaultController> _logger;

        public DefaultController(MyContext myContext,
            ILogger<DefaultController>  logger

         
            )
        {
            this._myContext = myContext;
        
            this._logger = logger;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult Get()
        {
             _logger.LogInformation("_-------------------------");
          
            throw new Exception("好借好还");

           var user= HttpContext.User;

           //var result= _myContext.Sys_Role.Find(Guid.NewGuid());
            return Content("ss");
        }
    }
}