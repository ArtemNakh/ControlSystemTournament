namespace ControlSystemTournament.DTOs
{
    public class MatchDTO
    {
        public DateTime ScheduledTime { get; set; }
        public int? ScoreTeamA { get; set; } = 0;
        public int? ScoreTeamB { get; set; } = 0;
        public int TournamentId { get; set; } 
        public int TeamAId { get; set; }
        public int TeamBId { get; set; }
        public int? WinnerTeamId { get; set; } = null;
    }
}
