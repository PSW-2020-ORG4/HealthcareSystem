using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:65117/html/login.html";
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

        public void WaitForFormSubmit()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe("http://localhost:65117/html/patients_home_page.html"));
        }


        public void Navigate() => driver.Navigate().GoToUrl(URI);
    }
}
