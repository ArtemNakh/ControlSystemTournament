using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlSystemTournament.Core.Models
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime ScheduledTime { get; set; }
        public int ScoreTeamA { get; set; }
        public int ScoreTeamB { get; set; }


        public virtual Tournament Tournament { get; set; }

        public virtual Team TeamA { get; set; }

        public virtual Team TeamB { get; set; }

        public virtual Team? WinnerTeam { get; set; }
    }
}
