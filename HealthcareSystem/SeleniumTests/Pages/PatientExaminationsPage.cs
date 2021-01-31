using OpenQA.Selenium;

namespace PatientWebAppE2ETests.Pages
{
    public class PatientExaminationsPage
    {
        private readonly IWebDriver driver;
        private readonly string URI;

        public const string InvalidCommentMessage = "Cancelling was not successful.";

        public const string ValidCommentMessage = "Examination successfully cancelled.";


        public PatientExaminationsPage(IWebDriver driver, string uri)
        {
            this.driver = driver;
            URI = uri;
        }

        public string CancelExaminationClick()
        {
            driver.FindElement(By.Name("cancelButton")).Click();
            return driver.FindElement(By.Name("alert_container")).FindElement(By.Name("alert_msg")).Text;
        }

        public void Navigate() => driver.Navigate().GoToUrl(URI);

    }
}
