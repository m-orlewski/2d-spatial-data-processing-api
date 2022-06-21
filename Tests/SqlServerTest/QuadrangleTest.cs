using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqlServerTest
{
    [TestClass]
    public class QuadrangleTest
    {
        Quadrangle q;
        public QuadrangleTest()
        {
            q = new Quadrangle();
            q.P1 = Point.Parse("(0; 0)");
            q.P2 = Point.Parse("(1; 0)");
            q.P3 = Point.Parse("(1; 1)");
            q.P4 = Point.Parse("(0; 1)");
        }

        [TestMethod]
        public void TestToString()
        {
            Assert.AreEqual("(0; 0),(1; 0),(1; 1),(0; 1)", q.ToString());
        }

        [TestMethod]
        public void TestParse()
        {
            string s = "(0; 0),(1; 0),(1; 1),(0; 1)";

            Quadrangle q2 = Quadrangle.Parse(s);
            Assert.AreEqual(q, q2);
        }

        [TestMethod]
        public void TestGetSurfaceArea()
        {
            double expectedArea = 1;
            Assert.AreEqual(expectedArea, q.getSurfaceArea());
        }

        [TestMethod]
        public void TestValidateQuadrangle()
        {
            Assert.IsTrue(q.ValidateQuadrangle());

            q.P4 = Point.Parse("(1; 1)");
            Assert.IsFalse(q.ValidateQuadrangle());
        }
    }
}
