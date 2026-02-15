using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektnaNaloga
{
    internal class Asistent : Zaposleni
    {
        public Veterinar NadrejeniVeterinar {  get; set; }
        public List<string> Odgovornosti { get; set; } = new List<string>();

        public void GenerirajRacun(Racun racun)
        {
            Console.WriteLine($"Generiran račun #{racun.Id} za {racun.Lastnik.Ime} {racun.Lastnik.Priimek}");
        }
    }
}
