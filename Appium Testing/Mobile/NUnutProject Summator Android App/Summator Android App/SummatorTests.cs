using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;

namespace NUnitTestProject_Summator__Android_App
{
    public class Tests
    {
        private const string AppiumServerUri = "http://127.0.0.1:4723/wd/hub";
        private string SummatroAppPath = @"C:\com.example.androidappsummator.apk";
        private AndroidDriver<AndroidElement> driver;
        private AndroidElement firstField;
        private AndroidElement secondField;
        private AndroidElement result;
        private AndroidElement calcButton;
        [OneTimeSetUp]
        public void Setup()
        {
            var option = new AppiumOptions() { PlatformName = "Android" };
            option.AddAdditionalCapability("app",SummatroAppPath);
            driver = new AndroidDriver<AndroidElement>(new Uri(AppiumServerUri), option);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            firstField = driver.FindElementById("com.example.androidappsummator:id/editText1");
            secondField = driver.FindElementById("com.example.androidappsummator:id/editText2");
            result = driver.FindElementById("com.example.androidappsummator:id/editTextSum");
            calcButton = driver.FindElementById("com.example.androidappsummator:id/buttonCalcSum");
        }
        public void Clear()
        {
            firstField.Clear();
            secondField.Clear();
        }
        [Test]
        public void Test_AndroidSummator_With_ValidInput()
        {
            Clear();
            firstField.SendKeys("5");
            secondField.SendKeys("5");
            calcButton.Click();
            Assert.AreEqual("10", result.Text);
        } 
        [TestCase("asd","1","error")]
        [TestCase("asdd",".","error")]
        [TestCase("as2d","","error")]
        [TestCase("3","asdfg","error")]
        [TestCase(".","ujyuj7","error")]
        [TestCase("","ujyuj7","error")]
        [TestCase("a2dc","ujyuj7","error")]
        [TestCase(".",".","error")]
        [TestCase("","","error")]
        
        public void Test_AndroidSummator_With_InvalidInput(string num1 , string num2 , string expected)
        {
            Clear();
            firstField.SendKeys(num1);
            secondField.SendKeys(num2);
            calcButton.Click();
            Assert.AreEqual(expected, result.Text);
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}