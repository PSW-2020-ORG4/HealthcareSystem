using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace PatientWebAppE2ETests.Pages
{
    public class BlockMaliciousPatient
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:65117/html/malicious_patients.html";

        public const string ValidCommentMessage = "Patient was successfully blocked.";

        public BlockMaliciousPatient(IWebDriver driver)
        {
            this.driver = driver;
        }

        public string BlockPatient()
        {
            driver.FindElement(By.Name("block_malicious")).Click();
            return driver.FindElement(By.Name("alert_container")).FindElement(By.Name("alert_msg")).Text;
        }

        public void Navigate() => driver.Navigate().GoToUrl(URI);
    }
}
