using Domain.Enums;

namespace Domain.Entities
{
    public class Match
    {
        public Guid Id { get; set; }
        public Guid TournamentId { get; set; }
        public Guid? GroupId { get; set; }

        public Guid HomeTeamId { get; set; }
        public Guid AwayTeamId { get; set; }

        public DateTime Date { get; set; }
        public int? HomeGoals { get; set; }
        public int? AwayGoals { get; set; }

        public MatchStage Stage { get; set; }

        public Tournament Tournament { get; set; } = null!;
        public Group? Group { get; set; }
        public Team HomeTeam { get; set; } = null!;
        public Team AwayTeam { get; set; } = null!;
    }
}
