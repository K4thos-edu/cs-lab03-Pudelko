using System;

namespace PudelkoLibrary
{
    public enum UnitOfMeasure
    {
        milimeter,
        centimeter,
        meter
    }

    public class UnitOfMeasureMethods
    {
        /// <summary>
        /// Converts value between units of measure
        /// </summary>
        /// <param name="value">Value to be converted</param>
        /// <param name="fromUnit">Input unit of measure</param>
        /// <param name="toUnit">Output unit of measure</param>
        /// <param name="precision">Rounding precision</param>
        /// <returns>Converted Value</returns>
        public static double Convert(double val, UnitOfMeasure fromUnit, UnitOfMeasure toUnit, double precision = 3)
        {
            return Math.Floor(val * UnitMult(toUnit) / UnitMult(fromUnit) * Math.Pow(10, precision)) / Math.Pow(10, precision);
        }

        /// <summary>
        /// Multiplier for units of measure conversions
        /// </summary>
        /// <param name="unit">Unit of measure</param>
        /// <returns>Unit of measure multiplier</returns>
        public static double UnitMult(UnitOfMeasure unit)
        {
            double mult = 1;
            switch (unit)
            {
                case UnitOfMeasure.milimeter:
                    mult = 1000;
                    break;
                case UnitOfMeasure.centimeter:
                    mult = 100;
                    break;
            }
            return mult;
        }
    }
}
