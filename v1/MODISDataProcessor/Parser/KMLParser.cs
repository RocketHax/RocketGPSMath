using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpKml.Dom;
using SharpKml.Base;
using SharpKml.Engine;
using System.IO;
using System.Xml;
using KMLModule.Model;
using RocketGPS.Model;

namespace KMLModule.Parser
{
    public class KMLParser
    {
        private static KMLParser parser = new KMLParser();

        public static KMLParser Get()
        {
            return parser;
        }

        public bool Read(string filePath)
        {
            List<KMLData> mDatas = new List<KMLData>();

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                string xmlcontents = doc.InnerXml;

                KmlFile file;
                using (var stream = new MemoryStream(ASCIIEncoding.UTF8.GetBytes(xmlcontents)))
                    file = KmlFile.Load(stream);

                foreach (var p in file.Root.Flatten().OfType<Placemark>())
                {
                    KMLData m = new KMLData();
                    m.name = p.Name;
                    m.Description = p.Description.Text;

                    var pt = p.Flatten().OfType<Point>().ElementAt(0);
                    m.coordinate = new GPSCoordinate(pt.Coordinate.Latitude, pt.Coordinate.Longitude);

                    mDatas.Add(m);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

    }
}
