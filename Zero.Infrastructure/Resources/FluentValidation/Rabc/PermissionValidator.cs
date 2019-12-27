
/**************************************************************************
*   
*   =================================
*   CLR版本    ：4.0.30319.42000
*   命名空间    ：Zero.Infrastructure.Resources.FluentValidation.Rabc
*   文件名称    ：PermissionValidator.cs
*   =================================
*   创 建 者    ：zhangmeng
*   创建日期    ：2019/12/27 10:43:19 
*   邮箱        ：1458976756@qq.com
*   =================================
*  
***************************************************************************/

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Zero.Infrastructure.Resources.ViewModels.Rabc;

namespace Zero.Infrastructure.Resources.FluentValidation.Rabc
{
    public class PermissionValidator:AbstractValidator<SysPermissionCreateOrUpdateViewModel>
    {
        public PermissionValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithName("权限名称")
                .WithMessage("{PropertyName}必填");
            RuleFor(x => x.MenuId)
               .NotEmpty()
               .WithName("所属菜单")
               .WithMessage("{PropertyName}必填");
            RuleFor(x => x.ActionCode)
               .NotEmpty()
               .WithName("操作码")
               .WithMessage("{PropertyName}必填");

        }
    }
}
