using BusinessLogicException;
using BusinessLogicInterface;
using Castle.Core.Internal;
using Domain;
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
                if (lodgingsForTouristSpotSearched.IsNullOrEmpty())
                {
                    return NotFound("No se encontraron puntos turisticos para los datos seleccionados.");
                }
                LodgingForSearchModel lodgingForSearchModel = new LodgingForSearchModel()
                {
                    CheckIn = model.CheckIn,
                    CheckOut = model.CheckOut,
                    QuantityOfGuest = new int[3] { model.QuantityOfAdult, model.QuantityOfChilds, model.QuantityOfBabies }
                };
                return Ok(lodgingForSearchModel.ToModel(lodgingsForTouristSpotSearched)); 
            }
            catch (ExceptionBusinessLogic e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}
