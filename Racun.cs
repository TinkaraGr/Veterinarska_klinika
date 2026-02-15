using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektnaNaloga
{
    public enum StatusRacuna
    {
        Neplacan,
        Placan,
        Storniran
    }

    internal class Racun
    {
        public int Id {  get; set; }
        public DateTime DatumIzdaje { get; set; }
        public Lastnik Lastnik { get; set; }
        public Zival Zival { get; set; }
        public List<IStoritev> Storitve { get; set; } = new List<IStoritev>();
        public List<Zdravilo> Zdravila { get; set; } = new List<Zdravilo>();
        public StatusRacuna Status {  get; set; }

        public double IzracunajSkupnoCeno()
        {
            double skupaj = 0;
            foreach(var storitev in Storitve)
            {
                skupaj += storitev.IzracunajCeno();
            }
            foreach(var zdravilo in Zdravila)
            {
                skupaj += zdravilo.Cena;
            }
            return skupaj;
        }

        public double IzracunajCenoZDDV()
        {
            var konfiguracija = Klinika.Instance;
            return konfiguracija.IzracunajCenoZDDV(IzracunajSkupnoCeno());
        }
    }
}
