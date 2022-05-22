using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace NUnitProjectSelenium_Summator
{
    public class SummatorTests
    {
        private ChromeDriver driver;
        IWebElement firstField;
        IWebElement secondField;
        IWebElement calculateButton;
        IWebElement resetButton;
        IWebElement resultField;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Url = "https://sum-numbers.nakov.repl.co/";
            firstField = driver.FindElement(By.Id("number1"));
            secondField = driver.FindElement(By.Id("number2"));
            calculateButton = driver.FindElement(By.Id("calcButton"));
            resetButton = driver.FindElement(By.Id("resetButton"));
            resultField = driver.FindElement(By.CssSelector("#result"));
        }

        [Test]
        public void Test_AddTwoValidPositiveNumbers()
        {
            firstField.SendKeys("1");
            secondField.SendKeys("2");
            calculateButton.Click();
            var result = resultField.Text;
            Assert.AreEqual("Sum: 3", result);

        }
        [Test]
        public void Test_AddTwoValidPositiveDecimalNumbers()
        {
            firstField.SendKeys("1.78");
            secondField.SendKeys("2.29");
            calculateButton.Click();
            var result = resultField.Text;
            Assert.AreEqual("Sum: 4.07", result);

        }
        [Test]
        public void Test_AddTwoValidNegativeNumbers()
        {
            firstField.SendKeys("-1");
            secondField.SendKeys("-2");
            calculateButton.Click();
            var result = resultField.Text;
            Assert.AreEqual("Sum: -3", result);

        }
        [Test]
        public void Test_AddTwoValidNegativeDecimalNumbers()
        {
            firstField.SendKeys("-1.63");
            secondField.SendKeys("-2.54");
            calculateButton.Click();
            var result = resultField.Text;
            Assert.AreEqual("Sum: -4.17", result);

        }
        [Test]
        public void Test_AddTwoInvalidNumbers()
        {
            driver.FindElement(By.Id("number1")).SendKeys("as1");
            driver.FindElement(By.Id("number2")).SendKeys("ddz3");
            driver.FindElement(By.Id("calcButton")).Click();
            var result = resultField.Text;
            Assert.AreEqual("Sum: invalid input", result);
        }
        [Test]
        public void Test_AddTwoValidNumbers_ResetButton()
        {
            firstField.SendKeys("5");
            secondField.SendKeys("6");
            calculateButton.Click();
            var result = resultField.Text;
            Assert.AreEqual("Sum: 11", result);
            resetButton.Click();
            var firstNumber = driver.FindElement(By.Id("number1")).GetAttribute("value");
            var secondNumber = driver.FindElement(By.Id("number2")).GetAttribute("value");
            Assert.IsEmpty(firstNumber.ToString());
            Assert.IsEmpty(secondNumber.ToString());
        }
        [Test]
        public void Test_AddTwoInvalidNumbers_ResetButton()
        {
            driver.FindElement(By.Id("number1")).SendKeys("asd2");
            driver.FindElement(By.Id("number2")).SendKeys("zc123");
            calculateButton.Click();
            var result = resultField.Text;
            Assert.AreEqual("Sum: invalid input", result);
            resetButton.Click();
            var firstNumber = driver.FindElement(By.Id("number1")).GetAttribute("value");
            var secondNumber = driver.FindElement(By.Id("number2")).GetAttribute("value");         
            Assert.IsEmpty(firstNumber.ToString());
            Assert.IsEmpty(secondNumber.ToString());
        }
        [Test]
        public void Test_AddFirstValidAndSecondInvalidNumbers()
        {
            driver.FindElement(By.Id("number1")).SendKeys("2");
            driver.FindElement(By.Id("number2")).SendKeys("pop3e");
            calculateButton.Click();
            var result = resultField.Text;
            Assert.AreEqual("Sum: invalid input", result);
            resetButton.Click();
            var firstElement = driver.FindElement(By.Id("number1")).GetAttribute("value");
            var secondElement = driver.FindElement(By.Id("number2")).GetAttribute("value");
            Assert.IsEmpty(firstElement.ToString());
            Assert.IsEmpty(secondElement.ToString());
        }
        [Test]
        public void Test_AddFirstInvalidAndSecondValidNumbers()
        {
            driver.FindElement(By.Id("number1")).SendKeys("jubh8");
            driver.FindElement(By.Id("number2")).SendKeys("5");
            calculateButton.Click();
            var result = resultField.Text;
            Assert.AreEqual("Sum: invalid input", result);
            resetButton.Click();
            var firstElement = driver.FindElement(By.Id("number1")).GetAttribute("value");
            var secondElement = driver.FindElement(By.Id("number2")).GetAttribute("value");
            Assert.IsEmpty(firstElement.ToString());
            Assert.IsEmpty(secondElement.ToString());
        }
        [Test]
        public void Test_AddFirstValidAndSecondEmptyNumbers()
        {
            driver.FindElement(By.Id("number1")).SendKeys("7");
            calculateButton.Click();
            var result = resultField.Text;
            Assert.AreEqual("Sum: invalid input", result);
        }
        [Test]
        public void Test_AddFirstEmptyAndSecondValidNumbers()
        {
            driver.FindElement(By.Id("number2")).SendKeys("10");
            calculateButton.Click();
            var result = resultField.Text;
            Assert.AreEqual("Sum: invalid input", result);
        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}