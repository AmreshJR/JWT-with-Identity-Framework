using ProjectDAL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDAL.Business_Rules.Filter
{
    public interface IFilter
    {
        public dynamic GetAllDetail();
        public dynamic FilterByTeamLead(DtoFilterTeamLead TeamLead);
        public dynamic FilterByDate(DtoFilterByDate DateObject);
        public dynamic FilterByTeam(DtoFilterByTeam TeamName);
        
    }
}
