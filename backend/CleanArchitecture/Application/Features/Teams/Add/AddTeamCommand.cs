using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Teams.Add
{
    public class AddTeamCommand
    {
        [Required]
        public string Name { get; set; }
        public string? Badge { get; set; }
    }
}
