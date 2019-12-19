﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Intefaces;
using Zero.Infrastructure.DataBase;

namespace Zero.Infrastructure.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class, new()
    {
        private readonly MyContext _myContext;

        public RepositoryBase(MyContext myContext)
        {
            this._myContext = myContext;
        }

        public async Task<TEntity> FindEntity(object key)
        {
          return  await _myContext.FindAsync<TEntity>(key);
        }

        int IRepositoryBase<TEntity>.ExecuteBySql(string strSql)
        {
            throw new NotImplementedException();
        }

        int IRepositoryBase<TEntity>.Insert(TEntity entity)
        {
            throw new NotImplementedException();
        }

        int IRepositoryBase<TEntity>.Insert(List<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        int IRepositoryBase<TEntity>.Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        int IRepositoryBase<TEntity>.Update(List<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        



    }
}