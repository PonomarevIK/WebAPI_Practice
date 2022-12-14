using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HR_API.Models;
using System.Diagnostics.Metrics;

namespace HR_API.Controllers;

[Route("api/departments")]
// [ApiController]
public class DepartmentsController : ControllerBase
{
    private readonly IHrDatabaseContext _context;

    public DepartmentsController(IHrDatabaseContext context)
    {
        _context = context;
    }

    // GET: api/Departments
    [HttpGet]
    public Task<List<Department>> GetDepartments()
    {
        return _context.Departments.ToListAsync();
    }

    // GET: api/Departments/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Department>> GetDepartment(int id)
    {
        var department = await _context.Departments.FindAsync(id);

        if (department == null)
        {
            return NotFound();
        }

        return department;
    }

    // PUT: api/Departments/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDepartment(int id, Department department)
    {
        if (id != department.DepartmentId)
        {
            return BadRequest();
        }

        _context.MarkAsModified(department);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DepartmentExists(id))
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

    // POST: api/Departments
    [HttpPost]
    public async Task<ActionResult<Department>> PostDepartment(Department department)
    {
        _context.Departments.Add(department);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetDepartment", new { id = department.DepartmentId }, department);
    }

    // DELETE: api/Departments/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartment(int id)
    {
        var department = await _context.Departments.FindAsync(id);
        if (department == null)
        {
            return NotFound();
        }

        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool DepartmentExists(int id)
    {
        return _context.Departments.Any(e => e.DepartmentId == id);
    }
}
