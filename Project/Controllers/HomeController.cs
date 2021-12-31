using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectDAL.Business_Rules.Filter;
using ProjectDAL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IFilter filter;

        public HomeController(IFilter Filter)
        {
            filter = Filter;
        }
        [HttpGet]
        [Route("GetWithoutFilter")]
        public IActionResult GetWithoutFilter()
        {
            try
            {
                var users = filter.GetAllDetail();
                if (users != null)
                    return Ok(new { Status = StatusCodes.Status200OK, Result = users });
                else
                    return Ok(new {Status = StatusCodes.Status204NoContent});

            }
            catch(Exception error)
            {
                return StatusCode(StatusCodes.Status501NotImplemented);
            }
        }
        [HttpPost]
        [Route("FilterByTeamLead")]
        public IActionResult FilterByTeamLead(DtoFilterTeamLead TeamLeadObject)
        {
            try
            {
                var users = filter.FilterByTeamLead(TeamLeadObject);
                if (users != null)
                    return Ok(new { Status = StatusCodes.Status200OK, Result = users });
                else
                    return Ok(new { Status = StatusCodes.Status204NoContent });

            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status501NotImplemented);
            }
        }

        [HttpPost]
        [Route("FilterByDate")]
        public IActionResult FilterByDate(DtoFilterByDate DateObject)
        {
            try
            {
                var users = filter.FilterByDate(DateObject);
                if (users != null)
                    return Ok(new { Status = StatusCodes.Status200OK, Result = users });
                else
                    return Ok(new { Status = StatusCodes.Status204NoContent });

            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status501NotImplemented);
            }
        }

        [HttpPost]
        [Route("FilterByTeam")]
        public IActionResult FilterByTeam(DtoFilterByTeam TeamObject)
        {
            try
            {
                var users = filter.FilterByTeam(TeamObject);
                if (users != null)
                    return Ok(new { Status = StatusCodes.Status200OK, Result = users });
                else
                    return Ok(new { Status = StatusCodes.Status204NoContent });

            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status501NotImplemented);
            }
        }
    }
}
