﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicException;
using BusinessLogicInterface;
using Domain;
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
                List<Category> categories = categoryManagement.GetAllCategories(); 
                if (categories == null)
                {
                    return NotFound("No se pudo encontrar hospedajes");
                }
                return Ok(CategoryModel.ToModel(categories));
            }
            catch(ExceptionBusinessLogic e)
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
                if (category == null)
                {
                    return NotFound("El objeto solicitado no fue encontrado");
                }
                return Ok(CategoryModel.ToModel(category));
            }catch(ExceptionBusinessLogic e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
            
        }

        [HttpPost]
        public IActionResult Post([FromBody] CategoryModel categoryModel)
        {
            try
            {
                Category category = categoryManagement.Create(CategoryModel.ToEntity(categoryModel));
                return CreatedAtRoute("Get", new { id = category.Id }, CategoryModel.ToModel(category));
            }
            catch (ExceptionBusinessLogic e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}