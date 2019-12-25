using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Zero.Infrastructure.Resources.ViewModels;

namespace Zero.Infrastructure.Resources.FluentValidation.Rabc
{
   public class UserRoleValidator:AbstractValidator<UserRoleViewModel>
    {
        public UserRoleValidator()
        {
            RuleFor(x => x.UserId).NotNull()
                .WithName("用户ID")
                .WithMessage("{PropertyName}必填");
            RuleFor(x => x.RoleIds).NotNull()
               .WithName("角色ID")
                 .WithMessage("{PropertyName}必填");
        }
    }
}
