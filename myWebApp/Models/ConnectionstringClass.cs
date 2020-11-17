using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;



namespace myWebApp.Models
{
    public class ConnectionstringClass: DbContext
    {
        public ConnectionstringClass(DbContextOptions<ConnectionstringClass> options) :base(options)
        {

        }
        public DbSet<Reservation> Reservation {get; set;}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=Login;User Id=postgres;Password=123456");

        public override int SaveChanges()  
        {  
            ChangeTracker.DetectChanges();  
            return base.SaveChanges();  
        }  
    }
}