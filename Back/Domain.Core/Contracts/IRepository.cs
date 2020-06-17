using Domain.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Core.Contracts
{
    public interface IRepository<TEntity>
       where TEntity : class
    {
        #region Contracts

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The Entity's state</returns>
        EntityState Add(TEntity entity);

        void AddRange(IList<TEntity> entities);

        /// <summary>
        /// Deletes the specified <paramref name="entity"/>
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns>The Entity's state</returns>
        EntityState Delete(TEntity entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The Entity's state</returns>
        EntityState Update(TEntity entity);

        void UpdateRange(IList<TEntity> entities);

        /// <summary>
        /// Checks whether a entity matching the <paramref name="predicate"/> exists
        /// </summary>
        /// <param name="predicate">The predicate to filter on</param>
        /// <returns>Whether an entity matching the <paramref name="predicate"/> exists</returns>
        bool Exists(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns>Entity</returns>
        TEntity Get<TKey>(TKey id);

        /// <summary>
        /// Gets the specified identifier. Asynchronous version.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns>Task Entity</returns>
        Task<TEntity> GetAsync<TKey>(TKey id);

        /// <summary>
        /// Gets an entity by the keys specified in <paramref name="keyValues"/>
        /// </summary>
        /// <param name="keyValues">Composite Primary Key Identifiers</param>
        /// <returns>The requested Entity</returns>
        TEntity Get(params object[] keyValues);

        /// <summary>
        /// Generic get and option to include child entities
        /// </summary>
        /// <param name="include">The include sub entities</param>
        /// <returns>Queryable</returns>
        IQueryable<TEntity> GetAsQueryable(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        /// <summary>
        /// Generic find by predicate and option to include child entities
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <param name="include">Include sub entities</param>
        /// <returns>Queryable</returns>
        IQueryable<TEntity> GetAsQueryable(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        /// <summary>
        /// Generic async get and option to include child entities
        /// </summary>
        /// <param name="include">The include sub entities</param>
        /// <returns>Task enumerable entity</returns>
        Task<IEnumerable<TEntity>> GetAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        /// <summary>
        /// Generic async filter operation (where)
        /// </summary>
        /// <param name="predicate">Predicate condition</param>
        /// <returns>Task enumerable entity</returns>
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Generic async filter operation (where) and option to include child entities
        /// </summary>
        /// <param name="predicate">Predicate condition</param>
        /// <param name="include">Include sub entities</param>
        /// <returns>Task enumerable entity</returns>
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        /// <summary>
        /// Generic no tracking filter operation (where) and option to include child entities
        /// </summary>
        /// <param name="predicate">Predicate condition</param>
        /// <param name="include">Include sub entities</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetNotTracking(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        /// <summary>
        /// Generic patch operation
        /// </summary>
        /// <typeparam name="TEntity1"></typeparam>
        /// <param name="entityName"></param>
        /// <param name="patchDtos"></param>
        /// <returns></returns>
        Task<int> ApplyPatchAsync<TEntity1>(TEntity1 entityName, List<PatchDto> patchDtos) where TEntity1 : class;

        /// <summary>
        /// Generic remove operation
        /// </summary>
        /// <param name="entities"></param>
        void RemoveRange(IEnumerable<TEntity> entities);

        #endregion
    }
}
