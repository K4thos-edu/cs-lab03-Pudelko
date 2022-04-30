using PudelkoLibrary;
using System;
using System.Collections.Generic;

namespace PudelkoApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Pudelko> list = new List<Pudelko>();
            list.Add(new Pudelko(1.24, 0.3, 1.1, UnitOfMeasure.meter));
            list.Add(new Pudelko(132.176, 11, 22.333, UnitOfMeasure.centimeter));
            list.Add(new Pudelko(9000, null, 700, UnitOfMeasure.milimeter));
            list.Add(new Pudelko());
            list.Sort();
            list.ForEach(p => Console.WriteLine(p.ToString()));
        }
    }
}
