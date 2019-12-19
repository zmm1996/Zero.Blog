using System;
using System.Collections.Generic;
using System.Text;
using Zero.Core.Entities;
using Zero.Core.Intefaces.Sys;
using Zero.Infrastructure.DataBase;

namespace Zero.Infrastructure.Repository.Sys
{
    public class SysUserRepo:RepositoryBase<Sys_User>, ISysUserRepo
    {
        public SysUserRepo(MyContext myContext):base(myContext)
        {

        }
    }
}
