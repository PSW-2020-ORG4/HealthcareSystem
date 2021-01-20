using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace PatientWebAppE2ETests.Pages
{
    public class PublishFeedback
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:65117/html/admins_home_page.html";

        public const string ValidCommentMessage = "Feedback successfully published.";
        public PublishFeedback(IWebDriver driver)
        {
            this.driver = driver;
        }

        public string Publish()
        {
            driver.FindElement(By.Name("publish")).Click();
            return driver.FindElement(By.Name("alert_container")).FindElement(By.Name("alert_msg")).Text;
        }

        public void Navigate() => driver.Navigate().GoToUrl(URI);
    }
}


