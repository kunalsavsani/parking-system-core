using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingSystemCore
{

    public class dbcontext : DbContext
    {
        public dbcontext(DbContextOptions<dbcontext> options) : base(options)
        {


        }

        public DbSet<parkingslot> Parkingslots { get; set; }

        public DbSet<details> Details { get; set; }

        public DbSet<backup> Backups { get; set; }

        public DbSet<UserInfo> userInfos { get; set; }
    }
}