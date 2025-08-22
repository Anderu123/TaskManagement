using Microsoft.EntityFrameworkCore;

using TaskManagement.SharedData;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(y => y.UseSqlite(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Allowed Origins",
        policy =>
        {
            policy.WithOrigins("https://localhost:7107").AllowAnyHeader().AllowAnyMethod();
        });
});
builder.Services.AddScoped<ITaskAccessLayer, TasksAccessLayer>();
var app = builder.Build();




app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors("Allowed Origins");
app.Run();
