using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskBackend.Models; // <----------

namespace TaskBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestiController : ControllerBase
    {
        // Alustetaan tietokantayhteys
        private readonly TaskDbContext db = new TaskDbContext(); //<-----

        [HttpGet]
        public IActionResult Get()
        {
            //return Ok("Hello World");
            var tasks = db.Tasks.ToList(); // <-------
            return Ok(tasks); // <-------
        }

    }
}
