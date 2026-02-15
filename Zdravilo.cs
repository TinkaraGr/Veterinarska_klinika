using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektnaNaloga
{
    internal class Zdravilo
    {
        public int Id { get; set; }
        public string Naziv {  get; set; }
        public string Proizvajalec { get; set; }
        public double Cena { get; set; }
        public DateTime DatumVeljavnosti { get; set; }
        public int KolicinaNaZalogi { get; set; }
        public string Enota { get; set; }

        public bool JeVeljavno()
        {
            return DatumVeljavnosti > DateTime.Now;
        }
    }
}
