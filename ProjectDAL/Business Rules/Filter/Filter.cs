using Microsoft.EntityFrameworkCore;
using ProjectDAL.DataModels;
using ProjectDAL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDAL.Business_Rules.Filter
{
    public class Filter : IFilter
    {
        public dynamic FilterByDate(DtoFilterByDate DateObject)
        {
            try
            {
                using (AppDbContext dbContext = new AppDbContext())
                {
                    var users = dbContext.FilteredLists
                               .FromSqlRaw("SpFilterByDate {0}, {1}", DateObject.StartDate, DateObject.EndDate).ToList();
                    return users;

                }
            }
            catch(Exception error)
            {
                throw error;
            }
        }

        public dynamic FilterByTeam(DtoFilterByTeam TeamName)
        {
            try
            {
                using (AppDbContext dbContext = new AppDbContext())
                {
                    var users = dbContext.FilteredLists
                               .FromSqlRaw("SpFilterByTeamName {0}", TeamName.TeamName).ToList();
                    return users;

                }
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public dynamic FilterByTeamLead(DtoFilterTeamLead TeamLead)
        {
            try
            {
                using (AppDbContext dbContext = new AppDbContext())
                {
                    var users = dbContext.FilteredLists
                               .FromSqlRaw("SpFilterByTeamLead {0}", TeamLead.TeamLead).ToList();
                    return users;

                }
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public dynamic GetAllDetail()
        {
            try
            {
                using (AppDbContext dbContext = new AppDbContext())
                {
                    var users = dbContext.FilteredLists
                               .FromSqlRaw("SpWithoutFilter").ToList();
                    return users;

                }
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}
