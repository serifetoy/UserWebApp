using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UserWebApp.Data;
using UserWebApp.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UserWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MyController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://dummyjson.com/users");
                var content = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<User>>(content);

                _context.Users.AddRange(users);
                await _context.SaveChangesAsync();

                return Ok(users);
            }
        }

        
        
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return _context.Users.ToList();
        }

        
        [HttpGet("{id}")]
        public ActionResult<User> GetProduct(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        
        [HttpPost]
        public ActionResult<User> PostProduct(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetProduct), new { id = user.Id }, user);
        }

        
        [HttpPut("{id}")]
        public IActionResult PutProduct(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public ActionResult<User> DeleteProduct(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return user;
        }
    }
}
