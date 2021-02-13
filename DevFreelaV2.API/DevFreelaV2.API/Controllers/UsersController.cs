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
        //Ex: api/users/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateUserModel createUserModel)
        {
            return CreatedAtAction(nameof(GetById), new { id = 1 }, createUserModel);
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
