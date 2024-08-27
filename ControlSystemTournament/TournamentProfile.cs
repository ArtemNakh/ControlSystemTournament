using AutoMapper;
using ControlSystemTournament.Core.Models;
using ControlSystemTournament.DTOs;

namespace ControlSystemTournament
{
    public class TournamentProfile : Profile
    {
        public TournamentProfile()
        {
            CreateMap<Location, LocationDTO>().ReverseMap();
            CreateMap<Match, MatchDTO>().ReverseMap();
            CreateMap<Player, PlayerDTO>().ReverseMap();
            CreateMap<PlayerRole, PlayerRoleDTO>().ReverseMap();
            CreateMap<Sponsor, SponsorDTO>().ReverseMap();
            CreateMap<Team, TeamDTO>().ReverseMap();
            CreateMap<Tournament, TournamentDTO>().ReverseMap();
        }
    }
}
