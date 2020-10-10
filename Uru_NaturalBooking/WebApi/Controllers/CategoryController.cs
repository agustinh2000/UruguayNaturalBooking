using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicException;
using BusinessLogicInterface;
using Domain;
using Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.ForResponseAndRequest;

namespace WebApi.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryManagement categoryManagement; 

        public CategoryController(ICategoryManagement categoryLogic)
        {
            categoryManagement = categoryLogic; 
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Category> allCategories = categoryManagement.GetAllCategories(); 
                return Ok(CategoryModel.ToModel(allCategories));
            }catch(ClientBusinessLogicException)
            {
                return NotFound("No se pudo encontrar hospedajes");
            }
            catch(ServerBusinessLogicException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet("{id}", Name="Get")]
        public IActionResult Get(Guid id)
        {
            try
            {
                Category category = categoryManagement.GetById(id);
                return Ok(CategoryModel.ToModel(category));
            }
            catch (ClientBusinessLogicException)
            {
                return NotFound("El objeto solicitado no fue encontrado.");
            }
            catch (ServerBusinessLogicException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
            
        }

        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpPost]
        public IActionResult Post([FromBody] CategoryModel categoryModel)
        {
            try
            {
                Category category = categoryManagement.Create(CategoryModel.ToEntity(categoryModel));
                return CreatedAtRoute("Get", new { id = category.Id }, CategoryModel.ToModel(category));
            }
            catch(DomainBusinessLogicException e)
            {
                return BadRequest(e.Message);
            }
            catch (ServerBusinessLogicException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}
