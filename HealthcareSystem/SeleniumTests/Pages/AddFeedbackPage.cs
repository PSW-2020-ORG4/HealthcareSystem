using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests.Pages
{
    public class AddFeedbackPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:65117/html/add_feedback.html";
        private IWebElement CommentElement => driver.FindElement(By.Id("text_area_id"));
        private IWebElement IsAnonymousElement => driver.FindElement(By.XPath("//input[@id = 'no_anonymous']"));
        private IWebElement IsAllowedElement => driver.FindElement(By.XPath(".//div[contains(.,'I don't want my feedback to be published')]/input"));
        private IWebElement SubmitButtonElement => driver.FindElement(By.Id("submit_button"));
        private IWebElement AlertElement => driver.FindElement(By.Id("alert"));

        public AddFeedbackPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void InsertComment(string comment)
        {
            CommentElement.SendKeys(comment);
        }

        public void InsertIsAnonymous(string isAnonymous)
        {
            if (isAnonymous.Equals("no"))
            {
                IsAnonymousElement.Click();
            }
        }

        public void InsertIsAllowed(string isAllowed)
        {
            if (isAllowed.Equals("no"))
            {
                IsAllowedElement.Click();
            }
        }

        public void SubmitForm()
        {
            SubmitButtonElement.Click();
        }

        public string GetDialogMessage()
        {
            return AlertElement.Text;
        }

        public void WaitForFormSubmit()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe("http://localhost:65117/html/add_feedback.html"));
        }

        public void Navigate() => driver.Navigate().GoToUrl(URI);
    }
}
