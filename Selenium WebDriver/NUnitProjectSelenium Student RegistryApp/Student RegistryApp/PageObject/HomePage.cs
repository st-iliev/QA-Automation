using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitProjectTest_Student_RegistryApp.PageObject
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver)
        {
        }
        public override string PageURL => "https://mvc-app-node-express.nakov.repl.co/";
        public IWebElement ElementStudentCount => driver.FindElement(By.XPath("//body/p/b"));
        public string GetStudentCount()
        {
            return ElementStudentCount.Text;
        }
    }
}
