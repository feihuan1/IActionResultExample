using Microsoft.AspNetCore.Mvc;

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
                Response.StatusCode = 400;
                return Content("Book id is not supplied");
            }

            // book id should not be empty // request can be directly called
            if (string.IsNullOrEmpty(Convert.ToString(Request.Query["bookid"])))
            {
                Response.StatusCode = 400;
                return Content("book id can't be empty");
            }

            // book id need be 1 - 1000 
            int bookId = Convert.ToInt16(ControllerContext.HttpContext.Request.Query["bookid"]);
            if(bookId is not > 0)
            {
                Response.StatusCode = 400;
                return Content("book id can't be less than 0");
            }
            if (bookId is > 1000)
            {
                Response.StatusCode = 400;
                return Content("Book id cant be greater than 1000");
            }

            // isloggedin should be true
            if (Convert.ToBoolean(Request.Query["isloggedin"]) == false)
            {
                Response.StatusCode = 401;
                return Content("you are not logged in");
            }

            // by default, status code is 200
            return File("/sample.pdf", "application/pdf");
        }
    }
}
