using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tournaments.GetGroupsClassification
{
    public class GroupClassificationResponse
    {
        public string GroupName { get; set; }
        public List<TeamClassificationResponse> Teams { get; set; } = new List<TeamClassificationResponse>();
    }
}
