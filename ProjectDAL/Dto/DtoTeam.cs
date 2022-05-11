using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDAL.Dto
{
    public class DtoTeam
    {
        public string LeadName { get; set; }
    }
    public class DtoUpdateTeam
    {
        public int UserId { get; set; }

        public string AssignedTOUser { get; set; }

        public int StatusId { get; set; }

        public int TeamTypeId { get; set; }
    }
}
