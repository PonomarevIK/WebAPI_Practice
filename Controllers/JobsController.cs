using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HR_API.Models;

namespace HR_API.Controllers;

[Route("api/jobs")]
// [ApiController]
public class JobsController : ControllerBase
{
    private readonly IHrDatabaseContext _context;

    public JobsController(IHrDatabaseContext context)
    {
        _context = context;
    }

    // GET: api/Jobs
    [HttpGet]
    public Task<List<Job>> GetJobs()
    {
        return _context.Jobs.ToListAsync();
    }

    // GET: api/Jobs/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Job>> GetJob(int id)
    {
        var job = await _context.Jobs.FindAsync(id);

        if (job == null)
        {
            return NotFound();
        }

        return job;
    }

    // PUT: api/Jobs/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutJob(int id, Job job)
    {
        if (id != job.JobId)
        {
            return BadRequest();
        }

        _context.MarkAsModified(job);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!JobExists(id))
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

    // POST: api/Jobs
    [HttpPost]
    public async Task<ActionResult<Job>> PostJob(Job job)
    {
        _context.Jobs.Add(job);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetJob", new { id = job.JobId }, job);
    }

    // DELETE: api/Jobs/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteJob(int id)
    {
        var job = await _context.Jobs.FindAsync(id);
        if (job == null)
        {
            return NotFound();
        }

        _context.Jobs.Remove(job);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool JobExists(int id)
    {
        return _context.Jobs.Any(e => e.JobId == id);
    }
}
