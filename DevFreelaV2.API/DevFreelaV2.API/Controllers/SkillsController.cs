using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.CreateSkill;
using DevFreela.Application.Queries.GetAllSkills;
using DevFreela.Application.Queries.GetSkillById;
using DevFreela.Application.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevFreelaV2.API.Controllers
{
    [Route("api/skills")]
    public class SkillsController : ControllerBase
    {
        //private readonly ISkillService _skillservice;
        private readonly IMediator _mediator;

        public SkillsController(IMediator mediator)
        {
            //_skillservice = skillservice;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var skills = _skillservice.GetAll();
            var query = new GetAllSkillsQuery();
            var skills = await _mediator.Send(query);

            return Ok(skills);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            //var skill = _skillservice.GetById(id);
            var query = new GetSkillByIdQuery(id);
            var skill = await _mediator.Send(query);

            if(skill == null)
            {
                return NotFound();
            }

            return Ok(skill);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateSkillCommand command)
        {
            //var id = _skillservice.Create(command);
            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = id }, command);
        }
    }
}
