using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDAL.Authentication
{
    public class TeamDetail
    {
        [Key]
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string AssignedTOUser { get; set; }

        public string TeamName { get; set; }
    }
}
