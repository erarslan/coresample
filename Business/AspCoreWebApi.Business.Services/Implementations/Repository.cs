using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AspCoreWebApi.Business.Services.ServiceContracts;
using AspCoreWebApi.Data.Context;
using AspCoreWebApi.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace AspCoreWebApi.Business.Services.Implementations
{
    public class Repository<T> : IRepository where T : Entity
    {
        //private readonly ILogger _logger;
        private readonly ProductContext _productContext;

        public Repository(ProductContext productContext)
        {
            _productContext = productContext;
            //_logger = logger;
        }

        public async Task<TEntity> GetAsync<TEntity>(long id, params Expression<Func<TEntity, object>>[] includes) where TEntity : Entity
        {
            return !includes.Any() ? await _productContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id)
                : await includes.Aggregate((_productContext.Set<TEntity>() as IQueryable<TEntity>), (current, include) => current.Include(include)).AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<TEntity>> GetAllAsync<TEntity>(params Expression<Func<TEntity, object>>[] includes) where TEntity : Entity
        {
            return !includes.Any() ? await _productContext.Set<TEntity>().AsNoTracking().ToListAsync()
                : await includes.Aggregate((_productContext.Set<TEntity>() as IQueryable<TEntity>), (current, include) => current.Include(include)).AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> FindAsync<TEntity>(Expression<Func<TEntity, bool>> match, params Expression<Func<TEntity, object>>[] includes) where TEntity : Entity
        {
            return !includes.Any() ? await _productContext.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(match)
                : await includes.Aggregate((_productContext.Set<TEntity>().Where(match)), (current, include) => current.Include(include)).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<List<TEntity>> FindAllAsync<TEntity>(Expression<Func<TEntity, bool>> match, params Expression<Func<TEntity, object>>[] includes) where TEntity : Entity
        {
            return !includes.Any() ? await _productContext.Set<TEntity>().Where(match).AsNoTracking().ToListAsync()
                : await includes.Aggregate((_productContext.Set<TEntity>().Where(match)), (current, include) => current.Include(include)).AsNoTracking().ToListAsync();
        }

        public async Task<long> SaveAsync<TEntity>(TEntity entity) where TEntity : Entity
        {
            _productContext.Add(entity);
            try
            {
                await _productContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _productContext.Dispose();
                //_logger.LogError("Error on save :" + ex);
                throw;
            }

            return entity.Id;
        }

        public async Task<long> SaveBulkAsync<TEntity>(List<TEntity> entityList) where TEntity : Entity
        {
            foreach (var entity in entityList)
            {
                _productContext.Add(entity);

            }

            try
            {
                await _productContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _productContext.Dispose();
                //_logger.LogError("Error on save :" + ex);
                throw;
            }



            return 0;
        }
    }
}
