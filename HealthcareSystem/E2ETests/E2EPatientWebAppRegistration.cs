using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace E2ETests
{
    public class E2EPatientWebAppRegistration
    {
        [Fact]
        public void TestSubmitRegistration()
        {
            IWebDriver driver = new FirefoxDriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl("https://vlaksi-patientwebapp.herokuapp.com/html/index.html");

            driver.FindElement(By.ClassName("nav-link")).Click();
            driver.FindElement(By.Id("register")).Click();
        }
    }
}
