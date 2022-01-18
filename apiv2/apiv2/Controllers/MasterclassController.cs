using System;
using apiv2.Models;
using apiv2.Services.MasterclassService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apiv2.Controllers
{
    [Route("apiv2/[controller]")]
    [ApiController]
    public class MasterclassController : ControllerBase
    {
        private IMasterclassService _masterclassService;

        public MasterclassController(IMasterclassService masterclassService)
        {
            _masterclassService = masterclassService;
            
        }


        [HttpPost("create")]
        public IActionResult Create(Masterclass masterclass)
        {
            if (masterclass.MasterclassApprentices == null)
            {
                return BadRequest(new { Message = "Level field should not be empty." });
            }

            var MasterclassToCreate = new Masterclass
            {
                MasterclassApprentices = masterclass.MasterclassApprentices,
            };

            _masterclassService.Create(MasterclassToCreate);
            return Ok(new { Message = "Apprentice created with success." });
        }


        [HttpGet("get_all")]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            var masterclasses = _masterclassService.GetAllMasterclasses();

            if (masterclasses == null)
            {
                return Ok("No masterclasses...");
            }
            return Ok(masterclasses);
        }

        [HttpDelete("delete")]
        [AllowAnonymous]
        public IActionResult Delete(Masterclass masterclass)
        {
            
            if (masterclass == null)
            {
                return BadRequest(new { Message = "Masterclass empty" });
            }
            _masterclassService.Delete(masterclass);
            return Ok("masterclass deleted");
        }

        [HttpPut("update")]
        [AllowAnonymous]
        public IActionResult Update(Masterclass masterclass)
        {
            if (masterclass == null)
            {
                return BadRequest(new { Message = "Masterclass empty" });
            }
            _masterclassService.Update(masterclass);
            return Ok("Department Updated");
        }

    }
}
