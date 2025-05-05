using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tournaments
{
    public class TournamentResponse
    {
        public string Name { get; set; }
        public int TeamCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<string> Teams { get; set; }
    }
}
