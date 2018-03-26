using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AspCoreWebApi.Data.Model;

namespace AspCoreWebApi.Business.Services.ServiceContracts
{
    public interface IRepository
    {
        Task<TEntity> GetAsync<TEntity>(long id, params Expression<Func<TEntity, object>>[] includes) where TEntity : Entity;

        Task<List<TEntity>> GetAllAsync<TEntity>(params Expression<Func<TEntity, object>>[] includes) where TEntity : Entity;

        Task<TEntity> FindAsync<TEntity>(Expression<Func<TEntity, bool>> match, params Expression<Func<TEntity, object>>[] includes) where TEntity : Entity;

        Task<List<TEntity>> FindAllAsync<TEntity>(Expression<Func<TEntity, bool>> match, params Expression<Func<TEntity, object>>[] includes) where TEntity : Entity;

        Task<long> SaveAsync<TEntity>(TEntity entity) where TEntity : Entity;

        Task<long> SaveBulkAsync<TEntity>(List<TEntity> entityList) where TEntity : Entity;
    }
}
