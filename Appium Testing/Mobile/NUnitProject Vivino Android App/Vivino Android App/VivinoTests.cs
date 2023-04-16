using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;

namespace TestVivino
{
    public class Tests
    {
        private const string AppiumServerUri = "http://127.0.0.1:4723/wd/hub";
        private const string VivinoAppPath = @"WRITE YOUR APP PATH";
        private const string VivinoAppPackage = "vivino.web.app";
        private const string VivinoAppStartupActivity = "com.sphinx_solution.activities.SplashActivity";
        private const string VivinoTestAccountEmail = "qaautomation@test.bg";
        private const string VivinoTestAccountPassowrd = "qaautomation";
        private AndroidDriver<AndroidElement> driver;


        [OneTimeSetUp]
        public void Setup()
        {
            var appiumOptions = new AppiumOptions() { PlatformName = "Android" };
            appiumOptions.AddAdditionalCapability("app", VivinoAppPath);
            appiumOptions.AddAdditionalCapability("appPackage", VivinoAppPackage);
            appiumOptions.AddAdditionalCapability("appActivity", VivinoAppStartupActivity);
            driver = new AndroidDriver<AndroidElement>(new Uri(AppiumServerUri), appiumOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);


        }
        [Test]
        public void Test_VivinoApp_Login()
        {
            var login = driver.FindElementById("vivino.web.app:id/getstarted_layout");
            login.Click();
            var emailField = driver.FindElementById("vivino.web.app:id/edtEmail");
            emailField.SendKeys(VivinoTestAccountEmail);
            var passwordField = driver.FindElementById("vivino.web.app:id/edtPassword");
            passwordField.SendKeys(VivinoTestAccountPassowrd);
            var nextButton = driver.FindElementById("vivino.web.app:id/action_next");
            nextButton.Click();
            string header = "Trending wines from the Vivino community";
            var pageHeader = driver.FindElementById("vivino.web.app:id/header_text");
            Assert.AreEqual(header, pageHeader.Text);
        }
        [Test]
        public void Test_VivinoApp_Search()
        {
            var explorerTab = driver.FindElementById("vivino.web.app:id/wine_explorer_tab");
            explorerTab.Click();
            var searchField = driver.FindElementById("vivino.web.app:id/search_vivino");
            searchField.Click();
            var searchInputText = driver.FindElementById("vivino.web.app:id/editText_input");
            searchInputText.SendKeys("Katarzyna Reserve Red 2006");
            var resultList = driver.FindElementById("vivino.web.app:id/listviewWineListActivity");
            var firstResult = resultList.FindElementByClassName("android.widget.LinearLayout");
            firstResult.Click();
            var wineryName = driver.FindElementById("vivino.web.app:id/winery_name");
            Assert.AreEqual("Katarzyna",wineryName.Text);
            var wineName = driver.FindElementById("vivino.web.app:id/wine_name");
            Assert.AreEqual("Reserve Red 2006", wineName.Text);
            var wineRating = driver.FindElementById("vivino.web.app:id/rating");
            double raiting = double.Parse(wineRating.Text);
            Assert.IsTrue(raiting >= 0 && raiting <= 5);
            var tabSummary = driver.FindElementById("vivino.web.app:id/tabs");
            var tabHighlights = tabSummary.FindElementByXPath("//android.widget.TextView[1]");
            var tabFacts = tabSummary.FindElementByXPath("//android.widget.TextView[2]");
            tabHighlights.Click();
            var highlightsDescription = driver.FindElementById("vivino.web.app:id/highlight_description");
            Assert.AreEqual("Among top 1% of all wines in the world", highlightsDescription.Text);
            tabFacts.Click();
            var factTitle = driver.FindElementById("vivino.web.app:id/wine_fact_title");
            Assert.AreEqual("Grapes", factTitle.Text); 
            var factText = driver.FindElementById("vivino.web.app:id/wine_fact_text");
            Assert.AreEqual("Cabernet Sauvignon,Merlot", factText.Text);

        }
        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
