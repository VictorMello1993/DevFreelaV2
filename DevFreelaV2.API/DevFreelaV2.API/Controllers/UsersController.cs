using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Commands.DeleteUser;
using DevFreela.Application.Commands.UpdateUser;
using DevFreela.Application.InputModels;
using DevFreela.Application.Queries.GetAllUsers;
using DevFreela.Application.Queries.GetUserById;
using DevFreela.Application.Services.Interfaces;
using DevFreelaV2.API.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreelaV2.API.Controllers
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        //private readonly IUserService _userService;
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            //_userService = userService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var users = _userService.GetAll();
            var query = new GetAllUsersQuery();
            var users = await _mediator.Send(query);

            return Ok(users);
        }

        //Ex: api/users/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            //var user = _userService.GetById(id);
            var query = new GetUserByIdQuery(id);
            var user = await _mediator.Send(query);

            if(user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
        {
            //var id = _userService.Create(createUserInputModel);
            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = id }, command);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //_userService.Delete(id);
            var command = new DeleteUserCommand(id);
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateUserInputModel inputModel, int id)
        {
            //_userService.Update(id, inputmodel);      
            var command = new UpdateUserCommand(id, inputModel.Email);
            await _mediator.Send(command);

            return NoContent();
        }

        //Ex: api/users/1/login
        [HttpPut("{id}/login")]
        public IActionResult Login(int id, [FromBody] LoginModel loginModel)
        {
            return NoContent();

            //Será refatorado para implementar a lógica para retornar uma senha encriptada através da chave de autenticação gerada do token JWT
        }
    }
}
