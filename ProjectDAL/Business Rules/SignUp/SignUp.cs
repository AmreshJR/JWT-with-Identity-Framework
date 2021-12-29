using Microsoft.AspNetCore.Identity;
using ProjectDAL.Constant;
using ProjectDAL.Custom;
using ProjectDAL.DataModels;
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
        private readonly RoleManager<IdentityRole> roleManager;

        public SignUp(UserManager<ApplicationUser> UserManager,SignInManager<ApplicationUser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            userManager = UserManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        public async Task<dynamic> Register(DtoSignUp UserData)
        {
            try
            {
                using(TrainingContext dbContext = new TrainingContext())
                {
                    if (UserData != null)
                    {
                        var isExist = await userManager.FindByEmailAsync(UserData.Email);

                        if (isExist != null)
                            return ResponseStatus.duplicate;
                        else
                        {
                            var user = new ApplicationUser
                            {
                                UserName = UserData.UserName,
                                Email = UserData.Email
                            };

                            var create = await userManager.CreateAsync(user, UserData.Password);
                            
                            if (create.Succeeded)
                            {
                                var userAuth = await userManager.FindByEmailAsync(UserData.Email);
                                User newUser = new()
                                {
                                    FirstName = UserData.FirstName,
                                    LastName = UserData.LastName,
                                    AuthId = userAuth.Id,
                                    StatusId = 2,
                                    Address = UserData.Address,
                                    Dob = UserData.Dob
                                };

                                dbContext.Users.Add(newUser);
                                dbContext.SaveChanges();
                                var temp = await userManager.FindByIdAsync(userAuth.Id);
                                var role = await roleManager.FindByIdAsync("4");
                                
                                await userManager.AddToRoleAsync(temp, role.Name);

                                var userDetail = dbContext.Users.FirstOrDefault(x => x.AuthId == userAuth.Id);

                                UserDetail newUserDetails = new()
                                {
                                    UserDetailId = userDetail.UserId,
                                    DateOfJoin = DateTime.Now.ToString("yyyy-MM-dd"),
                                    CurrentOrganizationName = "Optisol Business Solution"
                                };

                                dbContext.UserDetails.Add(newUserDetails);
                                dbContext.SaveChanges();

                                return ResponseStatus.sucess;

                            }
                            else
                                return ResponseStatus.fail;
                        }
                    }
                    else
                        return ResponseStatus.fail;
                }
                
            }
            catch(Exception error)
            {
                return ResponseStatus.fail;
            }
           
            
        }


    }
}
