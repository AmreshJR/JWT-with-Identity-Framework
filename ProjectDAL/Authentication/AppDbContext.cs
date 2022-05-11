using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectDAL.Authentication;
using ProjectDAL.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDAL.DataModels
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {

            }

        public virtual DbSet<GetAllUser> GetAllUsers { get; set; }
        public virtual DbSet<UserByRole> UserByRoles { get; set; }
        public virtual DbSet<TeamDetail> TeamDetails { get; set; }
        public virtual DbSet<InactiveEmployee> InactiveEmployees { get; set; }
        public virtual DbSet<UserProfileDetails> UserProfileDetail { get; set; }
        public virtual DbSet<FilteredList> FilteredLists { get; set; }

  
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
/*#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.*/
                optionsBuilder.UseSqlServer("Server=183.82.35.178;Database=Training_2;User Id=sql;password=Optisol@123;Trusted_Connection=false;MultipleActiveResultSets=true;Integrated Security=False;Persist Security Info=False;");
            }
        }

    }

}
