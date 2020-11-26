using System;
using System.Collections.Generic;
using BusinessLogicException;
using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
