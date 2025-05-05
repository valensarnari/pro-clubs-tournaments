using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tournaments.GenerateGroups
{
    public class GenerateGroupsHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenerateGroupsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ExecuteAsync(GenerateGroupsCommand command, CancellationToken cancellationToken = default)
        {
            var tournament = await _unitOfWork.Tournaments.GetByIdAsync(command.TournamentId, cancellationToken);
            if (tournament == null)
            {
                throw new Exception("Tournament not found");
            }

            if (tournament.Status != TournamentStatus.NotStarted)
            {
                throw new Exception("Tournament is not in a valid state to generate groups");
            }


            // Logica para generar grupos

            // Calcular cantidad de equipos y grupos necesarios
            var teamCount = tournament.TeamCount;
            var groupCount = teamCount / 4;
            var groupNames = Enumerable.Range(0, groupCount).Select(i => ((char)('A' + i)).ToString()).ToList();

            // Mezclar aleatoriamente los equipos
            var teams = tournament.TournamentTeams.OrderBy(_ => Guid.NewGuid()).ToList();

            var groups = new List<Group>();
            for (int i = 0; i < groupCount; i++)
            {
                // Crear un nuevo grupo
                var group = new Group
                {
                    Id = Guid.NewGuid(),
                    Name = "Grupo " + groupNames[i].ToString(),
                    TournamentId = tournament.Id
                };
                await _unitOfWork.Groups.AddAsync(group, cancellationToken);
                groups.Add(group);

                // Asignar 4 equipos a este grupo
                var groupTeams = teams.Skip(i * 4).Take(4).ToList();
                // Setear el grupo a los equipos
                foreach (var tt in groupTeams)
                    tt.GroupId = group.Id;

                // Generar 3 fechas con partidos balanceados (cada equipo local y visitante al menos 1 vez)
                // Formato clásico de partidos en grupos de 4:
                // Fecha 1: Equipo0 vs Equipo1, Equipo2 vs Equipo3
                // Fecha 2: Equipo2 vs Equipo0, Equipo1 vs Equipo3
                // Fecha 3: Equipo3 vs Equipo0, Equipo1 vs Equipo2

                var e0 = groupTeams[0].TeamId;
                var e1 = groupTeams[1].TeamId;
                var e2 = groupTeams[2].TeamId;
                var e3 = groupTeams[3].TeamId;

                await _unitOfWork.Matches.AddAsync(new Match
                {
                    Id = Guid.NewGuid(),
                    TournamentId = tournament.Id,
                    GroupId = group.Id,
                    HomeTeamId = e0,
                    AwayTeamId = e1,
                    Date = DateTime.UtcNow,
                    Stage = MatchStage.Group
                }, cancellationToken);

                await _unitOfWork.Matches.AddAsync(new Match
                {
                    Id = Guid.NewGuid(),
                    TournamentId = tournament.Id,
                    GroupId = group.Id,
                    HomeTeamId = e2,
                    AwayTeamId = e3,
                    Date = DateTime.UtcNow,
                    Stage = MatchStage.Group
                }, cancellationToken);

                await _unitOfWork.Matches.AddAsync(new Match
                {
                    Id = Guid.NewGuid(),
                    TournamentId = tournament.Id,
                    GroupId = group.Id,
                    HomeTeamId = e2,
                    AwayTeamId = e0,
                    Date = DateTime.UtcNow,
                    Stage = MatchStage.Group
                }, cancellationToken);

                await _unitOfWork.Matches.AddAsync(new Match
                {
                    Id = Guid.NewGuid(),
                    TournamentId = tournament.Id,
                    GroupId = group.Id,
                    HomeTeamId = e1,
                    AwayTeamId = e3,
                    Date = DateTime.UtcNow,
                    Stage = MatchStage.Group
                }, cancellationToken);

                await _unitOfWork.Matches.AddAsync(new Match
                {
                    Id = Guid.NewGuid(),
                    TournamentId = tournament.Id,
                    GroupId = group.Id,
                    HomeTeamId = e3,
                    AwayTeamId = e0,
                    Date = DateTime.UtcNow,
                    Stage = MatchStage.Group
                }, cancellationToken);

                await _unitOfWork.Matches.AddAsync(new Match
                {
                    Id = Guid.NewGuid(),
                    TournamentId = tournament.Id,
                    GroupId = group.Id,
                    HomeTeamId = e1,
                    AwayTeamId = e2,
                    Date = DateTime.UtcNow,
                    Stage = MatchStage.Group
                }, cancellationToken);
            }

            // Actualizar el estado del torneo
            tournament.Status = TournamentStatus.Groups;
            await _unitOfWork.Tournaments.UpdateAsync(tournament, cancellationToken);

            // ...

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
