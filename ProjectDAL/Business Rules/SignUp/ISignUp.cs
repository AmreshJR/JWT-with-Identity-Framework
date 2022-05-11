using ProjectDAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDAL.Business_Rules.SignUp
{
    public interface ISignUp
    {
        public Task<dynamic> Register(DtoSignUp UserData);
    }
}
