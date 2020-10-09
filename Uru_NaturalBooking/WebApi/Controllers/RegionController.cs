using System;
using System.Collections.Generic;
using BusinessLogicException;
using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.ForRequest;
using Model.ForResponse;

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
                List<Region> allRegions = regionManagement.GetAllRegions();
                return Ok(RegionForResponseModel.ToModel(allRegions));
            }
            catch (ClientBusinessLogicException)
            {
                return NotFound("No se encontraron regiones.");
            }
            catch (ServerBusinessLogicException e)
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
                return Ok(RegionForResponseModel.ToModel(region));
            }
            catch (ClientBusinessLogicException)
            {
                return NotFound("La region solicitada no fue encontrada");
            }
            catch (ServerBusinessLogicException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}