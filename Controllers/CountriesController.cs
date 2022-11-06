using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HR_API.Models;

namespace HR_API.Controllers;

[Route("api/countries")]
[ApiController]
public class CountriesController : ControllerBase
{
    private readonly sample_hr_databaseContext _context;

    public CountriesController(sample_hr_databaseContext context)
    {
        _context = context;
    }

    // GET: api/Countries
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
    {
        return await _context.Countries.ToListAsync();
    }

    // GET: api/Countries/UK
    [HttpGet("{id}")]
    public async Task<ActionResult<Country>> GetCountry(string id)
    {
        var country = await _context.Countries.FindAsync(id);

        if (country == null)
        {
            return NotFound();
        }

        return country;
    }

    // PUT: api/Countries/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCountry(string id, Country country)
    {
        if (id != country.CountryId)
        {
            return BadRequest();
        }

        _context.Entry(country).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CountryExists(id))
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

    // POST: api/Countries
    [HttpPost]
    public async Task<ActionResult<Country>> PostCountry(Country country)
    {
        _context.Countries.Add(country);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (CountryExists(country.CountryId))
            {
                return Conflict();
            }
            else
            {
                throw;
            }
        }

        return CreatedAtAction("GetCountry", new { id = country.CountryId }, country);
    }

    // DELETE: api/Countries/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCountry(string id)
    {
        var country = await _context.Countries.FindAsync(id);
        if (country == null)
        {
            return NotFound();
        }

        _context.Countries.Remove(country);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CountryExists(string id)
    {
        return _context.Countries.Any(e => e.CountryId == id);
    }
}
