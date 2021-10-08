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
    public class LaundryBookingsController : ControllerBase
    {
        private readonly LIADbContext _context;

        public LaundryBookingsController(LIADbContext context)
        {
            _context = context;
        }

        // GET: api/LaundryBookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LaundryBooking>>> GetLaundryBookings()
        {
            return await _context.LaundryBookings.ToListAsync();
        }

        // GET: api/LaundryBookings/User/5
        [HttpGet("User/{userId}")]
        public async Task<ActionResult<IEnumerable<LaundryBooking>>> GetLaundryBookings(int userId)
        {
            return await _context.LaundryBookings.Where(x => x.UserId == userId).ToListAsync();
        }

        // GET: api/LaundryBookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LaundryBooking>> GetLaundryBooking(int id)
        {
            var laundryBooking = await _context.LaundryBookings.FindAsync(id);

            if (laundryBooking == null)
            {
                return NotFound();
            }

            return laundryBooking;
        }

        // PUT: api/LaundryBookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLaundryBooking(int id, LaundryBooking laundryBooking)
        {
            if (id != laundryBooking.Id)
            {
                return BadRequest();
            }

            _context.Entry(laundryBooking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LaundryBookingExists(id))
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

        // POST: api/LaundryBookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LaundryBooking>> PostLaundryBooking(LaundryBooking laundryBooking)
        {
            _context.LaundryBookings.Add(laundryBooking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLaundryBooking", new { id = laundryBooking.Id }, laundryBooking);
        }

        // DELETE: api/LaundryBookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLaundryBooking(int id)
        {
            var laundryBooking = await _context.LaundryBookings.FindAsync(id);
            if (laundryBooking == null)
            {
                return NotFound();
            }

            _context.LaundryBookings.Remove(laundryBooking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LaundryBookingExists(int id)
        {
            return _context.LaundryBookings.Any(e => e.Id == id);
        }
    }
}
