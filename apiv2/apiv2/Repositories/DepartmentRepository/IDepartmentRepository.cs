using System;
using apiv2.Models;
using apiv2.Repositories.GenericRepository;

namespace apiv2.Repositories.DepartmentRepository
{
    public interface IDepartmentRepository: IGenericRepository<Department>
    {
        public Department FindByLevel(string level);
    }
}
