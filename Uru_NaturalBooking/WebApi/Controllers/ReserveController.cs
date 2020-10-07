using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicException;
using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Model.ForRequest;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/reserves")]
    [ApiController]
    public class ReserveController : ControllerBase
    {
        private readonly IReserveManagement reserveManagement;

        public ReserveController(IReserveManagement reserveLogic)
        {
            reserveManagement = reserveLogic;
        }

        [HttpPost]
        public IActionResult Post([FromBody] ReserveModelForRequest aReserveModel)
        {
            try
            {
                Reserve reserve = reserveManagement.Create(ReserveModelForRequest.ToEntity(aReserveModel), aReserveModel.IdOfLodgingToReserve);
                return CreatedAtRoute("GetReserve", new { id = reserve.Id }, reserve);
            }
            catch(ExceptionBusinessLogic e)
            {
                return BadRequest(e.Message); 
            }
        }
    }
}
