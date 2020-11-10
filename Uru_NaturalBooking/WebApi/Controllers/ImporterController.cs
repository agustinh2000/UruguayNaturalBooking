using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicInterface;
using Castle.Core.Internal;
using Domain;
using Importation;
using ImporterException;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.ForRequest;

namespace WebApi.Controllers
{
    [Route("api/imports")]
    [ApiController]
    public class ImporterController : ControllerBase
    {
        private readonly ILodgingManagementForImportation lodgingManagement;
        private readonly ReflectionLogic reflectionLogic;

        public ImporterController(ILodgingManagementForImportation lodgingLogic, ReflectionLogic logicOfReflection)
        {
            lodgingManagement = lodgingLogic;
            reflectionLogic = logicOfReflection;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<IImport> listOfImportersAvailables = reflectionLogic.GetAvailableImporters().ToList();
            if (listOfImportersAvailables.IsNullOrEmpty())
            {
                return NotFound("No hay importadores disponibles actualmente");
            }
            else
            {
                return Ok(listOfImportersAvailables.ConvertAll(i => i.GetName()));
            }
        }

        [HttpGet("getTheParameterOfDllSelected")]
        public IActionResult GetParameters([FromQuery] string pathOfDll)
        {
            try
            {
                List<Parameter> listOfParametersRequired = reflectionLogic.GetTheParametersRequired(pathOfDll);
                return Ok(listOfParametersRequired);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] List<Parameter> informationForTheImporters, [FromQuery] string pathOfDll)
        {
            List<LodgingModelForImport> lodgingsNotCreated = new List<LodgingModelForImport>(); 
            try
            {
                List<LodgingModelForImport> lodgingsImported = reflectionLogic.ImportLodgings(pathOfDll, informationForTheImporters);
                foreach (LodgingModelForImport lodging in lodgingsImported)
                {
                    try
                    {
                        lodgingManagement.Create(lodging.ToEntity(), lodging.TouristSpot.ToEntity(), lodging.Images);
                    }catch(Exception)
                    {
                        lodgingsNotCreated.Add(lodging); 
                    }
                }

                if (lodgingsNotCreated.IsNullOrEmpty() && !lodgingsImported.IsNullOrEmpty())
                {
                    return Ok("Todos los hospedajes fueron agregados al sistema.");
                }
                else if (lodgingsImported.IsNullOrEmpty())
                {
                    return BadRequest("No se importaron hospedajes.");
                }
                else
                {
                    return BadRequest(lodgingsNotCreated);
                }
            }
            catch (ImportationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
