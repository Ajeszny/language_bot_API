using System;
using System.Collections.Generic;
using System.Linq;
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
    public class LanguagesController : ControllerBase
    {
        private readonly APIContext _context;

        public LanguagesController(APIContext context)
        {
            _context = context;
        }

        // GET: api/Languages
        [HttpGet]
        [EnableCors]
        public async Task<ActionResult<IEnumerable<Language>>> GetLanguages()
        {
            return await _context.Languages.ToListAsync();
        }

        // GET: api/Languages/5
        [HttpGet("{id}")]
        [EnableCors]
        public async Task<ActionResult<Language>> GetLanguage(string id)
        {
            var language = await _context.Languages.FindAsync(id);

            if (language == null)
            {
                return NotFound();
            }

            return language;
        }


        // POST: api/Languages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [EnableCors]
        public async Task<ActionResult<Language>> PostLanguage(Language language)
        {
            _context.Languages.Add(language);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Conflict();
            }

            return CreatedAtAction(nameof(GetLanguage), new { id = language.LANG_CODE }, language);
        }
    }
}
