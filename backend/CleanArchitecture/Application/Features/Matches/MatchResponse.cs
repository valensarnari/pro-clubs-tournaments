using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Matches
{
    public class MatchResponse
    {
        public Guid Id { get; set; }

        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }

        public int? HomeGoals { get; set; }
        public int? AwayGoals { get; set; }
    }
}
