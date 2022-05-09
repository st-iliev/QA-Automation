using NUnit.Framework;
using SeleniumBasicDemo;
using System;

namespace BankAccount

{
    public class Tests
    {
        BankAcount account = new BankAcount(1000);
        [SetUp]
        public void Setup()
        {
            account = new BankAcount(1000);
        }
        [Test]
        public void Test_CreateNew_BackAccount_With_PositiveAmount()
        {
            BankAcount account = new BankAcount(500);
            double amount = 500;
            Assert.AreEqual(amount, account.Amount);
        }
        [Test]
        public void Test_CreateNew_BackAccount_With_NegativeAmount()
        {
            Assert.That(() => { BankAcount account = new BankAcount(-10); },Throws.ArgumentException);
        }
        [Test]
        public void Test_Deposit_With_PositiveAmount()
        {
            decimal amount = 250;
           account.Deposit(amount);
            Assert.AreEqual(1250, account.Amount);

        }
        [Test]
        public void Test_Deposit_With_NegativeAmount()
        {
            decimal amount = -20;
            Assert.That(() => { account.Deposit(amount); }, Throws.ArgumentException);
        }
        [Test]
        public void Test_Withdraw_With_PositiveAmount()
        {
            account = new BankAcount(3000);
            decimal amount = 500;
            account.Withdraw(amount);
            Assert.AreEqual(2475, account.Amount,"Test BackAccount Wihtdraw with amount 500 + 5 % should be equal to 2475");
            amount = 1000;
            account.Withdraw(amount);
            Assert.AreEqual(1455, account.Amount,"Test BackAccount withdraw with amount 1000 + 2% should be equal to 1455");

        }
       [Test]
        public void Test_Withdraw_With_NegativeAmount()
        {
            account = new BankAcount(1000);
            Assert.That(() => { account.Withdraw(-100); }, Throws.ArgumentException);
        }
    }
}