using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Grundfos.GeometryModel.Builders.Tests
{
    [TestFixture]
    public class UnitVectorBuildTests
    {
        [Test]
        public void Build_Test()
        {
            var vector1 = UnitVector2D.Build(4, 4);
            var expected = 1 / Math.Sqrt(2);
            Assert.AreEqual(expected, vector1.X);
            Assert.AreEqual(expected, vector1.Y);

            var vector2 = UnitVector2D.Build(-4, -4);
            Assert.AreEqual(-expected, vector2.X);
            Assert.AreEqual(-expected, vector2.Y);

            var vector3 = UnitVector2D.Build(-4, 4);
            Assert.AreEqual(-expected, vector3.X);
            Assert.AreEqual(expected, vector3.Y);

            var vector4 = UnitVector2D.Build(0, 100);
            Assert.AreEqual(0, vector4.X);
            Assert.AreEqual(1, vector4.Y);

            var vector5 = UnitVector2D.Build(110, 0);
            Assert.AreEqual(1, vector5.X);
            Assert.AreEqual(0, vector5.Y);
        }
    }
}
