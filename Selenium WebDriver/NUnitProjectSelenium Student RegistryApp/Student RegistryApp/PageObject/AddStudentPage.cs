using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitProjectTest_Student_RegistryApp.PageObject
{
    public class AddStudentPage : BasePage
    {
        public AddStudentPage(IWebDriver driver) : base(driver)
        {
        }
        public override string PageURL => "https://mvc-app-node-express.nakov.repl.co/add-student";
        public IWebElement ElementErrorMessage => driver.FindElement(By.XPath("//div"));
        public IWebElement FieldStudentName => driver.FindElement(By.Id("name"));
        public IWebElement FieldStudentEmail => driver.FindElement(By.Id("email"));
        public IWebElement ButtonAdd => driver.FindElement(By.XPath("//form/button"));
        public void AddStudent(string name, string email)
        {
            FieldStudentName.SendKeys(name);
            FieldStudentEmail.SendKeys(email);
            ButtonAdd.Click();
        }
        public string GetErrorMsg()
        {
            string errorMessage = ElementErrorMessage.Text;
            return errorMessage;
        }
    }
}
