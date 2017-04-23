using GPSTriangulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPSTriangulator.GPSMath
{
    public class GPSMathProcessor
    {
        static GPSMathProcessor calculator = new GPSMathProcessor();

        public const double EarthCircumference = 40000.0; // Earth's circumference at the equator in km

        //For lazy bum
        public static GPSMathProcessor Get()
        {
            return calculator;
        }

        public double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }

        public double RadiansToDegree(double radians)
        {
            return radians * (180.0 / Math.PI);
        }

        //Distance in KM, using Haversine formula
        public double CalculateDistance(GPSCoordinate from, GPSCoordinate to)
        {
            //Calculate radians
            double latitude1Rad = DegreesToRadians(from.latitude.ToDouble());
            double longitude1Rad = DegreesToRadians(from.longitude.ToDouble());
            double latititude2Rad = DegreesToRadians(to.latitude.ToDouble());
            double longitude2Rad = DegreesToRadians(to.longitude.ToDouble());

            double logitudeDiff = Math.Abs(longitude1Rad - longitude2Rad);

            if (logitudeDiff > Math.PI)
                logitudeDiff = 2.0 * Math.PI - logitudeDiff;

            double angleCalculation =
                Math.Acos(
                  Math.Sin(latititude2Rad) * Math.Sin(latitude1Rad) +
                  Math.Cos(latititude2Rad) * Math.Cos(latitude1Rad) * Math.Cos(logitudeDiff));

            return EarthCircumference * angleCalculation / (2.0 * Math.PI);
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

        //[AH] WRONG!!!
        public GPSCoordinate CalculateMiddleCoordinate(GPSCoordinate from, GPSCoordinate to)
        {
            double dLon = DegreesToRadians(to.longitude.ToDouble() - from.latitude.ToDouble());

            //convert to radians
            var lat1 = DegreesToRadians(from.latitude.ToDouble());
            var lat2 = DegreesToRadians(to.latitude.ToDouble());
            var lon1 = DegreesToRadians(from.longitude.ToDouble());

            double Bx = Math.Cos(lat2) * Math.Cos(dLon);
            double By = Math.Cos(lat2) * Math.Sin(dLon);
            double lat3 = Math.Atan2(Math.Sin(lat1) + Math.Sin(lat2), Math.Sqrt((Math.Cos(lat1) + Bx) * (Math.Cos(lat1) + Bx) + By * By));
            double lon3 = lon1 + Math.Atan2(By, Math.Cos(lat1) + Bx);

            //Console.WriteLine(lat3.ToString());
            //Console.WriteLine(lon3.ToString());
            //Console.WriteLine(RadiansToDegree(lat3).ToString());
            //Console.WriteLine(RadiansToDegree(lon3).ToString());

            return new GPSCoordinate(DecimalToGPSDegree(RadiansToDegree(lat3)), DecimalToGPSDegree(RadiansToDegree(lon3)));
        }

        //////////////////////////////////////////////////////////////////////////////////////////
        //DMS to DD///////////////////////////////////////////////////////////////////////////////
        //DMS(127o 30' 00") = 127 + (30/60) + (0/3600) = DEC(127.5)///////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////

        public double GPSDegreeToDecimal(double d, double m, double s)
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

            return new GPSDegree(Math.Truncate(decdeg), Math.Truncate(minsec), sec);
        }

    }
}