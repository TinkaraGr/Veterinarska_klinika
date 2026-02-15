using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektnaNaloga
{
    public enum Spol
    {
        Moski,
        Zenski,
        Neznano
    }

    internal class Zival
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Vrsta { get; set; }
        public string Pasma {  get; set; }
        public int Starost { get; set; }
        public double Teza { get; set; }
        public DateTime DatumRojstva { get; set; }
        public Spol Spol { get; set; }
        public Lastnik Lastnik { get; set; }
        public List<Pregled> ZgodovinaPregledov { get; set; } = new List<Pregled>();
        public List<Cepivo> Cepiva { get; set; } = new List<Cepivo>();
               
        public virtual string PridobiOpis()
        {
            return $"{Ime} ({Vrsta} - {Pasma}, {Starost} let, {Teza} kg)";
        }
    }
}
