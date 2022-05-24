using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Threading;

namespace NUnitProjectTest_Summator_App
{
    public class Tests
    {
        private AppiumLocalService appiumLocalService;
        private WindowsDriver<WindowsElement> driver;
        private WindowsElement firstField;
        private WindowsElement secondField;
        private WindowsElement result;
        private WindowsElement calcButton;
        [OneTimeSetUp]
        public void Setup()
        {
            appiumLocalService = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            appiumLocalService.Start();
            var appiumOptions = new AppiumOptions() { PlatformName = "Windows" };
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.App, @"E:\QA\QA Automation\Appium\SummatorDesktopApp.exe");
            driver = new WindowsDriver<WindowsElement>(appiumLocalService, appiumOptions);
            firstField = driver.FindElementByAccessibilityId("textBoxFirstNum");
            secondField = driver.FindElementByAccessibilityId("textBoxSecondNum");
            result = driver.FindElementByAccessibilityId("textBoxSum");
            calcButton = driver.FindElementByAccessibilityId("buttonCalc");
        }
        public void Clear()
        {
            firstField.Clear();
            secondField.Clear();
        }
        [TestCase("1","2","3")]
        [TestCase("-5","-9","-14")]
        [TestCase("asdas","13","error")]
        [TestCase("9","1v133","error")]
        [TestCase("","","error")]
        [TestCase("","8","error")]
        [TestCase("4","","error")]
        [TestCase("gg","","error")]
        [TestCase("","asd","error")]
        public void Test_Summator(string num1,string num2,string expecred)
        {
            firstField.SendKeys(num1);
            secondField.SendKeys(num2);
            calcButton.Click();
            Assert.AreEqual(expecred, result.Text);
            Thread.Sleep(500);
            Clear();
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            driver.CloseApp();
        }
    }
}