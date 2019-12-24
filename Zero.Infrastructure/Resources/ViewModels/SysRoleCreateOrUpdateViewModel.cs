using System;
using System.Collections.Generic;
using System.Text;

namespace Zero.Infrastructure.Resources.ViewModels
{
   public class SysRoleCreateOrUpdateViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<bool> IsSuperAdministrator { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public Nullable<bool> IsBuiltin { get; set; }
    }
}
