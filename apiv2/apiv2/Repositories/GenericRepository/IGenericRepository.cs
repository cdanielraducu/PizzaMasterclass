using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace apiv2.Repositories.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity: class
    {
        IEnumerable<TEntity> GetAll();
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        TEntity FindById(object id);
        bool Save();
    }
}
