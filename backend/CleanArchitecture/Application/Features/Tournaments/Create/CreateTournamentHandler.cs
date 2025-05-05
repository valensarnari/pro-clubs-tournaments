using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tournaments.Create
{
    public class CreateTournamentHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTournamentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Tournament> ExecuteAsync(Guid userId, CreateTournamentCommand command, CancellationToken cancellationToken = default)
        {
            if (command.TeamCount != command.TeamsId.Count)
            {
                throw new Exception("The specified number of teams does not match the team list");
            }

            var tournament = await _unitOfWork.Tournaments.AddAsync(new Tournament
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                UserId = userId,
                TeamCount = command.TeamCount,
                CreatedAt = DateTime.UtcNow,
                TournamentTeams = new List<TournamentTeam>(),
            }, cancellationToken);

            foreach (var teamId in command.TeamsId)
            {
                if (_unitOfWork.Teams.GetByIdAsync(teamId, cancellationToken) == null)
                {
                    throw new Exception($"Team with ID {teamId} not found");
                }

                tournament.TournamentTeams.Add(new TournamentTeam
                {
                    TournamentId = tournament.Id,
                    TeamId = teamId,
                    GroupId = null
                });
            }

            foreach (var tournamentTeam in tournament.TournamentTeams)
            {
                await _unitOfWork.TournamentTeams.AddAsync(tournamentTeam, cancellationToken);
            }
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return tournament;
        }
    }
}
