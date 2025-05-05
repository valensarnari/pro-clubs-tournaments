using Application.Features.Tournaments;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Matches.GetByTournamentAndMatchDay
{
    public class GetMatchesByTournamentAndRoundHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetMatchesByTournamentAndRoundHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<MatchResponse>> ExecuteAsync(GetMatchesByTournamentAndRoundQuery command, CancellationToken cancellationToken = default)
        {
            var tournamentMatches = await _unitOfWork.Matches.GetByTournamentAsync(command.TournamentId, cancellationToken);

            var tournamentGroupsMatches = tournamentMatches.Where(m => m.Stage == MatchStage.Group).ToList();

            int round = command.Round;
            int startIndex = (round - 1) * 2;
            int groupSize = 6; // 2 tomados + 4 saltados

            var selectedMatches = tournamentGroupsMatches
                .Select((match, index) => new { match, index })
                .Where(x =>
                    x.index >= startIndex &&
                    ((x.index - startIndex) % groupSize == 0 || (x.index - startIndex - 1) % groupSize == 0)
                )
                .Select(x => x.match)
                .ToList();

            var matchResponses = selectedMatches.Select(m => new MatchResponse
            {
                Id = m.Id,
                HomeTeam = m.HomeTeam.Name,
                AwayTeam = m.AwayTeam.Name,
                HomeGoals = m.HomeGoals,
                AwayGoals = m.AwayGoals,
            }).ToList();

            return matchResponses;
        }
    }
}
