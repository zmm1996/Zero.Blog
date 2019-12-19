using System;
using System.Collections.Generic;

namespace Zero.Core.Entities
{
    public partial class Sys_Permission
    {
        public System.Guid Id { get; set; }
        public Nullable<System.Guid> MenuId { get; set; }
        public string Name { get; set; }
        public string ActionCode { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> IsDelete { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<System.DateTime> CreatedTime { get; set; }
        public Nullable<System.Guid> CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public Nullable<System.DateTime> ModifiedTime { get; set; }
        public Nullable<System.Guid> ModifiedByUserId { get; set; }
        public string ModifiedByUserName { get; set; }
    }
}
