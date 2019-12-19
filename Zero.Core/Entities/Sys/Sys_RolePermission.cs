using System;
using System.Collections.Generic;

namespace Zero.Core.Entities
{
    public partial class Sys_RolePermission
    {
        public System.Guid Id { get; set; }
        public Nullable<System.Guid> RoleId { get; set; }
        public Nullable<System.Guid> PermissionId { get; set; }
    }
}
