using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiv2.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace apiv2.Repositories.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly ProjectContext _context;
        protected readonly DbSet<TEntity> _table;

        public GenericRepository(ProjectContext projectContext)
        {
            _context = projectContext;
            _table = _context.Set<TEntity>();
        }

        public void Create(TEntity entity)
        {
            _table.Add(entity);
        }

        public TEntity FindById(object id)
        {
            return _table.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _table;
        }

        public void Remove(TEntity entity)
        {
            _table.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _table.Update(entity);
        }

        public bool Save()
        {
            try
            {
                return _context.SaveChanges() > 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }
    }
}
