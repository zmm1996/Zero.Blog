using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zero.Core.Entities;
using Zero.Core.Intefaces;
using Zero.Core.Intefaces.Sys;
using Zero.Infrastructure.Resources.ViewModels;
using Zero.Util.Helpers;
using Zero.Web.Api.Extensions;
using Zero.Web.Api.Filters;
using Zero.Web.Util.Extensions.AuthContext;

namespace Zero.Web.Api.Controllers.V1
{

    [Route("api/v1/rabc/sysuser")]
    [ApiController]
    [Authorize]
    public class SysUserController:ControllerBase
    {
        private readonly ISysUserRepo _sysUserRepo;
        private readonly IUserRoleRepo _userRoleRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SysUserController(
            ISysUserRepo sysUserRepo,
            IUserRoleRepo userRoleRepo,
            IMapper mapper,
            IUnitOfWork unitOfWork
            )
        {
            this._sysUserRepo = sysUserRepo;
            this._userRoleRepo = userRoleRepo;
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 用户集合
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name ="GetAll")]
        [ActionLog("获取用户集合")]
        public IActionResult GetAll()
        {
           var response= ResponseModelFactory.CreateResultInstance;
           var data= _sysUserRepo.FindList();
            response.SetData(data, data.Count());
            return Ok(response); 
        }

        /// <summary>
        /// 获取单个用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}",Name = "GetUserById")]
        [ActionLog("获取单个user")]
        public IActionResult GetUserById(Guid id)
        {
         var response= ResponseModelFactory.CreateInstance;

          var data=  _sysUserRepo.FindEntity(id);
           if(data==null)
            {
                response.SetNotFound();
            }

            response.SetData(data);
            return Ok(response);
        }


        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost( Name = "CreateUser")]
        [ActionLog("创建用户")]
        public IActionResult CreateUser([FromBody] SysUserCreateOrUpdateViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            var data = _sysUserRepo.FindEntity(x=>x.LoginName==model.LoginName);

            if (data != null)
            {
                response.SetFailed("登录名已存在");
                return Ok(response);
            }
            //if (!AuthContextService.IsAdministrator)
            //{
            //    response.SetFailed("权限不足");
            //    return Ok(response);
            //}
            Sys_User adduser = new Sys_User();
            
            _mapper.Map(model, adduser);
           // adduser.Password = CrypToHelper.HashPassword(model.Password);
            adduser.Create();

            _sysUserRepo.Insert(adduser);
            if (!_unitOfWork.Save())
            {
                response.SetFailed("添加失败");
                return Ok(response);
            }
            response.SetData(adduser.Id);
            return Ok(response);
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}",Name ="UpdateUser")]
        [ActionLog("更新用户")]
        public IActionResult UpdateUser(Guid id,[FromBody] SysUserCreateOrUpdateViewModel model)
        {
           var response= ResponseModelFactory.CreateInstance;
           var sysUser= _sysUserRepo.FindEntity(id);

            if(sysUser==null)
            {
                response.SetNotFound();
                return Ok(response);
            }
            if(sysUser.UserType==0&& !AuthContextService.IsAdministrator)
            {
                response.SetFailed("权限不足");
                return Ok(response);
            }
            _mapper.Map(model, sysUser);
          //  sysUser.Password =model.Password== sysUser.Password?sysUser.Password:CrypToHelper.HashPassword(model.Password);
            sysUser.Update();

            _sysUserRepo.Update(sysUser);
            if(!_unitOfWork.Save())
            {
                response.SetFailed("更新失败");
                return Ok(response);
            }
            response.SetData(sysUser.Id);
            return Ok(response);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ActionLog("删除用户")]
        public IActionResult DeleteUser(Guid id)
        {
            var response = ResponseModelFactory.CreateInstance;
            var data= _sysUserRepo.FindEntity(id);
            if(data==null)
            {
                response.SetNotFound();
                return Ok(response);
            }

            if(data.UserType==0)
            {
                response.SetFailed("超级管理不允许删除!");
                return Ok(response);
            }

            data.IsDeleted =(int) CommonEnum.IsDeleted.Yes;

            data.Update();

            _sysUserRepo.Update(data);
            if(!_unitOfWork.Save())
            {
                response.SetFailed("删除失败");
                return Ok(response);
            }
            response.SetData(id);
            return Ok(response);
        }



        #region 用户角色

      
        /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost("SaveRoles",Name = "SaveRoles")]
        [ActionLog("用户分配角色")]
        public IActionResult SaveRoles([FromBody]UserRoleViewModel viewModel)
        {
            var response = ResponseModelFactory.CreateInstance;

          var roles=  viewModel.RoleIds.Select(x => new Sys_UserRole
            {
                 Id=NumberNo.SequentialGuid(),
                  UserId=viewModel.UserId,
                   RoleId=x
            }).ToList();

            _userRoleRepo.ExecuteBySql($"delete from Sys_UserRole where UserId ='{viewModel.UserId}'");
            _userRoleRepo.Insert(roles);

           if(!_unitOfWork.Save())
            {
                response.SetFailed();
                return Ok(response);
            }
            return Ok(response);

        }

        #endregion

    }
}
