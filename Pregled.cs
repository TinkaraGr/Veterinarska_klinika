using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektnaNaloga
{
    internal class Pregled : Intervencija, IZdravljenje
    {
        public string Diagnoza { get; set; }
        public string Priporocila { get; set; }
        public List<Zdravilo> PredpisanaZdravila { get; set; } = new List<Zdravilo>();
        public bool PreventivniPregled { get; set; }

        public override double IzracunajCeno()
        {
            double cena = OsnovnaCena;
            if (PreventivniPregled) cena *= 0.8;
            return cena;
        }

        public override string OpisStoritve => $"Pregled: {Opis}";

        public override async Task Izvedi()
        {
            Status = StatusIntervencije.VTeku;
            Console.WriteLine($"Začenjam pregled za {Pacient.Ime}...");
            await Task.Delay(500);
            Status = StatusIntervencije.Zakljucena;
            Console.WriteLine($"Pregled zaključen. Diagnoza: {Diagnoza}...");
        }

        public void DodajZdravilo(Zdravilo zdravilo)
        {
            PredpisanaZdravila.Add( zdravilo );
        }

        public List<Zdravilo> PridobiPredpisanaZdravila()
        {
            return PredpisanaZdravila;
        }

        public bool JePotrebnaHospitalizacija()
        {
            return PredpisanaZdravila.Count > 3 || (Diagnoza?.ToLower().Contains("akutno") ?? false);
        }
    }
}
