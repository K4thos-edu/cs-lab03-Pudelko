using System;
using PudelkoLibrary;

namespace PudelkoApp
{
    public static class PudelkoExtensions
    {
        public static Pudelko Kompresuj(this Pudelko obj)
        {
            double side = Math.Cbrt(obj.Objetosc);
            return new Pudelko(side, side, side);
        }
    }
}
