using System;
using System.Collections.Generic;

namespace Zero.Core.Entities
{
    public partial class Sys_Role
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public Nullable<bool> IsSuperAdministrator { get; set; }
        public Nullable<bool> IsBuiltin { get; set; }
        public Nullable<System.DateTime> CreatedTime { get; set; }
        public Nullable<System.Guid> CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public Nullable<System.DateTime> ModifiedTime { get; set; }
        public Nullable<System.Guid> ModifiedByUserId { get; set; }
        public string ModifiedByUserName { get; set; }
    }
}
