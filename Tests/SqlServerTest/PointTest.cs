using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SqlServerTest
{
    [TestClass]
    public class PointTest
    {
        private Point p;

        public PointTest()
        {
            p = new Point();
            p.X = 1.5;
            p.Y = 2;
        }

        [TestMethod]
        public void TestToString()
        {
            Assert.AreEqual("(1,5; 2)", p.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestParse()
        {
            string s = "(1,5; 2)";

            Point p2 = Point.Parse(s);
            Assert.AreEqual(p, p2);

            string s2 = "err";
            p2 = Point.Parse(s2);
            var ex = Assert.ThrowsException<FormatException>(() => { });
            Assert.AreEqual("Invalid coordinates", ex.Message);
        }

        [TestMethod]
        public void TestDistanceFrom()
        {
            Point p2 = new Point();
            p2.X = 2;
            p2.Y = 2;

            Assert.AreEqual(0.0, p.DistanceFrom(p));
            Assert.AreEqual(0.5, p.DistanceFrom(p2));
        }

        [TestMethod]
        public void TestIsInsideCircle()
        {
            Circle c = new Circle();

            // Punkt wewnątrzy okręgu
            c.C = Point.Parse("(1; 1)");
            c.R = 3;
            Assert.IsTrue(p.IsInsideCircle(c));

            // Punkt na okręgu
            c.C = Point.Parse("(1,5; 1,5)");
            c.R = 0.5;
            Assert.IsTrue(p.IsInsideCircle(c));

            // Punkt poza okręgiem
            c.R = 0.2;
            Assert.IsFalse(p.IsInsideCircle(c));
        }

        [TestMethod]
        public void TestIsInsideTriangle()
        {
            Triangle t = new Triangle();

            // Punkt wewnątrz trójkąta
            t.P1 = Point.Parse("(0; 0)");
            t.P2 = Point.Parse("(4; 0)");
            t.P3 = Point.Parse("(0; 4)");
            Assert.IsTrue(p.IsInsideTriangle(t));

            // Punkt na krawędzi
            t.P1 = Point.Parse("(1,5; 2)");
            Assert.IsTrue(p.IsInsideTriangle(t));

            // Punkt poza trójkątem
            t.P1 = Point.Parse("(0; 0)");
            t.P2 = Point.Parse("(1; 0)");
            t.P3 = Point.Parse("(0; 1)");
            Assert.IsFalse(p.IsInsideTriangle(t));
        }

        [TestMethod]
        public void TestIsInsideQuadrangle()
        {
            Quadrangle q = new Quadrangle();

            // Punkt wewnątrz czworokąta
            q.P1 = Point.Parse("(0; 0)");
            q.P2 = Point.Parse("(4; 0)");
            q.P3 = Point.Parse("(4; 4)");
            q.P4 = Point.Parse("(0; 4)");
            Assert.IsTrue(p.IsInsideQuadrangle(q));

            // Punkt na krawędzi czworokąta
            q.P1 = Point.Parse("(1; 0)");
            q.P2 = Point.Parse("(1,5; 0)");
            q.P3 = Point.Parse("(1,5; 4)");
            q.P4 = Point.Parse("(1; 4)");
            Assert.IsTrue(p.IsInsideQuadrangle(q));

            // Punkt poza czworokątem
            q.P1 = Point.Parse("(1; 0)");
            q.P2 = Point.Parse("(1,5; 0)");
            q.P3 = Point.Parse("(1,5; 1)");
            q.P4 = Point.Parse("(1; 1)");
            Assert.IsFalse(p.IsInsideQuadrangle(q));
        }
    }
}
