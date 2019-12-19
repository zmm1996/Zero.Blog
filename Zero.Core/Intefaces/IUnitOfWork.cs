using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Core.Intefaces
{
   public interface IUnitOfWork
    {
        Task<bool> SaveAsync();
    }
}
