using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zero.Core.Entities;
using Zero.Core.Intefaces;
using Zero.Core.Intefaces.Sys;
using Zero.Infrastructure.Repository;
using Zero.Infrastructure.Resources.ViewModels;
using Zero.Util.Helpers;

namespace Zero.Web.Api.Controllers
{
    [Route("Api/Value")]
    [ApiController]
    public class ValueController:ControllerBase
    {
        private readonly ISysUserRepo _sysUserRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ValueController(
            ISysUserRepo sysUserRepo,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this._sysUserRepo = sysUserRepo;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        /// <summary>
        /// 默认是
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name ="GetValue")]
        public  ActionResult Index()
        {

            throw new Exception("asajkjk ");
            var result=  _sysUserRepo.FindEntity(Guid.Parse("8EB66204-8023-C1EB-6162-39F22EF35F5F"));
            //Sys_User sys_User = new Sys_User
            //{
            //    Id = NumberNo.SequentialGuid(),
            //    LoginName = "admin",
            //    DisplayName = "超级管理员",
            //    Password = "123456"
            //};
            //_sysUserRepo.Insert(sys_User);

            //if (!await _unitOfWork.SaveAsync())
            //{
            //    return Ok("失败");
            //}
            //return Ok("成功");

           var resuleResult= _mapper.Map<Sys_User, SysUserViewModel>(result);
            return Ok(resuleResult);
        }
    }
}
