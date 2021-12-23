using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectDAL.Business_Rules.EditRole;
using ProjectDAL.Constant;
using ProjectDAL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministratorController : ControllerBase
    {
        private readonly IEditRole editRole;

        public AdministratorController(IEditRole EditRole)
        {
            editRole = EditRole;
        }
        [HttpPost]
        [Route("EditUserRole")]

        public IActionResult EditUserRole([FromBody]DtoEdit userdata)
        { 
            return Ok(editRole.editRole(userdata.userId, userdata.roleId).Result);
        }
        [HttpGet]
        [Route("GetAllUser")]

        public IActionResult GetAllUser()
        {
            try
            {
                var users = editRole.GetAllUser();

                if (User != null)

                    return Ok(editRole.GetAllUser());
                else

                    return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status501NotImplemented);
            }
        }
        [HttpPost]
        [Route("UpdateRole")]
        public IActionResult UpdateRole(DtoUpdateRole UserData)
        {
            try
            {
                var response = editRole.UpdateRole(UserData).Result;

                if (response == status.sucess)

                    return Ok(new { StatusCodes.Status200OK, status = true });

                else 
                    return StatusCode(StatusCodes.Status204NoContent);

            }
            catch(Exception error)
            {
                return StatusCode(StatusCodes.Status501NotImplemented,error.Message);
            }
        }
    }
}
