using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zero.Core.Entities;
using Zero.Core.Intefaces.Sys;
using Zero.Web.Api.Extensions;
using Zero.Web.Api.Filters;

namespace Zero.Web.Api.Controllers.V1
{
    [Route("api/v1/rabc/sysuserlog")]
    [ApiController]
    //[Authorize]
    [CustomAuthorization]
    public class SysUserLogController: ControllerBase
    {
        private readonly ISysUserActionLogRepo _sysUserActionLogRepo;

        public SysUserLogController(ISysUserActionLogRepo sysUserActionLogRepo)
        {
            this._sysUserActionLogRepo = sysUserActionLogRepo;
        }

        /// <summary>
        /// 获取所有操作记录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll(int pageIndex,int pageSize=30)
        {
            var response = ResponseModelFactory.CreateResultInstance;
             var data=  _sysUserActionLogRepo.IQueryable().OrderByDescending(x => x.ActionTime).Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();
            var totalCount=_sysUserActionLogRepo.IQueryable().Count();
            response.SetData(data, totalCount);

            return Ok(response);
        }


    }
}
