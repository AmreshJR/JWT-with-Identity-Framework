using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectDAL.DataModels
{
    public partial class UserTeam
    {
        public int UserId { get; set; }
        public string AssignedToUser { get; set; }
        public int StatusId { get; set; }
        public int? TeamTypeId { get; set; }

        public virtual User User { get; set; }
    }
}
