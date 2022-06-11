using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;
using System.Threading;

namespace ContactBook_Selenium_UI_Automation_Tests
{
    public class Tests
    {
        private const string url = "https://contactbook.nakov.repl.co/";
        WebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [Test]
        public void ContactBookTest_CheckFirstContact()
        {
            driver.Navigate().GoToUrl(url);
            var contactLink = driver.FindElement(By.LinkText("Contacts"));

            contactLink.Click();

            var firstName = driver.FindElement(By.CssSelector("#contact1 > tbody > tr.fname > td"));
            var lastName = driver.FindElement(By.CssSelector("#contact1 > tbody > tr.lname > td"));

            Assert.AreEqual(firstName.Text, "Steve");
            Assert.AreEqual(lastName.Text, "Jobs");

        }
        [Test]
        public void ContactBookTest_SearchValidContact_CheckFirstContact()
        {
            driver.Navigate().GoToUrl(url);
            var searchContactLink = driver.FindElement(By.LinkText("Search"));

            searchContactLink.Click();
            var searchField = driver.FindElement(By.Id("keyword"));
            searchField.SendKeys("Albert");
            var searchButton = driver.FindElement(By.Id("search"));
            searchButton.Click();

            var resultMsg = driver.FindElement(By.Id("searchResult"));
            Assert.That(resultMsg.Text, Is.EqualTo("1 contacts found."));

            var firstName = driver.FindElement(By.CssSelector("#contact3 > tbody > tr.fname > td"));
            var lastName = driver.FindElement(By.CssSelector("#contact3 > tbody > tr.lname > td"));

            Assert.AreEqual(firstName.Text, "Albert");
            Assert.AreEqual(lastName.Text, "Einstein");

        }
        [Test]
        public void ContactBookTest_SearchInvalidContact_CheckFirstContact()
        {
            driver.Navigate().GoToUrl(url);
            var searchContactLink = driver.FindElement(By.LinkText("Search"));

            searchContactLink.Click();
            var searchField = driver.FindElement(By.Id("keyword"));
            searchField.SendKeys("invalid2635");
            var searchButton = driver.FindElement(By.Id("search"));
            searchButton.Click();

            var resultMsg = driver.FindElement(By.Id("searchResult"));
            Assert.That(resultMsg.Text, Is.EqualTo("No contacts found."));
        }
        [Test]
        public void ContactBookTest_CreateNewInvalidContact()
        {
            driver.Navigate().GoToUrl(url);
            var createLink = driver.FindElement(By.LinkText("Create"));

            createLink.Click();
            var firstNameField = driver.FindElement(By.Id("firstName"));
            firstNameField.SendKeys("AlAbala");
            var emailField = driver.FindElement(By.Id("email"));
            emailField.SendKeys("alabala@abv.bg");
            var phoneField = driver.FindElement(By.Id("phone"));
            phoneField.SendKeys("0887369581");
            var createButton = driver.FindElement(By.Id("create"));
            createButton.Click();

            var resultMsg = driver.FindElement(By.CssSelector("body > main > div"));
            Assert.That(resultMsg.Text, Is.EqualTo("Error: Last name cannot be empty!"));
        }
        [Test]
        public void ContactBookTest_CreateNewValidContact()
        {
            driver.Navigate().GoToUrl(url);
            var createLink = driver.FindElement(By.LinkText("Create"));
            string firstName = "MyFirstName" + DateTime.Now.Ticks;
            string lastName = "MyLastName" + DateTime.Now.Ticks;
            string email = +DateTime.Now.Ticks + "MyName@abv.bg";
            createLink.Click();
            var firstNameField = driver.FindElement(By.Id("firstName"));
            firstNameField.SendKeys(firstName);
            var lastNameField = driver.FindElement(By.Id("lastName"));
            lastNameField.SendKeys(lastName);
            var emailField = driver.FindElement(By.Id("email"));
            emailField.SendKeys(email);
            var createButton = driver.FindElement(By.Id("create"));
            createButton.Click();

            var contacts = driver.FindElements(By.CssSelector("table.contact-entry"));
            var lastContact = contacts.Last();
            var firstNameLabel = lastContact.FindElement(By.CssSelector("table > tbody > tr.fname > td")).Text;
            var lastNameLabel = lastContact.FindElement(By.CssSelector("table > tbody > tr.lname > td")).Text;

            Assert.AreEqual(firstName, firstNameLabel);
            Assert.AreEqual(lastName, lastNameLabel);
        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}