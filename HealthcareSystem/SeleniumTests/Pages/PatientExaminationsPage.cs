using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumTests.Pages
{
    public class PatientExaminationsPage
    {
        private readonly IWebDriver driver;

        public const string URI = "http://localhost:65117/html/patient_examinations.html";

        private IWebElement ButtonElement => driver.FindElement(By.Name("cancelButton"));

        public const string InvalidCommentMessage = "Cancelling was not successful.";

        public const string ValidCommentMessage = "Examination successfully cancelled.";


        public PatientExaminationsPage(IWebDriver driver)
        {
            this.driver = driver;
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

        public int GetNumberOfFollowingExaminations()
        {
            return driver.FindElements(By.Name("cancelButton")).Count;
        }

        public void WaitForFormSubmit()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe(URI));
        }
        public void Navigate() => driver.Navigate().GoToUrl(URI);

    }
}
