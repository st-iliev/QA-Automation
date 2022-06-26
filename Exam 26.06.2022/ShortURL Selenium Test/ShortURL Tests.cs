using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShortURL_Selenium_Test
{
    public class Tests
    {
        private const string url = "https://shorturl.nakov.repl.co/";
        WebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [Test]
        public void ShortURL_SearchInShortURLs_FirstCell()
        {
            // Arrange
            driver.Navigate().GoToUrl(url);
            var shortURLs = driver.FindElement(By.XPath("/html/body/header/a[2]"));

            // Act
            shortURLs.Click();
            // Assert
            var tables = driver.FindElements(By.XPath("/html/body/main/table/thead/tr/th"));
            var tableName = tables[0].Text;

            Assert.That(tableName, Is.EqualTo("Original URL"));
        }
        [Test]
        public void ShortURL_CreateNewShortURL_With_ValidData()
        {
            // Arrange
            driver.Navigate().GoToUrl(url);
            var addURLs = driver.FindElement(By.XPath("/html/body/header/a[3]"));

            // Act
            addURLs.Click();
            string myUrl = "https://qa" + DateTime.Now.Ticks + ".com";
            var urlField = driver.FindElement(By.Id("url"));
            urlField.SendKeys(myUrl);
            var createButton = driver.FindElement(By.CssSelector("html body main form table tbody tr td button"));
            createButton.Click();
            IList<IWebElement> firstCol = driver.FindElements(By.XPath("//tbody/tr/td[1]"));
            bool isFound = false;
            for (int i = 0; i < firstCol.Count; i++)
            {
                if (firstCol[i].Text.Contains(myUrl))
                {
                    isFound = true;
                    break;
                }
            }
            // Assert
            Assert.True(isFound);
        }

        [Test]
        public void ShortURL_CreateNewShortURL_With_InvalidData()
        {
            // Arrange
            driver.Navigate().GoToUrl(url);
            var addURLs = driver.FindElement(By.XPath("/html/body/header/a[3]"));

            // Act
            addURLs.Click();
            string myUrl = DateTime.Now.Ticks + ".com";
            var urlField = driver.FindElement(By.Id("url"));
            urlField.SendKeys(myUrl);
            var createButton = driver.FindElement(By.CssSelector("button"));
            createButton.Click();
            var resultMsg = driver.FindElement(By.CssSelector(".err"));

            // Assert
            Assert.That(resultMsg.Text, Is.EqualTo("Invalid URL!"));
        }
        [Test]
        public void ShortURL_Visiting_InvalidShortUrl()
        {
            // Arrange
            string invalidUrl = "invalid" + DateTime.Now.Ticks;
            driver.Navigate().GoToUrl(url + "go/" + invalidUrl);

            // Act
            var resultMsg = driver.FindElement(By.CssSelector(".err")).Text;
            var pageTitle = driver.FindElement(By.CssSelector("body > main > h1")).Text;
            // Assert
            Assert.That(resultMsg, Is.EqualTo("Cannot navigate to given short URL"));
            Assert.That(pageTitle, Is.EqualTo("Error: Cannot navigate to given short URL"));
        }
        [Test]
        public void ShortURL_Visiting_ValidShortURL_CheckCounter()
        {
            // Arrange
            driver.Navigate().GoToUrl(url);
            var shortURLs = driver.FindElement(By.XPath("/html/body/header/a[2]"));

            // Act
            shortURLs.Click();
            var shortUrl = driver.FindElement(By.XPath("//tbody/tr[1]/td[2]/a"));
            var oldCounter = driver.FindElement(By.XPath("//td[4]")).Text;
            shortUrl.Click();
            driver.SwitchTo().Window(driver.WindowHandles[1]);
            Assert.AreEqual("https://nakov.com/", driver.Url);

            // Assert
            driver.SwitchTo().Window(driver.WindowHandles[0]);
            var currentCount = driver.FindElement(By.XPath("//tbody/tr[1]/td[4]")).Text;
            Assert.Greater(int.Parse(currentCount), int.Parse(oldCounter));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}