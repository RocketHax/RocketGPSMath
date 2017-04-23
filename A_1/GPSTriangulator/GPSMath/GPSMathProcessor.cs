using GPSTriangulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPSTriangulator.GPSMath
{
    public class GPSMathProcessor
    {
        //Constants
        const int DistancePrecision = 1;
        const double EarthRadius = 6371e3; // Earth radius = 6,371km

        //Core Instance for lazy bum
        static GPSMathProcessor Processor = new GPSMathProcessor();

        //For lazy bum
        public static GPSMathProcessor Get()
        {
            return Processor;
        }

        //////////////////////////////////////////////////////////////////////////////////////////
        //Basic Calculations//////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////

        public double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }

        public double RadiansToDegree(double radians)
        {
            return radians * (180.0 / Math.PI);
        }

        //////////////////////////////////////////////////////////////////////////////////////////
        //Distance in KM, using Haversine formula/////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////

        public double CalculateDistance(GPSCoordinate from, GPSCoordinate to)
        {
            var lat1Rad = DegreesToRadians(from.latitude.ToDouble());
            var lat2Rad = DegreesToRadians(to.latitude.ToDouble());
            var dlat1lat2 = DegreesToRadians((to.latitude.ToDouble() - from.latitude.ToDouble()));
            var dlong1long2 = DegreesToRadians((to.longitude.ToDouble() - from.longitude.ToDouble()));

            var a = Math.Sin(dlat1lat2 / 2) * Math.Sin(dlat1lat2 / 2) +
                    Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                    Math.Sin(dlong1long2 / 2) * Math.Sin(dlong1long2 / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            //Set to one decimal places
            return Math.Round((EarthRadius * c) / 1000, DistancePrecision); 
        }

        //Calculate total distance
        public double CalculateDistance(params GPSCoordinate[] coordinates)
        {
            double totalDistance = 0.0;

            for (int i = 0; i < coordinates.Length - 1; i++)
            {
                GPSCoordinate current = coordinates[i];
                GPSCoordinate next = coordinates[i + 1];

                totalDistance += CalculateDistance(current, next);
            }

            return totalDistance;
        }

        //////////////////////////////////////////////////////////////////////////////////////////
        //Mid Point calculation///////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////

        public GPSCoordinate CalculateMiddle(GPSCoordinate from, GPSCoordinate to)
        {
            double lat1Rad = DegreesToRadians(from.latitude.ToDouble());
            double lat2Rad = DegreesToRadians(to.latitude.ToDouble());
            double long1Rad = DegreesToRadians(from.longitude.ToDouble());
            double long2Rad = DegreesToRadians(to.longitude.ToDouble());

            double Bx = Math.Cos(lat2Rad) * Math.Cos(long2Rad - long1Rad);
            double By = Math.Cos(lat2Rad) * Math.Sin(long2Rad - long1Rad);
            double latM = Math.Atan2(Math.Sin(lat1Rad) + Math.Sin(lat2Rad),
                                Math.Sqrt((Math.Cos(lat1Rad) + Bx) * (Math.Cos(lat1Rad) + Bx) + By * By));
            double longM = long1Rad + Math.Atan2(By, Math.Cos(lat1Rad) + Bx);

            var a = RadiansToDegree(latM);
            var b = RadiansToDegree(longM);

            return new GPSCoordinate(new GPSDegree(RadiansToDegree(latM)), new GPSDegree(RadiansToDegree(longM)));
        }

        //////////////////////////////////////////////////////////////////////////////////////////
        //DMS to DD///////////////////////////////////////////////////////////////////////////////
        //DMS(127o 30' 00") = 127 + (30/60) + (0/3600) = DEC(127.5)///////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////

        public double GPSDegreeToDecimal(int d, int m, double s)
        {
            return GPSDegreeToDecimal(new GPSDegree(d, m, s));
        }

        public double GPSDegreeToDecimal(GPSDegree source)
        {
            double d = source.degrees;
            double m = source.minutes;
            double s = source.seconds;

            return (d + (m / 60) + (s / 3600)) * (d < 0 ? -1 : 1);
        }

        //////////////////////////////////////////////////////////////////////////////////////////
        //DD to DMS///////////////////////////////////////////////////////////////////////////////
        //DEC(127.5) = D(floor(127.5))o M(floor(fraction of deg*60))' S(fraction of min*60)"//////
        //////////////////////////////////////////////////////////////////////////////////////////

        public GPSDegree DecimalToGPSDegree(double source)
        {
            //decimal degrees
            double decdeg = source;
            double minsec = (decdeg - Math.Truncate(decdeg)) * 60;
            double sec = (minsec - Math.Truncate(minsec)) * 60;

            return new GPSDegree((int)Math.Truncate(decdeg), (int)Math.Truncate(minsec), sec);
        }



    }
}