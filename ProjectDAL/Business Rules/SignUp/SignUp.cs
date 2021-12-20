using Microsoft.AspNetCore.Identity;
using ProjectDAL.Custom;
using ProjectDAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDAL.Business_Rules.SignUp
{
    public class SignUp : ISignUp
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public SignUp(UserManager<ApplicationUser> UserManager,SignInManager<ApplicationUser> signInManager)
        {
            userManager = UserManager;
            this.signInManager = signInManager;
        }

        public async Task<dynamic> Register(SignUpDTO UserData)
        {
            var user = new ApplicationUser
            {
                UserName = UserData.UserName,
                FirstName = UserData.FirstName,
                LastName = UserData.LastName,
                Email = UserData.Email,
                Dob = UserData.Dob
            };
            var result = await userManager.CreateAsync(user, UserData.Password);

            return result;
        }


    }
}
