using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests.Pages
{
    class PatientExaminationsPage
    {
        private readonly IWebDriver driver;

        public const string URI = "http://localhost:65117/html/patient_examinations.html";

        private IWebElement ButtonElement => driver.FindElements(By.Name("cancelButton"))[0];

        public string Title => driver.Title;

        public const string InvalidCommentMessage = "Examination successfully cancelled.";

        public const string ValidCommentMessage = "Cancelling was not successful.";


        public PatientExaminationsPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public bool ButtonElementDisplayed()
        {
            return ButtonElement.Displayed;
        }


        public void CancelExaminationClick()
        {
            ButtonElement.Click();
        }

        public void WaitForAlertDialog()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
        }

        public string GetDialogMessage()
        {
            return driver.SwitchTo().Alert().Text;
        }

        public void ResolveAlertDialog()
        {
            driver.SwitchTo().Alert().Accept();
        }

        public void WaitForFormSubmit()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe("http://localhost:65117/html/patient_examinations.html"));
        }

        public void Navigate() => driver.Navigate().GoToUrl(URI);

    }
}
