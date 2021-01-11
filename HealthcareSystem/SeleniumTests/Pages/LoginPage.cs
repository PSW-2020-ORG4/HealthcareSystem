using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace PatientWebAppE2ETests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver driver;
        public const string URI = "https://psw-patientwebapp.herokuapp.com/html/login.html";
        private IWebElement EmailElement => driver.FindElement(By.Id("email"));
        private IWebElement PasswordElement => driver.FindElement(By.Id("password"));
        private IWebElement SubmitButtonElement => driver.FindElement(By.Id("login"));

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
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
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe("https://psw-patientwebapp.herokuapp.com/html/patients_home_page.html"));
        }

        public void WaitForLoginAdmin()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe("https://psw-patientwebapp.herokuapp.com/html/admins_home_page.html"));
        }

        public void Navigate() => driver.Navigate().GoToUrl(URI);
    }
}
