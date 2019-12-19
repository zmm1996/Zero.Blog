using System;
using System.Collections.Generic;

namespace Zero.Core.Entities
{
    public partial class Sys_Menu
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Alias { get; set; }
        public string Icon { get; set; }
        public Nullable<System.Guid> ParentId { get; set; }
        public string ParentName { get; set; }
        public Nullable<int> Level { get; set; }
        public string Description { get; set; }
        public Nullable<int> Sort { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public Nullable<int> IsDefaultRouter { get; set; }
        public string Component { get; set; }
        public Nullable<int> HideInMenu { get; set; }
        public Nullable<int> NotCache { get; set; }
        public string BeforeCloseFun { get; set; }
        public Nullable<System.DateTime> CreatedTime { get; set; }
        public Nullable<System.Guid> CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public Nullable<System.DateTime> ModifiedTime { get; set; }
        public Nullable<System.Guid> ModifiedByUserId { get; set; }
        public string ModifiedByUserName { get; set; }
    }
}
