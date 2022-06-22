using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SqlServerTest
{
    /*
     * Klasa TriangleTest testująca metody z UDT Triangle
    */
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

        // Test metody Triangle.ToString()
        [TestMethod]
        public void TestToString()
        {
            Assert.AreEqual("(0; 0),(1; 0),(0; 1)", t.ToString());
        }

        // Test metody Triangle.Parse()
        [TestMethod]
        public void TestParse()
        {
            string s = "(0; 0),(1; 0),(0; 1)";

            Triangle t2 = Triangle.Parse(s);
            Assert.AreEqual(t, t2);
        }

        // Test metody Triangle.GetSurfaceArea()
        [TestMethod]
        public void TestGetSurfaceArea()
        {
            double expectedArea = 0.5;
            Assert.AreEqual(expectedArea, t.getSurfaceArea());
        }

        // Test metody Triangle.ValidateTriangle()
        [TestMethod]
        public void TestValidateTriangle()
        {
            Assert.IsTrue(t.ValidateTriangle());

            t.P3 = Point.Parse("(2; 0)");
            Assert.IsFalse(t.ValidateTriangle());
        }
    }
}
