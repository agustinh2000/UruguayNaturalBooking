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
    [Route("api/touristSpots")]
    [ApiController]
    public class TouristSpotController : ControllerBase
    {

        private readonly ITouristSpotManagement touristSpotManagement;

        public TouristSpotController(ITouristSpotManagement touristSpotLogic)
        {
            touristSpotManagement = touristSpotLogic;
        }

        [HttpGet("{id}", Name = "touristSpot")]
        public IActionResult Get(Guid id)
        {
            try
            {
                TouristSpot touristSpot = touristSpotManagement.GetTouristSpotById(id);
                return Ok(TouristSpotForResponseModel.ToModel(touristSpot));
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

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<TouristSpot> allTouristSpots = touristSpotManagement.GetAllTouristSpot();
                return Ok(TouristSpotForResponseModel.ToModel(allTouristSpots));
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

        [HttpGet("byCategoriesAndRegion")]
        public IActionResult GetTouristSpotsByCategoriesAndRegionId([FromQuery] Guid[] categoriesId, [FromQuery] Guid regionId)
        {
            try
            {
                List<TouristSpot> touristSpotsByRegionAndCategories = touristSpotManagement.
                    GetTouristSpotsByCategoriesAndRegion(categoriesId.ToList(), regionId);
                return Ok(TouristSpotForResponseModel.ToModel(touristSpotsByRegionAndCategories));
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
        public IActionResult Post([FromBody] TouristSpotForRequestModel aTouristSpot)
        {
            try
            {
                TouristSpot touristSpotAdded = touristSpotManagement.Create(TouristSpotForRequestModel.ToEntity(aTouristSpot), aTouristSpot.RegionId, aTouristSpot.ListOfCategoriesId);
                return CreatedAtRoute("touristSpot", new { id = touristSpotAdded.Id }, TouristSpotForResponseModel.ToModel(touristSpotAdded));
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
    }
}