﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
         public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {

            }
            protected override void OnModelCreating(ModelBuilder builder)
            {
              base.OnModelCreating(builder);
        }
        
    }
}