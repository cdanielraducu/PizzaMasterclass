using System;
using System.Linq;
using apiv2.Data;
using apiv2.Models;
using apiv2.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace apiv2.Repositories.DepartmentRepository
{
    public class DepartmentRepository: GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ProjectContext context): base(context)
        {
        }

        public Department FindByLevel(string level)
        {
            return (Department)_table.Include(level);
        }
    }
}
