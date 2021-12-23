using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectDAL.Constant;
using ProjectDAL.Custom;
using ProjectDAL.DataModels;
using ProjectDAL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDAL.Business_Rules.EditRole
{
    public class EditRole : IEditRole
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public EditRole(UserManager<ApplicationUser> UserManager,RoleManager<IdentityRole> RoleManager)
        {
            userManager = UserManager;
            roleManager = RoleManager;
        }
        public async Task<dynamic> editRole(string userId, string roleId)
        {
            var role =await roleManager.FindByIdAsync(roleId);
            var user = await userManager.FindByIdAsync(userId);
            IdentityResult result = null;
            result = await userManager.AddToRoleAsync(user,role.Name);

            if (result.Succeeded)

                return true;

            else
                return false;
        }

        public dynamic GetAllUser()
        {
            using(AppDbContext dbContext = new AppDbContext())
            {
                try
                {
                    var users = dbContext.GetAllUsers.FromSqlRaw("GetAllUserAdmin").ToList();
                    if (users != null)

                        return users;
                    else

                        return users;
                }
                catch(Exception error)
                {
                    return null;
                }
               
            }
            
        }

        public async Task<int> UpdateRole(DtoUpdateRole UserData)
        {
            try
            {
                var user = await userManager.FindByNameAsync(UserData.UserName);

                if (user != null)
                {
                    await userManager.RemoveFromRoleAsync(user, UserData.OldRole);

                    await userManager.AddToRoleAsync(user, UserData.NewRole);

                    return status.sucess;
                }

                else

                    return status.fail;
            }
            catch(Exception error)
            {
                return status.fail;
            }
            
        }
    }
}
