using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController:BaseApiController
    {
        private readonly DataContext _context;

        public BuggyController(DataContext context)
        {
           _context = context;
        }

        //To test our 404 not authorized responses
        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "secret text";
        }

        [HttpGet("not-found")]
        public ActionResult<string> GetNotFound()
        {
            var item = _context.Users.Find(-1);

            if(item == null) return NotFound();

            return Ok(item);
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
           var item = _context.Users.Find(-1);

            var errorMessage = item.ToString();
            
            return errorMessage;
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("This was not a good request");
        }

    }
}
