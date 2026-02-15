using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektnaNaloga
{
    internal class Operacija : Intervencija, IZdravljenje
    {
        public TimeSpan Trajanje { get; set; }
        public bool JeNujna { get; set; }
        public List<Zdravilo> Zdravila { get; set; } = new List<Zdravilo>();

        public override double IzracunajCeno()
        {
            double cena = OsnovnaCena;
            if (JeNujna) cena *= 1.3;
            cena += (double)Trajanje.TotalHours * 50;
            return cena;
        }

        public override string OpisStoritve => $"Operacija: {Opis}";

        public override async Task Izvedi()
        {
            Status = StatusIntervencije.VTeku;
            Console.WriteLine($"Začenjam operacijo za {Pacient.Ime}...");
            await Task.Delay(500);
            Status = StatusIntervencije.Zakljucena;
            Console.WriteLine($"Operacija zaključena. Trajala je {Trajanje}");
        }

        public void DodajZdravilo(Zdravilo zdravilo)
        {
            Zdravila.Add(zdravilo);
        }

        public List<Zdravilo> PridobiPredpisanaZdravila()
        {
            return Zdravila;
        }

        public bool JePotrebnaHospitalizacija()
        {
            return true;
        }
    }
}
