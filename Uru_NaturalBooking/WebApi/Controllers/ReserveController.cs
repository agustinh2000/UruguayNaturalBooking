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
using Model.ForRequest;
using Model.ForResponse;

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

        [HttpGet("{id}", Name = "GetReserve")]
        public IActionResult Get(Guid id)
        {
            try
            {
                Reserve reserve = reserveManagement.GetById(id);
                return Ok(ReserveModelForResponse.ToModel(reserve));
            }
            catch (ClientBusinessLogicException)
            {
                return NotFound("La reserva solicitada no fue encontrado");
            }
            catch (ServerBusinessLogicException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] ReserveModelForRequest aReserveModel)
        {
            try
            {
                Reserve reserve = reserveManagement.Create(ReserveModelForRequest.ToEntity(aReserveModel), aReserveModel.IdOfLodgingToReserve);
                return CreatedAtRoute("GetReserve", new { id = reserve.Id }, ReserveModelForResponse.ToModel(reserve));
            }
            catch(DomainBusinessLogicException e)
            {
                return BadRequest(e.Message);
            }
            catch(ClientBusinessLogicException e)
            {
                return NotFound(e.Message); 
            }
            catch (ServerBusinessLogicException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpPut("{id}")]
        public IActionResult Put(Guid idForUpdateReserve, ReserveModelForRequestUpdate aReserveModelForUpdate)
        {
            try
            {
                Reserve reserve = reserveManagement.Update(idForUpdateReserve, ReserveModelForRequestUpdate.ToEntity(aReserveModelForUpdate));
                return CreatedAtRoute("GetReserve", new { id = reserve.Id }, ReserveModelForResponse.ToModel(reserve));
            }
            catch (ServerBusinessLogicException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
