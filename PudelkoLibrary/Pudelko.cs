using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PudelkoLibrary
{
    public class Pudelko : IFormattable, IEquatable<Pudelko>
    {
        private UnitOfMeasure Unit;

        private double a, b, c;
        public double A
        {
            get => Convert.ToDouble(a.ToString("0.000"));
            private set { a = value; }
        }
        public double B
        {
            get => Convert.ToDouble(b.ToString("0.000"));
            private set { b = value; }
        }
        public double C
        {
            get => Convert.ToDouble(c.ToString("0.000"));
            private set { c = value; }
        }

        public double Objetosc { get => Math.Round((A * B * C), 9); }

        public double Pole { get => Math.Round(2 * (A * B + A * C + B * C), 6); }

        public override string ToString()
        {
            return ToString("m");
        }

        public string ToString(string format)
        {
            if (format == null)
            {
                return ToString("m", null);
            }
            return ToString(format, null);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            var a = A;
            var b = B;
            var c = C;
            switch (format)
            {
                case "m":
                    return $"{a.ToString("0.000")} {format} × {b.ToString("0.000")} {format} × {c.ToString("0.000")} {format}";
                case "cm":
                    a = UnitOfMeasureMethods.Convert(A, UnitOfMeasure.meter, UnitOfMeasure.centimeter, 3);
                    b = UnitOfMeasureMethods.Convert(B, UnitOfMeasure.meter, UnitOfMeasure.centimeter, 3);
                    c = UnitOfMeasureMethods.Convert(C, UnitOfMeasure.meter, UnitOfMeasure.centimeter, 3);
                    return $"{a.ToString("0.0")} {format} × {b.ToString("0.0")} {format} × {c.ToString("0.0")} {format}";
                case "mm":
                    a = UnitOfMeasureMethods.Convert(A, UnitOfMeasure.meter, UnitOfMeasure.milimeter, 3);
                    b = UnitOfMeasureMethods.Convert(B, UnitOfMeasure.meter, UnitOfMeasure.milimeter, 3);
                    c = UnitOfMeasureMethods.Convert(C, UnitOfMeasure.meter, UnitOfMeasure.milimeter, 3);
                    return $"{a} {format} × {b} {format} × {c} {format}";
                default:
                    throw new FormatException();
            }
        }
        public override bool Equals(object obj)
        {
            if (obj is Pudelko)
            {
                return Equals((Pudelko)obj);
            }

            return base.Equals(obj);
        }
        public bool Equals(Pudelko obj)
        {
            return (Objetosc == obj.Objetosc && Pole == obj.Pole);
        }

        public override int GetHashCode()
        {
            return A.GetHashCode() + B.GetHashCode() + C.GetHashCode() + Unit.GetHashCode();
        }

        public static bool operator ==(Pudelko obj1, Pudelko obj2)
        {
            return obj1.Equals(obj2);
        }
        public static bool operator !=(Pudelko obj1, Pudelko obj2)
        {
            return !obj1.Equals(obj2);
        }

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
