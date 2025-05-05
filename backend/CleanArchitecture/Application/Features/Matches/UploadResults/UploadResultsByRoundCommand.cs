using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Matches.UploadResults
{
    public class UploadResultsByRoundCommand
    {
        public Guid Id { get; set; }
        public int? HomeGoals { get; set; }
        public int? AwayGoals { get; set; }
    }
}
