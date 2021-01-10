using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using Xunit;

namespace SeleniumTests
{
    public class CancelExaminationTests
    {
        private readonly IWebDriver driver;
        private Pages.PatientExaminationsPage patientExaminationsPage;
        private Pages.LoginPage loginPage;

        public CancelExaminationTests()
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
        public void TestSuccessfulCancellation()
        {
            loginPage.InsertEmail("zana998@gmail.com");
            loginPage.InsertPassword("12345678");
            loginPage.SubmitForm();
            loginPage.WaitForLoginPatient();

            patientExaminationsPage = new Pages.PatientExaminationsPage(driver);
            patientExaminationsPage.Navigate();
            Assert.Equal(driver.Url, Pages.PatientExaminationsPage.URI);

            if (patientExaminationsPage.GetNumberOfFollowingExaminations() > 0)
                Assert.Contains(Pages.PatientExaminationsPage.ValidCommentMessage, patientExaminationsPage.CancelExaminationClick());
            else
                Assert.Equal(0, patientExaminationsPage.GetNumberOfFollowingExaminations());
        }

    }
}
