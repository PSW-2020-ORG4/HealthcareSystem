using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace PatientWebAppE2ETests.Pages
{
    public class AddFeedbackPage
    {
        private readonly IWebDriver driver;
        private readonly string URI;
        private IWebElement CommentElement => driver.FindElement(By.Id("text_area_id"));
        private IWebElement IsAnonymousElement => driver.FindElement(By.XPath("//input[@id = 'no_anonymous']"));
        private IWebElement IsAllowedElement => driver.FindElement(By.XPath(".//div[contains(.,'I don't want my feedback to be published')]/input"));
        private IWebElement SubmitButtonElement => driver.FindElement(By.Id("submit_button"));
        private IWebElement AlertElement => driver.FindElement(By.Id("alert"));

        public const string ValidCommentMessage = "You have successfuly left a feedback";
        public const string InvalidCommentMessage = "Feedback cannot be empty";

        public AddFeedbackPage(IWebDriver driver, string uri)
        {
            this.driver = driver;
            URI = uri;
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
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe(URI));
        }

        public void Navigate() => driver.Navigate().GoToUrl(URI);
    }
}
