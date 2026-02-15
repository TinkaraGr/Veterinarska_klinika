using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektnaNaloga
{
    internal interface IObserver
    {
        void PrejmiObvestilo(string sporocilo);
        bool ZaLastnika(int lastnikId);
    }
}
