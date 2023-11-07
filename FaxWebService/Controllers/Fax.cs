using Microsoft.AspNetCore.Mvc;

namespace FaxWebService.Controllers
{
    public class Fax : Controller
    {
        [HttpGet("/getAllFax")]
        public IActionResult GetAllFax()
        {
            return Ok(Worker.Quotes);
        }

        [HttpPost("/addFax")]
        public IActionResult AddFax([FromBody] string fax)
        {
            Worker.Quotes.Add(fax);
            return Ok("Fax added.");
        }

        [HttpPost("/removeFax")]
        public IActionResult RemoveFax([FromBody] string fax)
        {
            Worker.Quotes.Remove(fax);
            return Ok("Fax removed.");
        }
    }
}
