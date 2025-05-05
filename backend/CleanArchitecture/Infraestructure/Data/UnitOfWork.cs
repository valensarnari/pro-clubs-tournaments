using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public ITournamentRepository Tournaments { get; }
        public ITournamentTeamRepository TournamentTeams { get; }
        public ITeamRepository Teams { get; }
        public IMatchRepository Matches { get; }
        public IGroupRepository Groups { get; }
        public IUserRepository Users { get; }
        public UnitOfWork(ApplicationContext context, ITournamentRepository tournaments, ITournamentTeamRepository tournamentTeams, ITeamRepository teams, IMatchRepository matches, IGroupRepository groups, IUserRepository users)
        {
            _context = context;
            Tournaments = tournaments;
            TournamentTeams = tournamentTeams;
            Teams = teams;
            Matches = matches;
            Groups = groups;
            Users = users;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
