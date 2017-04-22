using GPSTriangulator.GPSMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSTriangulator.Model
{
    public class GPSDegree : IGPSCoordinateMath
    {
        public double degrees;
        public double minutes;
        public double seconds;

        public GPSDegree()
        {
        degrees = 0;
        minutes = 0;
        seconds = 0.0f;
        }

        public GPSDegree(double degrees, double minutes, double seconds)
        {
            this.degrees = degrees;
            this.minutes = minutes;
            this.seconds = seconds;
        }

        public GPSDegree(double degrees)
        {
            //Console.WriteLine("//////////////////////////");
            //Console.WriteLine(degrees.ToString());
            //Console.WriteLine("//////////////////////////");
            FromDouble(degrees);
        }

        public void FromDouble(double degrees)
        {
            var r = GPSMathUtil.DecimalToGPSDegree(degrees);
            degrees = r.degrees;
            minutes = r.minutes;
            seconds = r.seconds;

            //Console.WriteLine("//////////////////////////");
            //Console.WriteLine(r.degrees.ToString());
            //Console.WriteLine(r.minutes.ToString());
            //Console.WriteLine(r.seconds.ToString());
            //Console.WriteLine(ToDouble().ToString());
            //Console.WriteLine(GPSMathUtil.RadiansToDegree( ToDouble()).ToString());
            //Console.WriteLine("//////////////////////////");

        }

        public double ToDouble()
        {
            return GPSMathUtil.DegreeToDecimal(this);
        }

        public static GPSDegree operator+(GPSDegree instance, GPSDegree other)
        {
            return new GPSDegree(instance.degrees + other.degrees, instance.minutes + other.minutes, instance.seconds + other.seconds);
        }
    }
}
