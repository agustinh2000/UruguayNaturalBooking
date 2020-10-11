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
    public class UserController : ControllerBase
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
                UserSession userSession = userManagement.LogIn(loginModel.Email, loginModel.Password);
                return Ok(UserModelForResponse.ToModel(userSession.User));
            }
            catch (ClientBusinessLogicException e)
            {
                return BadRequest(e.Message);
            }
            catch (ServerBusinessLogicException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<User> allUsers = userManagement.GetAll();
                return Ok(UserModelForResponse.ToModel(allUsers));
            }
            catch (ClientBusinessLogicException e)
            {
                return NotFound(e.Message);
            }
            catch (ServerBusinessLogicException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpGet("{id}", Name = "getUser")]
        public IActionResult Get(Guid id)
        {
            try
            {
                User user = userManagement.GetUser(id);
                return Ok(UserModelForResponse.ToModel(user));
            }
            catch (ClientBusinessLogicException e)
            {
                return NotFound(e.Message);

            }
            catch (ServerBusinessLogicException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpPost]
        public IActionResult Post([FromBody]User user)
        {
            try
            {
                User userCreated = userManagement.Create(user);
                return CreatedAtRoute("getUser", new { id = user.Id }, UserModelForResponse.ToModel(userCreated));
            }
            catch (DomainBusinessLogicException e)
            {
                return BadRequest(e.Message);
            }
            catch (ServerBusinessLogicException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] User user)
        {
            try
            {
                User userUpdated = userManagement.UpdateUser(id, user);
                return CreatedAtRoute("getUser", new { id = userUpdated.Id }, UserModelForResponse.ToModel(userUpdated));
            }
            catch (DomainBusinessLogicException e)
            {
                return BadRequest(e.Message);
            }
            catch (ClientBusinessLogicException e)
            {
                return NotFound(e.Message);
            }
            catch (ServerBusinessLogicException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
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
            catch (ClientBusinessLogicException e)
            {
                return NotFound(e.Message);
            }
            catch (ServerBusinessLogicException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpDelete("logout")]
        public IActionResult Logout([FromHeader] string token)
        {
            try
            {
                userManagement.LogOut(token);
                return Ok("La sesion se ha cerrado con exito.");
            }
            catch (ClientBusinessLogicException e)
            {
                return NotFound(e.Message);
            }
            catch (ServerBusinessLogicException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
