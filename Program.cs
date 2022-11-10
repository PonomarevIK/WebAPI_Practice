using Microsoft.EntityFrameworkCore;

namespace HR_API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connection = builder.Configuration.GetConnectionString("DefaultConnection");

        // Add services to the container.
        builder.Services.AddDbContext<HrDatabaseContext>(opt => opt.UseSqlServer(connection));
        builder.Services.AddScoped<IHrDatabaseContext>(provider => provider.GetService<HrDatabaseContext>());
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();   
        builder.Services.AddSwaggerGen();             


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseMiddleware<Middleware.LoggingMiddleware>();
        app.UseMiddleware<Middleware.ErrorHandlingMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();     
            app.UseSwaggerUI();   
        }
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}