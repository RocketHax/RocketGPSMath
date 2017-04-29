using RocketGPS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketGPSMath.Model
{
    public class ReportingModel
    {
        public GPSCoordinate location;
        public GPSDegree bearing;

        public ReportingModel()
        {
            location = new GPSCoordinate();
            bearing = new GPSDegree();
        }

        public ReportingModel(GPSCoordinate location, GPSDegree bearing) 
        {
            this.location = location;
            this.bearing = bearing;
        }

        public ReportingModel(double latitude, double longitude, double bearing)
        {
            this.location = new GPSCoordinate(latitude, longitude);
            this.bearing = new GPSDegree(bearing);
        }

        public static bool operator ==(ReportingModel instance, ReportingModel other)
        {
            return (instance.bearing == other.bearing && instance.location == other.location);
        }

        public static bool operator !=(ReportingModel instance, ReportingModel other)
        {
            return instance == other;
        }

        //[AH] Not changing this until it makes sense
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
