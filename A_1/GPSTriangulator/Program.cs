using GPSTriangulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSTriangulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("GPS Triangulator is running...");

            GPSCoordinate gps1, gps2;
            //gps1 = new GPSCoordinate(new GPSDegree(50, 3, 59), new GPSDegree(5, 42, 53));
            //gps2 = new GPSCoordinate(new GPSDegree(58, 38, 38), new GPSDegree(3, 04, 12));
            gps1 = new GPSCoordinate(new GPSDegree(37.7977008), new GPSDegree(-122.1637914));
            gps2 = new GPSCoordinate(new GPSDegree(37.798392), new GPSDegree(-122.161464));

            double distance = GPSMath.GPSMathUtil.CalculateDistance(gps1, gps2);
            Console.WriteLine("Distance : " + distance.ToString());

            var mid = GPSMath.GPSMathUtil.CalculateMiddleCoordinate(gps1, gps2);
            Console.WriteLine("Mid : " + mid.ToString());


            Console.ReadLine();
        }
    }
}
