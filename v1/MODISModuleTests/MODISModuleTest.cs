using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KMLModule.Parser;

namespace KMLModuleTests
{
    [TestClass]
    public class KMLParserTest
    {
        [TestMethod]
        public void TestReadKml()
        {
            var parser = KMLParser.Get();

            string path = "TestData\\MODISFire\\MODIS_C6_USA_contiguous_and_Hawaii_24h.kml";
            bool success = parser.Read(path);

            Assert.IsTrue(success);

            path = "TestData\\VIIRSFire\\VNP14IMGTDL_NRT_USA_contiguous_and_Hawaii_24h.kml";
            success = parser.Read(path);

            Assert.IsTrue(success);

            success = parser.Read("");
            Assert.IsFalse(success);
        }
    }
}
