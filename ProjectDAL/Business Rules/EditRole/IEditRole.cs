using ProjectDAL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDAL.Business_Rules.EditRole
{
    public interface IEditRole
    {
        public Task<dynamic> editRole(string userId, string roleId);
        public dynamic GetAllUser();
        public Task<int> UpdateRole(DtoUpdateRole UserData);
    }
}
