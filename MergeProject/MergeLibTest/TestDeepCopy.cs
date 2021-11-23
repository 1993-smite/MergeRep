using MergeLib;
using NUnit.Framework;
using System;

namespace MergeLibTest
{
    public class TestDeepCopy
    {
        [Serializable]
        public class Simple
        {
            public int Id;
            public string Name;
        }

        [Test]
        [TestCase(1,"Name 1")]
        [TestCase(int.MinValue, "Name Min Val")]
        [TestCase(int.MaxValue, "Name Max Val")]
        public void TestDeepCopySimple(int id, string name)
        {
            var simpleOne = new Simple()
            {
                Id = id,
                Name = name
            };
            var simpleCopy = simpleOne.DeepCopy();

            Assert.AreEqual(simpleOne.Id, simpleCopy.Id);
            Assert.AreEqual(simpleOne.Name, simpleCopy.Name);
            Assert.AreNotEqual(simpleOne, simpleCopy);
        }

        [Serializable]
        public class Middle
        {
            private int id;
            public int Id => id;

            protected string name;
            public string Name => name;

            internal static string Type = "Hard";

            public const long MaxVal = long.MaxValue;

            private Simple model;
            public Simple Simple => model;

            public Middle(int id, string name, int simpleId = 0)
            {
                this.id = id;
                this.name = name;

                model = new Simple()
                {
                    Id = simpleId,
                    Name = name
                };
            }
        }

        [Test]
        [TestCase(1, "Name 1", 1000)]
        [TestCase(int.MinValue, "Name Min Val", int.MaxValue)]
        [TestCase(int.MaxValue, "Name Max Val", int.MinValue)]
        [TestCase(int.MaxValue, "Name Max Val")]
        public void TestDeepCopyMiddle(int id, string name, int simpleId = 10000)
        {
            var middleOne = new Middle(id, name, simpleId);
            var middleCopy = middleOne.DeepCopy();

            Assert.AreEqual(middleOne.Id, middleCopy.Id);
            Assert.AreEqual(middleOne.Name, middleCopy.Name);

            Assert.AreNotEqual(middleOne.Simple, middleCopy.Simple);
            Assert.AreEqual(middleOne.Simple.Id, middleCopy.Simple.Id);
            Assert.AreEqual(middleOne.Simple.Name, middleCopy.Simple.Name);

            Assert.AreNotEqual(middleOne, middleCopy);
        }
    }
}
