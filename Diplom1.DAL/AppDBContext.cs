using Diplom.Domain;
using Diplom.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Diplom.DAL
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Good> Goods { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Basket> Baskets { get; set; }

        public DbSet<Order> Orders { get; set; }
    }
}
