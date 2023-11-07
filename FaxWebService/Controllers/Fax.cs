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
            TextFileReader.SetQuote(fax);
            return Ok("Fax added.");
        }

        [HttpPost("/removeFax")]
        public IActionResult RemoveFax([FromBody] string fax)
        {
            TextFileReader.RemoveQuote(fax);
            return Ok("Fax removed.");
        }
    }
}
