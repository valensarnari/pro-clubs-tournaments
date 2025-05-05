
using Domain.Enums;

namespace Domain.Entities
{
    public class Tournament
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid UserId { get; set; }
        public int TeamCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public TournamentStatus Status { get; set; } = TournamentStatus.NotStarted;

        public User User { get; set; } = null!;
        public ICollection<TournamentTeam> TournamentTeams { get; set; } = new List<TournamentTeam>();
        public ICollection<Group> Groups { get; set; } = new List<Group>();
        public ICollection<Match> Matches { get; set; } = new List<Match>();
    }
}
