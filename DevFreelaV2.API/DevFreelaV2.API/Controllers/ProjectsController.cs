using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevFreelaV2.API.Controllers
{
    [Route("api/projects")]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        //private readonly OpeningTimeOption _option;
        //public ProjectsController(IOptions<OpeningTimeOption> option, ExampleClass exampleClass)
        //{
        //    exampleClass.Name = "Updated at ProjectsControoler";
        //    _option = option.Value;
        //}

        //private readonly IProjectService _projectService;
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            //_projectService = projectService;
            _mediator = mediator;
        }

        //EX: api/projects?query=net core - Busca pelo query string
        [HttpGet]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> Get()
        {
            //var projects = _projectService.GetAll();
            var query = new GetAllProjectsQuery();
            var projects = await _mediator.Send(query);

            return Ok(projects);
        }

        //EX: api/projects/2
        [HttpGet("{id}")]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> GetById(int id)
        {
            //var project = _projectService.GetById(id);
            var query = new GetProjectByIdQuery(id);
            var project = await _mediator.Send(query);

            if(project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpPost]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Post([FromBody] CreateProjectCommand command)
        {
            //Chamando a camada de validação do Fluent Validation - Usando a verificação do ModelState em uma Action (mais tradicional)
            //if (!ModelState.IsValid)
            //{
            //    var messages = ModelState.SelectMany(ms => ms.Value.Errors).Select(e => e.ErrorMessage).ToList();
            //    return BadRequest(messages);
            //}            

            //var id = _projectService.Create(inputModel);
            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = id}, command);
        }

        //EX: api/projects/2
        [HttpPut("{id}")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateProjectCommand command)
        {            
            //if (!ModelState.IsValid)
            //{
            //    var messages = ModelState.SelectMany(ms => ms.Value.Errors).Select(e => e.ErrorMessage).ToList();
            //    return BadRequest(messages);
            //}
            
            //_projectService.Update(id, inputModel);
            await _mediator.Send(command);

            return NoContent();
        }

        //EX: api/projects/3/
        [HttpDelete("{id}")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteProjectCommand(id);

            //_projectService.Delete(id);
            await _mediator.Send(command);

            return NoContent();
        }

        //EX: api/projects/1/comments POST
        [HttpPost("{id}/comments")]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> PostComment(int id, [FromBody] CreateCommentCommand command)
        {
            //_projectService.CreateComment(inputModel);
            await _mediator.Send(command);

            return NoContent();
        }

        //EX: api/projects/1/start
        [HttpPut("{id}/start")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Start(int id)
        {
            //_projectService.Start(id);
            var command = new StartProjectCommand(id);
            await _mediator.Send(command);

            return NoContent();
        }

        //EX: api/projects/1/finish
        [HttpPut("{id}/finish")]
        [Authorize(Roles = "client")]
        public async Task <ActionResult> Finish(int id, [FromBody] FinishProjectCommand command)
        {
            //_projectService.Finish(id);
            //var command = new FinishProjectCommand(id);

            command.Id = id;

            var result = await _mediator.Send(command);

            if (!result)
            {
                return BadRequest("O pagamento não pôde ser processado.");
            }            

            return NoContent();
        }
    }
}
