using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tournaments.GetGroupsClassification
{
    public class GetGroupsClassificationHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetGroupsClassificationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GroupClassificationResponse>> ExecuteAsync(GetGroupsClassificationQuery command, CancellationToken cancellationToken = default)
        {
            var tournament = await _unitOfWork.Tournaments.GetByIdAsync(command.TournamentId, cancellationToken);

            if (tournament == null)
            {
                throw new Exception("Tournament not found");
            }

            List<GroupClassificationResponse> groupClassifications = new List<GroupClassificationResponse>();

            foreach (var group in tournament.Groups)
            {
                groupClassifications.Add(new GroupClassificationResponse { GroupName = group.Name });

                foreach (var tournamentTeam in group.TournamentTeams)
                {
                    var team = await _unitOfWork.Teams.GetByIdAsync(tournamentTeam.TeamId, cancellationToken);

                    if (team != null)
                    {
                        groupClassifications.Last().Teams.Add(new TeamClassificationResponse { Name = team.Name });

                        var matches = await _unitOfWork.Matches.GetByTournamentAsync(tournament.Id, cancellationToken);

                        foreach (var match in matches)
                        {
                            // Si el partido es de la fase de grupos, está terminado, y el equipo está en el partido
                            if ((match.HomeTeam == team || match.AwayTeam == team) && match.Stage == MatchStage.Group && await _unitOfWork.Matches.IsFinishedAsync(match.Id, cancellationToken))
                            {
                                if (match.HomeTeam == team)
                                {
                                    // Primero le sumo los puntos y victoria, empate o derrota
                                    if (match.HomeGoals > match.AwayGoals)
                                    {
                                        groupClassifications.Last().Teams.Last().Points += 3;
                                        groupClassifications.Last().Teams.Last().Wins += 1;
                                    }
                                    else if (match.HomeGoals == match.AwayGoals)
                                    {
                                        groupClassifications.Last().Teams.Last().Points += 1;
                                        groupClassifications.Last().Teams.Last().Draws += 1;
                                    }
                                    else
                                    {
                                        groupClassifications.Last().Teams.Last().Losses += 1;
                                    }

                                    // Luego le sumo los goles a favor y en contra
                                    groupClassifications.Last().Teams.Last().GoalsFor += match.HomeGoals ?? 0;
                                    groupClassifications.Last().Teams.Last().GoalsAgainst += match.AwayGoals ?? 0;
                                }
                                else
                                {
                                    // Primero le sumo los puntos y victoria, empate o derrota
                                    if (match.AwayGoals > match.HomeGoals)
                                    {
                                        groupClassifications.Last().Teams.Last().Points += 3;
                                        groupClassifications.Last().Teams.Last().Wins += 1;
                                    }
                                    else if (match.AwayGoals == match.HomeGoals)
                                    {
                                        groupClassifications.Last().Teams.Last().Points += 1;
                                        groupClassifications.Last().Teams.Last().Draws += 1;
                                    }
                                    else
                                    {
                                        groupClassifications.Last().Teams.Last().Losses += 1;
                                    }

                                    // Luego le sumo los goles a favor y en contra
                                    groupClassifications.Last().Teams.Last().GoalsFor += match.AwayGoals ?? 0;
                                    groupClassifications.Last().Teams.Last().GoalsAgainst += match.HomeGoals ?? 0;
                                }

                                // Se le suma un partido jugado
                                groupClassifications.Last().Teams.Last().Played += 1;
                            }
                        }

                        // Por ultimo se calcula la diferencia de gol
                        groupClassifications.Last().Teams.Last().GoalDifference = groupClassifications.Last().Teams.Last().GoalsFor - groupClassifications.Last().Teams.Last().GoalsAgainst;
                    }
                }
            }

            // Se ordena la clasificacion por puntos, diferencia de gol y goles a favor
            foreach (var group in groupClassifications)
            {
                group.Teams = group.Teams.OrderByDescending(t => t.Points)
                    .ThenByDescending(t => t.GoalDifference)
                    .ThenByDescending(t => t.GoalsFor)
                    .ToList();
            }
            
            // Se ordena la clasificacion por nombre del grupo
            groupClassifications = groupClassifications.OrderBy(g => g.GroupName).ToList();

            return groupClassifications;
        }
    }
}
