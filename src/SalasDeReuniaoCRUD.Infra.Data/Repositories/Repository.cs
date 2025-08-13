using Microsoft.EntityFrameworkCore;
using SalasDeReuniaoCRUD.Domain.Core.Data;
using SalasDeReuniaoCRUD.Domain.Core.Model;
using SalasDeReuniaoCRUD.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SalasDeReuniaoCRUD.Infra.Data.Repositories
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class, IAggregateRoot
    {
        protected SalasDeReuniaoContext Db;
        protected DbSet<TEntity> DbSet;

        public Repository(SalasDeReuniaoContext context)
        {
            Db = context ?? throw new ArgumentNullException(nameof(context));
            DbSet = Db.Set<TEntity>();
        }

        /// <summary>
        /// Asynchronously adds a new entity to the database.
        /// </summary>
        public virtual async Task AddAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }

        /// <summary>
        /// Marks an existing entity as modified.
        /// The change will be saved to the database when SaveChangesAsync is called on the Unit of Work.
        /// </summary>
        public virtual void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        /// <summary>
        /// Marks an existing entity for deletion.
        /// The entity will be deleted from the database when SaveChangesAsync is called on the Unit of Work.
        /// </summary>
        public virtual void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        /// <summary>
        /// Asynchronously finds all entities that satisfy a given condition.
        /// </summary>
        /// <param name="predicate">The condition to filter entities.</param>
        /// <returns>A collection of entities that match the predicate.</returns>
        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }
    }
}
