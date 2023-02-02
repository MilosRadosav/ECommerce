using ECommerce.API.Errors;
using ECommerce.Core.Core.Data;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class ErrorController : BaseApiController
    {
        private readonly StoreDbContext _storeDbContext;

        public ErrorController(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }

        [HttpGet("not-found")]
        public ActionResult GetNotFoundRequest()
        {
            return NotFound(new ApiResponse(404));
        }

        [HttpGet("bad-request")]
        public ActionResult GetBadRequestRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("bad-request/{Id}")]
        public ActionResult GetBadRequestById(int id)
        {
            return Ok();
        }

        [HttpGet("server-error")]
        public ActionResult GetDerverErrorRequest()
        {
            return Ok();
        }
    }
}
