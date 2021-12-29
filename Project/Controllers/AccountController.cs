using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectDAL.Business_Rules.LogIn;
using ProjectDAL.Business_Rules.SignUp;
using ProjectDAL.Common;
using ProjectDAL.Constant;
using ProjectDAL.DTO;
using System;


namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly ISignUp signUp;
        private readonly ILogIn logIn;

        public AccountController(ISignUp _signUp, ILogIn _logIn)
        {
            signUp = _signUp;
            logIn = _logIn;
        }
        [HttpPost]
        [Route("Register")]

        public IActionResult Resgister(DtoSignUp UserData)
        {
            try
            {
                var result = signUp.Register(UserData).Result;

                if (result == status.sucess)

                    return Ok(new { StatusCode = StatusCodes.Status200OK, Status = true });

                else if (result == status.fail)

                    return Ok(new {StatusCode =  StatusCodes.Status204NoContent, Status = false } );

                else 

                    return StatusCode(StatusCodes.Status208AlreadyReported,false);

            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status501NotImplemented, false);

            }

        }

        [HttpPost]
        [Route("Login")]
        public IActionResult LogIn(DtoLogin UserData)
        {
            try
            {
                var result = logIn.Login(UserData).Result;

                if (result != null)

                    return Ok(new {Status = StatusCodes .Status200OK ,  Result = result });

                else

                    return Ok(new {status = StatusCodes.Status204NoContent });
            }
            catch(Exception error)
            {
                return StatusCode(StatusCodes.Status501NotImplemented, error.Message);
            }
            
        }
      
    }
}
