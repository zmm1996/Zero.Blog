using System;
using System.Collections.Generic;

namespace Zero.Core.Entities
{
    public partial class Sys_Menu:BaseEntity,IBaseEntity
    {
        public Sys_Menu()
        {
            Sys_Permissions = new List<Sys_Permission>();
        }

      
        /// <summary>
        /// 菜单名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// url地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 页面别名
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 父级id
        /// </summary>
        public Nullable<System.Guid> ParentId { get; set; }
        /// <summary>
        /// 上级菜单名
        /// </summary>
        public string ParentName { get; set; }
        /// <summary>
        /// 层级深度
        /// </summary>
        public Nullable<int> Level { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public Nullable<int> Sort { get; set; }
        /// <summary>
        /// 是否可用（0：禁用1：可用）
        /// </summary>
        public Nullable<int> Status { get; set; }
        /// <summary>
        /// 是否已删除
        /// </summary>
        public Nullable<int> IsDeleted { get; set; }
        /// <summary>
        /// 是否默认路由
        /// </summary>
        public Nullable<int> IsDefaultRouter { get; set; }
        /// <summary>
        /// 前端组件（.vue）
        /// </summary>
        public string Component { get; set; }
        /// <summary>
        /// 在菜单中隐藏
        /// </summary>
        public Nullable<int> HideInMenu { get; set; }
        /// <summary>
        /// 不缓存页面
        /// </summary>
        public Nullable<int> NotCache { get; set; }
        /// <summary>
        /// 页面关闭前的回调函数
        /// </summary>
        public string BeforeCloseFun { get; set; }

        public virtual List<Sys_Permission> Sys_Permissions { get; set; }
    }
}
