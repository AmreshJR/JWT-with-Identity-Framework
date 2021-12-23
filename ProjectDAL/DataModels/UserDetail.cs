using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectDAL.DataModels
{
    public partial class UserDetail
    {
        public int UserDetailId { get; set; }
        public string Experiance { get; set; }
        public string DateOfJoin { get; set; }
        public string PreviousOrganizationName { get; set; }
        public string CurrentOrganizationName { get; set; }
    }
}
