
namespace Domain.Entities
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid TournamentId { get; set; }

        public Tournament Tournament { get; set; } = null!;
        public ICollection<TournamentTeam> TournamentTeams { get; set; } = new List<TournamentTeam>();
        public ICollection<Match> Matches { get; set; } = new List<Match>();
    }
}
