﻿using Domain.contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, Tkey> : IGenericRepository<TEntity, Tkey>
        where TEntity : BaseEntity<Tkey>
    {
        private readonly StoreDbContext _context;

        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TEntity entity)
       => await _context.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity)
       => _context.Set<TEntity>().Remove(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool isTrackable = false)
        {
            if (isTrackable)
                return await _context.Set<TEntity>().ToListAsync();

            return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetAsync(Tkey _id)
        => await _context.Set<TEntity>().FindAsync(_id);


        public void Update(TEntity entity)
       => _context.Set<TEntity>().Update(entity);
    }
}
