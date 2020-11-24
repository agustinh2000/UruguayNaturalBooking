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
                return Ok(LodgingModelForSearchResponse.ToModel(allLodgings));
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

        [HttpGet("{id}", Name = "lodging")]
        public IActionResult Get(Guid id)
        {
            try
            {
                Lodging lodging = lodgingManagement.GetLodgingById(id);
                return Ok(LodgingModelForResponse.ToModel(lodging));
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
        public IActionResult Post([FromBody] LodgingModelForRequest lodgingModel)
        {
            try
            {
                Lodging lodging = lodgingManagement.Create(LodgingModelForRequest.ToEntity(lodgingModel), lodgingModel.TouristSpotId, lodgingModel.Images);
                return CreatedAtRoute("lodging", new { id = lodging.Id }, LodgingModelForSearchResponse.ToModel(lodging));
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
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] LodgingModelForRequest lodgingModel)
        {
            try
            {
                Lodging lodging = lodgingManagement.UpdateLodging(id, LodgingModelForRequest.ToEntity(lodgingModel));
                return CreatedAtRoute("lodging", new { id = lodging.Id }, LodgingModelForResponse.ToModel(lodging));
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
                lodgingManagement.RemoveLodging(id);
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
    }
}
