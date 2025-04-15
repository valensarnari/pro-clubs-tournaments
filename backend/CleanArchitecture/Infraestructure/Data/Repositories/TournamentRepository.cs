using Domain.Entities;
using Domain.Interfaces;
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
    }
}
