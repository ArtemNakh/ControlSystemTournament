namespace ControlSystemTournament.DTOs
{
    public class TournamentDTO
    {
        public string NameTournament { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PrizePool { get; set; }
        public int LocationId { get; set; }
    }
}
