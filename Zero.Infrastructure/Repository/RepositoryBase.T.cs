using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Entities;
using Zero.Core.Intefaces;
using Zero.Core.RequestPayloads;
using Zero.Infrastructure.DataBase;

namespace Zero.Infrastructure.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity  :  class, new()
    {
        private readonly MyContext _myContext;

        public RepositoryBase(MyContext myContext)
        {
            this._myContext = myContext;
        }

        public  TEntity FindEntity(object key)
        {
            return  _myContext.Find<TEntity>(key);
        }
        public TEntity FindEntity(Expression<Func<TEntity,bool>> expression)
        {
            return _myContext.Set<TEntity>().Where(expression).FirstOrDefault();
        }

        public void Insert(TEntity entity)
        {
            _myContext.Add(entity);
        }

       public void Insert(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _myContext.Entry<TEntity>(entity).State = EntityState.Added;
            }
        }

       public void Update(TEntity entity)
        {
            _myContext.Entry<TEntity>(entity).State = EntityState.Modified;
        }

       public void Update(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _myContext.Entry<TEntity>(entity).State = EntityState.Modified;
            }
        }

        public IEnumerable<TEntity> FindList(string strSql)
        {
            // _myContext.Database
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> FindList(string strSql, DbParameter parameter)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<TEntity> FindList(Expression<Func<TEntity,bool>> expression)
        {
            return _myContext.Set<TEntity>().Where(expression).ToList();
        }

        public IQueryable<TEntity> IQueryable()
        {
           return _myContext.Set<TEntity>().AsNoTracking();
        }

        public IQueryable<TEntity> IQueryable(Expression<Func<TEntity, bool>> expression)
        {
            return _myContext.Set<TEntity>().Where(expression).AsNoTracking();
        }

        int IRepositoryBase<TEntity>.ExecuteBySql(string strSql)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> FindList()
        {
          return   _myContext.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> FindPageList(RequestPayload requestPayload) {

         return   _myContext.Set<TEntity>().AsQueryable()
                 .Skip((requestPayload.PageIndex - 1) * requestPayload.PageSize)
                 .Take(requestPayload.PageSize).ToList();
              
          
        }
    }
}
