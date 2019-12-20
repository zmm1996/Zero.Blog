﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zero.Infrastructure.DataBase;
using Zero.Web.Api.Filters;

namespace Zero.Web.Api.Areas.Areas.Controllers
{
    [Route("api/Admin/[controller]")]
    [ApiController]
    [TypeFilter(typeof(CustomAuthorizationAttribute))]
    public class DefaultController : ControllerBase
    {
        private readonly MyContext _myContext;

        public DefaultController(MyContext myContext)
        {
            this._myContext = myContext;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult Get()
        {
           var user= HttpContext.User;

           //var result= _myContext.Sys_Role.Find(Guid.NewGuid());
            return Content("ss");
        }
    }
}