
/**************************************************************************
*   
*   =================================
*   CLR版本    ：4.0.30319.42000
*   命名空间    ：Zero.Infrastructure.Resources.ViewModels.Rabc
*   文件名称    ：MenuTree.cs
*   =================================
*   创 建 者    ：zhangmeng
*   创建日期    ：2019/12/26 17:42:45 
*   邮箱        ：1458976756@qq.com
*   =================================
*  
***************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Zero.Infrastructure.Resources.ViewModels.Rabc
{
    public class MenuTree
    {
        public MenuTree()
        {
            Childen = new List<MenuTree>();
        }
        /// <summary>
        /// GUID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid? ParentId { get; set; }
        /// <summary>
        /// 标题(菜单名称)
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 是否展开子节点
        /// </summary>
        public bool Expand { get; set; }
        /// <summary>
        /// 禁掉响应
        /// </summary>
        public bool Disabled { get; set; }
        /// <summary>
        /// 禁掉 checkbox
        /// </summary>
        public bool DisableCheckbox { get; set; }
        /// <summary>
        /// 是否选中子节点	
        /// </summary>
        public bool Selected { get; set; }
        /// <summary>
        /// 是否勾选(如果勾选，子节点也会全部勾选)
        /// </summary>
        public bool Checked { get; set; }

        public List<MenuTree> Childen { get; set; }
    }
}
