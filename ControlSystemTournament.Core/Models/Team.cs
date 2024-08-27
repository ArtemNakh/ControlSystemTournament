

namespace ControlSystemTournament.Core.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string NameTeam { get; set; }
        public string Slogan { get; set; }
        public string Country {  get; set; }
        public virtual ICollection<Player>? Players { get; set; }
        public virtual Player? Coach { get; set; }
        public virtual Tournament Tournament { get; set; }

    }
}
