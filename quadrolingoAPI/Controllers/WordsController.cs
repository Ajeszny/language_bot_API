using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quadrolingoAPI.Models;
using System.Text.Json;
using NuGet.Versioning;

namespace quadrolingoAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WordsController : ControllerBase
    {
        private readonly APIContext _context;

        public WordsController(APIContext context)
        {
            _context = context;
        }

        // GET: api/Words
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Word>>> GetWords()
        {
            return await _context.Words.ToListAsync();
        }

        // GET: api/Words/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Word>> GetWord(int id)
        {
            var word = await _context.Words.FindAsync(id);

            if (word == null)
            {
                return NotFound();
            }

            return word;
        }

        // POST: api/Words
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Word>> PostWord(Word word)
        {
            //_context.Words.Add(word);
            //await _context.SaveChangesAsync();

            var translations = JsonSerializer.Deserialize<Dictionary<string, string[]>>(word.WORD_TRANSLATION);

            foreach (var item in translations)
            {
                var lang = await _context.Languages.FindAsync(item.Key);
                var words = await _context.Words.ToListAsync();
                if (lang == null)
                {
                    //This should never happen
                    return BadRequest();
                }
                foreach (var translation in item.Value)
                {
                   
                }
            }

            return CreatedAtAction(nameof(GetWord), new { id = word.Id }, word);
        }

        // DELETE: api/Words/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWord(int id)
        {
            var word = await _context.Words.FindAsync(id);
            if (word == null)
            {
                return NotFound();
            }

            _context.Words.Remove(word);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WordExists(int id)
        {
            return _context.Words.Any(e => e.Id == id);
        }
    }
}
