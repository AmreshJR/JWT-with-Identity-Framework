using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProjectDAL.DTO;
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
    [Authorize(Roles = "Manager")]

    public class AdministratorController : ControllerBase
    {
        private readonly IEditRole editRole;

        public AdministratorController(IEditRole EditRole)
        {
            editRole = EditRole;
        }
        [HttpPost]
        [Route("GetAllUser")]

        public IActionResult GetAllUser(DtoPageNation PageData)
        {
            try
            {
                var users = editRole.GetAllUser(PageData);

                if (users != null)

                    return Ok(editRole.GetAllUser(PageData));
                else

                    return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status501NotImplemented);
            }
        }
        [HttpPost]
        [Route("UpdateUser")]
        public IActionResult UpdateUser(DtoUpdateRole UserData)
        {
            try
            {
                var response = editRole.UpdateRole(UserData).Result;

                if (response == ResponseStatus.sucess)

                    return Ok(new { StatusCodes.Status200OK, status = true });

                else 
                    return StatusCode(StatusCodes.Status204NoContent);

            }
            catch(Exception error)
            {
                return StatusCode(StatusCodes.Status501NotImplemented,error.Message);
            }
        }
        [HttpGet]
        [Route("GetAllRoles")]
        public IActionResult GetAllRoles()
        {
            try
            {
                var result = editRole.GetAllRole();
                if (result != null)

                    return Ok(result);
                else
                    return StatusCode(StatusCodes.Status204NoContent);
            }
            catch(Exception error)
            {
                return StatusCode(StatusCodes.Status501NotImplemented);
            }
            
        }
        [HttpGet]
        [Route("GetLength")]
        public IActionResult GetLength()
        {
            try
            {
                var result = editRole.GetUserLength();
                if (result != null)
                    return Ok(result);
                else
                    return StatusCode(StatusCodes.Status204NoContent);
            }
            catch(Exception error)
            {
                return StatusCode(StatusCodes.Status501NotImplemented);
            }
        }
        [HttpPost]
        [Route("UserByRole")]
        public IActionResult UserByRole(DtoGetByRole UserData)
        {
            try
            {
                var user = editRole.UserByRole(UserData.Role);
                if (user != null)

                    return Ok(user);
                else
                    return StatusCode(StatusCodes.Status204NoContent);
            }
            catch(Exception error)
            {
                return StatusCode(StatusCodes.Status501NotImplemented);
            }
        }
        [HttpPost]
        [Route("TeamUnderLead")]
        public IActionResult TeamUnderLead(DtoTeam UserData)
        {
            try
            {
                var team = editRole.TeamDetail(UserData.LeadName);
                if (team != null)

                    return Ok(team);
                else
                    return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status501NotImplemented);
            }
        }
        [HttpGet]
        [Route("InActiveEmployee")]
        public IActionResult InActiveEmployee( )
        {
            try
            {
                var team = editRole.GetInactive();
                if (team != null)

                    return Ok(team);
                else
                    return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status501NotImplemented);
            }
        }
        [HttpGet]
        [Route("GetTeamList")]
        public IActionResult GetTeamList()
        {
            try
            {
                var teamType = editRole.GetTeamType();
                if (teamType != null)
                    return Ok(teamType);
                else
                    return StatusCode(StatusCodes.Status204NoContent);
            }
            catch(Exception error)
            {
                return StatusCode(StatusCodes.Status501NotImplemented);
            }
        }
        [HttpPost]
        [Route("AddUserTeam")]
        public IActionResult AddUserTeam(DtoUpdateTeam UserData)
        {
            try
            {
                var response = editRole.UpdateTeamData(UserData);
                if (response == 1)
                    return StatusCode(StatusCodes.Status200OK);
                else if (response == 2)
                    return StatusCode(StatusCodes.Status204NoContent);
                else 
                    return StatusCode(StatusCodes.Status208AlreadyReported);

            }
            catch(Exception error)
            {
                return StatusCode(StatusCodes.Status501NotImplemented, error.Message);
            }
        }
    }
}
