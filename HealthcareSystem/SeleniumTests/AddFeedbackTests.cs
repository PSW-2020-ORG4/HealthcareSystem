using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Xunit;

namespace SeleniumTests
{
    public class AddFeedbackTests : IDisposable
    {
        private readonly IWebDriver driver;
        private Pages.AddFeedbackPage addFeedbackPage;
        private Pages.LoginPage loginPage;

        public AddFeedbackTests()
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
        public void TestSuccessfulSubmit() 
        {
            loginPage.InsertEmail("ana_anic98@gmail.com");
            loginPage.InsertPassword("11111111");
            loginPage.SubmitForm();
            loginPage.WaitForFormSubmit();

            addFeedbackPage = new Pages.AddFeedbackPage(driver);
            addFeedbackPage.Navigate();
            Assert.Equal(driver.Url, Pages.AddFeedbackPage.URI);

            addFeedbackPage.InsertComment("Sve je super!");
            addFeedbackPage.InsertIsAnonymous("yes");
            addFeedbackPage.InsertIsAllowed("yes");

            addFeedbackPage.SubmitForm();
            addFeedbackPage.WaitForFormSubmit();

            Assert.Contains("You have successfuly left a feedback", addFeedbackPage.GetDialogMessage());
        }

        [Fact]
        public void TestInvalidComment()
        {
            loginPage.InsertEmail("ana_anic98@gmail.com");
            loginPage.InsertPassword("11111111");
            loginPage.SubmitForm();
            loginPage.WaitForFormSubmit();

            addFeedbackPage = new Pages.AddFeedbackPage(driver);
            addFeedbackPage.Navigate();
            Assert.Equal(driver.Url, Pages.AddFeedbackPage.URI);

            addFeedbackPage.InsertIsAnonymous("yes");
            addFeedbackPage.InsertIsAllowed("yes");

            addFeedbackPage.SubmitForm();
            addFeedbackPage.WaitForFormSubmit();

            Assert.Contains("Feedback cannot be empty", addFeedbackPage.GetDialogMessage());

        }

    }
}
