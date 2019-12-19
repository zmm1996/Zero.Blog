using System;
using System.Collections.Generic;

namespace Zero.Core.Entities
{
    public partial class Sys_UserRole
    {
        public System.Guid Id { get; set; }
        public Nullable<System.Guid> UserId { get; set; }
        public Nullable<System.Guid> RoleId { get; set; }
    }
}
