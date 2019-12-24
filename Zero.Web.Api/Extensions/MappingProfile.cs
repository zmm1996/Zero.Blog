using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Zero.Core.Entities;
using Zero.Infrastructure.Resources.ViewModels;

namespace Zero.Web.Api.Extensions
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Sys_User, SysUserViewModel>();
               // .ForMember(x => x.UpdateTime, opt => opt.MapFrom(src => src.LastModified));//UpdateTime来自LastTime
            CreateMap<SysUserViewModel, Sys_User>();

            CreateMap<SysRoleCreateOrUpdateViewModel, Sys_Role>();
            CreateMap<Sys_Role, SysRoleCreateOrUpdateViewModel>();
        }
    }
}
