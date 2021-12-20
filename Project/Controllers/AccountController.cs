using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectDAL.Business_Rules.SignUp;
using ProjectDAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ISignUp signUp;

        public AccountController(ISignUp _signUp)
        {
            signUp = _signUp;
        }
        [HttpPost]
        [Route("Register")]

        public IActionResult Resgister(SignUpDTO UserData)
        {
            return Ok(signUp.Register(UserData).Result);

        }
    }
}
