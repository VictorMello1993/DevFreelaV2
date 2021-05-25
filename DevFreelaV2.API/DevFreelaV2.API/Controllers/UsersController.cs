using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Commands.DeleteUser;
using DevFreela.Application.Commands.LoginUser;
using DevFreela.Application.Commands.RedeemPassword;
using DevFreela.Application.Commands.UpdateUser;
using DevFreela.Application.InputModels;
using DevFreela.Application.Queries.GetAllUsers;
using DevFreela.Application.Queries.GetUserByEmail;
using DevFreela.Application.Queries.GetUserById;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreelaV2.API.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreelaV2.API.Controllers
{
    [Route("api/users")]
    [Authorize]
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
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
        {
            //Chamando a camada de validação do Fluent Validation - Usando a verificação do ModelState em uma Action (mais tradicional)
            //if (!ModelState.IsValid)
            //{
            //    var messages = ModelState.SelectMany(ms => ms.Value.Errors).Select(e => e.ErrorMessage).ToList();

            //    return BadRequest(messages);
            //}            

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
        
        [HttpPut("login")]
        [AllowAnonymous]
        public async Task <IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var loginUserViewModel = await _mediator.Send(command);

            if(loginUserViewModel == null)
            {
                return BadRequest("Usuário ou senha inválidos.");
            }

            return Ok(loginUserViewModel);
        }        

        [HttpPost("forgotPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordInputModel inputModel)
        {
            var query = new GetUserByEmailQuery(inputModel.Email);

            var userViewModel = await _mediator.Send(query);

            if(userViewModel == null)
            {
                return BadRequest("Usuário não encontrado com o e-mail especificado.");
            }

            var callbackUrl = Url.Action("forgotPassword", "users", new { id = userViewModel.Id }, protocol: Request.Scheme);

            var command = new RedeemPasswordCommand(userViewModel.Email, callbackUrl);

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
