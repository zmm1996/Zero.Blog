using System;
using System.Collections.Generic;
using System.Text;

namespace Zero.Core.Entities
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
        Nullable<System.DateTime> CreatedTime { get; set; }
        Nullable<System.Guid> CreatedByUserId { get; set; }
        string CreatedByUserName { get; set; }
        Nullable<System.DateTime> ModifiedTime { get; set; }
        Nullable<System.Guid> ModifiedByUserId { get; set; }
        string ModifiedByUserName { get; set; }
    }
}
