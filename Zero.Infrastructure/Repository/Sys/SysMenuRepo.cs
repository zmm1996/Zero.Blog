using System;
using System.Collections.Generic;
using System.Text;
using Zero.Core.Entities;
using Zero.Core.Intefaces.Sys;
using Zero.Infrastructure.DataBase;

namespace Zero.Infrastructure.Repository.Sys
{
   public class SysMenuRepo:RepositoryBase<Sys_Menu>,ISysMenuRepo
    {
        public SysMenuRepo(MyContext myContext):base(myContext)
        {

        }
    }
}
