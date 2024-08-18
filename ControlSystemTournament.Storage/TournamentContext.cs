using Microsoft.EntityFrameworkCore;
using ControlSystemTournament.Core.Models;

namespace ControlSystemTournament.Storage
{
    public class TournamentContext : DbContext
    {
        public TournamentContext(DbContextOptions<TournamentContext> options) : base(options)
        { }

      


        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<PlayerRole> PlayerRoles { get; set; }
    }
}
