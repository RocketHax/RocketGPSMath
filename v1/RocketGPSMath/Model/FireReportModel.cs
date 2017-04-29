﻿using RocketGPS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketGPSMath.Model
{
    public class FireReportModel
    {
        public GPSCoordinate location;
        public GPSDegree bearing;

        public FireReportModel()
        {
            location = new GPSCoordinate();
            bearing = new GPSDegree();
        }

        public FireReportModel(GPSCoordinate location, GPSDegree bearing) 
        {
            this.location = location;
            this.bearing = bearing;
        }

        public static bool operator ==(FireReportModel instance, FireReportModel other)
        {
            return (instance.bearing == other.bearing && instance.location == other.location);
        }

        public static bool operator !=(FireReportModel instance, FireReportModel other)
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
