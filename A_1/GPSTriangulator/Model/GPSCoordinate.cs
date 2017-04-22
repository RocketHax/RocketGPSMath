using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSTriangulator.Model
{
    public class GPSCoordinate
    {
        public GPSDegree latitude;
        public GPSDegree longitude;

        public GPSCoordinate()
        {
            latitude = new GPSDegree();
            longitude = new GPSDegree();
        }

        public GPSCoordinate(GPSDegree latitude, GPSDegree longitute)
        {
            this.latitude = latitude;
            this.longitude = longitute;
        }

        public new string ToString()
        {
            string lat = "Latitute : " + latitude.degrees.ToString() + " " + latitude.minutes.ToString() + " " + latitude.seconds.ToString();
            string lgt = "Longitude : " + longitude.degrees.ToString() + " " + longitude.minutes.ToString() + " " + longitude.seconds.ToString();

            return lat + " " + lgt;
        }
    }
}
