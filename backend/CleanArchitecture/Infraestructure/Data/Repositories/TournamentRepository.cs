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
    public class TournamentRepository : BaseRepository<Tournament>, ITournamentRepository
    {
        private readonly ApplicationContext _context;
        
        public TournamentRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Tournament>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _context.Tournaments
                .Include(t => t.TournamentTeams)
                    .ThenInclude(tt => tt.Team)
                .Include(t => t.Groups)
                    .ThenInclude(g => g.TournamentTeams)
                        .ThenInclude(gt => gt.Team) 
                .Include(t => t.Matches).ToListAsync(cancellationToken);

        public override async Task<Tournament?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _context.Tournaments
                .Include(t => t.TournamentTeams)
                    .ThenInclude(tt => tt.Team)
                .Include(t => t.Groups)
                    .ThenInclude(g => g.TournamentTeams)
                        .ThenInclude(gt => gt.Team)
                .Include(t => t.Matches)
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }
}
