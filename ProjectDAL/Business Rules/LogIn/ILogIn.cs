using ProjectDAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDAL.Business_Rules.LogIn
{
    public interface ILogIn
    {
        public Task<dynamic> Login(DtoLogin UserData);
    }
}
