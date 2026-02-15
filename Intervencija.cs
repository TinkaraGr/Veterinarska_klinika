using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektnaNaloga
{
    internal abstract class Intervencija : IStoritev
    {
        public int Id { get; set; }
        public DateTime DatumInUra { get; set; }
        public string Opis { get; set; }
        public double OsnovnaCena { get; set; }
        public Zival Pacient {  get; set; }
        public Veterinar Izvajalec { get; set; }
        public StatusIntervencije Status {  get; set; }
        public double Cena
        {
            get { return IzracunajCeno(); }
        }

        public abstract double IzracunajCeno();
        public abstract string OpisStoritve { get; }
        public abstract Task Izvedi();
              
        public enum StatusIntervencije
        {
            Planirana,
            VTeku,
            Zakljucena,
            Preklicana
        }
    }
}
