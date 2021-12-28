using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectDAL.Business_Rules.UserDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using ProjectDAL.Constant;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
/*    [Authorize]
*/    public class UserController : ControllerBase
    {
        private readonly IUserDetails userDetail;

        public UserController(IUserDetails userDetail)
        {
            this.userDetail = userDetail;
        }

        [HttpGet]
        [Route("GetUserProfileDetails")]
        public IActionResult GetUserProfileDetails(string UserId)
        {
            try
            {
                var user = userDetail.GetUserProfileDetail(UserId);
                if (user != null)
                    return Ok(user);
                else
                    return StatusCode(StatusCodes.Status204NoContent);

            }
            catch(Exception error)
            {
                return StatusCode(StatusCodes.Status501NotImplemented, error.Message);
            }
        }
        [HttpPost]
        [Route("UploadImage")]
        public IActionResult UploadImage()
        {
            try
            {
                var authId = User.Claims.First(x => x.Type == "UserId").Value;
                var file = Request.Form.Files[0];
                var result = userDetail.UpdateUserProfile(file, authId);
                if (result == status.sucess)
                {
                    return Ok(result);
                }
                else 
                    return StatusCode(StatusCodes.Status204NoContent);
            }
            catch(Exception error)
            {
                return StatusCode(StatusCodes.Status501NotImplemented);
            }
            
        }
        [HttpGet]
        [Route("GetUserProfile")]
        public IActionResult ProfileImage()
        {
            try
            {
                var authId = User.Claims.First(x => x.Type == "UserId").Value;
                var result = userDetail.GetUserProfileImage(authId);
                if (result != null)

                    return Ok(new { StatusCodes.Status200OK,dbPath = result });

                else
                    return StatusCode(StatusCodes.Status204NoContent);

            }
            catch(Exception error)
            {
                return StatusCode(StatusCodes.Status501NotImplemented);
            }
        }
    }
}
