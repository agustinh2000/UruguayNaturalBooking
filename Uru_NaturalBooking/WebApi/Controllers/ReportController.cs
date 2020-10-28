using System;
using System.Collections.Generic;
using BusinessLogicException;
using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.ForResponse;

namespace WebApi.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ILodgingManagement lodgingManagement;

        public ReportController(ILodgingManagement lodgingLogic)
        {
            lodgingManagement = lodgingLogic;
        }

        [HttpGet("generateReport")]
        public IActionResult Get([FromQuery] Guid idOfTouristSpot, [FromQuery] DateTime checkInMax, [FromQuery] DateTime checkOutMax)
        {
            try
            {
                List<Lodging> allLodgingsWithReserveAndQuantity = lodgingManagement.
                    GetLodgingsWithReservesBetweenDates(idOfTouristSpot, checkInMax, checkOutMax);
                ReportLodgingModelForResponse reportModelToGenerateReport = new ReportLodgingModelForResponse(); 
                return Ok(reportModelToGenerateReport.SetModel(allLodgingsWithReserveAndQuantity, checkInMax, checkOutMax));
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
