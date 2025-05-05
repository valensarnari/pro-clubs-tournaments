using Application.Features.Matches;
using Application.Features.Teams;
using Application.Features.Tournaments;
using Application.Features.Users;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tournament, TournamentResponse>()
                .ForMember(dest => dest.Teams, opt =>
                    opt.MapFrom(src => src.TournamentTeams.Select(tt => tt.Team.Name)));

            CreateMap<User, UserResponse>().ReverseMap();

            CreateMap<Team, TeamResponse>().ReverseMap();

            CreateMap<Match, MatchResponse>()
                .ForMember(dest => dest.HomeTeam, opt =>
                    opt.MapFrom(src => src.HomeTeam.Name))
                .ForMember(dest => dest.AwayTeam, opt =>
                    opt.MapFrom(src => src.AwayTeam.Name));
        }
    }
}
