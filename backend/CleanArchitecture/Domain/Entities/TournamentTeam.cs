
namespace Domain.Entities
{
    public class TournamentTeam
    {
        public Guid TournamentId { get; set; }
        public Guid TeamId { get; set; }
        public Guid? GroupId { get; set; }

        public Tournament Tournament { get; set; } = null!;
        public Team Team { get; set; } = null!;
        public Group? Group { get; set; }
    }
}
