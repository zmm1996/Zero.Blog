using System;
using System.Collections.Generic;

namespace Zero.Core.Entities
{
    public partial class Sys_User
    {
        public System.Guid Id { get; set; }
        public string LoginName { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public Nullable<int> UserType { get; set; }
        public Nullable<int> IsLocked { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> CreatedTime { get; set; }
        public Nullable<System.Guid> CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public Nullable<System.DateTime> ModifiedTime { get; set; }
        public Nullable<System.Guid> ModifiedByUserId { get; set; }
        public string ModifiedByUserName { get; set; }
    }
}
