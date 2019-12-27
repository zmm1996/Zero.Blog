
/**************************************************************************
*   
*   =================================
*   CLR版本    ：4.0.30319.42000
*   命名空间    ：Zero.Infrastructure.Repository.Sys
*   文件名称    ：SysRolePermissionRepo.cs
*   =================================
*   创 建 者    ：zhangmeng
*   创建日期    ：2019/12/27 16:50:21 
*   邮箱        ：1458976756@qq.com
*   =================================
*  
***************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using Zero.Core.Entities;
using Zero.Core.Intefaces.Sys;
using Zero.Infrastructure.DataBase;

namespace Zero.Infrastructure.Repository.Sys
{
    public class SysRolePermissionRepo:RepositoryBase<Sys_RolePermission>,ISysRolePermissionRepo
    {
        public SysRolePermissionRepo(MyContext myContext):base(myContext)
        {

        }
    }
}
