using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektnaNaloga
{
    internal class Veterinar : Zaposleni
    {
        public string Specializacija {  get; set; }
        public Queue<Intervencija> Cakalnica { get; set; } = new Queue<Intervencija>();

        public void DodajVCakalnico(Intervencija intervencija)
        {
            Cakalnica.Enqueue(intervencija);
        }

        public Intervencija NaslednjaIntervencija()
        {
            return Cakalnica.Count > 0 ? Cakalnica.Dequeue() : null;
        }
    }
}
