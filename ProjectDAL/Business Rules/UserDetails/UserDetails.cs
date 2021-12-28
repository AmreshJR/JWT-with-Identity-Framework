using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProjectDAL.Authentication;
using ProjectDAL.DataModels;
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
                    return 1;
                }

                return 2;
            }
            catch(Exception error)
            {
                return 2;
            }
            

        }
        public string GetUserProfileImage(string UserId)
        {
            try
            {
                using(TrainingContext dbContext = new TrainingContext())
                {
                    var user = dbContext.UserImageLibraries.FirstOrDefault(x => x.UserId == UserId);
                    var userProfileLink = user.ImageName;
                    return userProfileLink;
                }
                
            }
            catch(Exception error)
            {
                return null;
            }
        }
    }
}
