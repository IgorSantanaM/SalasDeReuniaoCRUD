using SalasDeReuniaoCRUD.Domain.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SalasDeReuniaoCRUD.Domain.Core.Data
{
    public interface IRepository<TEntity, TId> where TEntity : IAggregateRoot
    {
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
