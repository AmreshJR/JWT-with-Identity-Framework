using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectDAL.DataModels
{
    public partial class UserImageLibrary
    {
        public UserImageLibrary()
        {
            UserDetails = new HashSet<UserDetail>();
        }

        public int ImageId { get; set; }
        public string ImageName { get; set; }
        public string UserId { get; set; }

        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}
