using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace PatientWebAppE2ETests.Pages
{
    public class PublishFeedbackPage
    {
        private readonly IWebDriver driver;
        private readonly string URI;

        public const string ValidCommentMessage = "Feedback successfully published.";
        public PublishFeedbackPage(IWebDriver driver, string uri)
        {
            this.driver = driver;
            URI = uri;
        }

        public string Publish()
        {
            driver.FindElement(By.Name("publish")).Click();
            return driver.FindElement(By.Name("alert_container")).FindElement(By.Name("alert_msg")).Text;
        }

        public void Navigate() => driver.Navigate().GoToUrl(URI);
    }
}


