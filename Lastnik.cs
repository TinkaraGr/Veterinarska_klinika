using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektnaNaloga
{
    internal class Lastnik : Oseba
    {
        public string Naslov {  get; set; }
        public List<Zival> Ljubljencki { get; set; } = new List<Zival>();

        public override string PridobiOpis()
        {
            return $"Lastnik: {Ime} {Priimek}";
        }

        public void DodajZival(Zival zival)
        {
            Ljubljencki.Add(zival);
            zival.Lastnik = this;
        }
    }
}
