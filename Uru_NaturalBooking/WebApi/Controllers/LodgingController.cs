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
using Model.ForRequest;
using Model.ForResponse;

namespace WebApi.Controllers
{
    [Route("api/lodging")]
    [ApiController]
    public class LodgingController : ControllerBase
    {

        private readonly ILodgingManagement lodgingManagement;

        public LodgingController(ILodgingManagement lodgingLogic)
        {
            lodgingManagement = lodgingLogic;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Lodging> lodgings = lodgingManagement.GetAllLoadings();
                if (lodgings == null)
                {
                    return NotFound("No se pudo encontrar hospedajes");
                }
                return Ok(LodgingModelForResponse.ToModel(lodgings));
            } catch (ExceptionBusinessLogic e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet("{id}", Name = "GetLodging")]
        public IActionResult Get(Guid id)
        {
            try
            {
                Lodging lodging = lodgingManagement.GetLodgingById(id);
                if (lodging == null)
                {
                    return NotFound("El hospedaje solicitado no fue encontrado");
                }
                return Ok(LodgingModelForResponse.ToModel(lodging));
            }
            catch (ExceptionBusinessLogic e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }


        //[ServiceFilter(typeof(AuthorizationFilter))]
        [HttpPost]
        public IActionResult Post([FromBody] LodgingModelForRequest lodgingModel)
        {
            try
            {
                Lodging lodging = lodgingManagement.Create(LodgingModelForRequest.ToEntity(lodgingModel), lodgingModel.TouristSpotId);
                return CreatedAtRoute("GetLodging", new { id = lodging.Id }, LodgingModelForResponse.ToModel(lodging));
            }
            catch (ExceptionBusinessLogic e)
            {
                return BadRequest(e.Message);
            }
        }

        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] LodgingModelForRequest lodgingModel)
        {
            try
            {
                Lodging lodging = lodgingManagement.UpdateLodging(id, LodgingModelForRequest.ToEntity(lodgingModel)); 
                return CreatedAtRoute("GetLodging", new { id = lodging.Id }, LodgingModelForResponse.ToModel(lodging));
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
                lodgingManagement.RemoveLodging(id);
                return NoContent();
            }
            catch (ExceptionBusinessLogic e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}
