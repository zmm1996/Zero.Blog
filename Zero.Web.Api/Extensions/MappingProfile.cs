using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Zero.Core.Entities;
using Zero.Infrastructure.Resources.ViewModels;
using Zero.Infrastructure.Resources.ViewModels.Rabc;

namespace Zero.Web.Api.Extensions
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Sys_User, SysUserViewModel>();
               // .ForMember(x => x.UpdateTime, opt => opt.MapFrom(src => src.LastModified));//UpdateTime来自LastTime
            CreateMap<SysUserViewModel, Sys_User>();

            //角色
            CreateMap<SysRoleCreateOrUpdateViewModel, Sys_Role>();
            CreateMap<Sys_Role, SysRoleCreateOrUpdateViewModel>();


            //用户
            CreateMap<SysUserCreateOrUpdateViewModel, Sys_User>();

            //菜单

            CreateMap<SysMenuCreateOrUpdateViewModel, Sys_Menu>();

            //权限

            CreateMap<SysPermissionCreateOrUpdateViewModel, Sys_Permission>();
        }
    }
}
