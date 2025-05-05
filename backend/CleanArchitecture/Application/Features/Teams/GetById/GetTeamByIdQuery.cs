using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Teams.GetById
{
    public class GetTeamByIdQuery
    {
        [Required(ErrorMessage = "The team id is required")]
        public Guid TeamId { get; set; }
    }
}
