using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Core.Intefaces
{
   public interface IRepositoryBase<TEntity> where TEntity :class,new()
    {

        int ExecuteBySql(string strSql);
        void Insert(TEntity entity);
        int Insert(List<TEntity> entities);
        int Update(TEntity entity);
        int Update(List<TEntity> entities);
        TEntity FindEntity(object key);
    }
}
