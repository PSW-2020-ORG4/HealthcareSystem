using System;
using System.Collections.Generic;
using System.Text;
using Backend.Model;
using Microsoft.AspNetCore.Mvc;
using UserService.CustomException;
using UserService.Model;
using UserService.Repository;
using UserService.Repository.Implementation;
using UserService.Service.Implementation;
using Xunit;

namespace UserServiceTests.IntegrationTests
{
    public class UserServiceTests
    {
        private UserServiceClass SetupRepositoryAndService()
        {
            DbContextInMemory testData = new DbContextInMemory();
            testData.SeedDatas();
            MyDbContext context = testData._context;
            var userRepo = new UserRepository(context);
            return new UserServiceClass(userRepo);
        }

        [Fact]
        public void SuccessLoginPatient()
        {
            UserServiceClass userService = SetupRepositoryAndService();
            UserAccount userAccount = userService.GetByEmailAndPassword("ana_anic98@gmail.com", "11111111");

            Assert.False(userAccount is null);
        }

        [Fact]
        public void SuccessLoginAdmin()
        {
            UserServiceClass userService = SetupRepositoryAndService();
            UserAccount userAccount = userService.GetByEmailAndPassword("milic_milan@gmail.com", "milanmilic965");

            Assert.False(userAccount is null);
        }

        [Fact]
        public void NotSuccessLogin()
        {
            UserServiceClass userService = SetupRepositoryAndService();
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
