using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zero.Core.Entities;
using Zero.Core.Intefaces.Sys;
using Zero.Infrastructure.Repository;

namespace Zero.Web.Api.Controllers
{
    [Route("Api/Value")]
    public class ValueController:ControllerBase
    {
        private readonly ISysUserRepo _sysUserRepo;

        public ValueController(ISysUserRepo sysUserRepo)
        {
            this._sysUserRepo = sysUserRepo;
        }
        [HttpGet]
        public async Task<ActionResult> Index()
        {
           
          var result= await _sysUserRepo.FindEntity(Guid.NewGuid());
            return Ok("ssss");
        }
    }
}
