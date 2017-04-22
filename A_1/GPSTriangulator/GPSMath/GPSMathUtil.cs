using GPSTriangulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPSTriangulator.GPSMath
{
    public static class GPSMathUtil
    {
        public const double EarthCircumference = 40000.0; // Earth's circumference at the equator in km

        public static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }

        public static double RadiansToDegree(double radians)
        {
            return radians * (180.0 / Math.PI);
        }

        public static double DegreeToDecimal(GPSDegree deg)
        {
            return DegreeToDecimal(deg.degrees, deg.minutes, deg.seconds);
        }

        public static double DegreeToDecimal(double degrees, double minutes, double seconds)
        {
            return (degrees + (minutes / 60) + (seconds / 3600)) * (degrees < 0 ? -1 : 1);
        }

        public static GPSDegree DecimalToGPSDegree(double degrees)
        {
            int sec = (int)Math.Round(degrees * 3600);
            int deg = sec / 3600;
            sec = Math.Abs(sec % 3600);
            int min = sec / 60;
            sec %= 60;

            return new GPSDegree(deg, min, sec);
        }

        //Distance in KM
        public static double CalculateDistance(GPSCoordinate from, GPSCoordinate to)
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
        public static double CalculateDistance(params GPSCoordinate[] coordinates)
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
        public static GPSCoordinate CalculateMiddleCoordinate(GPSCoordinate from, GPSCoordinate to)
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

    }
}
