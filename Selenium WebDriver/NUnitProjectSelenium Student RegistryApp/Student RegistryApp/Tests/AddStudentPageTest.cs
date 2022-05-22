using NUnit.Framework;
using NUnitProjectTest_Student_RegistryApp.PageObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitProjectTest_Student_RegistryApp.Tests
{
    public class AddStudentPageTest: BaseTest
    {
        [Test]
        public void Test_AddStudentPage_Content()
        {
            var addStudentPage = new AddStudentPage(driver);
            addStudentPage.Open();
            Assert.AreEqual("Add Student", addStudentPage.GetPageTitle());
            Assert.AreEqual("Register New Student", addStudentPage.GetPageHeadingText());
            Assert.IsEmpty(addStudentPage.FieldStudentName.Text);
            Assert.IsEmpty(addStudentPage.FieldStudentEmail.Text);
            Assert.AreEqual("Add", addStudentPage.ButtonAdd.Text);
        }
        [Test]
        public void Test_AddStudentPage_Links()
        {
            var addStudentPage = new AddStudentPage(driver);
            addStudentPage.Open();
            addStudentPage.LinkHomePage.Click();
            Assert.IsTrue(new HomePage(driver).IsOpen());
            addStudentPage.Open();
            addStudentPage.LinkViewStudentPage.Click();
            Assert.IsTrue(new ViewStudentPage(driver).IsOpen());
            addStudentPage.Open();
            addStudentPage.LinkAddStudentPage.Click();
            Assert.IsTrue(new AddStudentPage(driver).IsOpen());
        }
        [Test]
        public void Test_AddStudentPage_AddValidStudent()
        {
            var homePage = new HomePage(driver);
            homePage.Open();
            var studentCount = homePage.ElementStudentCount.Text;
            var addStudentPage = new AddStudentPage(driver);
            addStudentPage.Open();
            string name = "testStudent" + DateTime.Now.Ticks;
            string email = "studentEmail" + DateTime.Now.Ticks + "@test.bg";
            addStudentPage.AddStudent(name, email);
            var viewStudentPage = new ViewStudentPage(driver);
            viewStudentPage.Open();
            Assert.That(viewStudentPage.GetRegistredStudents().Contains($"{name} ({email})"));
            homePage.Open();
            Assert.Greater(int.Parse(homePage.ElementStudentCount.Text), int.Parse(studentCount));
        }
        [Test]
        public void Test_AddStudentPage_AddInvalidStudent()
        {
            var addStudentPage = new AddStudentPage(driver);
            addStudentPage.Open();
            string name = "";
            string email = "adddz@aeed";
            addStudentPage.AddStudent(name, email);
            Assert.IsTrue(new AddStudentPage(driver).IsOpen());
            addStudentPage.ButtonAdd.Click();
            Assert.AreEqual("Cannot add student. Name and email fields are required!", addStudentPage.ElementErrorMessage.Text);
            addStudentPage.GetErrorMsg();
        }
    }
}
