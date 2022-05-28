using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;

namespace NUnitTestProject_ConctactBook_Android_App
{
    public class Tests
    {
        private const string AppiumServerUri = "http://127.0.0.1:4723/wd/hub";
        private string ContactBookAppPath = @"C:\contactbook-androidclient.apk";
        private const string ApiServiceUrl = "https://contactbook.nakov.repl.co/api";
        private WebDriverWait wait;
        private AndroidDriver<AndroidElement> driver;


        [SetUp]
        public void Setup()
        {
            var options = new AppiumOptions() { PlatformName = "Android" };
            options.AddAdditionalCapability("app", ContactBookAppPath);
            driver = new AndroidDriver<AndroidElement>(new Uri(AppiumServerUri),options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            wait = new WebDriverWait(driver,TimeSpan.FromSeconds(2));
            
        }

        [Test]
        public void Test_ContactBookApp_SearchSingalValidContact()
        {
            var apiSearchField = driver.FindElementById("contactbook.androidclient:id/editTextApiUrl");
            apiSearchField.Clear();
            apiSearchField.SendKeys(ApiServiceUrl);
            var connectButton = driver.FindElementById("contactbook.androidclient:id/buttonConnect");
            connectButton.Click();
            var contactSearchField = driver.FindElementById("contactbook.androidclient:id/editTextKeyword");
            contactSearchField.SendKeys("Steve");
            var searchButton = driver.FindElementById("contactbook.androidclient:id/buttonSearch");
            searchButton.Click();
            var searchResult = driver.FindElementById("contactbook.androidclient:id/textViewSearchResult");
            wait.Until(s => searchResult.Text != "");
            Assert.AreEqual("Contacts found: 1",searchResult.Text);
            var viewFirstName = driver.FindElementById("contactbook.androidclient:id/textViewFirstName");
            Assert.AreEqual("Steve", viewFirstName.Text);
            var viewLastName = driver.FindElementById("contactbook.androidclient:id/textViewLastName");
            Assert.AreEqual("Jobs", viewLastName.Text);
        }
        [Test]
        public void Test_ContactBookApp_SearchInvalidContact()
        {
            var apiSearchField = driver.FindElementById("contactbook.androidclient:id/editTextApiUrl");
            apiSearchField.Clear();
            apiSearchField.SendKeys(ApiServiceUrl);
            var connectButton = driver.FindElementById("contactbook.androidclient:id/buttonConnect");
            connectButton.Click();
            var contactSearchField = driver.FindElementById("contactbook.androidclient:id/editTextKeyword");
            contactSearchField.SendKeys("Gosho");
            var searchButton = driver.FindElementById("contactbook.androidclient:id/buttonSearch");
            searchButton.Click();
            var searchResult = driver.FindElementById("contactbook.androidclient:id/textViewSearchResult");
            wait.Until(s => searchResult.Text != "");
            Assert.AreEqual("Contacts found: 0",searchResult.Text);            
        }
        [Test]
        public void Test_ContactBookApp_SearchMultipleContacts()
        {
            var apiSearchField = driver.FindElementById("contactbook.androidclient:id/editTextApiUrl");
            apiSearchField.Clear();
            apiSearchField.SendKeys(ApiServiceUrl);
            var connectButton = driver.FindElementById("contactbook.androidclient:id/buttonConnect");
            connectButton.Click();
            var contactSearchField = driver.FindElementById("contactbook.androidclient:id/editTextKeyword");
            contactSearchField.SendKeys("e");
            var searchButton = driver.FindElementById("contactbook.androidclient:id/buttonSearch");
            searchButton.Click();
            var searchResult = driver.FindElementById("contactbook.androidclient:id/textViewSearchResult");
            wait.Until(s => searchResult.Text != "");
            Assert.AreEqual("Contacts found: 3",searchResult.Text);            
        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}