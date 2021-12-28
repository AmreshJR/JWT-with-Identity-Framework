using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniProjectDAL.DTO;
using ProjectDAL.Authentication;
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

        public dynamic GetAllUser(DtoPageNation PageData)
        {
            using(AppDbContext dbContext = new AppDbContext())
            {
                try
                {
                    var users = dbContext.GetAllUsers.FromSqlRaw("GetAllUserAdmin").ToList();
                    var userSliced = users.Skip((int)(PageData.NoOfData * (PageData.Page))).Take((int)PageData.NoOfData).ToList();

                    return userSliced;
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
                var user = await userManager.FindByIdAsync(UserData.AuthId);

                if (user != null)
                {
                    user.UserName = UserData.UserName;
                    user.Email = UserData.Email;
                    await userManager.UpdateAsync(user);

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

        public List<DtoRole> GetAllRole()
        {
            try
            {
                using (TrainingContext dbContext = new TrainingContext())
                {
                    List<DtoRole> roles = new List<DtoRole>();
                    var role = dbContext.AspNetRoles.ToList();
                    roles = role.ConvertAll(x => new DtoRole
                    {
                        Role = x.Name
                    });

                    return roles;
                }
            }
            catch(Exception error)
            {
                return null;
            }
            
        }

        public string GetUserLength()
        {


            try
            {
                using (AppDbContext dbContext = new AppDbContext())
                {
                    var users = dbContext.GetAllUsers.FromSqlRaw("GetAllUserAdmin").ToList();
                    return users.Count.ToString();
                }
            }
            catch (Exception error)
            {
                return null;
            }

                
            
        }

        public List<UserByRole> UserByRole(string UserRole)
        {
            using (AppDbContext dbContext = new AppDbContext())
            {
                try
                {
                    var user = dbContext.UserByRoles.FromSqlRaw("SpUserByRole {0}", UserRole).ToList();

                    return user;
                }
                catch (Exception error)
                {
                    return null;
                }

            }
        }
        public List<TeamDetail> TeamDetail(string LeadName)
        {
            using (AppDbContext dbContext = new AppDbContext())
            {
                try
                {
                    var team = dbContext.TeamDetails.FromSqlRaw("SpTeamUnderLead {0}", LeadName).ToList();

                    return team;
                }
                catch (Exception error)
                {
                    return null;
                }

            }
        }
        public List<InactiveEmployee> GetInactive()
        {
            using (AppDbContext dbContext = new AppDbContext())
            {
                try
                {
                    var team = dbContext.InactiveEmployees.FromSqlRaw("SpGetInActive").ToList();

                    return team;
                }
                catch (Exception error)
                {
                    return null;
                }

            }
        }

        public dynamic GetTeamType()
        {
           using(TrainingContext dbContext = new TrainingContext())
            {
                try
                {
                    var teamType = dbContext.TeamTypes.ToList();
                    return teamType;

                }
                catch(Exception error)
                {
                    return null;
                }
            }
        }

        public int UpdateTeamData(DtoUpdateTeam UserData)
        {
            using(TrainingContext dbContext = new TrainingContext())
            {
                try
                {
                    var isExist = dbContext.UserTeams.FirstOrDefault(x => x.UserId == UserData.UserId);
                    if (isExist == null)
                    {
                        UserTeam userTeam = new()
                        {
                            UserId = UserData.UserId,
                            AssignedToUser = UserData.AssignedTOUser,
                            StatusId = UserData.StatusId,
                            TeamTypeId = UserData.TeamTypeId
                        };
                        dbContext.UserTeams.Add(userTeam);
                        var user = dbContext.Users.FirstOrDefault(x => x.UserId == UserData.UserId);
                        user.StatusId = UserData.StatusId;
                        dbContext.Users.Update(user);
                        dbContext.SaveChanges();
                        return status.sucess;
                    }
                    else
                        return status.duplicate;
                    
                }
                catch(Exception error)
                {
                    return status.fail;
                }
            }
        }
    }
}
