using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Zero.Infrastructure.Resources.ViewModels;

namespace Zero.Infrastructure.Resources.FluentValidation.Rabc
{
   public class UserValidator:AbstractValidator<SysUserCreateOrUpdateViewModel>
    {
        public UserValidator()
        {
            RuleFor(x => x.LoginName).NotNull()
                .WithName("登录名")
                .NotEmpty()
                .WithMessage("{PropertyName}必填")
                .MinimumLength(5)
                .WithMessage("{PropertyName}不能小于{MinLength}");
            RuleFor(x => x.Password).NotNull()
               .WithName("密码")
               .NotEmpty()
               .WithMessage("{PropertyName}必填")
               .MinimumLength(6)
               .WithMessage("{PropertyName}不能小于{MinLength}");
            RuleFor(x => x.DisplayName).NotNull()
               .WithName("昵称")
               .NotEmpty()
               .WithMessage("{PropertyName}必填")
               .MinimumLength(5)
               .WithMessage("{PropertyName}不能小于{MinLength}");
        }
    }
}
