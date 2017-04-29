﻿using RocketGPS.GPSMath;
using RocketGPS.Model;
using RocketGPSMath.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketGPSMath.GPSMath
{
    public class RegionTriangulator
    {
        public List<ReportingModel> reports;

        public RegionTriangulator()
        {

        }

        public RegionTriangulator(List<ReportingModel> reports)
        {
            this.reports = reports;
        }

        //Should return number of coordinates where N > 1, Count = N(N-1)/2
        public List<GPSCoordinate> Triangulate()
        {
            if (reports == null || reports.Count < 2)
                return null;

            List<GPSCoordinate> resultingRegion = new List<GPSCoordinate>();

            for (int i = 0; i < reports.Count; ++i)
            {
                var currReport = reports[i];

                for(int j = i + 1; j < reports.Count; ++j)
                {
                    var otherReport = reports[j];
                    var res = GPSMathProcessor.Get().CalculateIntersection(currReport, otherReport);
                    resultingRegion.Add(res);
                }
            }

            return resultingRegion;
        }

    }
}