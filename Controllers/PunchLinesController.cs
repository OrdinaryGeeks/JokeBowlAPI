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
    public class PunchLinesController : ControllerBase
    {
        private readonly DBContext _context;

        public PunchLinesController(DBContext context)
        {
            _context = context;
        }

        // GET: api/PunchLines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PunchLine>>> GetPunchLine()
        {
            return await _context.PunchLine.ToListAsync();
        }

        // GET: api/PunchLines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PunchLine>> GetPunchLine(int id)
        {
            var punchLine = await _context.PunchLine.FindAsync(id);

            if (punchLine == null)
            {
                return NotFound();
            }

            return punchLine;
        }

        // PUT: api/PunchLines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPunchLine(int id, PunchLine punchLine)
        {
            if (id != punchLine.PunchLineID)
            {
                return BadRequest();
            }

            _context.Entry(punchLine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PunchLineExists(id))
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

        // POST: api/PunchLines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PunchLine>> PostPunchLine(PunchLine punchLine)
        {
            _context.PunchLine.Add(punchLine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPunchLine", new { id = punchLine.PunchLineID }, punchLine);
        }

        // DELETE: api/PunchLines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePunchLine(int id)
        {
            var punchLine = await _context.PunchLine.FindAsync(id);
            if (punchLine == null)
            {
                return NotFound();
            }

            _context.PunchLine.Remove(punchLine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PunchLineExists(int id)
        {
            return _context.PunchLine.Any(e => e.PunchLineID == id);
        }
    }
}
