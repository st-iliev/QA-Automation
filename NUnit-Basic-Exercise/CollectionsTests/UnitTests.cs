require 'coveralls'
Coveralls.wear!
using NUnit.Framework;
using System;
using System.Linq;

namespace Collections
{
    [TestFixture]
    public class Tests
    {
        private Collection<int> collection;
        [SetUp]
        public void Setup()
        {
            collection = new Collection<int>();
        }
        [Test]
        [Timeout(1000)]
        public void Test_OneMillionItems()
        {
            int items = 1000000;
            Collection<int> collection = new Collection<int>();
            collection.AddRange(Enumerable.Range(0, items).ToArray());
            Assert.That(collection.Count, Is.EqualTo(items),"Test Collection count should be equal to 1000000");
            Assert.That(collection.Capacity, Is.GreaterThanOrEqualTo(items),"Test Collection capacity should be greater or equal to 1000000");
            collection.Clear();
            Assert.AreEqual(collection.ToString(), "[]");
            Assert.GreaterOrEqual(collection.Capacity,collection.Count);
        }
        [Test]
        public void Test_AddItem()
        {
            int lenght = 100;
            collection.Add(100);
            Assert.AreEqual(lenght,collection[collection.Count-1]);
        }
        [Test]
        public void Test_AddRangeItem()
        {
            int[] element = new int[] { 1, 2, 3 };
            collection.AddRange(element);
            Assert.That(collection[collection.Count - 2], Is.EqualTo(element[element.Length - 2]));
        }
        [Test]
        public void Test_AddRangeWithGrow()
        {
            int[] elements = new int[17];
            int oldLenght = collection.Capacity;
            collection.AddRange(elements);
            Assert.AreEqual(2 * oldLenght, collection.Capacity);

        }
        [Test]
        public void Test_Clear()
        {
            collection.Clear();
            Assert.AreEqual(0, collection.Count);
        }
        public void Test_EmptyConstructor()
        {
            collection = new Collection<int>();
            int expected = 0;
            Assert.AreEqual(expected, collection.Count);

        }
        [Test]
        public void Test_ConstructorSingleItem()
        {
            collection = new Collection<int>(new int[] { 1 });
            int collectionLenght = 1;
            Assert.That(collection[0], Is.EqualTo(collectionLenght));

        }
        [Test]
        public void Test_ConstructorMultipleItems()
        {
            collection = new Collection<int>(new int[] { 1, 2, 3 });
            string expected = "[1, 2, 3]";
            Assert.AreEqual(expected, collection.ToString());

        }
        [Test]
        public void Test_CountAndCapacity()
        {
            collection = new Collection<int>(new int[] { 10, 20, 30, 40, 50 });
            int collectionCapacity = 16;
            int collectionCount = 5;
            Assert.AreEqual(collectionCapacity, collection.Capacity, "Test Collection Capacity should be equal to 16");
            Assert.AreEqual(collectionCount, collection.Count, "Test Collection Count should be equal to 5 ");
        }
        [Test]
        public void Test_ExchangeFirstLast()
        {
            Collection<string> collection = new Collection<string>(new string[] { "uno", "dos", "tres" });
            string oldFirstElement = collection[0];
            string oldLastElement = collection[collection.Count - 1];
            collection.Exchange(0, collection.Count - 1);
            Assert.AreEqual(oldFirstElement, collection[collection.Count - 1], "Test Collection firstelement should be lastelement");
            Assert.AreEqual(oldLastElement, collection[0], "Test Collection lastelement should be firstlement");
        }
        [Test]
        public void Test_ExchangeInvalidIndexes()
        {
            Collection<string> collection = new Collection<string>(new string[] { "uno", "dos", "tres" });
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => collection.Exchange(-1,collection.Count+1));
            Assert.IsTrue(exception.Message.Contains($"Parameter should be in the range [0...{collection.Count-1}]"));
        }
        [Test]
        public void Test_GetByIndex()
        {
            Collection<string> collection = new Collection<string>(new string[] { "uno", "dos", "tres" });
            string firstElement = collection[0];
            string secondElement = collection[1];
            Assert.AreEqual(firstElement, collection[0], "Test Collection firstelement should be equal to uno");
            Assert.AreEqual(secondElement, collection[1], "Test Collection secondelement should be qual to dos");
        }
        [Test]
        public void Test_GetByInvalidIndex()
        {
            Collection<string> collection = new Collection<string>(new string[] { "uno", "dos", "tres" });
            Assert.That(() => { string name = collection[-1]; }, Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { string name = collection[3]; }, Throws.InstanceOf<ArgumentOutOfRangeException>());

        }
        [Test]
        public void Test_InsertAtEnd()
        {
            Collection<string> collection = new Collection<string>(new string[] { "uno", "dos", "tres" });
            string element = "cuatro";
            collection.InsertAt(collection.Count, element);
            Assert.AreEqual(element, collection[collection.Count - 1], "Test Collection lastelement should be equal to cuatro");
        }
        [Test]
        public void Test_InsertAtInvalidIndex()
        {
            Collection<string> collection = new Collection<string>(new string[] { "uno", "dos", "tres" });
            string element = "cuatro";
            int index = -1;
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => collection.InsertAt(index, element));
            Assert.IsTrue(exception.Message.Contains($"Parameter should be in the range [0...{collection.Count}]"));
        }
        [Test]
        public void Test_InsertAtMiddle()
        {
            Collection<string> collection = new Collection<string>(new string[] { "uno", "dos", "tres", "cuatro" });
            string element = "middle";
            collection.InsertAt(collection.Count / 2, element);
            Assert.AreEqual(element, collection[collection.Count / 2], "Test Collection element at middle should be equal to middle");
        }
        [Test]
        public void Test_InsertAtStart()
        {
            Collection<string> collection = new Collection<string>(new string[] { "uno", "dos", "tres", "cuatro" });
            string element = "zero";
            collection.InsertAt(0, element);
            Assert.AreEqual(element, collection[0], "Test Collection firstelement should be equal to zero");
        }
        [Test]
        public void Test_RemoveAtEnd()
        {
            Collection<string> collection = new Collection<string>(new string[] { "uno", "dos", "tres", "cuatro", });
            string lastElement = collection[collection.Count - 1];
            collection.RemoveAt(collection.Count - 1);
            Assert.AreNotEqual(lastElement, collection[collection.Count - 1]);
        }
        [Test]
        public void Test_RemoveAtInvalidIndex()
        {
            Collection<string> collection = new Collection<string>(new string[] { "uno", "dos", "tres", "cuatro", });
            int index = -1;
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => collection.RemoveAt(index));
            Assert.IsTrue(exception.Message.Contains($"Parameter should be in the range [0...{collection.Count-1}]"));
        }
        [Test]
        public void Test_RemoveAtMiddle()
        {
            Collection<string> collection = new Collection<string>(new string[] { "uno", "dos", "tres", });
            string middleElement = collection[collection.Count / 2];
            collection.RemoveAt(collection.Count / 2);
            Assert.IsFalse(middleElement == collection[collection.Count / 2]);

        }
        [Test]
        public void Test_RemoveAtStart()
        {
            Collection<string> collection = new Collection<string>(new string[] { "uno", "dos", "tres", });
            string firstElement = collection[0];
            collection.RemoveAt(0);
            Assert.IsFalse(firstElement == collection[0]);
        }
        [Test]
        public void Test_ToStringCollectionOfCollections()
        {
            Collection<string> firstCollection = new Collection<string>(new string[] { "tzatziki", "uzo", "fosalada" }); Collection<double> secondCollection = new Collection<double>(new double[] { 5.59, 4.41, 3.79 });
            Collection<object> thirdCollection = new Collection<object>(firstCollection, secondCollection);
            string thirdCollectionToString = thirdCollection.ToString();
            Assert.AreEqual("[[tzatziki, uzo, fosalada], [5.59, 4.41, 3.79]]", thirdCollectionToString);
        }
        [Test]
        public void Test_ToStringEmpty()
        {
            Collection<int> collection = new Collection<int>();
            string collectionToString = collection.ToString();
            Assert.AreEqual("[]", collectionToString);
        }
        [Test]
        public void Test_ToStringSingle()
        {
            Collection<int> collection = new Collection<int>(new int[] { 3256624 });
            string collectiontostring = collection.ToString();
            string expected = "[3256624]";
            Assert.AreEqual(expected, collectiontostring);
        }
    }
}
