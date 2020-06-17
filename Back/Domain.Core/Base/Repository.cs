using Domain.Core.Contracts;
using Domain.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Core.Base
{
    public class Repository<TEntity> : IRepository<TEntity>
           where TEntity : class
    {
        #region Fields

        /// <summary>
        /// EF data base context
        /// </summary>
        private readonly IDbContext context;

        /// <summary>
        /// Used to query and save instances of
        /// </summary>
        private readonly DbSet<TEntity> dbSet;

        #endregion

        #region Constructor

        public Repository(IDbContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        #endregion

        #region IRepository members

        /// <inheritdoc />
        public virtual EntityState Add(TEntity entity)
        {
            return dbSet.Add(entity).State;
        }

        /// <inheritdoc />
        public void AddRange(IList<TEntity> entities)
        {
            if (entities.Count == 0)
                throw new ArgumentNullException(nameof(entities));

            dbSet.AddRange(entities);
        }

        /// <inheritdoc />
        public EntityState Delete(TEntity entity)
        {
            return dbSet.Remove(entity).State;
        }

        /// <inheritdoc />
        public virtual EntityState Update(TEntity entity)
        {
            return dbSet.Update(entity).State;
        }

        /// <inheritdoc />
        public void UpdateRange(IList<TEntity> entities)
        {
            if (entities.Count == 0)
                throw new ArgumentNullException(nameof(entities));
            dbSet.UpdateRange(entities);
        }

        /// <inheritdoc />
        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.Any(predicate);
        }

        /// <inheritdoc />
        public TEntity Get<TKey>(TKey id)
        {
            return dbSet.Find(id);
        }

        /// <inheritdoc />
        public async Task<TEntity> GetAsync<TKey>(TKey id)
        {
            return await dbSet.FindAsync(id);
        }

        /// <inheritdoc />
        public TEntity Get(params object[] keyValues)
        {
            return dbSet.Find(keyValues);
        }

        /// <inheritdoc />
        public IQueryable<TEntity> GetAsQueryable(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = dbSet;
            if (include != null)
                query = include(query);

            return query;
        }

        /// <inheritdoc />
        public IQueryable<TEntity> GetAsQueryable(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = dbSet;
            if (include != null)
                query = include(query);

            if (predicate != null)
                query = query.Where(predicate);

            return query;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return await dbSet.Where(predicate).ToListAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> GetAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (include != null)
                query = include(query);

            return await query.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = dbSet;
            if (include != null)
                query = include(query);

            if (predicate != null)
                query = query.Where(predicate);

            return await query.ToListAsync();
        }

        public IEnumerable<TEntity> GetNotTracking(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = dbSet;
            if (include != null)
                query = include(query);

            if (predicate != null)
                query = query.Where(predicate);

            return query.AsNoTracking().ToList();
        }

        public async Task<int> ApplyPatchAsync<TEntity1>(TEntity1 entityName, List<PatchDto> patchDtos) where TEntity1 : class
        {
            var nameValuePairProperties = patchDtos.ToDictionary(a => a.PropertyName, a => a.PropertyValue);

            var dbEntityEntry = context.Entry(entityName);
            dbEntityEntry.CurrentValues.SetValues(nameValuePairProperties);
            dbEntityEntry.State = EntityState.Modified;
            return await context.SaveChangesAsync();
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("item");

            dbSet.RemoveRange(entities);
        }

        #endregion
    }
}
