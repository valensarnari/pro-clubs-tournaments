using Domain.Entities;
using Domain.Interfaces;
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

        public async Task<bool> IsFinishedAsync(Guid matchId)
        {
            throw new NotImplementedException();
        }
    }
}
