using ConsoleAppSummator;
using NUnit.Framework;

namespace Summator_Tests
{
    [TestFixture]
    public class SummatorTests
    {
        private Summator summator;
        [SetUp]
        public void SetUp()
        {
           summator = new Summator();
        }
        [Test]
        public void SumOfNumbers()
        {
            long expected = 6;
            long actual = Summator.Sum(new int[] { 1, 2, 3 });
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void AverageofNumbers()
        {
            double expected = 20;
            double actual = Summator.Average(new int[] { 10, 20, 30 });
            Assert.That(expected == actual);
        }
        [Test]
        public void FindMaxNumber()
        {
            int expected = 100;
            int actual = Summator.Max(new int[] { 7, 25, 48, 100, 83 });
            Assert.That(expected == actual);
        }
        [Test]
        public void FindMinNumber()
        {
            int expected = 3;
            int actual = Summator.Min(new int[] { 3, 16, 4, 18, 9 });
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void FindFirstElement()
        {
            int expected = 55;
            int actual = Summator.FirstElement(new int[] { 55, 17, 55 });
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void FindLastElement()
        {
            int expected = 10;
            int actual = Summator.LastElement(new int[] { 55,54,88,73, 17, 10 });
            Assert.AreEqual(expected, actual);
        }
    }
}