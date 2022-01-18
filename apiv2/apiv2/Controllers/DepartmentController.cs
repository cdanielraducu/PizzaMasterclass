using System;
using apiv2.Models;
using apiv2.Services.DepartmentService;
using apiv2.UOF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace apiv2.Controllers
{
    [Route("apiv2/[controller]")]
    [ApiController]
    public class DepartmentController: ControllerBase
    {
        private IDepartmentService _departmentService;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IDepartmentService departmentService, IUnitOfWork unitOfWork)
        {
            _departmentService = departmentService;
            _unitOfWork = unitOfWork;
        }

      

        [HttpPost("create")]
        public IActionResult Create(Department department)
        {
            if (department.Level == null)
            {
                return BadRequest(new { Message = "Level field should not be empty." });
            }

            var sameEmail = _departmentService.GetByLevel(department.Level);
            if (sameEmail != null)
            {
                return BadRequest(new { Message = "Email is already used, pick another one." });
            }

            var departmentToCreate = new Department
            {
                Level = department.Level,
            };

            _departmentService.Create(departmentToCreate);
            return Ok(new { Message = "Apprentice created with success." });
        }


        [HttpGet("get_all")]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            var departments = _departmentService.GetAllDepartments();

            if (departments == null)
            {
                return Ok("No departments...");
            }
            return Ok(departments);
        }

        [HttpGet("get_by_level")]
        [AllowAnonymous]
        public IActionResult GetByLevel(string level)
        {
            if (level == null)
            {
                return BadRequest(new { Message = "level field should not be empty." });
            }

            var apprentice = _departmentService.GetByLevel(level);
            if (apprentice == null)
            {
                return Ok("Department by this level not found");
            }
            return Ok(apprentice);
        }

        [HttpDelete("delete")]
        [AllowAnonymous]
        public IActionResult Delete(string level)
        {
            var departmentToDelete = _departmentService.GetByLevel(level);
            if (departmentToDelete == null)
            {
                return BadRequest(new { Message = "Apprentice by this email not found" });
            }
            _departmentService.Delete(departmentToDelete);
            return Ok("Department deleted");
        }

        [HttpPut("update")]
        [AllowAnonymous]
        public IActionResult Update(Department department)
        {
            var departmentToUpdate = _departmentService.GetByLevel(department.Level);
            if (departmentToUpdate == null)
            {
                return BadRequest(new { Message = "Apprentice by this email not found" });
            }

            departmentToUpdate.Level = department.Level ?? departmentToUpdate.Level;

            _departmentService.Update(departmentToUpdate);
            return Ok("Department Updated");
        }

    }
}
