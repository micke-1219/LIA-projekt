using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BulletinMessagesController : ControllerBase
    {
        private readonly LIADbContext _context;

        public BulletinMessagesController(LIADbContext context)
        {
            _context = context;
        }

        // GET: api/BulletinMessages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BulletinMessage>>> GetBulletinMessages()
        {
            return await _context.BulletinMessages.ToListAsync();
        }

        // GET: api/BulletinMessages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BulletinMessage>> GetBulletinMessage(int id)
        {
            var bulletinMessage = await _context.BulletinMessages.FindAsync(id);

            if (bulletinMessage == null)
            {
                return NotFound();
            }

            return bulletinMessage;
        }

        // PUT: api/BulletinMessages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBulletinMessage(int id, BulletinMessage bulletinMessage)
        {
            if (id != bulletinMessage.Id)
            {
                return BadRequest();
            }

            _context.Entry(bulletinMessage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BulletinMessageExists(id))
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

        // POST: api/BulletinMessages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BulletinMessage>> PostBulletinMessage(BulletinMessage bulletinMessage)
        {
            _context.BulletinMessages.Add(bulletinMessage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBulletinMessage", new { id = bulletinMessage.Id }, bulletinMessage);
        }

        // DELETE: api/BulletinMessages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBulletinMessage(int id)
        {
            var bulletinMessage = await _context.BulletinMessages.FindAsync(id);
            if (bulletinMessage == null)
            {
                return NotFound();
            }

            _context.BulletinMessages.Remove(bulletinMessage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BulletinMessageExists(int id)
        {
            return _context.BulletinMessages.Any(e => e.Id == id);
        }
    }
}
