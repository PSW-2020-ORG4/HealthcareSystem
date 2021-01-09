using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using Xunit;

namespace SeleniumTests
{
    public class BlockMaliciousPatientTests : IDisposable
    {
        private readonly IWebDriver driver;
        private Pages.BlockMaliciousPatient blockPatientPage;
        private Pages.LoginPage loginPage;

        public BlockMaliciousPatientTests()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.AddArguments("--disable-extensions");
            options.AddArguments("--disable-gpu");
            options.AddArguments("--disable-dev-shm-usage");
            options.AddArguments("--no-sandbox");
            options.AddArguments("--disable-notifications");

            driver = new FirefoxDriver(options);

            loginPage = new Pages.LoginPage(driver);
            loginPage.Navigate();
            Assert.Equal(driver.Url, Pages.LoginPage.URI);
        }
        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Fact]
        public void SuccessfulBlockTest()
        {
            loginPage.InsertEmail("milic_milan@gmail.com");
            loginPage.InsertPassword("milanmilic965");
            loginPage.SubmitForm();
            loginPage.WaitForLoginAdmin();

            blockPatientPage = new Pages.BlockMaliciousPatient(driver);
            blockPatientPage.Navigate();
            Assert.Equal(driver.Url, Pages.BlockMaliciousPatient.URI);


            if (blockPatientPage.GetNumberOfUnblockedMaliciousPatients() > 0)
                Assert.Contains("Patient was successfully blocked.", blockPatientPage.BlockPatient());
            else
                Assert.Equal(0, blockPatientPage.GetNumberOfUnblockedMaliciousPatients());

        }

    }
}
