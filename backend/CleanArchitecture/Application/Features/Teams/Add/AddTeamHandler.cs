using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Teams.Add
{
    public class AddTeamHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddTeamHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Team> ExecuteAsync(AddTeamCommand command, CancellationToken cancellationToken = default)
        {
            var team = await _unitOfWork.Teams.AddAsync(new Team
            {
                Name = command.Name,
                Badge = command.Badge,
            });

            await _unitOfWork.SaveChangesAsync();
            return team;
        }
    }
}
