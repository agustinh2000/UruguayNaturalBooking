using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicException;
using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace WebApi.Controllers
{
    [Route("api/regions")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionManagement regionManagement;

        public RegionController(IRegionManagement regionLogic)
        {
            regionManagement = regionLogic;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Region> regions = regionManagement.GetAllRegions();
                if(regions == null)
                {
                    return NotFound("No se encontraron regiones.");
                }
                return Ok(RegionModel.ToModel(regionManagement.GetAllRegions()));
            }
            catch (ExceptionBusinessLogic e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
                    
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                Region region = regionManagement.GetById(id);
                if (region == null)
                {
                    return NotFound("La region solicitada no fue encontrada");
                }
                return Ok(RegionModel.ToModel(region));
            }
            catch (ExceptionBusinessLogic e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}