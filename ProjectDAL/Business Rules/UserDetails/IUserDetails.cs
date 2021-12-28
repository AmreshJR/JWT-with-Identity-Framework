using Microsoft.AspNetCore.Http;
using ProjectDAL.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDAL.Business_Rules.UserDetails
{
    public interface IUserDetails
    {
        public UserProfileDetails GetUserProfileDetail(string UserId);
        public int UpdateUserProfile(IFormFile file,string AuthId);
        public string GetUserProfileImage(string UserId);
    }
}
