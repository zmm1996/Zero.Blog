using System;
using System.Collections.Generic;
using System.Text;
using Zero.Core.Entities;
using Zero.Core.Intefaces.Sys;
using Zero.Infrastructure.DataBase;

namespace Zero.Infrastructure.Repository.Sys
{
   public class UserRoleRepo: RepositoryBase<Sys_UserRole>, IUserRoleRepo
    {

        public UserRoleRepo(MyContext myContext):base(myContext)
        {

        }
    }
}
