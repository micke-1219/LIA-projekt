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
    public class ParkingContractsController : ControllerBase
    {
        private readonly LIADbContext _context;

        public ParkingContractsController(LIADbContext context)
        {
            _context = context;
        }

        // GET: api/ParkingContracts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingContract>>> GetParkingContracts()
        {
            return await _context.ParkingContracts.ToListAsync();
        }

        // GET: api/ApartmentContracts/User/5
        [HttpGet("User/{userId}")]
        public async Task<ActionResult<IEnumerable<ParkingContract>>> GetParkingContracts(int userId)
        {
            return await _context.ParkingContracts.Where(x => x.UserId == userId).ToListAsync();
        }

        // GET: api/ParkingContracts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingContract>> GetParkingContract(int id)
        {
            var parkingContract = await _context.ParkingContracts.FindAsync(id);

            if (parkingContract == null)
            {
                return NotFound();
            }

            return parkingContract;
        }

        // PUT: api/ParkingContracts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParkingContract(int id, ParkingContract parkingContract)
        {
            if (id != parkingContract.Id)
            {
                return BadRequest();
            }

            _context.Entry(parkingContract).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkingContractExists(id))
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

        // POST: api/ParkingContracts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ParkingContract>> PostParkingContract(ParkingContract parkingContract)
        {
            _context.ParkingContracts.Add(parkingContract);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParkingContract", new { id = parkingContract.Id }, parkingContract);
        }

        // DELETE: api/ParkingContracts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParkingContract(int id)
        {
            var parkingContract = await _context.ParkingContracts.FindAsync(id);
            if (parkingContract == null)
            {
                return NotFound();
            }

            _context.ParkingContracts.Remove(parkingContract);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParkingContractExists(int id)
        {
            return _context.ParkingContracts.Any(e => e.Id == id);
        }
    }
}
