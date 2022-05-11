using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDAL.Dto
{
    public class DtoFilterTeamLead
    {
        public string TeamLead { get; set; }
    }
    public class DtoFilterByDate
    {
        public string StartDate { get; set; }

        public string EndDate { get; set; }
    }
    public class DtoFilterByTeam
    {
        public string TeamName { get; set; }
    }
}
