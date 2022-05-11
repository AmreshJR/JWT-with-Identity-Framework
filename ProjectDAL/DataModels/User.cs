using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectDAL.DataModels
{
    public partial class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string AuthId { get; set; }
        public int StatusId { get; set; }
        public string Dob { get; set; }

        public virtual AspNetUser Auth { get; set; }
        public virtual Status Status { get; set; }
        public virtual UserTeam UserTeam { get; set; }
    }
}
