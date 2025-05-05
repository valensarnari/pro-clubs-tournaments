using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.GetById
{
    public class GetUserByIdQuery
    {
        [Required(ErrorMessage = "The user id is required")]
        public Guid Id { get; set; }
    }
}
