using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KMLModule.Parser;
using System.Collections.Generic;
using KMLModule.Model;

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

            List<KMLData> modisDatas;
            bool success = parser.Read(path, out modisDatas);

            Assert.IsTrue(success);
            Assert.IsTrue(modisDatas.Count == 904);

            path = "TestData\\VIIRSFire\\VNP14IMGTDL_NRT_USA_contiguous_and_Hawaii_24h.kml";

            List<KMLData> viirsDatas;
            success = parser.Read(path, out viirsDatas);

            Assert.IsTrue(success);
            Assert.IsTrue(viirsDatas.Count == 3743);

            List<KMLData> dummyDatas;
            success = parser.Read("", out dummyDatas);
            Assert.IsFalse(success);
        }
    }
}
