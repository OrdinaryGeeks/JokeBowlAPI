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
    public class JokeUpVotesController : ControllerBase
    {
        private readonly DBContext _context;

        public JokeUpVotesController(DBContext context)
        {
            _context = context;
        }

        // GET: api/JokeUpVotes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JokeUpVote>>> GetJokeUpVote()
        {
            return await _context.JokeUpVote.ToListAsync();
        }

        // GET: api/JokeUpVotes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JokeUpVote>> GetJokeUpVote(int id)
        {
            var jokeUpVote = await _context.JokeUpVote.FindAsync(id);

            if (jokeUpVote == null)
            {
                return NotFound();
            }

            return jokeUpVote;
        }

        [HttpGet()]
        [Route("findUpVotes")]
        public async Task<ActionResult<JokeUpVote>> GetJokeUpVote( int jokeID, [FromQuery] string userName)
        {
           

            var jokeUpVote =  await _context.JokeUpVote.FirstOrDefaultAsync(p => p.UserName == userName && p.JokeID == jokeID);

            if (jokeUpVote == null)
            {
                return NotFound();
            }

            return jokeUpVote;
        }

        [HttpPost]
        [Route("switch/up/switchVote/")]
        public async Task<ActionResult<Joke>> SwitchUpVote([FromBody]SwitchVote switchVote)
        {
            JokeUpVote? jokeUpVote = await _context.JokeUpVote.FindAsync(switchVote.jokeVoteId);
            Joke? joke = null;
            if (jokeUpVote != null)
            {



                joke = _context.Joke.Find(jokeUpVote.JokeID);
                if (joke != null)
                {
                    joke.Score--;
                    if (switchVote.voteResult == "Bad")
                    {
                        joke.Score--;

                    }
                    _context.JokeUpVote.Remove(jokeUpVote);

                    _context.Entry(joke).State = EntityState.Modified;
                }
            }
            await _context.SaveChangesAsync();
            if (joke != null)
                return CreatedAtAction("SwitchUpVote", joke);

            else
                return NotFound();

        }


        // PUT: api/JokeUpVotes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJokeUpVote(int id, JokeUpVote jokeUpVote)
        {
            if (id != jokeUpVote.JokeUpVoteID)
            {
                return BadRequest();
            }

            _context.Entry(jokeUpVote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JokeUpVoteExists(id))
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

        // POST: api/JokeUpVotes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Joke>> PostJokeUpVote(JokeUpVote jokeUpVote)
        {
            _context.JokeUpVote.Add(jokeUpVote);
            Joke? joke = _context.Joke.Find(jokeUpVote.JokeID);
            if (joke != null)
            {
                joke.Score++;

                _context.Entry(joke).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();


            return CreatedAtAction("GetJokeUpVote", new { id = jokeUpVote.JokeUpVoteID }, joke);
        }

        // DELETE: api/JokeUpVotes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJokeUpVote(int id)
        {
            var jokeUpVote = await _context.JokeUpVote.FindAsync(id);
            if (jokeUpVote == null)
            {
                return NotFound();
            }

            _context.JokeUpVote.Remove(jokeUpVote);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JokeUpVoteExists(int id)
        {
            return _context.JokeUpVote.Any(e => e.JokeUpVoteID == id);
        }
    }
}
