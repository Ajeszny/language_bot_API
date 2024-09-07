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
using NuGet.Protocol;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.AspNetCore.Cors;

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
        public async Task<ActionResult<IEnumerable<Word>>> GetWords(string? base_lang, string? order, string? contains, string? startswith)
        {
            var result = from word in _context.Words
                         select word;
            if (!result.Any())
            {
                return await _context.Words.ToListAsync();
            }
            if (base_lang != null)
            {
                result = from word in result
                         where word.WORD_LANG == base_lang
                         select word;
            }
            if (order != null)
            {
                if (order == "desc")
                {
                    result = from word in result
                             orderby word descending
                             select word;
                }
                if (order == "asc")
                {
                    result = from word in result
                             orderby word ascending
                             select word;
                }
            }
            if (contains != null)
            {
                result = from word in result
                         where word.WORD_BASE.Contains(contains)
                         select word;
            }
            if (startswith != null)
            {
                result = from word in result
                         where word.WORD_BASE.StartsWith(startswith)
                         select word;
            }
            return await result.ToListAsync();
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

        [HttpPut("{id}")]
        public async Task<IActionResult> PutWord(int id, Word word)
        {
            if (id != word.Id)
            {
                return BadRequest();
            }

            _context.Entry(word).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/Words
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Word>> PostWord(Word word)
        {
            var check = from w in _context.Words
                        where w.WORD_BASE == word.WORD_BASE
                        select w;
            if (check.Any())
            {
                return NoContent();
            }
            _context.Words.Add(word);

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
                    //_context.Languages.Add(lang);
                    var translated = from w in _context.Words
                                     where w.WORD_BASE == translation
                                     select w;
                    if (translated.Any())
                    {
                        var w = translated.FirstOrDefault();
                        var trs = JsonSerializer.Deserialize<Dictionary<string, string[]>>(w.WORD_TRANSLATION);
                        if (trs.ContainsKey(word.WORD_LANG))
                        {
                            
                           if (!trs[word.WORD_LANG].Contains(word.WORD_BASE)) trs[word.WORD_LANG].Append(word.WORD_BASE);
                        } else
                        {
                            trs.Add(word.WORD_LANG, [word.WORD_BASE]);
                        }
                        w.WORD_TRANSLATION = trs.ToJson();

                    }
                    else
                    {
                        Word s = new Word();
                        s.WORD_LANG = item.Key;
                        s.WORD_TRANSLATION = "{\"" + word.WORD_LANG + "\": [\"" + word.WORD_BASE + "\"" + "]" + "}";
                        s.WORD_BASE = translation;
                        await _context.Words.AddAsync(s);
                    }
                }
            }

            await _context.SaveChangesAsync();
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
