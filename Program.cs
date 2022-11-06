using Microsoft.EntityFrameworkCore;
using HR_API.Models;
using HrDbContext = HR_API.sample_hr_databaseContext; // Short alias for a long class name

namespace HR_API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        string connection = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<HrDbContext>(opt => opt.UseSqlServer(connection));
        builder.Services.AddEndpointsApiExplorer();   //
        builder.Services.AddSwaggerGen();             //

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();     //
            app.UseSwaggerUI();   //
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}