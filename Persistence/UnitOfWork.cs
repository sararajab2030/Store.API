using Domain.contracts;
using Domain.Entities;
using Persistence.Data;
using Persistence.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;
        private ConcurrentDictionary<string, object> _repositories;
        public UnitOfWork(StoreDbContext context) 
        {
            _context = context;
        }
        public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
       => _repositories.GetOrAdd(typeof(TEntity).Name, _ => new GenericRepository<TEntity, Tkey>(_context)) as IGenericRepository<TEntity, Tkey>;

        public async Task<int> SaveChangeAsync()
       => await _context.SaveChangesAsync();
    }
}
