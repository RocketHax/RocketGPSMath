using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSTriangulator.Model
{
    interface IGPSCoordinateMath
    {
        double ToDouble();
        void FromDouble(double degrees);
    }
}
