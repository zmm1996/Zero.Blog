using System;
using System.Collections.Generic;
using System.Text;
using Zero.Core.Entities;
using Zero.Core.Intefaces.Sys;
using Zero.Infrastructure.DataBase;

namespace Zero.Infrastructure.Repository.Sys
{
   public class SysUserActionLogRepo : RepositoryBase<Sys_UserActionLog>,ISysUserActionLogRepo
    {
        public SysUserActionLogRepo(MyContext myContext):base(myContext)
        {

        }
    }
}
