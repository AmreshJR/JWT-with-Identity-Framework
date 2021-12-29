using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectDAL.Authentication;
using ProjectDAL.Constant;
using ProjectDAL.Custom;
using ProjectDAL.DataModels;
using ProjectDAL.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDAL.Business_Rules.UserDetails
{
    public class UserDetails : IUserDetails

    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public UserDetails(UserManager<ApplicationUser> UserManager,SignInManager<ApplicationUser> signInManager)
        {
            userManager = UserManager;
            this.signInManager = signInManager;
        }

        public UserProfileDetails GetUserProfileDetail(string UserId)
        {

            using (AppDbContext dbContext = new AppDbContext())
            {
                try
                {
                    var user = dbContext.UserProfileDetail.FromSqlRaw("SpUserProfileDetails {0}", UserId).ToList().FirstOrDefault();
                    return user;


                }
                catch (Exception error)
                {
                    return null;
                }
            } 
        }

     

        public int UpdateUserProfile(IFormFile file,string AuthId)
        {
            try
            {
                var folderName = Path.Combine("Resource", "Image");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var imageName = new String(Path.GetFileNameWithoutExtension(file.FileName).Take(10).ToArray()).Replace(' ', '-');
                    imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(file.FileName);
                    /* var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');*/
                    var fullPath = Path.Combine(pathToSave, imageName);
                    var dbPath = Path.Combine(folderName, imageName);
                    using (Stream stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    using(TrainingContext dbContext = new TrainingContext())
                    {
                        var isExist = dbContext.UserImageLibraries.FirstOrDefault(x => x.UserId == AuthId);
                        if(isExist != null)
                        {
                            isExist.ImageName = dbPath;
                            dbContext.UserImageLibraries.Update(isExist);
                            dbContext.SaveChanges();
                        }
                        else
                        {
                            UserImageLibrary image = new()
                            {
                                UserId = AuthId,
                                ImageName = dbPath
                            };
                            dbContext.UserImageLibraries.Add(image);
                            dbContext.SaveChanges();
                        }
                    }
                    return status.sucess;
                }

                return status.fail;
            }
            catch(Exception error)
            {
                return status.fail;
            }
            

        }
        public string GetUserProfileImage(string UserId)
        {
            try
            {
                using(TrainingContext dbContext = new TrainingContext())
                {
                    var user = dbContext.UserImageLibraries.FirstOrDefault(x => x.UserId == UserId);
                    if (user != null)
                    {
                        var userProfileLink = user.ImageName;
                        return userProfileLink;
                    }
                    else
                        return null;
                }
                
            }
            catch(Exception error)
            {
                return null;
            }
        }

        public async Task<int> UpdateProfileData(string AuthId,DtoUserProfileDetails UserProfileObject)
        {
            try
            {
                var authUser = await userManager.FindByIdAsync(AuthId);
               
                using(TrainingContext dbContext = new TrainingContext())
                {
                    var user = dbContext.Users.FirstOrDefault(x => x.AuthId == authUser.Id);

                    user.FirstName = UserProfileObject.FirstName;
                    user.LastName = UserProfileObject.LastName;
                    user.Address = UserProfileObject.Address;

                    dbContext.Users.Update(user);
                    dbContext.SaveChanges();
                    var userDetail = dbContext.UserDetails.FirstOrDefault(y => y.UserDetailId == user.UserId);

                    userDetail.Experiance = UserProfileObject.Experiance;
                    userDetail.PreviousOrganizationName = UserProfileObject.PreviousOrganizationName;

                    dbContext.UserDetails.Update(userDetail);
                    dbContext.SaveChanges();

                    return status.sucess;

                }
                
            }
            catch(Exception error)
            {
                return status.fail;
            }
        }

        public async Task<int> ConfirmPassword(DtoConfirmPassword UserData,string AuthId)
        {
            try
            {
                var user = await userManager.FindByIdAsync(AuthId);

                if (user != null && await userManager.CheckPasswordAsync(user, UserData.Password))

                    return status.sucess;
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
