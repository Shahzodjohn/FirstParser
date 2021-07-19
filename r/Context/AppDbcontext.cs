using Microsoft.EntityFrameworkCore;
using r.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace r.Context
{
    public class AppDbcontext : DbContext
    {
        public AppDbcontext(DbContextOptions<AppDbcontext> options) : base(options)
        {
        }
        public DbSet<Drop> Drops { get; set; }
        public DbSet<DRT> DRTS { get; set; }
        public DbSet<Categories> CategorySSS { get; set; }

    }
}
