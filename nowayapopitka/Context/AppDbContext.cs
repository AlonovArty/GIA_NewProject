using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using nowayapopitka.Model;

namespace nowayapopitka.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<orderStatus> orderStatuses { get; set; }
        public DbSet<pickUpPoint> pickUpPoints { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string Connection = "Server=ALONOVKA;User=Alono;Database=Shadow;TrustServerCertificate=true;Trusted_Connection=true";
            optionsBuilder.UseSqlServer(Connection);
        }

    }
}
