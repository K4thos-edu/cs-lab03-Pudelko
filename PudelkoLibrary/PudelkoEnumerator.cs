using System;
using System.Collections.Generic;

namespace PudelkoLibrary
{
    class PudelkoEnumerator : IEnumerator<double>
    {
        private readonly Pudelko p;

        private int idx = 0;

        public PudelkoEnumerator(Pudelko obj)
        {
            p = obj;
        }

        public object Current => p[idx++];

        double IEnumerator<double>.Current => p[idx++];

        public bool MoveNext()
        {
            return idx <= 1;
        }

        public void Reset()
        {
            idx = 0;
        }

        public void Dispose()
        {

        }
    }
}
