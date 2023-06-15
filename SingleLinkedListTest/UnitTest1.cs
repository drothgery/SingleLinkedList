using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SingleLinkedList;

namespace SingleLinkedListTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethodAddFirstOneInt()
        {
            var valueToAdd = 5;
            var sll = new SingleLinkedList<int>();
            sll.AddFirst(valueToAdd);

            Assert.IsTrue(sll?.First?.Value == valueToAdd);
        }
        [TestMethod]
        public void TestMethodAddFirstOneObject()
        {
            var valueToAdd = new MinimalTestClass { fieldOne = 1, fieldTwo = "two" };
            var sll = new SingleLinkedList<MinimalTestClass>();
            sll.AddFirst(valueToAdd);

            Assert.IsTrue(sll?.First?.Value == valueToAdd);
        }

        [TestMethod]
        public void TestMethodAddFirstManyInts()
        {
            var values = new[] { 1, 2, 3, 4 };
            var sll = CreateShortList(values);

            Assert.IsTrue(sll?.First?.Value == values.Last());
        }

        [TestMethod]
        public void TestMethodAddAfter()
        {
            var valueToAdd = 8;
            var valueToAddAfter = 6;

            var values = new[] { 3, valueToAddAfter, 9, 12 };
            var sll = CreateShortList(values);

            sll.AddAfter(valueToAdd, valueToAddAfter);

            Assert.IsNotNull(sll);
            Assert.IsNotNull(sll.First);

            var itemToAddAfter = sll.First;
            while (itemToAddAfter != null)
            {
                if (itemToAddAfter.Value.Equals(valueToAddAfter))
                {
                    var itemAfterItemToAddAfter = itemToAddAfter.Next;
                    Assert.IsNotNull(itemAfterItemToAddAfter);
                    Assert.IsTrue(itemAfterItemToAddAfter.Value.Equals(valueToAdd));
                    return;
                }

                itemToAddAfter = itemToAddAfter.Next;
            }

            Assert.Fail("item to add after not found");

        }

        private static SingleLinkedList<int> CreateShortList(int[] values)
        {
            var sll = new SingleLinkedList<int>();
            foreach (var value in values)
            {
                sll.AddFirst(value);
            }
            return sll;
        }

        [TestMethod]
        public void TestMethodAddLast()
        {
            var values = new[] { 2, 4, 6, 8 };
            var sll = CreateShortList(values);

            var valueToAddLast = 1;
            sll.AddLast(valueToAddLast);

            Assert.IsNotNull(sll);
            Assert.IsNotNull(sll.First);

            var lastItem = sll.First;
            while (lastItem.Next != null)
            {
                lastItem = lastItem.Next;
            }

            Assert.IsTrue(lastItem.Value == valueToAddLast);
        }

        [TestMethod]
        public void TestRemove()
        {
            var valueToRemove = 17;

            var values = new[] { 10, valueToRemove, 4, 120 };
            var sll = CreateShortList(values);

            sll.Remove(valueToRemove);

            Assert.IsNotNull(sll);
            var valuesInList = sll.GetValues();
            Assert.IsFalse(valuesInList.Contains(valueToRemove));
        }

    }

    internal class MinimalTestClass
    {
        internal int fieldOne;
        internal string fieldTwo;
    }
}