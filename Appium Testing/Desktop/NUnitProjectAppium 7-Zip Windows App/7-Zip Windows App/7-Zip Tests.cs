using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.IO;
using System.Threading;

namespace NUnitProjectTest_7_Zip_Windows_App
{
    public class Tests
    {
        private const string AppiumServerUri = "http://127.0.1:4723/wd/hub";
        private WindowsDriver<WindowsElement> driver;
        private WindowsDriver<WindowsElement> desktopDriver;
        private string workDir;
        [SetUp]
        public void Setup()
        {
            var appiumOptions = new AppiumOptions() { PlatformName = "Windows" };
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.App, @"WRITE YOUR APP PATH");
            driver = new WindowsDriver<WindowsElement>(new Uri(AppiumServerUri), appiumOptions);
            workDir = Directory.GetCurrentDirectory() + @"\workdir";
            if (Directory.Exists(workDir))
            {
                Directory.Delete(workDir, true);
            }
            Directory.CreateDirectory(workDir);
            var appiumOptionDesktop = new AppiumOptions() { PlatformName = "Windows" };
            appiumOptionDesktop.AddAdditionalCapability(MobileCapabilityType.App, "Root");
            desktopDriver = new WindowsDriver<WindowsElement>(new Uri(AppiumServerUri), appiumOptionDesktop);
        }
        [Test]
        public void Test_7zip_AddToArchive()
        {
            var locationFolder = driver.FindElementByXPath("/Window/Pane/Pane/ComboBox/Edit");
            locationFolder.SendKeys(@"APP PATH" + Keys.Enter);
            var listBoxFiles = driver.FindElementByXPath("/Window/Pane/List");
            listBoxFiles.SendKeys(Keys.Control + "a");
            var addButton = driver.FindElementByXPath("/Window/ToolBar/Button[@Name='Add']");
            addButton.Click();
            Thread.Sleep(1000);
            var windowsAddToArchive = desktopDriver.FindElementByName("Add to Archive");
           var archiveNameTextBox = windowsAddToArchive.FindElementByXPath("/Window/ComboBox/Edit[@Name='Archive:']");
            string archiveFileName = workDir + "\\" + DateTime.Now.Ticks + ".7z";
            archiveNameTextBox.SendKeys(archiveFileName);
            var archiveFormatTextBox = windowsAddToArchive.FindElementByXPath("/Window/ComboBox[@Name='Archive format:']");
            archiveFormatTextBox.SendKeys("7z");
            var comboboxCompLevel = windowsAddToArchive.FindElementByXPath("/Window/ComboBox[@Name='Compression level:']");
            comboboxCompLevel.SendKeys("9-Ultra");
            var comboboxCompMethod = windowsAddToArchive.FindElementByXPath("/Window/ComboBox[@Name='Compression method:']");
            comboboxCompMethod.SendKeys("LZMA2");
            var comboboxDictionarySize = windowsAddToArchive.FindElementByXPath("/Window/ComboBox[@Name='Dictionary size:']");
            comboboxDictionarySize.SendKeys("1536");
            var comboboxWordSize = windowsAddToArchive.FindElementByXPath("/Window/ComboBox[@Name='Word size:']");
            comboboxWordSize.SendKeys("273");
            var comboboxSolidBlockSize = windowsAddToArchive.FindElementByXPath("/Window/ComboBox[@Name='Solid Block size:']");
            comboboxSolidBlockSize.SendKeys("16 GB");
            var comboboxCpuThreads = windowsAddToArchive.FindElementByXPath("/Window/ComboBox[@Name='Number of CPU threads:']");
            comboboxCpuThreads.SendKeys("4");
            var comboboxButtonOk = windowsAddToArchive.FindElementByXPath("/Window/Button[@Name = 'OK']");
            comboboxButtonOk.Click();
            Thread.Sleep(1000);
            locationFolder.SendKeys(archiveFileName + Keys.Enter);
            var extractButton = driver.FindElementByXPath("/Window/ToolBar/Button[@Name='Extract']");
            extractButton.Click();
            var dialogButtonOk = driver.FindElementByXPath("/Window/Window/Button[@Name='OK']");
            dialogButtonOk.Click();
            Thread.Sleep(1000);
            string original7Zip = @"APP PATH";
            string extracted7Zip = workDir + @"\7zFM.exe";
            FileAssert.AreEqual(original7Zip, extracted7Zip);
        }
        [TearDown]
        public void TearDown()
        {
            driver.CloseApp();
            driver.Quit();
        }
    }
}
