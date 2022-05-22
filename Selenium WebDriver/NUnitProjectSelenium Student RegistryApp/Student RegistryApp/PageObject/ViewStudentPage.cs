using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitProjectTest_Student_RegistryApp.PageObject
{
    public class ViewStudentPage : BasePage
    {
        public ViewStudentPage(IWebDriver driver) : base(driver)
        {
        }
        public override string PageURL => "https://mvc-app-node-express.nakov.repl.co/students";
        public IReadOnlyCollection<IWebElement> ListItemsStudents => driver.FindElements(By.XPath("//ul/li"));
        public string[] GetRegistredStudents()
        {
            var elementStudents = this.ListItemsStudents.Select(s=>s.Text).ToArray();
            return elementStudents;
        }
    }
}
