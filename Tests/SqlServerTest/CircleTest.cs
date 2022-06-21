using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqlServerTest
{
    [TestClass]
    public class CircleTest
    {

        Circle c;
        public CircleTest()
        {
            c = new Circle();
            c.C = Point.Parse("(0; 0)");
            c.R = 2;
        }

        [TestMethod]
        public void TestToString()
        {
            Assert.AreEqual("c=(0; 0) r=2", c.ToString());
        }

        [TestMethod]
        public void TestParse()
        {
            string s = "c=(0; 0) r=2";

            Circle c2 = Circle.Parse(s);
            Assert.AreEqual(c, c2);
        }

        [TestMethod]
        public void TestGetSurfaceArea()
        {
            double expectedArea = Math.PI * 4;
            Assert.AreEqual(expectedArea, c.getSurfaceArea());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestValidateCircle()
        {
            Assert.IsTrue(c.ValidateCircle());

            c.R = -1;
            var ex = Assert.ThrowsException<ArgumentException>(() => {});
            Assert.AreEqual("Invalid radius", ex.Message);
        }
    }
}
