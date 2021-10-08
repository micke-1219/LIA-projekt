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
    public class ApartmentContractsController : ControllerBase
    {
        private readonly LIADbContext _context;

        public ApartmentContractsController(LIADbContext context)
        {
            _context = context;
        }

        // GET: api/ApartmentContracts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApartmentContract>>> GetApartmentContracts()
        {
            return await _context.ApartmentContracts.ToListAsync();
        }

        // GET: api/ApartmentContracts/User/5
        [HttpGet("User/{userId}")]
        public async Task<ActionResult<IEnumerable<ApartmentContract>>> GetApartmentContracts(int userId)
        {
            return await _context.ApartmentContracts.Where(x => x.UserId == userId).ToListAsync();
        }

        // GET: api/ApartmentContracts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApartmentContract>> GetApartmentContract(int id)
        {
            var apartmentContract = await _context.ApartmentContracts.FindAsync(id);

            if (apartmentContract == null)
            {
                return NotFound();
            }

            return apartmentContract;
        }

        // PUT: api/ApartmentContracts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApartmentContract(int id, ApartmentContract apartmentContract)
        {
            if (id != apartmentContract.Id)
            {
                return BadRequest();
            }

            _context.Entry(apartmentContract).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApartmentContractExists(id))
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

        // POST: api/ApartmentContracts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApartmentContract>> PostApartmentContract(ApartmentContract apartmentContract)
        {
            _context.ApartmentContracts.Add(apartmentContract);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApartmentContract", new { id = apartmentContract.Id }, apartmentContract);
        }

        // DELETE: api/ApartmentContracts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApartmentContract(int id)
        {
            var apartmentContract = await _context.ApartmentContracts.FindAsync(id);
            if (apartmentContract == null)
            {
                return NotFound();
            }

            _context.ApartmentContracts.Remove(apartmentContract);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApartmentContractExists(int id)
        {
            return _context.ApartmentContracts.Any(e => e.Id == id);
        }
    }
}
