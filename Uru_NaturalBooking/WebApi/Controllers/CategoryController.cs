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

        [HttpGet("{id}", Name = "get")]
        public IActionResult Get(Guid id)
        {
            try
            {
                Category category = categoryManagement.GetById(id);
                return Ok(CategoryModel.ToModel(category));
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
        public IActionResult Post([FromBody] CategoryModel categoryModel)
        {
            try
            {
                Category category = categoryManagement.Create(CategoryModel.ToEntity(categoryModel));
                return CreatedAtRoute("get", new { id = category.Id }, CategoryModel.ToModel(category));
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
    }
}
