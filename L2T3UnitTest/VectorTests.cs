using System;
using L2T3VectorUnitTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace L2T3UnitTest
{
    [TestClass]
    public class VectorTests
    {
        private const double Epsilon = 2.2204460492503131e-016;

        [TestMethod]
        public void TestGetLength()
        {
            var vector = new Vector(2, new[] { 3.0, 4.0 });

            Assert.IsTrue(Math.Abs(vector.GetLength() - 5) < Epsilon, "Должно быть 5");
        }

        [TestMethod]
        public void TestGetSize()
        {
            var vector = new Vector(4, new[] {1.0,1.0, 3.0, 4.0 });

            Assert.IsTrue(Math.Abs(vector.GetSize() - 4) < Epsilon, "Должно быть 4");
        }
    }
}
