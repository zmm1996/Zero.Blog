using System;
using System.Collections.Generic;
using System.Text;
using Zero.Core.Intefaces;

namespace Zero.Core.Entities
{
    public class BaseEntity :EntityCreateOrUpdate,IBaseEntity
    {
        public Guid Id { get; set ; }
        public Nullable<System.DateTime> CreatedTime { get; set; }
        public Nullable<System.Guid> CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public Nullable<System.DateTime> ModifiedTime { get; set; }
        public Nullable<System.Guid> ModifiedByUserId { get; set; }
        public string ModifiedByUserName { get; set; }
    }
}
