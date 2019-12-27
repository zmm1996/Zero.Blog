
/**************************************************************************
*   
*   =================================
*   CLR版本    ：4.0.30319.42000
*   命名空间    ：Zero.Infrastructure.Resources.ViewModels.Rabc
*   文件名称    ：SysPermissionCreateOrUpdateViewModel.cs
*   =================================
*   创 建 者    ：zhangmeng
*   创建日期    ：2019/12/27 10:41:45 
*   邮箱        ：1458976756@qq.com
*   =================================
*  
***************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Zero.Infrastructure.Resources.ViewModels.Rabc
{
    public class SysPermissionCreateOrUpdateViewModel
    {
        public Nullable<System.Guid> MenuId { get; set; }
        public string Name { get; set; }
        public string ActionCode { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> IsDelete { get; set; }
        public Nullable<int> Type { get; set; }
    }
}
