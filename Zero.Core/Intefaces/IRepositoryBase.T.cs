using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.RequestPayloads;

namespace Zero.Core.Intefaces
{
   public interface IRepositoryBase<TEntity> where TEntity :class,new()
    {

        int ExecuteBySql(string strSql);
        int ExecuteBySql(string strSql, DbParameter parameter);
        void Insert(TEntity entity);
        void Insert(List<TEntity> entities);
        void Update(TEntity entity);
        void Update(List<TEntity> entities);
        TEntity FindEntity(object key);
        TEntity FindEntity(Expression<Func<TEntity,bool>> expression);
        IEnumerable<TEntity> FindList();
        IEnumerable<TEntity> FindPageList(RequestPayload requestPayload);
        IEnumerable<TEntity> FindList(string strSql);

        IEnumerable<TEntity> FindList(string strSql, DbParameter parameter);
        IEnumerable<TEntity> FindList(Expression<Func<TEntity, bool>> expression);
        IQueryable<TEntity> IQueryable();
        IQueryable<TEntity> IQueryable(Expression<Func<TEntity,bool>> expression);

    }
}
