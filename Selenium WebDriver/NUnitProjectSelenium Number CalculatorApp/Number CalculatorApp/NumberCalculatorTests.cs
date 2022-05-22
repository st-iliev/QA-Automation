using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace NUnitProjectTest_Number_CalculatorApp
{
    public class NumberCalculatorTests
    {
        ChromeDriver driver;
        IWebElement firstNumber;
        IWebElement secondNumber;
        IWebElement operation;
        IWebElement calculateButton;
        IWebElement resetButton;
        IWebElement result;
        
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Url = "https://number-calculator.nakov.repl.co/";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            firstNumber = driver.FindElement(By.Id("number1"));
            secondNumber = driver.FindElement(By.Id("number2"));
            operation = driver.FindElement(By.Id("operation"));
            calculateButton = driver.FindElement(By.Id("calcButton"));
            resetButton = driver.FindElement(By.Id("resetButton"));
            result = driver.FindElement(By.Id("result"));
        }
        public void FieldsClean()
        {
            Thread.Sleep(500);
            firstNumber.Clear();
            Thread.Sleep(500);
            secondNumber.Clear();
            Thread.Sleep(500);
            driver.FindElement(By.XPath("//select/option[1]")).Click();
        }
        [TestCase("1","+","2","Result: 3")]
        [TestCase("6","-","1","Result: 5")]
        [TestCase("5","*","20","Result: 100")]
        [TestCase("500","/","100","Result: 5")]
        public void Test_CalculatorWith_PositiveIntegers(string num1,string op , string num2,string expected)
        {
            Thread.Sleep(500);
            firstNumber.SendKeys(num1);
            operation.SendKeys(op);
            secondNumber.SendKeys(num2);
            calculateButton.Click();
            Assert.AreEqual(expected, result.Text);
            FieldsClean();
        }
        [TestCase("-5", "+", "-6", "Result: -11")]
        [TestCase("-8", "-", "-2", "Result: -6")]
        [TestCase("-7", "*", "-3", "Result: 21")]
        [TestCase("-900", "/", "-3", "Result: 300")]
        public void Test_CalculatorWith_NegativeIntegers(string num1, string op, string num2, string expected)
        {
            Thread.Sleep(500);
            firstNumber.SendKeys(num1);
            operation.SendKeys(op);
            secondNumber.SendKeys(num2);
            calculateButton.Click();
            Assert.AreEqual(expected, result.Text);
            FieldsClean();
        }
        [TestCase("5.6", "+", "3.4", "Result: 9")]
        [TestCase("8.1", "-", "3.6", "Result: 4.5")]
        [TestCase("9.5", "*", "2.7", "Result: 25.65")]
        [TestCase("205.2", "/", "20.3", "Result: 10.1083743842")]
        public void Test_CalculatorWith_PositiveDecimalNumbers(string num1, string op, string num2, string expected)
        {
            Thread.Sleep(500);
            firstNumber.SendKeys(num1);
            operation.SendKeys(op);
            secondNumber.SendKeys(num2);
            calculateButton.Click();
            Assert.AreEqual(expected, result.Text);
            FieldsClean();
        }
        [TestCase("-5.6", "+", "-3.4", "Result: -9")]
        [TestCase("-8.1", "-", "-3.6", "Result: -4.5")]
        [TestCase("-9.5", "*", "-2.7", "Result: 25.65")]
        [TestCase("-205.5", "/", "-20.5", "Result: 10.0243902439")]
        public void Test_CalculatorWith_NegativeDecimalNumbers(string num1, string op, string num2, string expected)
        {
            Thread.Sleep(500);
            firstNumber.SendKeys(num1);
            operation.SendKeys(op);
            secondNumber.SendKeys(num2);
            calculateButton.Click();
            Assert.AreEqual(expected, result.Text);
            FieldsClean();
        }
        [TestCase("asd", "+", "v333", "Result: invalid input")]
        [TestCase("$$$", "-", "53a", "Result: invalid input")]
        [TestCase("v1v1", "*", "b100", "Result: invalid input")]
        [TestCase("v16", "/", "1.6TDI", "Result: invalid input")]
        public void Test_CalculatorWith_InvalidNumbers(string num1, string op, string num2, string expected)
        {
            Thread.Sleep(500);
            firstNumber.SendKeys(num1);
            operation.SendKeys(op);
            secondNumber.SendKeys(num2);
            calculateButton.Click();
            Assert.AreEqual(expected, result.Text);
            FieldsClean();
        }
        [TestCase("5.6", "!", "3.4", "Result: invalid operation")]
        [TestCase("8.1", "@", "3.6", "Result: invalid operation")]
        [TestCase("9.5", "#", "2.7", "Result: invalid operation")]
        [TestCase("205.2", "$", "20.3", "Result: invalid operation")]
        public void Test_CalculatorWith_InvalidOperations(string num1, string op, string num2,string expected)
        {
            Thread.Sleep(500);
            firstNumber.SendKeys(num1);
            operation.SendKeys(op);
            secondNumber.SendKeys(num2);
            calculateButton.Click();
            Assert.AreEqual(expected, result.Text);
            FieldsClean();
        }
        [TestCase("Infinity", "+", "9", "Result: Infinity")]
        [TestCase("Infinity", "-", "5", "Result: Infinity")]
        [TestCase("Infinity", "*", "1", "Result: Infinity")]
        [TestCase("Infinity", "/", "7", "Result: Infinity")]
        public void Test_CalculatorWith_InfinityNumber(string num1, string op, string num2, string expected)
        {
            Thread.Sleep(500);
            firstNumber.SendKeys(num1);
            operation.SendKeys(op);
            secondNumber.SendKeys(num2);
            calculateButton.Click();
            Assert.AreEqual(expected, result.Text);
            FieldsClean();
        }
        [TestCase("-5", "+", "-6")]
        [TestCase("-8.1", "-", "-3.6")]
        [TestCase("8.1", "@", "3.6")]
        [TestCase("Infinity", "/", "7")]
        public void Test_Calculator_ResetButton(string num1, string op, string num2)
        {
            Thread.Sleep(500);
            firstNumber.SendKeys(num1);
            operation.SendKeys(op);
            secondNumber.SendKeys(num2);
            calculateButton.Click();
            resetButton.Click();
            var oper = driver.FindElement(By.XPath("//*[@id='operation']")).GetDomAttribute("disable");
            Assert.IsEmpty(firstNumber.Text);
            Assert.IsNull(oper);
            Assert.IsEmpty(secondNumber.Text);
            
            
        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}