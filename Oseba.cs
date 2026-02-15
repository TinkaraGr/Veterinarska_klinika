using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektnaNaloga
{
    internal abstract class Oseba
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Priimek { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public DateTime DatumRojstva { get; set; }

        public abstract string PridobiOpis();

        public virtual void IzpisPodatkov()
        {
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Ime: {Ime} {Priimek}");
        }
    }
}
