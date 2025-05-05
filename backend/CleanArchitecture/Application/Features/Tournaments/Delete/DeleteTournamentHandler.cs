using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tournaments.Delete
{
    public class DeleteTournamentHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTournamentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ExecuteAsync(DeleteTournamentCommand command, CancellationToken cancellationToken = default)
        {
            var tournament = await _unitOfWork.Tournaments.GetByIdAsync(command.TournamentId, cancellationToken);
            if (tournament == null)
            {
                throw new Exception("Tournament not found");
            }

            if (tournament.UserId != command.UserId)
            {
                throw new Exception("You are not authorized to delete this tournament");
            }

            var result = await _unitOfWork.Tournaments.DeleteAsync(tournament.Id, cancellationToken);
            if (result)
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return true;
            }
            else
            {
                throw new Exception("Failed to delete tournament");
            }
        }
    }
}
