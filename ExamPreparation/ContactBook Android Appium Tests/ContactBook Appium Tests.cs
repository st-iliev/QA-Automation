using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Linq;

namespace ContactBook_Android_Appium_Tests
{
    public class Tests
    {
        private AndroidDriver<AndroidElement> driver;
        private AppiumOptions options;
        private const string appiumUrl = "http://127.0.0.1:4723/wd/hub";
        private const string contactBookUrl = "https://contactbook.nakov.repl.co/api";
        private const string appLocation = @"WRITE YOUR APP PATH";
        [SetUp]
        public void Setup()
        {
            options = new AppiumOptions() { PlatformName = "Android" };
            options.AddAdditionalCapability("app", appLocation);
            driver = new AndroidDriver<AndroidElement>(new Uri(appiumUrl), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [Test]
        public void ContactBookTest_SearchSteve()
        {
            var connectButton = driver.FindElementById("contactbook.androidclient:id/buttonConnect");
            connectButton.Click();
            var textField = driver.FindElementById("contactbook.androidclient:id/editTextKeyword");
            textField.SendKeys("Steve");
            var searchButton = driver.FindElementById("contactbook.androidclient:id/buttonSearch");
            searchButton.Click();
            var contacts = driver.FindElementsById("contactbook.androidclient:id/recyclerViewContacts");
            var contact = contacts.First();
            var firstName = contact.FindElementByXPath("//android.widget.TableLayout[1]/android.widget.TableRow[3]/android.widget.TextView[2]").Text;
            var lastName = contact.FindElementByXPath("//android.widget.TableLayout[1]/android.widget.TableRow[4]/android.widget.TextView[2]").Text;

            Assert.That(firstName, Is.EqualTo("Steve"));
            Assert.That(lastName, Is.EqualTo("Jobs"));
        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
