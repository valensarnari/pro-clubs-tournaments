using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Matches.GetByTournamentAndMatchDay
{
    public class GetMatchesByTournamentAndRoundQuery
    {
        [AllowedValues(1, 2, 3, ErrorMessage = "The group stage of the tournament only has 3 rounds of games")]
        [Required(ErrorMessage = "The round is required")]
        public int Round { get; set; }
        [Required(ErrorMessage = "The tournament id is required")]
        public Guid TournamentId { get; set; }
    }
}
