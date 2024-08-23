using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JokeAIAPI.Models;

namespace JokeAIAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JokesController : ControllerBase
    {
        private readonly DBContext _context;

        public JokesController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Jokes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Joke>>> GetJoke()
        {
            return await _context.Joke.ToListAsync();
        }

        // GET: api/Jokes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Joke>> GetJoke(int id)
        {
            var joke = await _context.Joke.FindAsync(id);

            if (joke == null)
            {
                return NotFound();
            }

            return joke;
        }

        // PUT: api/Jokes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJoke(int id, Joke joke)
        {
            if (id != joke.JokeID)
            {
                return BadRequest();
            }

            _context.Entry(joke).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JokeExists(id))
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

        [HttpGet]
        [Route("similarJoke")]
        public async Task<ActionResult<Joke>> SimilarJoke([FromQuery] int jokeId)
        {

            //Switch subject for another subject

            Joke? oldJoke = await _context.Joke.FindAsync(jokeId);
            if (oldJoke != null)
            {
                Joke? tempJoke = await _context.Joke.FirstAsync(compareJoke => compareJoke.Subject != oldJoke.Subject && (compareJoke.Setup != compareJoke.Setup || compareJoke.PunchLine != oldJoke.PunchLine));
                
                if (tempJoke != null)
                {
                    Joke switchJoke2 = new()
                    {
                        Text = oldJoke.Subject + " " + tempJoke.Setup + " " + tempJoke.PunchLine,
                        Subject = oldJoke.Subject,
                        Setup = tempJoke.Setup,
                        PunchLine = tempJoke.PunchLine,
                        Category = tempJoke.Category,
                        JokeName = oldJoke.JokeName + " TEMP ",
                        JokeType = oldJoke.JokeType,
                        UserName = oldJoke.UserName,
                        Score = 0
                    };

                    return switchJoke2;
                }
            }

            return NotFound();



        }

        // POST: api/Jokes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Joke>> PostJoke(Joke joke)
        {
            _context.Joke.Add(joke);
            await _context.SaveChangesAsync();

            if (joke != null)
            {
                Subject newSubject = new()
                {
                    Text = joke.Subject,
                    JokeID = joke.JokeID
                };
                PunchLine newPunchLine = new()
                {
                    Text = joke.PunchLine,
                    JokeID = joke.JokeID
                };
                Setup newSetup = new()
                {
                    Text = joke.Setup,
                    JokeID = joke.JokeID
                };

               
               

                if (!(_context.Category.Where(p => p.CategoryName == joke.Category).Count() == 0))
                {
                    Category newCategory = new() { CategoryName = joke.Category };
                    _context.Category.Add(newCategory);
                }
                if (!(_context.JokeType.Where(p => p.JokeTypeName == joke.JokeType).Count() == 0))
                {
                    JokeType newJokeType = new() { JokeTypeName = joke.JokeType };
                    _context.JokeType.Add(newJokeType);
                }


                _context.Subject.Add(newSubject);
                _context.PunchLine.Add(newPunchLine);
                _context.Setup.Add(newSetup);
                await _context.SaveChangesAsync();





            }

            return CreatedAtAction("GetJoke", new { id = joke.JokeID }, joke);
        }

        // DELETE: api/Jokes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJoke(int id)
        {
            var joke = await _context.Joke.FindAsync(id);
            if (joke == null)
            {
                return NotFound();
            }

            _context.Joke.Remove(joke);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JokeExists(int id)
        {
            return _context.Joke.Any(e => e.JokeID == id);
        }
    }
}
