using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("start-maximized");
            options.AddArguments("disable-infobars");
            options.AddArguments("--disable-extensions");
            options.AddArguments("--disable-gpu");
            options.AddArguments("--disable-dev-shm-usage");
            options.AddArguments("--no-sandbox");
            options.AddArguments("--disable-notifications");

            driver = new ChromeDriver(options);

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
        public void TestUnloggedPatient()
        {
            addFeedbackPage = new Pages.AddFeedbackPage(driver);
            addFeedbackPage.Navigate();
            Assert.Equal(driver.Url, Pages.LoginPage.URI);    
        }

        [Fact]
        public void TestInvalidLogin()
        {
            loginPage.InsertEmail("ana_anic98@gmail.com");
            loginPage.InsertPassword("fdfdfd");
            loginPage.SubmitForm();

            addFeedbackPage = new Pages.AddFeedbackPage(driver);
            addFeedbackPage.Navigate();

            Assert.Equal(driver.Url, Pages.LoginPage.URI);

        }

        [Fact]
        public void TestSuccessfulSubmit() 
        {
            loginPage.InsertEmail("ana_anic98@gmail.com");
            loginPage.InsertPassword("11111111");
            loginPage.SubmitForm();
            loginPage.WaitForLoginPatient();

            addFeedbackPage = new Pages.AddFeedbackPage(driver);
            addFeedbackPage.Navigate();
            Assert.Equal(driver.Url, Pages.AddFeedbackPage.URI);

            addFeedbackPage.InsertComment("Sve je super!");
            addFeedbackPage.InsertIsAnonymous("yes");
            addFeedbackPage.InsertIsAllowed("yes");

            addFeedbackPage.SubmitForm();
            addFeedbackPage.WaitForFormSubmit();

            Assert.Equal("Success", Pages.AddFeedbackPage.ValidCommentMessage);
        }

        [Fact]
        public void TestInvalidComment()
        {
            loginPage.InsertEmail("ana_anic98@gmail.com");
            loginPage.InsertPassword("11111111");
            loginPage.SubmitForm();
            loginPage.WaitForLoginPatient();

            addFeedbackPage = new Pages.AddFeedbackPage(driver);
            addFeedbackPage.Navigate();
            Assert.Equal(driver.Url, Pages.AddFeedbackPage.URI);

            addFeedbackPage.InsertIsAnonymous("yes");
            addFeedbackPage.InsertIsAllowed("yes");

            addFeedbackPage.SubmitForm();

            Assert.Equal("Not success", Pages.AddFeedbackPage.InvalidCommentMessage);
            
        }

    }
}
