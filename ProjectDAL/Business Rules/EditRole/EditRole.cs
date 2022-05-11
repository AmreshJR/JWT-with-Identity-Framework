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

        public EditRole(UserManager<ApplicationUser> UserManager)
        {
            userManager = UserManager;
        }
       
        public dynamic GetAllUser(DtoPageNation PageData)
        {
            using(AppDbContext dbContext = new AppDbContext())
            {
                try
                {
                    var users = dbContext.GetAllUsers.FromSqlRaw("GetAllUserAdmin").ToList();
                    var userSliced = users.Skip((PageData.NoOfData * (PageData.Page))).Take(PageData.NoOfData).ToList();

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

                    return ResponseStatus.sucess;
                }

                else

                    return ResponseStatus.fail;
            }
            catch(Exception error)
            {
                return ResponseStatus.fail;
            }
            
        }

        public List<DtoRole> GetAllRole()
        {
            try
            {
                using (TrainingContext dbContext = new TrainingContext())
                {
                    
                    var role = dbContext.AspNetRoles.ToList();
                    var roles = role.ConvertAll(x => new DtoRole
                    {
                        Role = x.Name
                    });

                    return roles;
                }
            }
            catch(Exception error)
            {
                throw error;
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
                throw error;
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
                    throw error;
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
                    throw error;
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
                    throw error;
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
                        return ResponseStatus.sucess;
                    }
                    else
                        return ResponseStatus.duplicate;
                    
                }
                catch(Exception error)
                {
                    return ResponseStatus.fail;
                }
            }
        }
    }
}
