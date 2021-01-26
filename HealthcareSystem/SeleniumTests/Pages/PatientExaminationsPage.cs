using OpenQA.Selenium;

namespace PatientWebAppE2ETests.Pages
{
    public class PatientExaminationsPage
    {
        private readonly IWebDriver driver;
        private readonly string URI;

        private IWebElement ButtonElement => driver.FindElement(By.Name("cancelButton"));

        public const string InvalidCommentMessage = "Cancelling was not successful.";

        public const string ValidCommentMessage = "Examination successfully cancelled.";


        public PatientExaminationsPage(IWebDriver driver, string uri)
        {
            this.driver = driver;
            URI = uri;
        }

        public bool ButtonElementDisplayed()
        {
            return ButtonElement.Displayed;
        }

        public string CancelExaminationClick()
        {
            ButtonElement.Click();
            return driver.FindElement(By.Name("alert_container")).FindElement(By.Name("alert_msg")).Text;
        }

        public void Navigate() => driver.Navigate().GoToUrl(URI);

    }
}
