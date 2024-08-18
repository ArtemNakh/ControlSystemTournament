

using System;

namespace ControlSystemTournament.Core.Models
{
    public class Tournament
    {
        public int Id { get; set; }
        public string NameTournament { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get;  set; }
        public decimal PrizePool {  get; set; }

        public virtual Location Location { get; set; }

        public virtual ICollection<Team>? Teams { get; set; }

    }
}
