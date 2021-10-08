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
    public class ErrorReportsController : ControllerBase
    {
        private readonly LIADbContext _context;

        public ErrorReportsController(LIADbContext context)
        {
            _context = context;
        }

        // GET: api/ErrorReports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ErrorReport>>> GetErrorReports()
        {
            return await _context.ErrorReports.ToListAsync();
        }

        // GET: api/ErrorReports/User/5
        [HttpGet("User/{userId}")]
        public async Task<ActionResult<IEnumerable<ErrorReport>>> GetErrorReports(int userId)
        {
            return await _context.ErrorReports.Where(x => x.UserId == userId).ToListAsync();
        }

        // GET: api/ErrorReports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ErrorReport>> GetErrorReport(int id)
        {
            var errorReport = await _context.ErrorReports.FindAsync(id);

            if (errorReport == null)
            {
                return NotFound();
            }

            return errorReport;
        }

        // PUT: api/ErrorReports/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutErrorReport(int id, ErrorReport errorReport)
        {
            if (id != errorReport.Id)
            {
                return BadRequest();
            }

            _context.Entry(errorReport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ErrorReportExists(id))
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

        // POST: api/ErrorReports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ErrorReport>> PostErrorReport(ErrorReport errorReport)
        {
            _context.ErrorReports.Add(errorReport);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetErrorReport", new { id = errorReport.Id }, errorReport);
        }

        // DELETE: api/ErrorReports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteErrorReport(int id)
        {
            var errorReport = await _context.ErrorReports.FindAsync(id);
            if (errorReport == null)
            {
                return NotFound();
            }

            _context.ErrorReports.Remove(errorReport);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ErrorReportExists(int id)
        {
            return _context.ErrorReports.Any(e => e.Id == id);
        }
    }
}
