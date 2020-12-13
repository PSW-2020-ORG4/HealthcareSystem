using Microsoft.EntityFrameworkCore;
using Model.Users;

namespace Backend.Model
{
    public class DbContextWithTestData : MyDbContext
    {
        private static string connectionString = $"server='dummy' ;userid='dummy'; pwd='dummy';"
                                                + $"port='3306'; database='dummy'";
        private static DbContextOptionsBuilder<MyDbContext> dummy =
            new DbContextOptionsBuilder<MyDbContext>().UseMySql(connectionString);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Country>().HasData(new Country() { Id = 1, Name = "Srbija" });

            builder.Entity<City>().HasData(new City() { ZipCode = 21000, Name = "Novi Sad", CountryId = 1 });
        }

        public DbContextWithTestData(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbContextWithTestData() : base(dummy.Options) { }

    }
}
