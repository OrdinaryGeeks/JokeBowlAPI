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
    public class JokeDownVotesController : ControllerBase
    {
        private readonly DBContext _context;

        public JokeDownVotesController(DBContext context)
        {
            _context = context;
        }

        // GET: api/JokeDownVotes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JokeDownVote>>> GetJokeDownVote()
        {
            return await _context.JokeDownVote.ToListAsync();
        }

        // GET: api/JokeDownVotes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JokeDownVote>> GetJokeDownVote(int id)
        {
            var jokeDownVote = await _context.JokeDownVote.FindAsync(id);

            if (jokeDownVote == null)
            {
                return NotFound();
            }

            return jokeDownVote;
        }

        // PUT: api/JokeDownVotes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJokeDownVote(int id, JokeDownVote jokeDownVote)
        {
            if (id != jokeDownVote.JokeDownVoteID)
            {
                return BadRequest();
            }

            _context.Entry(jokeDownVote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JokeDownVoteExists(id))
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

        // POST: api/JokeDownVotes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Joke>> PostJokeDownVote(JokeDownVote jokeDownVote)
        {
            _context.JokeDownVote.Add(jokeDownVote);
            Joke? joke = _context.Joke.Find(jokeDownVote.JokeID);
            if (joke != null)
            {
                joke.Score--;

                _context.Entry(joke).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJokeDownVote", new { id = jokeDownVote.JokeDownVoteID }, joke);
        }


        [HttpPost]
        [Route("switch/down/switchVote/")]
        public async Task<ActionResult<Joke>> SwitchDownVote([FromBody] SwitchVote switchVote)
        {
            JokeDownVote? jokeDownVote = await _context.JokeDownVote.FindAsync(switchVote.jokeVoteId);
            Joke? joke = null;
            if (jokeDownVote != null)
            {



                joke = _context.Joke.Find(jokeDownVote.JokeID);
                if (joke != null)
                {
                    joke.Score--;
                    if (switchVote.voteResult == "Bad")
                    {
                        joke.Score--;

                    }
                    _context.JokeDownVote.Remove(jokeDownVote);

                    _context.Entry(joke).State = EntityState.Modified;
                }
            }
            await _context.SaveChangesAsync();
            if (joke != null)
                return CreatedAtAction("SwitchDownVote", joke);

            else
                return NotFound();

        }



        [HttpGet()]
        [Route("findDownVotes")]
        public async Task<ActionResult<JokeDownVote>> GetJokeDownVote(int jokeID, [FromQuery] string userName)
        {


            var jokeDownVote = await _context.JokeDownVote.FirstOrDefaultAsync(p => p.UserName == userName && p.JokeID == jokeID);

            if (jokeDownVote == null)
            {
                return NotFound();
            }

            return jokeDownVote;
        }

      


        // DELETE: api/JokeDownVotes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJokeDownVote(int id)
        {
            var jokeDownVote = await _context.JokeDownVote.FindAsync(id);
            if (jokeDownVote == null)
            {
                return NotFound();
            }

            _context.JokeDownVote.Remove(jokeDownVote);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JokeDownVoteExists(int id)
        {
            return _context.JokeDownVote.Any(e => e.JokeDownVoteID == id);
        }
    }
}
