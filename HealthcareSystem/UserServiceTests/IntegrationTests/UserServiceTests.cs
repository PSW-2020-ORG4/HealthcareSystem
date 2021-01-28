using Backend.Model;
using Microsoft.EntityFrameworkCore;
using System;
using UserService.CustomException;
using UserService.Model;
using UserService.Repository.Implementation;
using Xunit;

namespace UserServiceTests.IntegrationTests
{
    public class UserServiceTests
    {
        private UserService.Service.Implementation.UserService SetupRepositoryAndService()
        {
            DataSeeder dataSeeder = new DataSeeder();
            DbContextOptionsBuilder<MyDbContext> builder = new DbContextOptionsBuilder<MyDbContext>(); ;
            DbContextOptions<MyDbContext> options;
            MyDbContext context;
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            context = new MyDbContext(options);

            dataSeeder.SeedAll(context);

            var userRepo = new UserRepository(context);
            return new UserService.Service.Implementation.UserService(userRepo);
        }

        [Fact]
        public void SuccessLoginPatient()
        {
            UserService.Service.Implementation.UserService userService = SetupRepositoryAndService();
            UserAccount userAccount = userService.GetByEmailAndPassword("ana_anic98@gmail.com", "11111111");

            Assert.False(userAccount is null);
        }

        [Fact]
        public void SuccessLoginAdmin()
        {
            UserService.Service.Implementation.UserService userService = SetupRepositoryAndService();
            UserAccount userAccount = userService.GetByEmailAndPassword("milic_milan@gmail.com", "milanmilic965");

            Assert.False(userAccount is null);
        }

        [Fact]
        public void NotSuccessLogin()
        {
            UserService.Service.Implementation.UserService userService = SetupRepositoryAndService();
            try
            {
                UserAccount userAccount = userService.GetByEmailAndPassword("bla bla", "bla");
            }
            catch (Exception ex)
            {
                Assert.True(ex is NotFoundException);
            }
        }
    }
}
