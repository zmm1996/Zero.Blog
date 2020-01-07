using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using Zero.Core.Entities;
using Zero.Core.Intefaces;
using Zero.Core.Intefaces.Sys;
using Zero.Core.RequestPayloads.Rbac;
using Zero.Infrastructure.Resources.ViewModels;
using Zero.Infrastructure.Resources.ViewModels.Rabc;
using Zero.Util.Helpers;
using Zero.Web.Api.Extensions;
using Zero.Web.Api.Filters;
using Zero.Web.Util.Extensions.AuthContext;

namespace Zero.Web.Api.Controllers.V1
{
    [Route("api/v1/rabc/sysrole")]
    [ApiController]
    [Authorize]
    public class SysRoleController : ControllerBase
    {
        private readonly ISysRoleRepo _sysRoleRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISysRolePermissionRepo _sysRolePermissionRepo;
        private readonly ISysPermissionRepo _sysPermissionRepo;

        public SysRoleController(ISysRoleRepo sysRoleRepo,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ISysRolePermissionRepo sysRolePermissionRepo,
            ISysPermissionRepo sysPermissionRepo
            )
        {
            this._sysRoleRepo = sysRoleRepo;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._sysRolePermissionRepo = sysRolePermissionRepo;
            this._sysPermissionRepo = sysPermissionRepo;
        }

        [HttpGet(Name = "GetRoles")]
        [ActionLog("角色集合查询")]
        public IActionResult Get()//RoleRequestPayload payload)
        {

            var response = ResponseModelFactory.CreateResultInstance;
            var result = _sysRoleRepo.FindList();
            response.SetData(result, result.Count());
            return Ok(response);
        }

        [HttpGet("{id}", Name = "GetRole")]
        [ActionLog("角色查询")]
        public IActionResult Get(Guid id)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            var result = _sysRoleRepo.FindEntity(id);

            response.SetData(result);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ActionLog("创建角色")]
        public IActionResult Post([FromBody]SysRoleCreateOrUpdateViewModel viewModel)
        {

            var response = ResponseModelFactory.CreateInstance;
            if (!ModelState.IsValid)
            {
                response.SetFailed("验证失败");
                return Ok(response);
            }
            if (!AuthContextService.IsAdministrator)
            {
                response.SetFailed("没有权限");
                return Ok(response);
            }
            if (_sysRoleRepo.FindList(x => x.Name == viewModel.Name).Any())
            {
                response.SetFailed("角色名已存在");
                return Ok(response);
            }
            var role = _mapper.Map<Sys_Role>(viewModel);

            role.IsSuperAdministrator = false;
            role.IsBuiltin = false;
            role.Create();

            _sysRoleRepo.Insert(role);

            if (!_unitOfWork.Save())
            {
                response.SetFailed();
            }
            response.SetData(role.Id);
            return Ok(response);

        }


        [HttpPut("{id}", Name = "UpdateRole")]
        [ActionLog("修改角色")]
        public IActionResult Update(Guid id, [FromBody]SysRoleCreateOrUpdateViewModel viewModel)
        {
            var response = ResponseModelFactory.CreateInstance;
            if (!ModelState.IsValid)
            {
                response.SetFailed("验证失败");
                return Ok(response);
            }

            var role = _sysRoleRepo.FindEntity(id);
            if (role == null)
            {
                response.SetFailed("角色不存在");
                return Ok(response);
            }
            if (role.IsSuperAdministrator.Value && !AuthContextService.IsAdministrator)
            {
                response.SetFailed("权限不足");
                return Ok(response);
            }
            _mapper.Map(viewModel, role);
            role.Update();

            _sysRoleRepo.Update(role);


            if (!_unitOfWork.Save())
            {
                response.SetFailed("更新失败");
                return Ok(response);
            }
            response.SetData(id);
            return Ok(response);
        }

        [HttpDelete("{id}", Name = "DeleteRole")]
        [ActionLog("删除角色")]
        public IActionResult Delete(Guid id)
        {
            var response = ResponseModelFactory.CreateInstance;
            var role = _sysRoleRepo.FindEntity(id);
            if (role == null)
            {
                response.SetNotFound();
                return Ok(response);
            }
            role.IsDeleted = (int)CommonEnum.IsDeleted.Yes;
            role.Update();

            _sysRoleRepo.Update(role);


            if (!_unitOfWork.Save())
            {
                response.SetFailed("删除失败");
                return Ok(response);
            }
            response.SetData(id);
            return Ok(response);


        }


        /// <summary>
        /// 角色权限分配
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost("assign_permission")]
        public IActionResult AssignPermission(AssignPermissionViewModel viewModel)
        {

            var response = ResponseModelFactory.CreateInstance;
            var entity = _sysRoleRepo.FindEntity(viewModel.RoleId);

            if (entity == null)
            {
                response.SetNotFound("角色不存在");
                return Ok(response);
            }
            //如果是超级管理员则不写如角色权限映射表，（读取时直接跳过，默认所有权限）
            if (entity.IsSuperAdministrator.Value)
            {
                response.SetSuccess();
                return Ok(response);
            }

            string sql = "delete [Zero].[dbo].[Sys_RolePermission] where RoleId=@id";
            DbParameter parameter = new SqlParameter("@id", viewModel.RoleId);
            //先删除原有的
            _sysRolePermissionRepo.ExecuteBySql(sql, parameter);

            if (viewModel.Permissions != null && viewModel.Permissions.Count > 0)
            {
                //所有权限
                var entityList = _sysPermissionRepo.FindList();

                //筛选传递过来权限id，数据库中存在往里面添加
                List<Sys_RolePermission> addRolePermission = new List<Sys_RolePermission>();

                viewModel.Permissions.ForEach(x =>
                {
                  if (entityList.Where(e => e.Id == x).Any())
                    {
                        addRolePermission.Add(new Sys_RolePermission
                        {
                            Id = NumberNo.SequentialGuid(),
                            PermissionId = x,
                            RoleId = viewModel.RoleId
                        });
                    }

                });

               //var repmissionList = viewModel.Permissions.Select(x => new Sys_RolePermission
               // {
               //     Id = NumberNo.SequentialGuid(),
               //     PermissionId = x,
               //     RoleId = viewModel.RoleId
               // }).ToList();
                _sysRolePermissionRepo.Insert(addRolePermission);
              if (_unitOfWork.Save())
                {
                    response.SetSuccess();
                }
                else
                {
                    response.SetFailed();
                }

            }

            return Ok(response);


        }

        /// <summary>
        /// 根据用户获取角色列表
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpGet("GetRolesByUserId/{userid}")]
        public IActionResult GetRolesByUserId(Guid userid)
        {
            string sql = @"SELECT   r.*
  FROM[Zero].[dbo].[Sys_Role] AS R
  inner join[Zero].[dbo].[Sys_UserRole]
        AS SR
  ON R.Id=SR.RoleId
  WHERE SR.UserId= {0}";
            var query = _sysRoleRepo.FromSql(sql, userid);
            var assignedRoles = query.Select(x => x.Name);
            return Ok(assignedRoles);
        }
    }
}
