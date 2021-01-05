using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grundfos.GeometryModel.Builders.Utils;
using NUnit.Framework;

namespace Grundfos.GeometryModel.Builders.Tests.Utils
{
    [TestFixture]
    public class PathTravellerTests
    {
        [Test]
        public void GetPointBetween_Test()
        {
            var a = new Point2D(0, 0);
            var b = new Point2D(100, 0);

            var result1 = PathTraveller.GetPointBetween(a, b, 50);
            Assert.AreEqual(50, result1.X);
            Assert.AreEqual(0, result1.Y);

            var result2 = PathTraveller.GetPointBetween(a, b, 30);
            Assert.AreEqual(30, result2.X);
            Assert.AreEqual(0, result2.Y);

            var a1 = new Point2D(0, 0);
            var b1 = new Point2D(100, 100);
            var result3 = PathTraveller.GetPointBetween(a1, b1, Math.Sqrt(2 * Math.Pow(50, 2)));
            Assert.AreEqual(50, result3.X);
            Assert.AreEqual(50, result3.Y);
        }

        [Test]
        public void GetPointerAt_OneSegment_Test()
        {
            var path = new List<Point2D>
            {
                new Point2D(0, 0),
                new Point2D(100, 0),
            };

            var pathTraveller = new PathTraveller();

            var pointer = pathTraveller.GetPointerAt(path, 50);
            Assert.AreEqual(50, pointer.Point.X);
            Assert.AreEqual(0, pointer.Point.Y);
            Assert.AreEqual(1, pointer.Direction.X);
            Assert.AreEqual(0, pointer.Direction.Y);


            pointer = pathTraveller.GetPointerAt(path, 100);
            Assert.AreEqual(100, pointer.Point.X);
            Assert.AreEqual(0, pointer.Point.Y);
            Assert.AreEqual(1, pointer.Direction.X);
            Assert.AreEqual(0, pointer.Direction.Y);


            pointer = pathTraveller.GetPointerAt(path, 0);
            Assert.AreEqual(0, pointer.Point.X);
            Assert.AreEqual(0, pointer.Point.Y);
            Assert.AreEqual(1, pointer.Direction.X);
            Assert.AreEqual(0, pointer.Direction.Y);
        }

        [Test]
        public void GetPointerAt_OneSegment_ExceededDistance_ExceptionIsThrown()
        {
            var path = new List<Point2D>
            {
                new Point2D(0, 0),
                new Point2D(100, 0),
            };

            var pathTraveller = new PathTraveller();

            Assert.Throws<ArgumentException>(() => pathTraveller.GetPointerAt(path, 110));
        }

        [Test]
        public void GetPointerAt_TwoSegments_HalfDistanceOfSegment_CorrectPointerIsReturned()
        {
            var path = new List<Point2D>
            {
                new Point2D(0, 0),
                new Point2D(0, 20),
                new Point2D(20, 20),
            };

            var pathTraveller = new PathTraveller();

            var pointer = pathTraveller.GetPointerAt(path, 10);
            Assert.AreEqual(0, pointer.Point.X);
            Assert.AreEqual(10, pointer.Point.Y);
            Assert.AreEqual(0, pointer.Direction.X);
            Assert.AreEqual(1, pointer.Direction.Y);

            pointer = pathTraveller.GetPointerAt(path, 30);
            Assert.AreEqual(10, pointer.Point.X);
            Assert.AreEqual(20, pointer.Point.Y);
            Assert.AreEqual(1, pointer.Direction.X);
            Assert.AreEqual(0, pointer.Direction.Y);
        }

        [Test]
        public void GetPointerAt_TwoSegments_MidpointBtwSegments_CorrectPointerIsReturned()
        {
            var path = new List<Point2D>
            {
                new Point2D(0, 0),
                new Point2D(20, 20),
                new Point2D(40, 0),
            };

            var pathTraveller = new PathTraveller();

            var pointer = pathTraveller.GetPointerAt(path, Math.Sqrt(Math.Pow(20, 2) * 2));
            Assert.AreEqual(20, pointer.Point.X);
            Assert.AreEqual(20, pointer.Point.Y);
            Assert.AreEqual(1, pointer.Direction.X);
            Assert.AreEqual(0, pointer.Direction.Y);


            var path2 = new List<Point2D>
            {
                new Point2D(0, 0),
                new Point2D(20, 20),
                new Point2D(30, 10),
            };

            var pathTraveller2 = new PathTraveller();

            var pointer2 = pathTraveller.GetPointerAt(path, Math.Sqrt(Math.Pow(20, 2) * 2));
            Assert.AreEqual(20, pointer2.Point.X);
            Assert.AreEqual(20, pointer2.Point.Y);
            Assert.AreEqual(1, pointer2.Direction.X);
            Assert.AreEqual(0, pointer2.Direction.Y);

        }
    }
}
