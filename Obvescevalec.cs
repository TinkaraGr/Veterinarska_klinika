using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektnaNaloga
{
    internal class Obvescevalec
    {
        private List<IObserver> _narocniki = new List<IObserver>();

        public void Prijavi(IObserver observer)
        {
            if (!_narocniki.Contains(observer))
            {
                _narocniki.Add(observer);
                Console.WriteLine("Nov naročnik prijavljen na obvestila");
            }
        }

        public void Odjavi(IObserver observer)
        {
            if (_narocniki.Contains(observer))
            {
                _narocniki.Remove(observer);
                Console.WriteLine("Naročnik odjavljen od obvestil");
            }
        }

        public void ObvestiVse(string sporocilo)
        {
            if (_narocniki.Count == 0)
            {
                Console.WriteLine($"Ni naročnikov za obvestilo: {sporocilo}");
                return;
            }

            Console.WriteLine($"\nObveščam {_narocniki.Count} naročnikov: {sporocilo}");
            foreach (var narocnik in _narocniki)
            {
                narocnik.PrejmiObvestilo(sporocilo);
            }
        }

        public void ObvestiLastnika(int lastnikId, string sporocilo)
        {
            bool najden = false;

            foreach (var narocnik in _narocniki)
            {
                if (narocnik is LastnikObserver lo && lo.ZaLastnika(lastnikId))
                {
                    narocnik.PrejmiObvestilo(sporocilo);
                    najden = true;
                }
            }

            if (!najden)
            {
                Console.WriteLine($"Lastnik z ID {lastnikId} ni najden med naročniki");
            }
        }

        public void ObvestiLastnikeZivali(Zival zival, string sporocilo)
        {
            if (zival?.Lastnik == null)
            {
                Console.WriteLine("Žival nima lastnika!");
                return;
            }

            ObvestiLastnika(zival.Lastnik.Id, sporocilo);
        }
    }
}
