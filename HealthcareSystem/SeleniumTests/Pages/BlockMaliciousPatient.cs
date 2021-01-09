using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests.Pages
{
    public class BlockMaliciousPatient
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:65117/html/malicious_patients.html";
        private IWebElement BlockButtonElement => driver.FindElement(By.Name("block_malicious"));
        private IWebElement AlertMessageElement => driver.FindElement(By.Name("alert_container"));
        private int NumberOfBlockButtons => driver.FindElements(By.Name("block_malicious")).Count;

        public BlockMaliciousPatient(IWebDriver driver)
        {
            this.driver = driver;
        }

        public string BlockPatient()
        {
            BlockButtonElement.Click();
            return AlertMessageElement.FindElement(By.Name("alert_msg")).Text;
        }

        public int GetNumberOfMaliciousPatients()
        {
            return NumberOfBlockButtons;
        }

        public void WaitForFormSubmit()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe(URI));
        }

        public void Navigate() => driver.Navigate().GoToUrl(URI);
    }
}
