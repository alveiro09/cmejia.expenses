using Domain.Core.Contracts;
using System;
using System.Collections.Generic;

namespace Domain.Core.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContext dbContext;

        private Dictionary<Type, object> repositories;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork" /> class.
        /// </summary>
        /// <param name="contextFactory">The context factory.</param>
        public UnitOfWork(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Commit()
        {
            return dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(obj: this);
        }

        public IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            if (repositories == null)
                repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);
            if (!repositories.ContainsKey(type))
            {
                repositories[type] = new Repository<TEntity>(dbContext);
            }

            return (IRepository<TEntity>)repositories[type];
        }

        private void Dispose(bool disposing)
        {
            if (disposing && dbContext != null)
            {
                dbContext.Dispose();
                dbContext = null;
            }
        }
    }
}
