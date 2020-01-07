
/**************************************************************************
*   
*   =================================
*   CLR版本    ：4.0.30319.42000
*   命名空间    ：Zero.Infrastructure.Resources.ViewModels.Rabc
*   文件名称    ：PermissionMenuTree.cs
*   =================================
*   创 建 者    ：zhangmeng
*   创建日期    ：2019/12/27 14:16:47 
*   邮箱        ：1458976756@qq.com
*   =================================
*  
***************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Zero.Infrastructure.Resources.ViewModels.Rabc
{

    /// <summary>
    /// 角色权限的菜单树
    /// </summary>
    public class PermissionMenuTree
    {
        /// <summary>
        /// 
        /// </summary>
        public PermissionMenuTree()
        {
            Permissions = new List<PermissionElement>();
            Children = new List<PermissionMenuTree>();
        }
        /// <summary>
        /// GUID
        /// </summary>
        public Guid id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid? Parentid { get; set; }
        /// <summary>
        /// 标题(菜单名称)
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 是否展开子节点
        /// </summary>
        public bool Expand { get; set; }
        /// <summary>
        /// 存在子节点禁用父节点
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
        /// <summary>
        /// 当前菜单的所有权限都已分配到指定角色
        /// </summary>
        public bool AllAssigned { get; set; }
        
        /// <summary>
        /// 当前菜单拥有的权限功能
        /// </summary>
        public List<PermissionElement> Permissions { get; set; }
        /// <summary>
        /// 子节点属性数组
        /// </summary>
        public List<PermissionMenuTree> Children { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class PermissionElement
    {
        /// <summary>
        /// 权限编码
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 是否已分配到指定角色
        /// </summary>
        public bool IsAssignedToRole { get; set; }

       
    }



}
