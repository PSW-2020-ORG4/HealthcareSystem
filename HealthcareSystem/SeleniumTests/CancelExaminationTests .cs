using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
        public void TestSuccessfulCancellation()
        {
            loginPage.InsertEmail("zana998@gmail.com");
            loginPage.InsertPassword("12345678");
            loginPage.SubmitForm();
            loginPage.WaitForLoginPatient();

            patientExaminationsPage = new Pages.PatientExaminationsPage(driver);
            patientExaminationsPage.Navigate();
            Assert.Equal(driver.Url, Pages.PatientExaminationsPage.URI);

            //patientExaminationsPage.Selected
            if (patientExaminationsPage.GetNumberOfFollowingEaminations() > 0)
            {
                Assert.Contains(Pages.PatientExaminationsPage.ValidCommentMessage, patientExaminationsPage.CancelExaminationClick());
            }
            else
            {
                Assert.Equal(0, patientExaminationsPage.GetNumberOfFollowingEaminations());
            }
        }

    }
}
