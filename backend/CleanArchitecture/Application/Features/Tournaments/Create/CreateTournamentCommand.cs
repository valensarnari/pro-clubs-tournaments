using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tournaments.Create
{
    public class CreateTournamentCommand
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [AllowedValues(8, 16, 32)]
        public int TeamCount { get; set; }
        [Length(8, 32)]
        public List<Guid> TeamsId { get; set; }
    }
}
