using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using Xunit;

namespace PatientWebAppE2ETests
{
    public class BlockMaliciousPatientTests : IDisposable
    {
        private readonly IWebDriver driver;
        private Pages.BlockMaliciousPatientPage blockPatientPage;
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

            loginPage = new Pages.LoginPage(driver,
                                            Configuration.LoginPageURI,
                                            Configuration.AdminHomePageURI,
                                            Configuration.PatientHomePageURI);
            loginPage.Navigate();
            Assert.Equal(driver.Url, Configuration.LoginPageURI);
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Fact]
        public void SuccessfulBlockTest()
        {
            try
            {
                loginPage.InsertEmail("milic_milan@gmail.com");
                loginPage.InsertPassword("milanmilic965");
                loginPage.SubmitForm();
                loginPage.WaitForLoginAdmin();

                blockPatientPage = new Pages.BlockMaliciousPatientPage(driver, Configuration.BlockMaliciousPatientPageURI);
                blockPatientPage.Navigate();
                Assert.Equal(driver.Url, Configuration.BlockMaliciousPatientPageURI);

                Assert.Contains(Pages.BlockMaliciousPatientPage.ValidCommentMessage, blockPatientPage.BlockPatient());
            }
            finally
            {
                Dispose();
            }
        }

    }
}
