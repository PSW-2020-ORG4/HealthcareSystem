using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using Xunit;

namespace PatientWebAppE2ETests
{
    public class PublishFeedbackTests : IDisposable
    {
        private readonly IWebDriver driver;
        private Pages.PublishFeedbackPage publishFeedbackPage;
        private Pages.LoginPage loginPage;

        public PublishFeedbackTests()
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
        public void SuccessfulPublishingTest()
        {
            try
            {
                loginPage.InsertEmail("milic_milan@gmail.com");
                loginPage.InsertPassword("milanmilic965");
                loginPage.SubmitForm();
                loginPage.WaitForLoginAdmin();

                publishFeedbackPage = new Pages.PublishFeedbackPage(driver, Configuration.PublishFeedbackPageURI);
                publishFeedbackPage.Navigate();
                Assert.Equal(driver.Url, Configuration.PublishFeedbackPageURI);

                Assert.Contains(Pages.PublishFeedbackPage.ValidCommentMessage, publishFeedbackPage.Publish());
            }
            finally
            {
                Dispose();
            }
        }
    }
}
