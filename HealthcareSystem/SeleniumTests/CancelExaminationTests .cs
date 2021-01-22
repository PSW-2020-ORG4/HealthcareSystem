using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Xunit;

namespace PatientWebAppE2ETests
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
            try
            {
                loginPage.InsertEmail("ana_anic98@gmail.com");
                loginPage.InsertPassword("11111111");
                loginPage.SubmitForm();
                loginPage.WaitForLoginPatient();

                patientExaminationsPage = new Pages.PatientExaminationsPage(driver);
                patientExaminationsPage.Navigate();
                Assert.Equal(driver.Url, Pages.PatientExaminationsPage.URI);

                Assert.Contains(Pages.PatientExaminationsPage.ValidCommentMessage, patientExaminationsPage.CancelExaminationClick());
            }
            finally
            {
                Dispose();
            }
        }

    }
}
