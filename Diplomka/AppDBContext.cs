using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppStore.Models;


namespace AppStore
{
    internal class AppDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public AppDBContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySql("Server=localHost; Database=diplomka; Port=3306; User=root; Password=root");
        }
    }
}
