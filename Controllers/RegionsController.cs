﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HR_API.Models;

namespace HR_API.Controllers;

[Route("api/regions")]
[ApiController]
public class RegionsController : ControllerBase
{
    private readonly sample_hr_databaseContext _context;

    public RegionsController(sample_hr_databaseContext context)
    {
        _context = context;
    }

    // GET: api/Regions
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Region>>> GetRegions()
    {
        return await _context.Regions.ToListAsync();
    }

    // GET: api/Regions/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Region>> GetRegion(int id)
    {
        var region = await _context.Regions.FindAsync(id);

        if (region == null)
        {
            return NotFound();
        }

        return region;
    }

    // PUT: api/Regions/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRegion(int id, Region region)
    {
        if (id != region.RegionId)
        {
            return BadRequest();
        }

        _context.Entry(region).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RegionExists(id))
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

    // POST: api/Regions
    [HttpPost]
    public async Task<ActionResult<Region>> PostRegion(Region region)
    {
        _context.Regions.Add(region);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetRegion", new { id = region.RegionId }, region);
    }

    // DELETE: api/Regions/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRegion(int id)
    {
        var region = await _context.Regions.FindAsync(id);
        if (region == null)
        {
            return NotFound();
        }

        _context.Regions.Remove(region);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool RegionExists(int id)
    {
        return _context.Regions.Any(e => e.RegionId == id);
    }
}
