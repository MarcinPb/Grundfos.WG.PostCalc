using System;
using System.Collections.Generic;
using Grundfos.GeometryModel.Builders.Utils;
using NUnit.Framework;

namespace Grundfos.GeometryModel.Builders.Tests.Utils
{
    [TestFixture]
    public class PathLenghtResolverTests
    {
        [Test]
        public void GetLength_Test1()
        {
            var path = new List<Point2D>
            {
                new Point2D(0, 0),
                new Point2D(100, 0),
                new Point2D(100, 100),
                new Point2D(0, 100),
                new Point2D(0, 0),
            };

            var lengthResolver = new PathLenghtResolver();

            var actual = lengthResolver.GetLength(path);
            Assert.AreEqual(400d, actual);
        }

        [Test]
        public void GetLength_Test2()
        {
            var path = new List<Point2D>
            {
                new Point2D(0, 0),
                new Point2D(100, 100),
            };

            var lengthResolver = new PathLenghtResolver();

            var actual = lengthResolver.GetLength(path);
            Assert.AreEqual(Math.Sqrt(20000), actual);
        }
    }
}
