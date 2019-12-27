
/**************************************************************************
*   
*   =================================
*   CLR版本    ：4.0.30319.42000
*   命名空间    ：Zero.Core.Entities.Sys
*   文件名称    ：Sys_PermissionWithAssignProperty.cs
*   =================================
*   创 建 者    ：zhangmeng
*   创建日期    ：2019/12/27 14:49:35 
*   邮箱        ：1458976756@qq.com
*   =================================
*  
***************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Zero.Core.Entities.Sys
{
    /// <summary>
    /// 角色拥有的权限属性（不是表）
    /// </summary>
    public class Sys_PermissionWithAssignProperty
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
        /// 权限关联的菜单GUID
        /// </summary>
        public Guid? Menuid { get; set; }
        /// <summary>
        /// 权限操作码
        /// </summary>
        public string ActionCode { get; set; }
        /// <summary>
        /// 角色编码
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 权限是否已分配到当前角色
        /// </summary>
        public int IsAssigned { get; set; }
    }
}
