using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.InputModels;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Application.Services.Interfaces;
using DevFreelaV2.API.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreelaV2.API.Controllers
{
    [Route("api/projects")]
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
        public async Task<IActionResult> Get()
        {
            //var projects = _projectService.GetAll();
            var query = new GetAllProjectsQuery();
            var projects = await _mediator.Send(query);

            return Ok(projects);
        }

        //EX: api/projects/2
        [HttpGet("{id}")]
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
        public async Task<IActionResult> Post([FromBody] CreateProjectCommand command)
        {
            if (command.Title.Length > 50)
            {
                return BadRequest();
            }

            //var id = _projectService.Create(inputModel);
            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = id}, command);
        }

        //EX: api/projects/2
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateProjectInputModel inputModel)
        {
            var command = new UpdateProjectCommand(id, inputModel.Title, inputModel.Description, inputModel.TotalCost);

            if (command.Description.Length > 200)
            {
                return BadRequest();
            }
            
            //_projectService.Update(id, inputModel);
            await _mediator.Send(command);

            return NoContent();
        }

        //EX: api/projects/3/
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteProjectCommand(id);

            //_projectService.Delete(id);
            await _mediator.Send(command);

            return NoContent();
        }

        //EX: api/projects/1/comments POST
        [HttpPost("{id}/comments")]
        public async Task<IActionResult> PostComment(int id, [FromBody] CreateCommentCommand command)
        {
            //_projectService.CreateComment(inputModel);
            await _mediator.Send(command);

            return NoContent();
        }

        //EX: api/projects/1/start
        [HttpPut("{id}/start")]
        public async Task<IActionResult> Start(int id)
        {
            //_projectService.Start(id);
            var command = new StartProjectCommand(id);
            await _mediator.Send(command);

            return NoContent();
        }

        //EX: api/projects/1/finish
        [HttpPut("{id}/finish")]
        public async Task <ActionResult> Finish(int id)
        {
            //_projectService.Finish(id);
            var command = new FinishProjectCommand(id);
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
