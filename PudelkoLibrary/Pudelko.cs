using System;
using System.Collections;
using System.Collections.Generic;

namespace PudelkoLibrary
{
    public sealed class Pudelko : IFormattable, IEquatable<Pudelko>, IEnumerable<double>, IComparable<Pudelko>
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

        public static Pudelko Parse(string input)
        {
            var arrString = input.Split(' ');
            var arrDouble = new double[3];

            for (int i = 0; i < arrString.Length; i++)
            {
                if (!double.TryParse(arrString[i], out double temp) || i == arrDouble.Length)
                {
                    throw new FormatException($"Wrong string format: {input}");
                }
                arrDouble[i] = temp;
            }
            return new Pudelko(arrDouble[0], arrDouble[1], arrDouble[2]);
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

        public static explicit operator double[](Pudelko obj)
        {
            return new double[] { obj.A, obj.B, obj.C };
        }

        public static explicit operator Pudelko(double[] obj)
        {
            return new Pudelko(obj[0], obj[1], obj[2]);
        }

        public static implicit operator ValueTuple<double, double, double>(Pudelko obj)
        {
            return (obj.A, obj.B, obj.C);
        }

        public static implicit operator Pudelko(ValueTuple<double, double, double> obj)
        {
            return new Pudelko(obj.Item1, obj.Item2, obj.Item3, UnitOfMeasure.milimeter);
        }

        public static Pudelko operator +(Pudelko obj1, Pudelko obj2)
        {
            double[] a1 = (double[])obj1;
            double[] a2 = (double[])obj2;

            Array.Sort(a1);
            Array.Sort(a2);

            return new Pudelko(a1[0] + a2[0], a1[1] + a2[1], a1[2] + a2[2]);
        }

        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return A;
                    case 1:
                        return B;
                    case 2:
                        return C;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public IEnumerator<double> GetEnumerator()
        {
            return new PudelkoEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int CompareTo(Pudelko obj)
        {
            var p1Volume = this.Objetosc;
            var p2Volume = obj.Objetosc;
            if (p1Volume < p2Volume)
                return 1;
            if (p1Volume == p2Volume)
            {
                var p1Area = this.Pole;
                var p2Area = obj.Pole;
                if (p1Area < p2Area)
                    return 1;
                if (p1Area == p2Area)
                {
                    var p1SidesSum = this.A + this.B + this.C;
                    var p2SidesSum = obj.A + obj.B + obj.C;
                    if (p1SidesSum == p2SidesSum)
                        return 0;
                    if (p1SidesSum < p2SidesSum)
                        return 1;
                }
            }
            return -1;
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
            if (A > 10 || B > 10 || C > 10)
            {
                throw new ArgumentOutOfRangeException("Pudelko dimensions must be smaller or equal 10 m");
            }
        }
    }
}
