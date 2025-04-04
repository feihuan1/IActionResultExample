using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// status Code result
// status code result sends an empty response with specified status code

// StatusCodeResult: return new StatusCodeResult(code); 500 200

// UnauthorisedResult: return new UnauthorizedResult(); 401
// BadRequestResult: return new BasRequestResult(); 400
// NotFoundResult: return NotFoundResult(); 404

namespace IActionResultExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("book")]// book?bookid=1&isloggedin=true
        public IActionResult Index() // now this can return morethan one result
        {
            // book id should be in query
            if(!Request.Query.ContainsKey("bookid"))
            {
                //Response.StatusCode = 400;
                //return Content("Book id is not supplied");
                //return new BadRequestResult();
                return BadRequest("Book id is not supplied");// return 400 optional error message
            }

            // book id should not be empty // request can be directly called
            if (string.IsNullOrEmpty(Convert.ToString(Request.Query["bookid"])))
            {
                //Response.StatusCode = 400;
                //return Content("book id can't be empty");
                //return new BadRequestResult();
                return BadRequest("book id can't be empty");// return 400 optional error message
            }

            // book id need be 1 - 1000 
            int bookId = Convert.ToInt16(ControllerContext.HttpContext.Request.Query["bookid"]);
            if(bookId is not > 0)
            {
                //Response.StatusCode = 400;
                //return Content("book id can't be less than 0");
                //return new NotFoundResult();
                return NotFound("book id can't be less than 0");// no message -> NotFoundResult, message -> NotFoundObjectResult
            }
            if (bookId is > 1000)
            {
                //Response.StatusCode = 400;
                //return Content("Book id cant be greater than 1000");
                return BadRequest("Book id cant be greater than 1000");
            }

            // isloggedin should be true
            if (Convert.ToBoolean(Request.Query["isloggedin"]) == false)
            {
                //Response.StatusCode = 401;
                //return Content("you are not logged in");
                return Unauthorized("you are not logged in");
            }

            // by default, status code is 200
            return File("/sample.pdf", "application/pdf");
        }
    }
}
