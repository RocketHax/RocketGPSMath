using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GPSTriangulator.GPSMath;
using GPSTriangulator.Model;

namespace GPSTriangulatorTests
{
    [TestClass]
    public class GPSMathTest
    {
        const double dEpsilon = 0.00000001; // 10e-8 precision

        private bool DoubleEqual(double a, double b)
        {
            return Math.Abs(a - b) < dEpsilon;
        }

        [TestMethod]
        public void TestGPSDegreeToDD()
        {
            //input:  52 9 28.83, 5 23 23.85
            //to D :  52.15800833,   5.38995833

            //#1
            double actual = GPSMathProcessor.Get().GPSDegreeToDecimal(new GPSDegree(52, 9, 28.83));
            double expected = 52.15800833;

            Assert.IsTrue(DoubleEqual(actual, expected));

            //#2
            actual = GPSMathProcessor.Get().GPSDegreeToDecimal(new GPSDegree(5, 23, 23.85));
            expected = 5.38995833;

            Assert.IsTrue(DoubleEqual(actual, expected));

            //#3
            actual = GPSMathProcessor.Get().GPSDegreeToDecimal(5, 23, 23.85);

            Assert.IsTrue(DoubleEqual(actual, expected));
        }

        [TestMethod]
        public void TestDDToGPSDegree()
        {
            //input:  52.15800833, 5.38995833
            //toDMS:  52  9 28.8300,   5 23 23.8500

            //#1
            GPSDegree actual = GPSMathProcessor.Get().DecimalToGPSDegree(52.15800833);
            GPSDegree expected = new GPSDegree(52,9, 28.8300);

            Assert.IsTrue(actual == expected);

            //#2
            actual = GPSMathProcessor.Get().DecimalToGPSDegree(5.38995833);
            expected = new GPSDegree(5, 23, 23.8500);

            Assert.IsTrue(actual == expected);
        }

        [TestMethod]
        public void TestDistanceCalculation()
        {
            //input:  52.15800833, 5.38995833
            //toDMS:  52  9 28.8300,   5 23 23.8500



        }
    }
}