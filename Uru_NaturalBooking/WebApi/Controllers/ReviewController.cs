using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicException;
using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.ForRequest;
using Model.ForResponse;

namespace WebApi.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewManagement reviewManagement;

        public ReviewController(IReviewManagement reviewLogic)
        {
            reviewManagement = reviewLogic;
        }

        [HttpGet("{id}", Name = "getReview")]
        public IActionResult Get(Guid id)
        {
            try
            {
                Review review = reviewManagement.GetById(id);
                return Ok(ReviewModelForResponse.ToModel(review));
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

        [HttpPost]
        public IActionResult Post([FromBody] ReviewModelForRequest aReviewModel)
        {
            try
            {
                Review review = reviewManagement.Create(ReviewModelForRequest.ToEntity(aReviewModel), aReviewModel.IdOfReserveAssociated);
                return CreatedAtRoute("getReview", new { id = review.Id }, ReviewModelForResponse.ToModel(review));
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