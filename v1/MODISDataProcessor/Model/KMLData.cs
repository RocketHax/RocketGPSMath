using RocketGPS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMLModule.Model
{
    public class KMLData
    {
        public string name;
        public GPSCoordinate coordinate;
        public string Description;
        public DateTime date; //time in UTC
        public int fireConfidence; //Should not be here lol
        public string satelliteSource;
    }
}
