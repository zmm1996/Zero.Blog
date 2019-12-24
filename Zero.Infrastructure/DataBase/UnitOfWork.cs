using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Intefaces;

namespace Zero.Infrastructure.DataBase
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyContext _myContext;

        
        public UnitOfWork( MyContext myContext)
        {
            this._myContext = myContext;
        }
                
        /// <summary>
        /// 保存到数据库(工作单元模式)
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            return  _myContext.SaveChanges() > 0;
        }
    }
}
