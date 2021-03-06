using System;
using System.Collections.Generic;

namespace Zero.Core.Entities
{
    public partial class Sys_Role : BaseEntity, IBaseEntity
    {
      
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public Nullable<bool> IsSuperAdministrator { get; set; }
        /// <summary>
        /// 是否是系统内置角色(系统内置角色不允许删除,修改操作)
        /// </summary>
        public Nullable<bool> IsBuiltin { get; set; }
     
    }
}
