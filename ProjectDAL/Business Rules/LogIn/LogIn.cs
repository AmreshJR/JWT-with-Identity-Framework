using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectDAL.Custom;
using ProjectDAL.DTO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDAL.Business_Rules.LogIn
{
    public class LogIn : ILogIn
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration config;
        private readonly RoleManager<IdentityRole> roleManger;

        public LogIn(UserManager<ApplicationUser> UserManager, SignInManager<ApplicationUser> signInManager, IConfiguration config,RoleManager<IdentityRole> roleManger)
        {
            userManager = UserManager;
            this.signInManager = signInManager;
            this.config = config;
            this.roleManger = roleManger;
        }
        public async Task<dynamic> Login(DtoLogin UserData)
        {
            var user = await userManager.FindByEmailAsync(UserData.Email);
            if(user != null && await userManager.CheckPasswordAsync(user, UserData.Password))
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:IssuerSignInKey"]));
                var SignInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var userRole = await userManager.GetRolesAsync(user);
                var claims = new List<Claim> 
                {
                    
                    new Claim(ClaimTypes.UserData,user.Id)
                };
                foreach(var role in userRole)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                var token = new JwtSecurityToken(
                    issuer: config["JWT:ValidIssuer"],
                    audience: config["JWT:ValidAudience"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(10),
                    signingCredentials: SignInCredentials
                    );
                string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return new{ Token =  tokenString, TokenExp =  token.ValidTo.ToString("yyyy-MM-dd-HH-MM") };
            }
        
            else

            return new { };
        }
    }
}
