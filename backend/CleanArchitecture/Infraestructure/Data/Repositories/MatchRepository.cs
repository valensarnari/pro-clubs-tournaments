using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Data.Repositories
{
    public class MatchRepository : BaseRepository<Match>, IMatchRepository
    {
        private readonly ApplicationContext _context;
        public MatchRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Match>> GetByTournamentAsync(Guid tournamentId, CancellationToken cancellationToken = default)
        {
            return await _context.Matches
                .Where(m => m.TournamentId == tournamentId)
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.Group)
                .OrderBy(m => m.Date)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsFinishedAsync(Guid matchId, CancellationToken cancellationToken = default)
        {
            var match = await _context.Matches.FirstOrDefaultAsync(m => m.Id == matchId);

            if (match != null && match.HomeGoals != null && match.AwayGoals != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
