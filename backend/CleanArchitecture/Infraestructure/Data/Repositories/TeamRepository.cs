using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Data.Repositories
{
    public class TeamRepository : BaseRepository<Team>, ITeamRepository
    {
        private readonly ApplicationContext _context;

        public TeamRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}
