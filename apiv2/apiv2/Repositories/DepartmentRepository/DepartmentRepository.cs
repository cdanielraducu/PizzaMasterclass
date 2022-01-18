using System;
using System.Linq;
using apiv2.Data;
using apiv2.Models;
using apiv2.Repositories.GenericRepository;

namespace apiv2.Repositories.DepartmentRepository
{
    public class DepartmentRepository: GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ProjectContext context): base(context)
        {
        }

        public Department FindByLevel(string level)
        {
            return _table.FirstOrDefault(x => x.Level == level);
        }
    }
}
