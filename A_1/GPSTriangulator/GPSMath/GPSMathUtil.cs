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

        public static double RadiansToDegree(double degrees)
        {
            return degrees * (180.0 / Math.PI);
        }

        public static double DegreeToDecimal(GPSDegree deg)
        {
            return DegreeToDecimal(deg.degrees, deg.minutes, deg.seconds);
        }

        public static double DegreeToDecimal(double degrees, double minutes, double seconds)
        {
            return degrees + (minutes / 60) + (seconds / 3600);
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

        public static GPSCoordinate CalculateMiddleCoordinate(GPSCoordinate posA, GPSCoordinate posB)
        {
            GPSCoordinate midPoint = new GPSCoordinate();

            double dLon = DegreesToRadians(posB.longitude.ToDouble() - posA.longitude.ToDouble());
            double Bx = Math.Cos(DegreesToRadians(posB.latitude.ToDouble())) * Math.Cos(dLon);
            double By = Math.Cos(DegreesToRadians(posB.latitude.ToDouble())) * Math.Sin(dLon);

            midPoint.latitude = DecimalToGPSDegree(Math.Atan2(
                         Math.Sin(DegreesToRadians(posA.latitude.ToDouble())) + Math.Sin(DegreesToRadians(posB.latitude.ToDouble())),
                         Math.Sqrt(
                             (Math.Cos(DegreesToRadians(posA.latitude.ToDouble())) + Bx) *
                             (Math.Cos(DegreesToRadians(posA.latitude.ToDouble())) + Bx) + By * By)));

            midPoint.longitude = posA.longitude + DecimalToGPSDegree(Math.Atan2(By, Math.Cos(DegreesToRadians(posA.latitude.ToDouble())) + Bx));

            return midPoint;
        }

    }
}
