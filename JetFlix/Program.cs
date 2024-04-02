using JetFlix_API.Data;
using JetFlix_API.Models;
using Microsoft.EntityFrameworkCore;
using JetFlix_API.Controllers;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<JetFlixUser,JetFlixRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

ApplicationDbContext? context = app.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();
RoleManager<JetFlixRole>? roleManager = app.Services.CreateScope().ServiceProvider.GetService<RoleManager<JetFlixRole>>();
UserManager<JetFlixUser>? userManager = app.Services.CreateScope().ServiceProvider.GetService<UserManager<JetFlixUser>>();
DbInitializer dbInitializer = new DbInitializer(context, roleManager, userManager);


app.Run();

