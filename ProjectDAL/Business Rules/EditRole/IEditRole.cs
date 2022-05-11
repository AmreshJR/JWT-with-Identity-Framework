using MiniProjectDAL.DTO;
using ProjectDAL.Authentication;
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
        public dynamic GetAllUser(DtoPageNation PageData);
        public Task<int> UpdateRole(DtoUpdateRole UserData);
        public List<DtoRole> GetAllRole();
        public string GetUserLength();
        public List<UserByRole> UserByRole(string UserRole);
        public List<TeamDetail> TeamDetail(string LeadName);
        public List<InactiveEmployee> GetInactive();
        public dynamic GetTeamType();
        public int UpdateTeamData(DtoUpdateTeam UserData);
    }
}
