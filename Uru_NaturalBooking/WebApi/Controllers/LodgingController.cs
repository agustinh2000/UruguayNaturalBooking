using System;
using System.Collections.Generic;
using BusinessLogicException;
using BusinessLogicInterface;
using Domain;
using Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.ForRequest;
using Model.ForResponse;

namespace WebApi.Controllers
{
    [Route("api/lodgings")]
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
                List<Lodging> allLodgings = lodgingManagement.GetAllLoadings();

                return Ok(LodgingModelForResponse.ToModel(allLodgings));
            }
            catch (ClientBusinessLogicException)
            {
                return NotFound("No se pudo encontrar hospedajes");
            }
            catch (ServerBusinessLogicException e)
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
                return Ok(LodgingModelForResponse.ToModel(lodging));
            }
            catch (ClientBusinessLogicException)
            {
                return NotFound("El hospedaje solicitado no fue encontrado");
            }
            catch (ServerBusinessLogicException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }


        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpPost]
        public IActionResult Post([FromBody] LodgingModelForRequest lodgingModel)
        {
            try
            {
                Lodging lodging = lodgingManagement.Create(LodgingModelForRequest.ToEntity(lodgingModel), lodgingModel.TouristSpotId);
                return CreatedAtRoute("GetLodging", new { id = lodging.Id }, LodgingModelForResponse.ToModel(lodging));
            }
            catch (ServerBusinessLogicException e)
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
            catch (ServerBusinessLogicException e)
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
            catch (ServerBusinessLogicException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}
