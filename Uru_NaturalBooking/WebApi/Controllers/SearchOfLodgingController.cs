using BusinessLogicException;
using BusinessLogicInterface;
using Castle.Core.Internal;
using Domain;
using DomainException;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.ForRequest;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/searchOfLodgings")]
    [ApiController]
    public class SearchOfLodgingController : ControllerBase
    {
        private readonly ILodgingManagement lodgingManagement;

        public SearchOfLodgingController(ILodgingManagement lodgingLogic)
        {
            lodgingManagement = lodgingLogic;
        }

        [HttpPost]
        public IActionResult Post([FromBody] SearchOfLodgingModelForRequest model)
        {
            try
            {
                List<Lodging> lodgingsForTouristSpotSearched = lodgingManagement.GetAvailableLodgingsByTouristSpot(model.TouristSpotIdSearch);
                model.VerifyFormat(); 
                LodgingForSearchModel lodgingForSearchModel = new LodgingForSearchModel()
                {
                    CheckIn = model.CheckIn,
                    CheckOut = model.CheckOut,
                    QuantityOfGuest = new int[4] { model.QuantityOfAdult, model.QuantityOfChilds, model.QuantityOfBabies, model.QuantityOfRetireds }
                };
                return Ok(lodgingForSearchModel.ToModel(lodgingsForTouristSpotSearched)); 
            }
            catch(SearchException e)
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
