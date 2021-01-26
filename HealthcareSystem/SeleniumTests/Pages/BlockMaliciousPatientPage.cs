using OpenQA.Selenium;

namespace PatientWebAppE2ETests.Pages
{
    public class BlockMaliciousPatientPage
    {
        private readonly IWebDriver driver;
        private readonly string URI;

        public const string ValidCommentMessage = "Patient was successfully blocked.";

        public BlockMaliciousPatientPage(IWebDriver driver, string uri)
        {
            this.driver = driver;
            URI = uri;
        }

        public string BlockPatient()
        {
            driver.FindElement(By.Name("block_malicious")).Click();
            return driver.FindElement(By.Name("alert_container")).FindElement(By.Name("alert_msg")).Text;
        }

        public void Navigate() => driver.Navigate().GoToUrl(URI);
    }
}
