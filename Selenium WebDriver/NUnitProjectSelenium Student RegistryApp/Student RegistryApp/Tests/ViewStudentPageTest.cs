using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnitProjectTest_Student_RegistryApp.PageObject;

namespace NUnitProjectTest_Student_RegistryApp.Tests
{
    public class ViewStudentPageTest : BaseTest
    {
        [Test]
        public void Test_ViewStudentPage_Content()
        {
            var page = new ViewStudentPage(driver);
            page.Open();
            Assert.AreEqual("Students", page.GetPageTitle());
            Assert.AreEqual("Registered Students", page.GetPageHeadingText());
            var students = page.GetRegistredStudents();
            foreach (var student in students)
            {
                Assert.IsTrue(student.IndexOf("(") > 0);
                Assert.IsTrue(student.LastIndexOf(")") == student.Length-1);
            }
        }
        [Test]
        public void Test_ViewPage_Links()
        {
            var viewStudentPage = new ViewStudentPage(driver);
            viewStudentPage.Open();
            viewStudentPage.LinkHomePage.Click();
            Assert.IsTrue(new HomePage(driver).IsOpen());
            viewStudentPage.Open();
            viewStudentPage.LinkAddStudentPage.Click();
            Assert.IsTrue(new AddStudentPage(driver).IsOpen());
            viewStudentPage.Open();
            viewStudentPage.LinkViewStudentPage.Click();
            Assert.IsTrue(new ViewStudentPage(driver).IsOpen());
        }
    }
}
