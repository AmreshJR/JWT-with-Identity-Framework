using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDAL.Authentication
{
    public class FilteredList
    {
        [Key]
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Role { get; set; }

        public string DateOfJoin { get; set; }

        public string TeamName { get; set; }

        public string TeamLead { get; set; }

    }
}
