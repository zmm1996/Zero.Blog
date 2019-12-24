using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Zero.Infrastructure.Resources.ViewModels;

namespace Zero.Infrastructure.Resources.FluentValidation.Rabc
{
   public class RoleValidator: AbstractValidator<SysRoleCreateOrUpdateViewModel>
    {
        public RoleValidator()
        {
            RuleFor(x => x.Name).NotNull()
                .WithName("名称")
                .NotEmpty()
                .WithMessage("{PropertyName}必填")
                .MinimumLength(2)
                .WithMessage("{PropertyName}不小于{MinLength}");

          
        }
    }
}
