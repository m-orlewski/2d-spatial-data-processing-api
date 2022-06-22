using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SqlServerTest
{
    /*
     * Klasa CircleTest testująca metody z UDT Circle
    */
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

        // Test metody Circle.ToString()
        [TestMethod]
        public void TestToString()
        {
            Assert.AreEqual("c=(0; 0) r=2", c.ToString());
        }

        // Test metody Circle.Parse()
        [TestMethod]
        public void TestParse()
        {
            string s = "c=(0; 0) r=2";

            Circle c2 = Circle.Parse(s);
            Assert.AreEqual(c, c2);
        }

        // Test metody Circle.GetSurfaceArea()
        [TestMethod]
        public void TestGetSurfaceArea()
        {
            double expectedArea = Math.PI * 4;
            Assert.AreEqual(expectedArea, c.getSurfaceArea());
        }

        // Test metody Circle.ValidateCircle()
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
