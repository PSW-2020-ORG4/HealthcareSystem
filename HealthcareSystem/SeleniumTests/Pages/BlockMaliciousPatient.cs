using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace PatientWebAppE2ETests.Pages
{
    public class BlockMaliciousPatient
    {
        private readonly IWebDriver driver;
        public const string URI = "https://psw-patientwebapp.herokuapp.com/html/malicious_patients.html";

        public BlockMaliciousPatient(IWebDriver driver)
        {
            this.driver = driver;
        }

        public string BlockPatient()
        {
            driver.FindElement(By.Name("block_malicious")).Click();
            return driver.FindElement(By.Name("alert_container")).FindElement(By.Name("alert_msg")).Text;
        }

        public int GetNumberOfUnblockedMaliciousPatients()
        {
            return driver.FindElements(By.Name("block_malicious")).Count;
        }

        public void WaitForFormSubmit()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe(URI));
        }

        public void Navigate() => driver.Navigate().GoToUrl(URI);
    }
}
