using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RocketGPSMath.GPSMath;
using System.Collections.Generic;
using RocketGPSMath.Model;

namespace GPSTriangulatorTests
{
    [TestClass]
    public class RegionTriangulatorTests
    {
        [TestMethod]
        public void TestTriangulateRegion()
        {
            List<ReportingModel> reports = new List<ReportingModel>();

            RegionTriangulator triangulator = new RegionTriangulator(reports);

            Assert.IsNull(triangulator.Triangulate());

            reports.Add(new ReportingModel(52.00000, 5.00000, 90));
            reports.Add(new ReportingModel(51.00000, 7.00000, 0));
            reports.Add(new ReportingModel(51.44374, 5.98755, 90));
            reports.Add(new ReportingModel(51.08972, 6.4270, 0));

            var res = triangulator.Triangulate();

            Assert.IsNotNull(res);
            Assert.Equals(res.Count, 6);
        }
    }
}
