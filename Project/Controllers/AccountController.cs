using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectDAL.Business_Rules.EmailSender;
using ProjectDAL.Business_Rules.LogIn;
using ProjectDAL.Business_Rules.PasswordReset;
using ProjectDAL.Business_Rules.SignUp;
using ProjectDAL.Common;
using ProjectDAL.Constant;
using ProjectDAL.Custom;
using ProjectDAL.Dto;
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
        private readonly IEmailSender emailSender;
        private readonly IPasswordReset passwordReset;

        public AccountController(ISignUp _signUp, ILogIn _logIn,IEmailSender EmailSender,IPasswordReset passwordReset)
        {
            signUp = _signUp;
            logIn = _logIn;
            emailSender = EmailSender;
            this.passwordReset = passwordReset;
        }
        [HttpPost]
        [Route("Register")]

        public IActionResult Resgister(DtoSignUp UserData)
        {
            try
            {
                var result = signUp.Register(UserData).Result;

                if (result == ResponseStatus.sucess)

                    return Ok(new { StatusCode = StatusCodes.Status200OK, Status = true });

                else if (result == ResponseStatus.fail)

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

        [HttpPost]
        [Route("ForgetPassword")]
        public IActionResult ForgetPassword(DtoForgetPassword forgetPassword)
        {

            try
            {
                var result = passwordReset.ResetRequestPassword(forgetPassword).Result;

                if (result == ResponseStatus.sucess)

                    return Ok(new { StatusCode = StatusCodes.Status200OK, Status = true });
                else
                    return Ok(new { StatusCode = StatusCodes.Status204NoContent, Status = false });
            }
            catch
            {
                return Ok(new { StatusCode = StatusCodes.Status501NotImplemented, Status = false });
            }

            
        }
        [HttpPost]
        [Route("ResetPassword")]
        public IActionResult ResetAction(DtoResetPassword resetPassword)
        {
            try
            {
                var result = passwordReset.ResetPassword(resetPassword).Result;

                if (result == ResetResponse.success)

                    return Ok(new { StatusCode = StatusCodes.Status200OK, Status = true });

                else if (result == ResetResponse.tokenExpired)

                    return Ok(new { StatusCode = StatusCodes.Status203NonAuthoritative, Status = false });
                else

                    return Ok(new { StatusCode = StatusCodes.Status204NoContent, Status = false });
            }
            catch
            {
                return Ok(new { StatusCode = StatusCodes.Status501NotImplemented, Status = false });

            }
        }
      
    }
}
