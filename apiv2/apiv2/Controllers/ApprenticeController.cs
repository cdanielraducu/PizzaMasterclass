using System;
using apiv2.DTOs.ApprenticeDTO;
using apiv2.Models;
using apiv2.Services.ApprenticeService;
using apiv2.UOF;
using apiv2.Utilities.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using BCryptNet = BCrypt.Net.BCrypt;

namespace apiv2.Controllers
{

    [Route("apiv2/[controller]")]
    [ApiController]
    public class ApprenticeController: ControllerBase
    {
        private IApprenticeService _apprenticeService;
        private  readonly IUnitOfWork _unitOfWork;
        public  ApprenticeController(IApprenticeService apprenticeService, IUnitOfWork unitOfWork)
        {
            _apprenticeService = apprenticeService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("get_all_master_apprentices")]
        public IActionResult GetAllMasterApprentices()
        {
            var apprentices = _unitOfWork.Apprentices.GetAllMasterApprentices();
            if(apprentices == null)
            {
                return BadRequest(new { Message = "No master-apprentices found." });
            }
            return Ok(apprentices);
        }

        [HttpPost]
        public IActionResult AddApprenticeAndDepartment()
        {
            var apprentice = new Apprentice
            {
                Name = "Dani",
                Level = "Master",
                Email = "dani@tmail.com",
                Role = Role.Apprentice,
                Password = BCryptNet.HashPassword("1234")
            };
            var department = new Department
            { 
                Level = "Master"
            };
            _unitOfWork.Apprentices.Create(apprentice);
            _unitOfWork.Departments.Create(department);
            
             _unitOfWork.Complete();
            return Ok("Added Successfully");
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(ApprenticeRequestDTO apprentice)
        {
            var response = _apprenticeService.Authenticate(apprentice);

            if(response == null)
            {
                return BadRequest(new { Message = "Username or Password is invalid" });
            }

            return Ok(response);
        }

        [HttpPost("create")]
        public IActionResult Create(ApprenticeRequestDTO apprentice)
        {
            if (apprentice.Email == null)
            {
                return BadRequest(new { Message = "Email field should not be empty." });
            }

            var sameEmail = _apprenticeService.GetByEmail(apprentice.Email);
            if (sameEmail != null)
            {
                return BadRequest(new { Message = "Email is already used, pick another one." });
            }

            var apprenticeToCreate = new Apprentice
            {
                Name = apprentice.Name,
                Email = apprentice.Email,
                Level = "Master",
                Role = Role.Apprentice,
                Password = BCryptNet.HashPassword(apprentice.Password),
            };

            _apprenticeService.Create(apprenticeToCreate);
            return Ok(new { Message = "Apprentice created with success." });
        }

        [HttpGet("get_all")]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            var apprentices = _apprenticeService.GetAllApprentices();

            if(apprentices == null)
            {
                return Ok("No apprentices...");
            }
            return Ok(apprentices);
        }

        [HttpGet("get_by_email")]
        [AllowAnonymous]
        public IActionResult GetByEmail(string email)
        {
            if(email == null)
            {
                return BadRequest(new { Message = "Email field should not be empty." });
            }

            var apprentice = _apprenticeService.GetByEmail(email);
            if(apprentice == null)
            {
                return Ok("Apprentice by this email not found");
            }
            return Ok(apprentice);
        }

        [HttpDelete("delete")]
        [AllowAnonymous]
        public IActionResult Delete(string email)
        {
            var apprenticeToDelete = _apprenticeService.GetByEmail(email);
            if(apprenticeToDelete == null)
            {
                return BadRequest(new { Message = "Apprentice by this email not found" });
            }
            _apprenticeService.Delete(apprenticeToDelete);
            return Ok("Apprentice deleted");
        }

        [HttpPut("update")]
        [AllowAnonymous]
        public IActionResult Update(ApprenticeRequestDTO apprentice)
        {
            var apprenticeToUpdate = _apprenticeService.GetByEmail(apprentice.Email);
            if (apprenticeToUpdate == null)
            {
                return BadRequest(new { Message = "Apprentice by this email not found" });
            }

            apprenticeToUpdate.Name = apprentice.Name ?? apprenticeToUpdate.Name; ;
            apprenticeToUpdate.Password = apprentice.Password == null ? apprenticeToUpdate.Password : BCryptNet.HashPassword(apprentice.Password);

            _apprenticeService.Update(apprenticeToUpdate);
            return Ok("Apprentice Updated");
        }

        [Authorization(Role.Admin)]
        [HttpGet("get_all_apprentices")]
        public IActionResult GetAllApprentices()
        {
            var apprentices = _apprenticeService.GetAllApprentices();
            if (apprentices == null)
            {
                return BadRequest(new { Message = "No apprentices found." });
            }
            return Ok(apprentices);
        }
    }
}
