using HR_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HR_API;

public interface IHrDatabaseContext
{
    DbSet<Country>    Countries { get; set; }
    DbSet<Department> Departments { get; set; }
    DbSet<Dependent>  Dependents { get; set; }
    DbSet<Employee>   Employees { get; set; }
    DbSet<Job>        Jobs { get; set; }
    DbSet<Location>   Locations { get; set; }
    DbSet<Region>     Regions { get; set; }

    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    public void MarkAsModified<T>(T item);
}
