using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static System.Net.WebRequestMethods;

namespace NUnitTestProject_URL_Shortener
{
    public class URLShortenerTests
    {
        ChromeDriver driver;
        IWebElement homePage;
        IWebElement shortURLPage;
        IWebElement addURLPage;

        [SetUp]
        public void Setup()
        {
             driver = new ChromeDriver();
            driver.Url = "https://shorturl.nakov.repl.co/";
            homePage = driver.FindElement(By.XPath("/html/body/header/a[1]"));
            shortURLPage = driver.FindElement(By.XPath("/html/body/header/a[2]"));
            addURLPage = driver.FindElement(By.XPath("/html/body/header/a[3]"));
        }

        [Test]
        public void Test_HomePageTitle()
        {
           homePage.Click();
           string expected = "URL Shortener";

           Assert.AreEqual(expected, driver.Title);
        }
        [Test]
        public void Test_ShortURLs()
        {
            shortURLPage.Click();
            string expected = "Short URLs";

            Assert.AreEqual(expected, driver.Title);

            IList<IWebElement> firstCol = driver.FindElements(By.XPath("//tbody/tr/td[1]"));
            IList<IWebElement> secondCol = driver.FindElements(By.XPath("//tbody/tr/td[2]"));

            Assert.That(firstCol[0].Text.Contains("https://nakov.com"));
            Assert.That(secondCol[0].Text.Contains("http://shorturl.nakov.repl.co/go/nak"));

        }
        [Test]
        public void Test_AddValidURl()
        {
            string uniqueCode = "softuni" + DateTime.Now.Ticks;
            addURLPage.Click();
            driver.FindElement(By.Id("url")).SendKeys("https://softuni.bg/");
            driver.FindElement(By.XPath("//*[@id='code']")).Clear();
            driver.FindElement(By.XPath("//*[@id='code']")).SendKeys(uniqueCode);
            driver.FindElement(By.XPath("//td/button")).Click();
            IList<IWebElement> firstCol = driver.FindElements(By.XPath("//tbody/tr/td[2]"));
            bool isFound = false;
            for (int i = 0; i < firstCol.Count; i++)    
            {
                if (firstCol[i].Text.Contains($"http://shorturl.nakov.repl.co/go/{uniqueCode}"))
                {
                    isFound = true;
                    break;
                }
            }
            Assert.True(isFound);

        }
        [Test]
        public void Test_AddInvalidURl()
        {
            addURLPage.Click();
            driver.FindElement(By.Id("url")).SendKeys("asd123");
            driver.FindElement(By.XPath("//td/button")).Click();
            var result = driver.FindElement(By.XPath("//body/div")).Text;
            string expected = "Invalid URL!";
            Assert.AreEqual(expected, result);

        }
        [Test]
        public void Test_VisitExistURL()
        {
            shortURLPage.Click();
            var count = driver.FindElement(By.XPath("//tbody/tr[1]/td[4]")).Text;
            driver.FindElement(By.XPath("//tbody/tr[1]/td[2]/a")).Click();
            driver.SwitchTo().Window(driver.WindowHandles[1]);
            Thread.Sleep(1000);
            Assert.AreEqual("https://nakov.com/", driver.Url);
            driver.SwitchTo().Window(driver.WindowHandles[0]);
            var currentCount = driver.FindElement(By.XPath("//tbody/tr[1]/td[4]")).Text;
            Assert.Greater(int.Parse(currentCount), int.Parse(count));


        }  
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}