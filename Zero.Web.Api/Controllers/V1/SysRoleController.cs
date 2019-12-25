using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Zero.Core.Entities;
using Zero.Core.Intefaces;
using Zero.Core.Intefaces.Sys;
using Zero.Core.RequestPayloads.Rbac;
using Zero.Infrastructure.Resources.ViewModels;
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

        public SysRoleController(ISysRoleRepo sysRoleRepo,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            this._sysRoleRepo = sysRoleRepo;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        [HttpGet(Name ="GetRoles")]
        [ActionLog("角色集合查询")]
        public IActionResult Get()//RoleRequestPayload payload)
        {
 
            var response = ResponseModelFactory.CreateResultInstance;
            var result = _sysRoleRepo.FindList();
            response.SetData(result, result.Count());
            return Ok(response);
        }

        [HttpGet("{id}",Name ="GetRole")]
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
            if(!AuthContextService.IsAdministrator)
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


        [HttpPut("{id}",Name ="UpdateRole")]
        [ActionLog("修改角色")]
        public IActionResult Update(Guid id,[FromBody]SysRoleCreateOrUpdateViewModel viewModel)
        {
           var response= ResponseModelFactory.CreateInstance;
            if(!ModelState.IsValid)
            {
                response.SetFailed("验证失败");
                return Ok(response);
            }

            var role= _sysRoleRepo.FindEntity(id);
            if(role==null)
            {
                response.SetFailed("角色不存在");
                return Ok(response);
            }
            if(role.IsSuperAdministrator.Value&& !AuthContextService.IsAdministrator)
            {
                response.SetFailed("权限不足");
                return Ok(response);
            }
            _mapper.Map(viewModel,role);
            role.Update();

            _sysRoleRepo.Update(role);


            if(!_unitOfWork.Save())
            {
                response.SetFailed("更新失败");
                return Ok(response);
            }
            response.SetData(id);
            return Ok(response);
        }
        
        [HttpDelete("{id}",Name ="DeleteRole")]
        [ActionLog("删除角色")]
        public IActionResult Delete(Guid id)
        {
           var response= ResponseModelFactory.CreateInstance;
           var role= _sysRoleRepo.FindEntity(id);
            if(role==null)
            {
                response.SetNotFound();
                return Ok(response);
            }
            role.IsDeleted = (int)CommonEnum.IsDeleted.Yes;
            role.Update();

            _sysRoleRepo.Update(role);

            
            if(!_unitOfWork.Save())
            {
                response.SetFailed("删除失败");
                return Ok(response);
            }
            response.SetData(id);
            return Ok(response);


        }
    }
}
