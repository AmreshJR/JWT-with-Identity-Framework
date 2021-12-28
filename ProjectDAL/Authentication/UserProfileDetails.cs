using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDAL.Authentication
{
    public class UserProfileDetails
    {
        [Key]

        public int UserId { get; set; }

        public string AuthId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Dob { get; set; }

        public string DateOfJoin { get; set; }

        public string Experiance { get; set; }

        public string  CurrentOrganizationName { get; set; }

        public string PreviousOrganizationName { get; set; }

        public string ImagePath { get; set; }
    }
}
