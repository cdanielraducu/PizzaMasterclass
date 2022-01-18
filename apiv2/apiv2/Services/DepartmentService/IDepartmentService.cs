using System;
using System.Collections.Generic;
using apiv2.Models;

namespace apiv2.Services.DepartmentService
{
    public interface IDepartmentService
    {
        IEnumerable<Department> GetAllDepartments();
        Department GetByLevel(string level);
        void Create(Department department);
        void Delete(Department department);
        void Update(Department department);
    }
}
