using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.contracts
{
    public interface IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    { 
    Task<TEntity> GetAsync(Tkey _id);
        Task<IEnumerable<TEntity>> GetAllAsync(bool isTrackable=false);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);



    }
    

    
}
