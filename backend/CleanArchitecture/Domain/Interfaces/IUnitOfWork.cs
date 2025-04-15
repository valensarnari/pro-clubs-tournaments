using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        ITournamentRepository Tournaments { get; }
        ITournamentTeamRepository TournamentTeams { get; }
        ITeamRepository Teams { get; }
        IMatchRepository Matches { get; }
        IGroupRepository Groups { get; }
        IUserRepository Users { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
