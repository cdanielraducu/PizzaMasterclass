using System;
using apiv2.DTOs.ApprenticeDTO;
using apiv2.Models;
using apiv2.Services.ApprenticeService;
using apiv2.UOF;
using apiv2.Utilities.Attributes;
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
                Role = Role.Admin,
                Password = BCryptNet.HashPassword(apprentice.Password),
            };

            _apprenticeService.Create(apprenticeToCreate);
            return Ok(new { Message = "Apprentice created with success." });
        }

        [Authorization(Role.Admin)]
        [HttpGet("get_all")]
        public IActionResult GetAllUsers()
        {
            var users = _apprenticeService.GetByEmail("dani@tmail.com");
            if (users == null)
            {
                return BadRequest(new { Message = "No users found." });
            }
            return Ok(users);
        }
    }
}
