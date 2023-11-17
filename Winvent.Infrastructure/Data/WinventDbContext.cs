using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winvent.Domain.Models;

namespace Winvent.Infrastructure.Data
{
    public class WinventDbContext:DbContext
    {
        public WinventDbContext(DbContextOptions<WinventDbContext> options):base(options)
        {
                
        }
        public DbSet<Admin>? Admins { get; set; }
        public DbSet<Officer>? Officers { get; set; }
        public DbSet<Offering>? Offerings { get; set; }
        public DbSet<Expense>? Expenses { get; set; }
        public DbSet<Tithe>? Tithes { get; set; }
        public DbSet<TransportSeed>? TransportSeeds { get; set; }
    }
}
