
/**************************************************************************
*   
*   =================================
*   CLR版本    ：4.0.30319.42000
*   命名空间    ：Zero.Infrastructure.Resources.ViewModels.Rabc
*   文件名称    ：AssignPermissionViewModel.cs
*   =================================
*   创 建 者    ：zhangmeng
*   创建日期    ：2019/12/27 16:30:03 
*   邮箱        ：1458976756@qq.com
*   =================================
*  
***************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Zero.Infrastructure.Resources.ViewModels.Rabc
{
    public class AssignPermissionViewModel
    {
        public AssignPermissionViewModel()
        {
            Permissions = new List<Guid>();
        }
        public Guid RoleId { get; set; }

        public List<Guid> Permissions { get; set; }

    }
}
