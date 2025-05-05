using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Teams.GetById
{
    public class GetTeamByIdHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTeamByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Team> ExecuteAsync(GetTeamByIdQuery query, CancellationToken cancellationToken = default)
        {
            var team = await _unitOfWork.Teams.GetByIdAsync(query.TeamId, cancellationToken);

            if (team == null)
                throw new Exception($"Team with ID {query.TeamId} not found");

            return team;
        }
    }
}
