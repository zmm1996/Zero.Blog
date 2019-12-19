using System;
using System.Collections.Generic;

namespace Zero.Core.Entities
{
    public partial class Sys_UserActionLog
    {
        public System.Guid Id { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string ApiUrl { get; set; }
        public string IP { get; set; }
        public Nullable<System.DateTime> ActionTime { get; set; }
        public Nullable<System.Guid> ActionUserId { get; set; }
        public Nullable<System.Guid> ActionUserName { get; set; }
        public string Description { get; set; }
        public Nullable<int> ActionType { get; set; }
        public Nullable<System.Guid> ModelId { get; set; }
        public Nullable<int> IsDeleted { get; set; }
    }
}
