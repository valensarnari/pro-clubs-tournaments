using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tournaments.GetById
{
    public class GetTournamentByIdQuery
    {
        [Required(ErrorMessage = "The tournament id is required")]
        public Guid TournamentId { get; set; }
    }
}
