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
using Zero.Infrastructure.Resources.ViewModels.Rabc;
using Zero.Util.Helpers;
using Zero.Web.Api.Extensions;
using Zero.Web.Api.Filters;

namespace Zero.Web.Api.Controllers.V1
{
    [Route("api/v1/rabc/sysmenu")]
    [ApiController]
    [Authorize]
    public class SysMenuController : ControllerBase
    {
        private readonly ISysMenuRepo _sysMenuRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SysMenuController(
            ISysMenuRepo sysMenuRepo,
            IMapper mapper,
            IUnitOfWork unitOfWork
            )
        {
            this._sysMenuRepo = sysMenuRepo;
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = ResponseModelFactory.CreateResultInstance;
            var data = _sysMenuRepo.FindList();
            response.SetData(data, data.Count());
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetMenuById(Guid id)
        {
            var response = ResponseModelFactory.CreateInstance;
            var data = _sysMenuRepo.FindEntity(id);
            if (data == null)
            {
                response.SetNotFound();
                return Ok();
            }
            response.SetData(data);
            return Ok(response);
        }

        [HttpPost]
        [ActionLog("创建菜单")]
        public IActionResult CreateMenu([FromBody]SysMenuCreateOrUpdateViewModel viewModel)
        {
            var response = ResponseModelFactory.CreateInstance;
            var data = _sysMenuRepo.FindEntity(x => x.Name == viewModel.Name);
            if (data != null)
            {
                response.SetFailed("菜单名已存在");
                return Ok(response);
            }
            var createMenu = _mapper.Map<Sys_Menu>(viewModel);
            createMenu.IsDeleted = (int)CommonEnum.IsDeleted.No;
            createMenu.Icon = string.IsNullOrEmpty(viewModel.Icon) ? "md-menu" : viewModel.Icon;
            createMenu.Create();

            _sysMenuRepo.Insert(createMenu);

            if (!_unitOfWork.Save())
            {
                response.SetFailed("创建失败");
            }
            response.SetData(createMenu.Id);
            return Ok(response);


        }

        [HttpPut("{id}")]
        [ActionLog("编辑菜单")]
        public IActionResult UpdateMenu(Guid id, [FromBody]SysMenuCreateOrUpdateViewModel viewModel)
        {
            var response = ResponseModelFactory.CreateInstance;
            var data = _sysMenuRepo.FindEntity(x => x.Name == viewModel.Name && x.Id != id);
            if (data != null)
            {
                response.SetFailed("菜单名已存在");
                return Ok(response);
            }
            var entity = _sysMenuRepo.FindEntity(x => x.Id == id);
            if (entity == null)
            {
                response.SetNotFound();
                return Ok(response);
            }
            viewModel.Icon = string.IsNullOrEmpty(viewModel.Icon) ? "md-menu" : viewModel.Icon;
            _mapper.Map(viewModel, entity);
            entity.Update();

            _sysMenuRepo.Update(entity);

            if (!_unitOfWork.Save())
            {
                response.SetFailed("编辑");
            }
            response.SetData(entity.Id);
            return Ok(response);


        }

        [HttpDelete("id")]
        public IActionResult DeleteMenu(Guid id)
        {
            var response = ResponseModelFactory.CreateInstance;
            var data = _sysMenuRepo.FindEntity(id);
            if (data == null)
            {
                response.SetNotFound();
                return Ok(response);
            }
            data.IsDeleted = (int)CommonEnum.IsDeleted.Yes;

            data.Update();

            _sysMenuRepo.Update(data);

            if(!_unitOfWork.Save())
            {
                response.SetFailed();
                return Ok(response);
            }
          
            return Ok(response);


        }

        
        [HttpGet("MenuTree")]
        public IActionResult MenuTree(Guid? id)
        {
           var response= ResponseModelFactory.CreateInstance;
            var temp=  _sysMenuRepo.FindList().Select(x=>new MenuTree {
                 Id=x.Id,
                  ParentId=x.ParentId,
                  Title=x.Name
            }).ToList();

            var root = new MenuTree
            {
                Title="顶级菜单",
                 Id=Guid.Empty,
                  ParentId=null
            };
            temp.Insert(0, root);
           var tree= temp.BuildTree(id);

            response.SetData(tree);
            return Ok(response);

        }

       

      


    }
}
