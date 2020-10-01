using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryManagement categoryManagement; 

        public CategoryController(ICategoryManagement categoryLogic)
        {
            categoryManagement = categoryLogic; 
        }

        [HttpGet]
        public IActionResult  Get()
        {
            return Ok(CategoryModel.ToModel(categoryManagement.GetAllCategories())); 
        }

        [HttpGet("{id}", Name="Get")]
        public IActionResult Get(Guid id)
        {
            Category category = categoryManagement.GetById(id); 
            if(category== null)
            {
                return NotFound("El objeto solicitado no fue encontrado"); 
            }
            return Ok(CategoryModel.ToModel(category)); 
        }

        [HttpPost]
        public IActionResult Post([FromBody] CategoryModel categoryModel)
        {
            try
            {
                Category category = categoryManagement.Create(CategoryModel.ToEntity(categoryModel));
                return CreatedAtRoute("Get", new { id = category.Id }, CategoryModel.ToModel(category));
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
