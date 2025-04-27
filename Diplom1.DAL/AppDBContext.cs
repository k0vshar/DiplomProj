using Diplom.Domain;
using Diplom.Domain.Entities;
using Diplom.Domain.Enum;
using Diplom.Domain.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Diplom.DAL
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<Good> Goods { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Discount> Discounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.ToTable("Users").HasKey(x => x.Id);

                builder.HasData(new User[]
                {
                    new User()
                    {
                        Id = 1,
                        Name = "Admin",
                        Password = HashPasswordHelper.HashPassword("123456"),
                        Role = Role.Admin
                    },
                    new User()
                    {
                        Id = 2,
                        Name = "User",
                        Password = HashPasswordHelper.HashPassword("654321"),
                        Role = Role.User
                    }
                });

                builder.Property(x => x.Id).ValueGeneratedOnAdd();

                builder.Property(x => x.Password).IsRequired();
                builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

                builder.HasOne(x => x.Profile)
                    .WithOne(x => x.User)
                    .HasPrincipalKey<User>(x => x.Id)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasOne(x => x.Basket)
                    .WithOne(x => x.User)
                    .HasPrincipalKey<User>(x => x.Id)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Good>(builder =>
            {
                builder.ToTable("Goods").HasKey(x => x.Id);

                builder.HasData(new Good
                {
                    Id = 1,
                    Name = "Adidas XC",
                    Description = new string('A', 50),
                    Tag = Tags.New,
                    Image = null
                });
            });

            modelBuilder.Entity<Profile>(builder =>
            {
                builder.ToTable("Profiles").HasKey(x => x.Id);

                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.Property(x => x.Age);
                builder.Property(x => x.Address).HasMaxLength(200).IsRequired(false);

                builder.HasData(new Profile()
                {
                    Id = 1,
                    UserId = 1
                });
            });

            modelBuilder.Entity<Basket>(builder =>
            {
                builder.ToTable("Baskets").HasKey(x => x.Id);

                builder.HasData(new Basket()
                {
                    Id = 1,
                    UserId = 1
                });
            });

            modelBuilder.Entity<Order>(builder =>
            {
                builder.ToTable("Orders").HasKey(x => x.Id);

                builder.HasOne(r => r.Basket).WithMany(t => t.Orders)
                    .HasForeignKey(r => r.BasketId);
            });
        }
    }
}
