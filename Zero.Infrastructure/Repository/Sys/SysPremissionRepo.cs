
/**************************************************************************
*   
*   =================================
*   CLR版本    ：4.0.30319.42000
*   命名空间    ：Zero.Infrastructure.Repository.Sys
*   文件名称    ：SysPremissionRepo.cs
*   =================================
*   创 建 者    ：zhangmeng
*   创建日期    ：2019/12/27 10:26:32 
*   邮箱        ：1458976756@qq.com
*   =================================
*  
***************************************************************************/

using Zero.Core.Entities;
using Zero.Core.Intefaces.Sys;
using Zero.Infrastructure.DataBase;

namespace Zero.Infrastructure.Repository.Sys
{
    public class SysPremissionRepo:RepositoryBase<Sys_Permission>,ISysPermissionRepo
    {
        public SysPremissionRepo(MyContext myContext):base(myContext)
        {

        }
    }
}
