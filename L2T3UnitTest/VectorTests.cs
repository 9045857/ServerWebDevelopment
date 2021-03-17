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

            Assert.IsTrue(Math.Abs(vector.GetLength() - 5) < Epsilon, "������ ���� 5");
        }

        [TestMethod]
        public void TestGetSize()
        {
            var vector = new Vector(4, new[] { 1.0, 1.0, 3.0, 4.0 });

            Assert.IsTrue(Math.Abs(vector.GetSize() - 4) < Epsilon, "������ ���� 4");
        }

        [TestMethod]
        public void TestGetScalarProduct()
        {
            var vector1 = new Vector(2, new[] { 2.0, 2.0 });
            var vector2 = new Vector(2, new[] { 3.0, 3.0 });

            Assert.IsTrue(Math.Abs(Vector.GetScalarProduct(vector1, vector2) - 12) < Epsilon, "������ ���� 12");
        }

        [TestMethod]
        public void TestAddVector()
        {
            var vector1 = new Vector(2, new[] { 3.0, 3.0 });
            var vector2 = new Vector(2, new[] { 5.0, 4.0 });

            Assert.AreEqual(new Vector(2, new[] { 8.0, 7.0 }), vector1.AddVector(vector2));
        }
    }
}
