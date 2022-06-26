using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Linq;
using System.Threading;

namespace GitHubRepoBrowser_Appium_Test
{
    public class Tests
    {
        private AndroidDriver<AndroidElement> driver;
        private AppiumOptions options;
        private const string appiumUrl = "http://127.0.0.1:4723/wd/hub";
        private const string appLocation = @"C:\com.android.example.github.apk";
        [SetUp]
        public void Setup()
        {
            options = new AppiumOptions() { PlatformName = "Android" };
            options.AddAdditionalCapability("app", appLocation);
            driver = new AndroidDriver<AndroidElement>(new Uri(appiumUrl), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
        }

        [Test]
        public void TestGitHubRepoBrowser_Search()
        {
            // Arrange
            Thread.Sleep(500);
            var searchField = driver.FindElement(By.Id("com.android.example.github:id/input"));
            searchField.SendKeys("Selenium");
            Thread.Sleep(500);
            driver.PressKeyCode(AndroidKeyCode.Keycode_ENTER);

            //Act
            Thread.Sleep(1000);
            string repoName = "";
            bool isFound = false;
            var reposLists = driver.FindElements(By.Id("com.android.example.github:id/repo_list"));
            foreach (var item in reposLists)
            {
                repoName = item.FindElement(By.Id("com.android.example.github:id/name")).Text;
                if (repoName == "SeleniumHQ/selenium")
                {
                    isFound = true;
                    break;
                }
            }
            // Assert
            Assert.True(isFound);
            Assert.That(repoName, Is.EqualTo("SeleniumHQ/selenium"));

            //Act
            var person = driver.FindElement(By.Id("com.android.example.github:id/name"));
            person.Click();
            string personName = "";
            bool personFound = false;
            var personsList = driver.FindElements(By.Id("com.android.example.github:id/textView"));
            foreach (var item in personsList)
            {
                personName = item.Text;
                if (personName == "barancev")
                {
                    personFound = true;
                    item.Click();
                    break;
                }
            }
            var currentPersonName = driver.FindElement(By.Id("com.android.example.github:id/name")).Text;

            // Assert
            Assert.True(personFound);
            Assert.That(personName, Is.EqualTo("barancev"));
            Assert.That(currentPersonName, Is.EqualTo("Alexei Barantsev"));
        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}