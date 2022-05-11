using ProjectDAL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDAL.Business_Rules.PasswordReset
{
    public interface IPasswordReset
    {
        public Task<int> ResetRequestPassword(DtoForgetPassword ForgetPasswordObject );
        public Task<int> ResetPassword(DtoResetPassword resetPassword);
    }
}
