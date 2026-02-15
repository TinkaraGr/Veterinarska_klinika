using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektnaNaloga
{
    internal class Zaposleni : Oseba
    {
        public string DelovnoMesto { get; set; }
        public double Placa { get; set; }
        public DateTime DatumZaposlitve { get; set; }

        public override string PridobiOpis()
        {
            return $"Zaposleni: {Ime} {Priimek} - {DelovnoMesto}";
        }
    }
}
