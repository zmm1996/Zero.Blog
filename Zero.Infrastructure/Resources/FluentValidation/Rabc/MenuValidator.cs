
/**************************************************************************
*   
*   =================================
*   CLR版本    ：4.0.30319.42000
*   命名空间    ：Zero.Infrastructure.Resources.FluentValidation.Rabc
*   文件名称    ：MenuValidator.cs
*   =================================
*   创 建 者    ：zhangmeng
*   创建日期    ：2019/12/26 13:04:31 
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
    public class MenuValidator:AbstractValidator<SysMenuCreateOrUpdateViewModel>
    {
        public MenuValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .WithName("菜单名称")
                .WithMessage("{PropertyName}必填")
                .MinimumLength(2)
                .WithMessage("{PropertyName}最小长度为:{MinLength}");


        }
    }
}
