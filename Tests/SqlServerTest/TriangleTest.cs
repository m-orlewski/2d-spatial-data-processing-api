using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqlServerTest
{
    [TestClass]
    public class TriangleTest
    {

        Triangle t;
        public TriangleTest()
        {
            t = new Triangle();
            t.P1 = Point.Parse("(0; 0)");
            t.P2 = Point.Parse("(1; 0)");
            t.P3 = Point.Parse("(0; 1)");
        }

        [TestMethod]
        public void TestToString()
        {
            Assert.AreEqual("(0; 0),(1; 0),(0; 1)", t.ToString());
        }

        [TestMethod]
        public void TestParse()
        {
            string s = "(0; 0),(1; 0),(0; 1)";

            Triangle t2 = Triangle.Parse(s);
            Assert.AreEqual(t, t2);
        }

        [TestMethod]
        public void TestGetSurfaceArea()
        {
            double expectedArea = 0.5;
            Assert.AreEqual(expectedArea, t.getSurfaceArea());
        }

        [TestMethod]
        public void TestValidateTriangle()
        {
            Assert.IsTrue(t.ValidateTriangle());

            t.P3 = Point.Parse("(2; 0)");
            Assert.IsFalse(t.ValidateTriangle());
        }
    }
}
