using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using ProjectDAL.Business_Rules.EmailSender;
using ProjectDAL.Common;
using ProjectDAL.Constant;
using ProjectDAL.Custom;
using ProjectDAL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDAL.Business_Rules.PasswordReset
{
    public class PasswordReset : IPasswordReset
    {
        private readonly IEmailSender emailSender;
        private readonly UserManager<ApplicationUser> userManager;

        public PasswordReset(IEmailSender EmailSender, UserManager<ApplicationUser> UserManager)
        {
            emailSender = EmailSender;
            userManager = UserManager;
        }


        public async Task<int> ResetRequestPassword(DtoForgetPassword ForgetPasswordObject)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(ForgetPasswordObject.Email);
                if (user == null)
                    return ResponseStatus.fail;
                else

                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);
                    var param = new Dictionary<string, string>
                    {
                        { "token", token },
                        { "email", ForgetPasswordObject.Email }
                    };
                    var callBack = QueryHelpers.AddQueryString(ForgetPasswordObject.ClientURI, param);
                    var message = new Message(new string[] { user.Email }, "Reset Password", callBack);

                    await emailSender.SendEmailAsync(message);
                    return ResponseStatus.sucess;
                }
            }
            catch
            {
                return ResponseStatus.fail;
            }
        }
        public async Task<int> ResetPassword(DtoResetPassword resetPassword)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(resetPassword.Email);
                if (user == null)
                    return ResetResponse.failed;
                else
                {
                  var resetPassResult = await userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
                    if(!resetPassResult.Succeeded)
                      return ResetResponse.tokenExpired;

                    else
                    {
                        return ResetResponse.success;
                    }
                }
            }
            catch
            {
                return ResetResponse.failed;
            }
        }
    }
}
