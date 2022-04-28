using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PudelkoLibrary
{
    public class Pudelko
    {
        public double A;
        public double B;
        public double C;
        private UnitOfMeasure Unit;

        public Pudelko(double? a = null, double? b = null, double? c = null, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            // Default dimensions: 10 cm × 10 cm × 10 cm
            double defaultDimension = 0.1;
            switch (unit)
            {
                case UnitOfMeasure.milimeter:
                    defaultDimension = 100;
                    break;
                case UnitOfMeasure.centimeter:
                    defaultDimension = 10;
                    break;
            }

            // For internal use dimensions are stored as meters
            Unit = UnitOfMeasure.meter;
            A = UnitOfMeasureMethods.Convert(a ?? defaultDimension, unit, UnitOfMeasure.meter, 3);
            B = UnitOfMeasureMethods.Convert(b ?? defaultDimension, unit, UnitOfMeasure.meter, 3);
            C = UnitOfMeasureMethods.Convert(c ?? defaultDimension, unit, UnitOfMeasure.meter, 3);

            // Creation exceptions
            if (A <= 0 || B <= 0 || C <= 0)
            {
                throw new ArgumentOutOfRangeException("Pudelko dimensions must be larger than 0");
            }
            if (A >= 10 || B >= 10 || C >= 10)
            {
                throw new ArgumentOutOfRangeException("Pudelko dimensions must be smaller than 10 m");
            }
        }
    }
}
