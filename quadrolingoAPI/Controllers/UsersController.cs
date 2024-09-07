using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quadrolingoAPI.Models;

namespace quadrolingoAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly APIContext _context;

        public UsersController(APIContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers(string? name)
        {
            var result = await _context.Users.ToListAsync();
            if (name != null)
            {
                result = (from user in result
                         where user.USERNAME.Contains(name)
                         select user).ToList();
            }
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}/profile")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet("{id}/known_words")]

        //untested
        public async Task<ActionResult<Dictionary<string, string>>> GetKnownWords(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var results = from uw in _context.UserWords
                          where uw.USER_ID.Id == id
                          select uw.WORD_ID;

            var list = await results.ToListAsync();
            Dictionary<string, string> m = new Dictionary<string, string>();
            
            foreach (var item in list)
            {
                var tr = JsonSerializer.Deserialize<Dictionary<string, string[]>>(item.WORD_TRANSLATION);
                m.Add(item.WORD_BASE, tr[user.STUDY_LANG.LANG_CODE][0]);
            }

            return m;
        }

        [HttpGet("{id}/progress")]

        //untested
        public async Task<ActionResult<string[]>> GetProgress(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var exercises = await _context.Exercises.ToListAsync();
            string[] results = [];
            foreach (var e in exercises)
            {
                results.Append("");
                results[results.Length - 1] += e.TIMESTAMP + " Всего слов:";
                int total, guessed;
                var words = from we in _context.WordExercises
                            where we.EXERCISE_ID == e
                            select we;
                total = words.Count();
                words = from we in _context.WordExercises
                        where we.EXERCISE_ID == e && we.Guessed
                        select we;
                guessed = words.Count();
                results[results.Length - 1] += total + " Угадал:" + guessed;
            }

            return results;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/profile")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("profile")]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            //Language BASE = await _context.Languages.FindAsync(user.BASE_LANG.LANG_CODE);
            //Language STUDY = await _context.Languages.FindAsync(user.STUDY_LANG.LANG_CODE);
            //user.BASE_LANG = BASE;
            //user.STUDY_LANG = STUDY;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
