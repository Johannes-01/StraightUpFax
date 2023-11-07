using Microsoft.AspNetCore.Mvc;
using StraightUpFax;

namespace FaxWebService.Controllers
{
    public class Fax : Controller
    {
        private Worker worker;
        
        public Fax() {
            worker = new Worker();    
        }

        [HttpGet("/getAllFax")]
        public IActionResult GetAllFax()
        {
            return Ok(worker.Quotes);
        }

        [HttpPost("/addFax")]
        public IActionResult AddFax([FromBody] string fax)
        {
            worker.Quotes.Add(fax);
            return Ok("Fax added.");
        }

        [HttpPost("/removeFax")]
        public IActionResult RemoveFax([FromBody] string fax)
        {
            worker.Quotes.Remove(fax);
            return Ok("Fax removed.");
        }
    }
}
