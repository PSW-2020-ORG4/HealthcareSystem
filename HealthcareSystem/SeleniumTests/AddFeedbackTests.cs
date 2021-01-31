using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using Xunit;

namespace PatientWebAppE2ETests
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
        public void TestSuccessfulSubmit()
        {
            try
            {
                loginPage.InsertEmail("ana_anic98@gmail.com");
                loginPage.InsertPassword("11111111");
                loginPage.SubmitForm();
                loginPage.WaitForLoginPatient();

                addFeedbackPage = new Pages.AddFeedbackPage(driver, Configuration.AddFeedbackPageURI);
                addFeedbackPage.Navigate();
                Assert.Equal(driver.Url, Configuration.AddFeedbackPageURI);

                addFeedbackPage.InsertComment("Sve je super!");
                addFeedbackPage.InsertIsAnonymous("yes");
                addFeedbackPage.InsertIsAllowed("yes");

                addFeedbackPage.SubmitForm();
                addFeedbackPage.WaitForFormSubmit();

                Assert.Contains(Pages.AddFeedbackPage.ValidCommentMessage, addFeedbackPage.GetDialogMessage());
            }
            finally
            {
                Dispose();
            }
        }

        [Fact]
        public void TestInvalidComment()
        {
            try
            {
                loginPage.InsertEmail("ana_anic98@gmail.com");
                loginPage.InsertPassword("11111111");
                loginPage.SubmitForm();
                loginPage.WaitForLoginPatient();

                addFeedbackPage = new Pages.AddFeedbackPage(driver, Configuration.AddFeedbackPageURI);
                addFeedbackPage.Navigate();
                Assert.Equal(driver.Url, Configuration.AddFeedbackPageURI);

                addFeedbackPage.InsertIsAnonymous("yes");
                addFeedbackPage.InsertIsAllowed("yes");

                addFeedbackPage.SubmitForm();
                addFeedbackPage.WaitForFormSubmit();

                Assert.Contains(Pages.AddFeedbackPage.InvalidCommentMessage, addFeedbackPage.GetDialogMessage());
            }
            finally
            {
                Dispose();
            }

        }

    }
}
