using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tournaments.GetById
{
    public class GetTournamentByIdHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTournamentByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Tournament> ExecuteAsync(GetTournamentByIdQuery query, CancellationToken cancellationToken = default)
        {
            var tournament = await _unitOfWork.Tournaments.GetByIdAsync(query.TournamentId, cancellationToken);
            if (tournament == null)
            {
                throw new Exception($"Tournament with ID {query.TournamentId} not found");
            }

            return tournament;
        }
    }
}
