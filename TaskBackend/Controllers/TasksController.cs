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


        // Tehtävän poistaminen id:llä
        // polku: "https://jotain.com/api/tasks/7"
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var task = db.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }
            db.Tasks.Remove(task);
            db.SaveChanges();
            return Ok("Deleted task with id: " + id);
        }


        // Metodi , joka hakee tehtävät tietyn statuksen mukaan
        // polku: "https://jotain.com/api/tasks/status/2"
        [HttpGet]
        [Route("status/{status}")]
        public IActionResult GetByStatus(int status)
        {
            // hyväksytään vain status 1, 2 tai 3
            // muuten palautetaan BadRequest
            if (status < 1 || status > 3)
            {
                return BadRequest("Status must be 1, 2 or 3");
            }

            var tasks = db.Tasks.Where(t => t.Status == status);
            return Ok(tasks);
        }


        // Hae tehtäviä titlen mukaan hakusanalla esim. "kirjaut"
        [HttpGet]
        [Route("title/{search}")]
        public IActionResult GetByTitle(string search)
        {
            var tasks = db.Tasks.Where(t => t.Title.Contains(search));
            //var tasks = db.Tasks.Where(t => t.Title == search); <-- perfect match.
       
            return Ok(tasks);
        }


        // Perjantaiksi 27.3.
        // Tähän voisi tehdä muokkausmetodeja PUT tai PATCH.
        // Esim että voi muuttaa tehtävän statusta tai muita tietoja.

    }
}
