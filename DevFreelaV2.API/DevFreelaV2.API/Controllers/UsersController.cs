using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreelaV2.API.Models;
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
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var users = _userService.GetAll();

            return Ok(users);
        }

        //Ex: api/users/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);

            if(user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post([FromBody] NewUserInputModel createUserInputModel)
        {
            var id = _userService.Create(createUserInputModel);

            return CreatedAtAction(nameof(GetById), new { id = id }, createUserInputModel);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] UpdateUserInputModel updateUserInputModel, int id)
        {
            _userService.Update(id, updateUserInputModel);
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
