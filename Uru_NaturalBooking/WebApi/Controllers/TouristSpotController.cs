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

        [HttpGet("{id}", Name = "GetTouristSpot")]
        public IActionResult Get(Guid id)
        {
            try
            {
                TouristSpot touristSpot = touristSpotManagement.GetTouristSpotById(id);
                if (touristSpot == null)
                {
                    return NotFound("El punto turistico solicitado no fue encontrado");
                }
                return Ok(TouristSpotForResponseModel.ToModel(touristSpot));
            }
            catch (ExceptionBusinessLogic e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<TouristSpot> allTouristSpots = touristSpotManagement.GetAllTouristSpot();
                if(allTouristSpots == null)
                {
                    return NotFound("No se encontraron puntos turisticos.");
                }
                return Ok(TouristSpotForResponseModel.ToModel(allTouristSpots));
            }
            catch(ExceptionBusinessLogic e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet("findByRegion")]
        public IActionResult GetTouristSpotsByRegionId([FromQuery] Guid regionId)
        {
            try
            {
                List<TouristSpot> touristSpotsInARegion = touristSpotManagement.GetTouristSpotByRegion(regionId);
                if(touristSpotsInARegion == null)
                {
                    return NotFound("No se encontraron puntos turisticos asociados a la region indicada.");
                }
                return Ok(TouristSpotForResponseModel.ToModel(touristSpotsInARegion));
            }
            catch (ExceptionBusinessLogic e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet("findByCategories")]
        public IActionResult GetTouristSpotsByCategoriesId([FromQuery] Guid [] categoriesId)
        {
            try
            {
                List<TouristSpot> touristSpotsByCategories = touristSpotManagement.GetTouristSpotsByCategories(categoriesId.ToList());
                if(touristSpotsByCategories == null)
                {
                    return NotFound("No se encontraron puntos turisticos para las categorias seleccionadas");
                }
                return Ok(TouristSpotForResponseModel.ToModel(touristSpotsByCategories));
            }
            catch (ExceptionBusinessLogic e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }


        [HttpGet("findByCategoriesAndRegion")]
        public IActionResult GetTouristSpotsByCategoriesAndRegionId([FromQuery] Guid[] categoriesId, [FromQuery] Guid regionId)
        {
            try
            {
                List<TouristSpot> touristSpotsByRegionAndCategories = touristSpotManagement.
                    GetTouristSpotsByCategoriesAndRegion(categoriesId.ToList(), regionId);
                if (touristSpotsByRegionAndCategories == null)
                {
                    return NotFound("No se encontraron puntos turisticos para las categorias y region seleccionadas");
                }
                return Ok(TouristSpotForResponseModel.ToModel(touristSpotsByRegionAndCategories));
            }
            catch (ExceptionBusinessLogic e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpPost]
        public IActionResult Post([FromBody] TouristSpotForRequestModel aTouristSpot)
        {
            try
            {
                TouristSpot touristSpotAdded = touristSpotManagement.Create(TouristSpotForRequestModel.ToEntity(aTouristSpot), aTouristSpot.RegionId, aTouristSpot.ListOfCategoriesId);
                return CreatedAtRoute("GetTouristSpot", new { id = touristSpotAdded.Id }, TouristSpotForResponseModel.ToModel(touristSpotAdded));
            }
            catch (ExceptionBusinessLogic e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}