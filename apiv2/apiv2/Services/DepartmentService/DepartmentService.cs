using System;
using System.Collections.Generic;
using apiv2.Models;
using apiv2.Repositories.DepartmentRepository;

namespace apiv2.Services.DepartmentService
{
    public class DepartmentService: IDepartmentService
    {
        public IDepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public void Create(Department department)
        {
            _departmentRepository.Create(department);
            _departmentRepository.Save();
        }

        public void Delete(Department department)
        {
            _departmentRepository.Remove(department);
            _departmentRepository.Save();
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            var allDepartments = _departmentRepository.GetAll();
            if(allDepartments == null)
            {
                return null;
            }
            return allDepartments;
        }

        public Department GetByLevel(string level)
        {
            var department = _departmentRepository.FindByLevel(level);

            if(department == null)
            {
                return null;
            }
            return department;
        }
       

        public void Update(Department department)
        {
            _departmentRepository.Update(department);
            _departmentRepository.Save();
        }
    }
}
