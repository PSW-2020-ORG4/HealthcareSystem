using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace PatientWebAppE2ETests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver driver;
        private readonly string URI;
        private readonly string adminHomePageURI;
        private readonly string patientHomePageURI;
        private IWebElement EmailElement => driver.FindElement(By.Id("email"));
        private IWebElement PasswordElement => driver.FindElement(By.Id("password"));
        private IWebElement SubmitButtonElement => driver.FindElement(By.Id("login"));

        public LoginPage(IWebDriver driver, string uri, string adminUri, string patientUri)
        {
            this.driver = driver;
            URI = uri;
            adminHomePageURI = adminUri;
            patientHomePageURI = patientUri;
        }

        public bool EmailElementDisplayed()
        {
            return EmailElement.Displayed;
        }

        public bool PasswordElementDisplayed()
        {
            return PasswordElement.Displayed;
        }

        public bool SubmitButtonElementDisplayed()
        {
            return SubmitButtonElement.Displayed;
        }

        public void InsertEmail(string email)
        {
            EmailElement.SendKeys(email);
        }

        public void InsertPassword(string password)
        {
            PasswordElement.SendKeys(password);
        }

        public void SubmitForm()
        {
            SubmitButtonElement.Click();
        }

        public void WaitForLoginPatient()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 40));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe(patientHomePageURI));
        }

        public void WaitForLoginAdmin()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 40));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe(adminHomePageURI));
        }

        public void Navigate() => driver.Navigate().GoToUrl(URI);
    }
}
