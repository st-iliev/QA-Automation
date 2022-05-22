using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitProjectTest_Student_RegistryApp.PageObject
{
    public class BasePage
    {
        protected readonly IWebDriver driver;
        public virtual string PageURL { get; }
        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        }
        public IWebElement LinkHomePage => driver.FindElement(By.LinkText("Home"));
        public IWebElement LinkViewStudentPage => driver.FindElement(By.LinkText("View Students"));
        public IWebElement LinkAddStudentPage => driver.FindElement(By.LinkText("Add Student"));
        public IWebElement ElementPageHeading => driver.FindElement(By.XPath("//body/h1"));
        public void Open()
        {
            driver.Navigate().GoToUrl(PageURL);
        }
        public  bool IsOpen()
        {
            return driver.Url == PageURL;

        }
        public string GetPageTitle()
        {
            return driver.Title;
        }
        public string GetPageHeadingText()
        {
            return ElementPageHeading.Text;
        }
    }
}
