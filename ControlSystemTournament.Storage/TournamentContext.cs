using Microsoft.EntityFrameworkCore;
using ControlSystemTournament.Core.Models;

namespace ControlSystemTournament.Storage
{
    public class TournamentContext : DbContext
    {
        public TournamentContext(DbContextOptions<TournamentContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Налаштування зв'язку один-до-багатьох між Player і Team
            modelBuilder.Entity<Player>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey("TeamId") // Ім'я властивості зовнішнього ключа в таблиці Players
                .OnDelete(DeleteBehavior.Restrict);

            // Налаштування зв'язку один-до-одного між Team і Coach (Player)
            modelBuilder.Entity<Team>()
                .HasOne(t => t.Coach)
                .WithOne()
                .HasForeignKey<Team>("CoachId"); // Ім'я властивості зовнішнього ключа в таблиці Teams
        }


        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<PlayerRole> PlayerRoles { get; set; }
    }
}
