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
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Redefinir a senha do usuário
        /// </summary>
        /// <remarks>
        /// Requisição de exemplo:
        /// {
        ///     "email": "email@meusite.com.br"
        ///     "newPassword": "@123456",
        ///     "confirmPassword": "@123456"
        /// }
        /// </remarks>        
        /// <param name="command">E-mail e a nova senha para atualização</param>
        /// <returns>Booleano indicando se a senha foi alterada com sucesso ou não.</returns>
        /// <response code="204">Senha alterada com sucesso.</response>
        /// <response code="400">Não foi possível alterar a senha devido aos erros encontrados.</response>                
        [HttpPut("forgotPassword")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] RedeemPasswordCommand command)
        {
            var passwordChangedSuccessful = await _mediator.Send(command);            

            if (!passwordChangedSuccessful)
            {
                return BadRequest("Não foi possível redefinir a senha, pois não foi encontrado usuário com e-mail especificado.");
            }

            return NoContent();
        }
    }
}
