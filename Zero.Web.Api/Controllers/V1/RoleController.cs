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
using Zero.Web.Util.Extensions.AuthContext;

namespace Zero.Web.Api.Controllers.V1
{
    [Route("api/v1/rabc/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly ISysRoleRepo _sysRoleRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleController(ISysRoleRepo sysRoleRepo,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            this._sysRoleRepo = sysRoleRepo;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        [HttpGet(Name ="GetRoles")]
        public IActionResult Get()//RoleRequestPayload payload)
        {
           var u= AuthContextService.CurrentUser;

            var a = AuthContextService.IsAdministrator;
            var response = ResponseModelFactory.CreateResultInstance;
            var result = _sysRoleRepo.FindList();
            response.SetData(result, result.Count());
            return Ok(response);
        }

        [HttpGet("{id}",Name ="GetRole")]
        public IActionResult Get(Guid id)
        {
            var response = ResponseModelFactory.CreateResultInstance;
            var result = _sysRoleRepo.FindEntity(id);

            response.SetData(result);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Post([FromBody]SysRoleCreateOrUpdateViewModel viewModel)
        {
           
            var response = ResponseModelFactory.CreateInstance;
            if (!ModelState.IsValid)
            {
                return BadRequest();
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
        public IActionResult Update(Guid id,[FromBody]SysRoleCreateOrUpdateViewModel viewModel)
        {
           var response= ResponseModelFactory.CreateInstance;
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var role= _sysRoleRepo.FindEntity(id);
            if(role==null)
            {
                response.SetFailed("角色不存在");
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
