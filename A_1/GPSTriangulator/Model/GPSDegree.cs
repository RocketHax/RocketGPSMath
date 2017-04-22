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

        public void FromDouble(double degrees)
        {
            var r = GPSMathUtil.DecimalToGPSDegree(degrees);
            degrees = r.degrees;
            minutes = r.minutes;
            seconds = r.seconds;
        }

        public double ToDouble()
        {
            return GPSMathUtil.DegreeToDecimal(degrees, minutes, seconds);
        }

        public static GPSDegree operator+(GPSDegree instance, GPSDegree other)
        {
            return new GPSDegree(instance.degrees + other.degrees, instance.minutes + other.minutes, instance.seconds + other.seconds);
        }
    }
}
