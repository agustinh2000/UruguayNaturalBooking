using BusinessLogicException;
using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController: ControllerBase
    {
        private readonly IUserManagement userManagement;

        public UserController(IUserManagement logic)
        {
            userManagement = logic;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            try
            {
                return Ok(userManagement.LogIn(loginModel.Email, loginModel.Password));
            }
            catch (ExceptionBusinessLogic e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<User> users = userManagement.GetAll();
                if (users == null)
                {
                    return NotFound("No se encontraron usuarios.");
                }
                return Ok(UserModelForResponse.ToModel(users));
            }
            catch (ExceptionBusinessLogic e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult Get(Guid id)
        {
            try
            {
                User user = userManagement.GetUser(id);
                if (user == null)
                {
                    return NotFound("El usuario solicitado no fue encontrado");
                }
                return Ok(UserModelForResponse.ToModel(user));
            }
            catch (ExceptionBusinessLogic e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]User user)
        {
            try
            {
                User userCreated = userManagement.Create(user);
                return CreatedAtRoute("GetUser", new { id = user.Id }, userCreated);
            }
            catch (ExceptionBusinessLogic e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] User user)
        {
            try
            {
                User userUpdated = userManagement.UpdateUser(id, user);
                return CreatedAtRoute("GetUser", new { id = userUpdated.Id }, UserModelForResponse.ToModel(userUpdated));
            }
            catch (ExceptionBusinessLogic e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                userManagement.RemoveUser(id);
                return NoContent();
            }
            catch (ExceptionBusinessLogic e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpDelete("logout")]
        public IActionResult Logout([FromHeader] string token)
        {
            try
            {
                userManagement.LogOut(token);
                return Ok();
            }
            catch (ExceptionBusinessLogic e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
