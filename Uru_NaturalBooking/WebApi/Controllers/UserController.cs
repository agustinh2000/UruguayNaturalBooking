using BusinessLogicException;
using BusinessLogicInterface;
using Domain;
using Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.ForResponse;
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
        private  readonly IUserManagement userManagement;

        public UserController(IUserManagement logic)
        {
            userManagement = logic;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            try
            {
                UserSession userSession = userManagement.LogIn(loginModel.Email, loginModel.Password); 
                return Ok(UserModelForResponse.ToModel(userSession.User));
            }
            catch (ExceptionBusinessLogic e)
            {
                return BadRequest(e.Message);
            }
        }


        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<User> allUsers = userManagement.GetAll();
                if (allUsers == null)
                {
                    return NotFound("No se encontraron usuarios.");
                }
                return Ok(UserModelForResponse.ToModel(allUsers));
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

        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpPost]

        public IActionResult Post([FromBody]User user)
        {
            try
            {
                User userCreated = userManagement.Create(user);
                return CreatedAtRoute("GetUser", new { id = user.Id }, UserModelForResponse.ToModel(userCreated));
            }
            catch (ExceptionBusinessLogic e)
            {
                return BadRequest(e.Message);
            }
        }

        [ServiceFilter(typeof(AuthorizationFilter))]
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

        [ServiceFilter(typeof(AuthorizationFilter))]
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

        [ServiceFilter(typeof(AuthorizationFilter))]
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
