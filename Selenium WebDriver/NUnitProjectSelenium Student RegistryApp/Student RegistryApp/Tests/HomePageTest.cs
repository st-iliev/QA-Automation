using NUnit.Framework;
using NUnitProjectTest_Student_RegistryApp.PageObject;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitProjectTest_Student_RegistryApp.Tests
{
    public class HomePageTest : BaseTest
    {
        [Test]
        public void Test_HomePage_Content()
        {
            var page = new HomePage(driver);
            page.Open();
            Assert.AreEqual("MVC Example", page.GetPageTitle());
            Assert.AreEqual("Students Registry", page.GetPageHeadingText());
            page.GetStudentCount();
        }
        [Test]
        public void Test_HomePage_Links()
        {
            var homePage = new HomePage(driver);
            homePage.Open();
            homePage.LinkHomePage.Click();
            Assert.IsTrue(new HomePage(driver).IsOpen());
            homePage.Open();
            homePage.LinkAddStudentPage.Click();
            Assert.IsTrue(new AddStudentPage(driver).IsOpen());
            homePage.Open();
            homePage.LinkViewStudentPage.Click();
            Assert.IsTrue(new ViewStudentPage(driver).IsOpen());
        }

            

    }
}
