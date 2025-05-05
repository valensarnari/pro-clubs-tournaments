using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Matches.UploadResults
{
    public class UploadResultsByRoundHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public UploadResultsByRoundHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ExecuteAsync(List<UploadResultsByRoundCommand> command, CancellationToken cancellationToken = default)
        {
            foreach (var match in command)
            {
                var matchEntity = await _unitOfWork.Matches.GetByIdAsync(match.Id, cancellationToken);
                if (matchEntity == null)
                {
                    throw new Exception($"Match with ID {match.Id} not found.");
                }

                matchEntity.AwayGoals = match.AwayGoals;
                matchEntity.HomeGoals = match.HomeGoals;

                await _unitOfWork.Matches.UpdateAsync(matchEntity, cancellationToken);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
