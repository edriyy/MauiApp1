using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1
{
    public class AppDbContext : DbContext
    {
        private readonly string _dbPath;

        public DbSet<UserAccount> Users { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; } 
        


        public AppDbContext(string dbPath)
        {
            _dbPath = dbPath;
            Database.EnsureCreated();
        }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_dbPath}");
        }
        
    }
}
