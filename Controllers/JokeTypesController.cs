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
    public class JokeTypesController : ControllerBase
    {
        private readonly DBContext _context;

        public JokeTypesController(DBContext context)
        {
            _context = context;
        }

        // GET: api/JokeTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JokeType>>> GetJokeType()
        {
            return await _context.JokeType.ToListAsync();
        }

        // GET: api/JokeTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JokeType>> GetJokeType(int id)
        {
            var jokeType = await _context.JokeType.FindAsync(id);

            if (jokeType == null)
            {
                return NotFound();
            }

            return jokeType;
        }

        // PUT: api/JokeTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJokeType(int id, JokeType jokeType)
        {
            if (id != jokeType.JokeTypeID)
            {
                return BadRequest();
            }

            _context.Entry(jokeType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JokeTypeExists(id))
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

        // POST: api/JokeTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JokeType>> PostJokeType(JokeType jokeType)
        {
            _context.JokeType.Add(jokeType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJokeType", new { id = jokeType.JokeTypeID }, jokeType);
        }

        // DELETE: api/JokeTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJokeType(int id)
        {
            var jokeType = await _context.JokeType.FindAsync(id);
            if (jokeType == null)
            {
                return NotFound();
            }

            _context.JokeType.Remove(jokeType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JokeTypeExists(int id)
        {
            return _context.JokeType.Any(e => e.JokeTypeID == id);
        }
    }
}
