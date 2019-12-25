using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zero.Core.Intefaces;
using Zero.Core.Intefaces.Sys;
using Zero.Infrastructure.DataBase;
using Zero.Infrastructure.Repository.Sys;
using Zero.Infrastructure.Resources.FluentValidation.Rabc;
using Zero.Infrastructure.Resources.ViewModels;

namespace Zero.Web.Api.Extensions.AddConfigureServices
{
    public static class AddConfigureServices
    {
        public static void AddConfigureServicesExtension(this IServiceCollection services)
        {
            //fluentvalidator验证

            services.AddTransient<IValidator<SysRoleCreateOrUpdateViewModel>, RoleValidator>();
            services.AddTransient<IValidator<SysUserCreateOrUpdateViewModel>, UserValidator>();
            services.AddTransient<IValidator<UserRoleViewModel>, UserRoleValidator>();

            //仓储
            services.AddScoped<ISysUserRepo, SysUserRepo>();
            services.AddScoped<ISysRoleRepo, SysRoleRepo>();
            services.AddScoped<IUserRoleRepo, UserRoleRepo>();
            services.AddScoped<ISysMenuRepo, SysMenuRepo>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

    }
}
