using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreelaV2.API.Controllers
{
    [Route("api/skills")]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService _skillservice;

        public SkillsController(ISkillService skillservice)
        {
            _skillservice = skillservice;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var skills = _skillservice.GetAll();

            return Ok(skills);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var skill = _skillservice.GetById(id);

            if(skill == null)
            {
                return NotFound();
            }

            return Ok(skill);
        }

        [HttpPost]
        public IActionResult Post([FromBody] NewSkillInputModel inputModel)
        {
            var id = _skillservice.Create(inputModel);

            return CreatedAtAction(nameof(GetById), new { id = id }, inputModel);
        }
    }
}
