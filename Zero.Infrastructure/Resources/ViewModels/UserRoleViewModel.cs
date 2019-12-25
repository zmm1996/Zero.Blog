using System;
using System.Collections.Generic;
using System.Text;

namespace Zero.Infrastructure.Resources.ViewModels
{
   public class UserRoleViewModel
    {
        public Guid UserId { get; set; }
        public List<Guid> RoleIds { get; set; }
    }
}
