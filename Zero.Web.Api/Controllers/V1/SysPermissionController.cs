using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zero.Core.Entities;
using Zero.Core.Entities.Sys;
using Zero.Core.Intefaces;
using Zero.Core.Intefaces.Sys;
using Zero.Infrastructure.DataBase;
using Zero.Infrastructure.Resources.ViewModels.Rabc;
using Zero.Util.Helpers;
using Zero.Web.Api.Extensions;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;

namespace Zero.Web.Api.Controllers.V1
{
    [Route("api/v1/rabc/syspermission")]
    [ApiController]
    public class SysPermissionController : ControllerBase
    {
        private readonly ISysPermissionRepo _sysPermissionRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly MyContext _myContext;

        public SysPermissionController(
            ISysPermissionRepo sysPermissionRepo
            , IMapper mapper,
            IUnitOfWork unitOfWork,
            MyContext myContext
            )
        {
            this._sysPermissionRepo = sysPermissionRepo;
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
            this._myContext = myContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var response = ResponseModelFactory.CreateResultInstance;


            var data = _sysPermissionRepo.FindList();

            response.SetData(data, data.Count());
            return Ok(data);
        }

        [HttpGet("{id}")]
        public IActionResult GetPermissionById(Guid id)
        {
            var response = ResponseModelFactory.CreateInstance;
            var entity = _sysPermissionRepo.FindEntity(id);
            if (entity == null)
            {

                response.SetNotFound();
                return Ok(response);
            }

            return Ok(response);

        }
        [HttpPost]
        public IActionResult CreatePermission([FromBody] SysPermissionCreateOrUpdateViewModel viewModel)
        {
            var response = ResponseModelFactory.CreateInstance;
            var data = _sysPermissionRepo.FindEntity(x => x.MenuId == viewModel.MenuId && x.ActionCode == viewModel.ActionCode);

            if (data != null)
            {
                response.SetFailed("该菜单操作码已存在");
                return Ok(response);
            }

            var entity = _mapper.Map<Sys_Permission>(viewModel);
            entity.IsDelete = (int)CommonEnum.IsDeleted.No;
            entity.Create();
            _sysPermissionRepo.Insert(entity);

            if (!_unitOfWork.Save())
            {
                response.SetFailed("创建失败");
                return Ok(response);
            }

            response.SetData(entity.Id);
            return Ok(response);

        }

        [HttpPut("{id}")]
        public IActionResult UpdatePermission(Guid id, [FromBody] SysPermissionCreateOrUpdateViewModel viewModel)
        {
            var response = ResponseModelFactory.CreateInstance;
            var data = _sysPermissionRepo.FindEntity(x => x.MenuId == viewModel.MenuId && x.ActionCode == viewModel.ActionCode && x.Id != id);

            if (data != null)
            {
                response.SetFailed("该菜单操作码已存在");
                return Ok(response);
            }

            var entity = _sysPermissionRepo.FindEntity(id);
            if (data != null)
            {
                response.SetNotFound();
                return Ok(response);
            }
            _mapper.Map(viewModel, entity);
            entity.Update();
            _sysPermissionRepo.Update(entity);

            if (!_unitOfWork.Save())
            {
                response.SetFailed("编辑失败");
                return Ok(response);
            }

            response.SetData(entity.Id);
            return Ok(response);

        }

        [HttpDelete("{id}")]
        public IActionResult DeletePermission(Guid id)
        {
            var response = ResponseModelFactory.CreateInstance;
            var entity = _sysPermissionRepo.FindEntity(id);
            if (entity == null)
            {

                response.SetNotFound();
                return Ok(response);
            }
            entity.IsDelete = (int)CommonEnum.IsDeleted.Yes;
            entity.Update();

            _sysPermissionRepo.Update(entity);

            if (!_unitOfWork.Save())
            {
                response.SetFailed("删除失败");
                return Ok(response);
            }
            response.SetData(id);
            return Ok(response);

        }

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="id">角色Id</param>
        /// <returns></returns>
        [HttpGet("PermissionTree")]
        public IActionResult PermissionTree(Guid id)
        {
            var response = ResponseModelFactory.CreateInstance;
            var role = _myContext.Sys_Role.Find(id);
            if (role == null)
            {
                response.SetNotFound("角色不存在");
                return Ok(response);
            }
            var menu = _myContext.Sys_Menu
                            .Where(x => x.IsDeleted == (int)CommonEnum.IsDeleted.No && x.Status == (int)CommonEnum.Status.Normal)
                            .OrderBy(x => x.CreatedTime)
                            .ThenBy(x => x.Sort)
                            .Select(x => new PermissionMenuTree
                            {
                                id = x.Id,
                                Parentid = x.ParentId,
                                Title = x.Name
                            }).ToList();

            var sql = @"select  P.Id,P.MenuId,P.Name,P.ActionCode,R.RoleId,(case when R.PermissionId is not null then 1 else 0 end) as IsAssigned 
                          from Sys_Permission as P
                          left join(SELECT* from Sys_RolePermission where Id={0}) R
                         on R.PermissionId = P.Id
                          where P.IsDelete = 0 and P.Status = 1";
            var permissionList = _myContext.Sys_PermissionWithAssignProperty.FromSql(sql, id).ToList();
            var tree = menu.FillRecursive(permissionList, null, role.IsSuperAdministrator.Value);
            response.SetData(new { tree, selectedPermissions =permissionList.Where(x=>x.IsAssigned==1).Select(x=>x.Id)});
            return Ok(response);
                
        }
    }
    public static class PermissionTreeHelper
    {

        /// <summary>
        /// 获取菜单权限
        /// </summary>
        /// <param name="menus">菜单集合</param>
        /// <param name="permissions">权限集合</param>
        /// <param name="parentGuid">父级id</param>
        /// <param name="IsSuperAdministrator">是否超级管理员</param>
        /// <returns></returns>
        public static List<PermissionMenuTree> FillRecursive(this List<PermissionMenuTree> menus, List<Sys_PermissionWithAssignProperty> permissions, Guid? parentGuid, bool IsSuperAdministrator = false)
        {
            List<PermissionMenuTree> trees = new List<PermissionMenuTree>();
            foreach (var item in menus.Where(x => x.Parentid == parentGuid))
            {
                var childen = new PermissionMenuTree
                {
                    id = item.id,
                    Parentid = item.Parentid,
                    Title = item.Title,
                    Expand = true,
                    AllAssigned = IsSuperAdministrator ? true : (permissions.Where(x => x.Menuid == item.id).Count(x => x.IsAssigned == 0) == 0),
                    Permissions = permissions.Where(x => x.Menuid == item.id).Select(x => new PermissionElement
                    {
                        Id = x.Id,
                        IsAssignedToRole = IsAssigned(x.IsAssigned, IsSuperAdministrator),
                        Name = x.Name
                    }).ToList(),
                    Children = FillRecursive(menus, permissions, item.id)
                };
                trees.Add(childen);
            }
            return trees;
        }


        private static bool IsAssigned(int IsAssigned, bool isSuper)
        {
            if (isSuper)
            {
                return true;
            }
            return IsAssigned == 1;
        }
    }

}