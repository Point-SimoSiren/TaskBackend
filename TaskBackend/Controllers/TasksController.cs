using Microsoft.AspNetCore.Mvc;
using TaskBackend.Models;
using Task = TaskBackend.Models.Task;

namespace TaskBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {

        // Otetaan käyttöön TaskDbContext, joka on yhteys tietokantaan
        private readonly TaskDbContext db = new TaskDbContext();

        // Metodi, joka hakee kaikki tehtävät tietokannasta
        [HttpGet]
        public IActionResult Get()
        {
            var tasks = db.Tasks.ToList();
            return Ok(tasks);
        }

        // Metodi, joka luo uuden tehtävän
        [HttpPost]
        public IActionResult Post([FromBody] Task task)
        {
            db.Tasks.Add(task);
            db.SaveChanges();

            return Ok("Created new task: " + task.Title);
        }


        // Hakee pääavaimella tietyn tehtävän
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetOneById(int id)
        {
            var task = db.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);

        }
    }
}
