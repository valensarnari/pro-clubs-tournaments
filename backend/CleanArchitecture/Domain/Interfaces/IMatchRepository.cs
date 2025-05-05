using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IMatchRepository : IBaseRepository<Match>
    {
        Task<List<Match>> GetByTournamentAsync(Guid tournamentId, CancellationToken cancellationToken = default);
        Task<bool> IsFinishedAsync(Guid matchId, CancellationToken cancellationToken = default);
    }
}
