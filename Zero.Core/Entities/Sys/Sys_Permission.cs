using System;
using System.Collections.Generic;

namespace Zero.Core.Entities
{
    public partial class Sys_Permission : BaseEntity, IBaseEntity
    {
      
        public Nullable<System.Guid> MenuId { get; set; }
        public string Name { get; set; }
        public string ActionCode { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> IsDelete { get; set; }
        public Nullable<int> Type { get; set; }
   
         public virtual Sys_Menu Sys_Menu { get; set; }
    }
}
