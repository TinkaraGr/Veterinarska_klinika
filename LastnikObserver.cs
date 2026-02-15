using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektnaNaloga
{
    internal class LastnikObserver : IObserver
    {
        public Lastnik Lastnik { get; private set; }

        public LastnikObserver(Lastnik lastnik)
        {
            Lastnik = lastnik;
        }

        public void PrejmiObvestilo(string sporocilo)
        {
            Console.WriteLine($"Lastnik {Lastnik.Ime} {Lastnik.Priimek}: {sporocilo}");
        }

        public bool ZaLastnika(int lastnikId)
        {
            return Lastnik.Id == lastnikId;
        }
    }
}
