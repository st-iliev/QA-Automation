using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace ContactBook_Windows_Appium_Tests
{
    public class Tests
    {
        private WindowsDriver<WindowsElement> driver;
        private AppiumOptions options;
        private const string AppiumUrl = "http://127.0.0.1:4723/wd/hub";
        private const string ContactsBookUrl = "https://contactbook.nakov.repl.co/api";
        private const string appLocation = @"C:\Exam\ContactBook-DesktopClient.exe";


        [SetUp]
        public void Setup()
        {
            options = new AppiumOptions() { PlatformName = "Windows" };
            options.AddAdditionalCapability("app", appLocation);
            driver = new WindowsDriver<WindowsElement>(new Uri(AppiumUrl), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [Test]
        public void ContactBookTest_Search_Steve()
        {
            driver.FindElementByAccessibilityId("textBoxApiUrl").Clear();
            driver.FindElementByAccessibilityId("textBoxApiUrl").SendKeys(ContactsBookUrl);

            var buttonConnect = driver.FindElementByAccessibilityId("buttonConnect");
            buttonConnect.Click();

            string windowsName = driver.WindowHandles[0];
            driver.SwitchTo().Window(windowsName);

            var textField = driver.FindElementByAccessibilityId("textBoxSearch");
            textField.SendKeys("steve");

            var searchButton = driver.FindElementByAccessibilityId("buttonSearch");
            searchButton.Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            var element = wait.Until(s =>
            {
                var resultText = driver.FindElementByAccessibilityId("labelResult").Text;
                return resultText.StartsWith("Contacts found");
            });

            var resultText = driver.FindElementByAccessibilityId("labelResult").Text;
            Assert.That(resultText, Is.EqualTo("Contacts found: 1"));

            var firstName = driver.FindElement(By.XPath("//Edit[@Name=\"FirstName Row 0, Not sorted.\"]"));
            var lastName = driver.FindElement(By.XPath("//Edit[@Name=\"LastName Row 0, Not sorted.\"]"));

            Assert.That(firstName.Text, Is.EqualTo("Steve"));
            Assert.That(lastName.Text, Is.EqualTo("Jobs"));

        }
        [TearDown]
        public void CloseApp()
        {
            driver.Quit();
        }
    }
}