
/**************************************************************************
*   
*   =================================
*   CLR版本    ：4.0.30319.42000
*   命名空间    ：Zero.Infrastructure.Resources.ViewModels.Rabc
*   文件名称    ：InitMenuTree.cs
*   =================================
*   创 建 者    ：zhangmeng
*   创建日期    ：2020/1/9 10:19:33 
*   邮箱        ：1458976756@qq.com
*   =================================
*  
***************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Zero.Infrastructure.Resources.ViewModels.Rabc
{
    public class InitMenuTree
    {

        public InitMenuTree()
        {
            Children = new List<InitMenuTree>();
        }
        public Guid Id { get; set; }
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
        /// 排序
        /// </summary>
        public Nullable<int> Sort { get; set; }
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

        public  List<InitMenuTree> Children { get; set; }
    }
}
