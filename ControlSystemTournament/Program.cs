using ControlSystemTournament.Core.Interfaces;
using ControlSystemTournament.Core.Models;
using ControlSystemTournament.Core.Services;
using ControlSystemTournament.Storage;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TournamentContext>(options => options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TournnamentDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddTransient<ILocationService, LocationService>();
builder.Services.AddTransient<IMatchService, MatchService>();
builder.Services.AddTransient<IPlayerService, PlayerService>();
builder.Services.AddTransient<ISponsorService, SponsorService>();
builder.Services.AddTransient<ITeamService, TeamService>();
builder.Services.AddTransient<IPlayerRoleService, PlayerRoleService>();
builder.Services.AddTransient<ITournamentService, TournamentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
