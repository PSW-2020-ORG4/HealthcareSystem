using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using Xunit;

namespace SeleniumTests
{
    public class PublishFeedbackTests : IDisposable
    {
        private readonly IWebDriver driver;
        private Pages.PublishFeedback publishFeedbackPage;
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
        public void SuccessfulPublishingTest()
        {
            loginPage.InsertEmail("milic_milan@gmail.com");
            loginPage.InsertPassword("milanmilic965");
            loginPage.SubmitForm();
            loginPage.WaitForLoginAdmin();

            publishFeedbackPage = new Pages.PublishFeedback(driver);
            publishFeedbackPage.Navigate();
            Assert.Equal(driver.Url, Pages.PublishFeedback.URI);


            if (publishFeedbackPage.GetNumberOfUnpublishedFeedback() > 0)
                Assert.Contains("Feedback successfully published.", publishFeedbackPage.Publish());
            else
                Assert.Equal(0, publishFeedbackPage.GetNumberOfUnpublishedFeedback());

        }
    }
}
