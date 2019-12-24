using System;
using System.Collections.Generic;
using System.Text;
using Zero.Core.Entities;
using Zero.Core.Intefaces.Sys;
using Zero.Infrastructure.DataBase;

namespace Zero.Infrastructure.Repository.Sys
{
   public class SysRoleRepo:RepositoryBase<Sys_Role>,ISysRoleRepo
    {
        public SysRoleRepo(MyContext myContext):base(myContext)
        {

        }
    }
}
