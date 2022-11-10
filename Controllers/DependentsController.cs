using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HR_API.Models;

namespace HR_API.Controllers;

[Route("api/dependents")]
// [ApiController]
public class DependentsController : ControllerBase
{
    private readonly IHrDatabaseContext _context;

    public DependentsController(IHrDatabaseContext context)
    {
        _context = context;
    }

    // GET: api/Dependents
    [HttpGet]
    public Task<List<Dependent>> GetDependents()
    {
        return _context.Dependents.ToListAsync();
    }

    // GET: api/Dependents/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Dependent>> GetDependent(int id)
    {
        var dependent = await _context.Dependents.FindAsync(id);

        if (dependent == null)
        {
            return NotFound();
        }

        return dependent;
    }

    // PUT: api/Dependents/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDependent(int id, Dependent dependent)
    {
        if (id != dependent.DependentId)
        {
            return BadRequest();
        }

        _context.MarkAsModified(dependent);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DependentExists(id))
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

    // POST: api/Dependents
    [HttpPost]
    public async Task<ActionResult<Dependent>> PostDependent(Dependent dependent)
    {
        _context.Dependents.Add(dependent);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetDependent", new { id = dependent.DependentId }, dependent);
    }

    // DELETE: api/Dependents/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDependent(int id)
    {
        var dependent = await _context.Dependents.FindAsync(id);
        if (dependent == null)
        {
            return NotFound();
        }

        _context.Dependents.Remove(dependent);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool DependentExists(int id)
    {
        return _context.Dependents.Any(e => e.DependentId == id);
    }
}
