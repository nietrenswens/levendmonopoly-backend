
using LevendMonopoly.Api.Data;
using LevendMonopoly.Api.Interfaces.Services;
using LevendMonopoly.Api.Models;
using LevendMonopoly.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddTransient<ITeamService, TeamService>();
builder.Services.AddTransient<LevendMonopoly.Api.Interfaces.Services.ILogger, Logger>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ITeamSessionService, TeamSessionService>();
builder.Services.AddTransient<IUserSessionService, UserSessionService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Urls.Add("https://*:5000");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
