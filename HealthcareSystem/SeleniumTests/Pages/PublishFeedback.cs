using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumTests.Pages
{
    public class PublishFeedback
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:65117/html/admins_home_page.html";
        public PublishFeedback(IWebDriver driver)
        {
            this.driver = driver;
        }

        public string Publish()
        {
            driver.FindElement(By.Name("publish")).Click();
            return driver.FindElement(By.Name("alert_container")).FindElement(By.Name("alert_msg")).Text;
        }

        public int GetNumberOfUnpublishedFeedback()
        {
            return driver.FindElements(By.Name("publish")).Count;
        }

        public void WaitForFormSubmit()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe(URI));
        }

        public void Navigate() => driver.Navigate().GoToUrl(URI);
    }
}


