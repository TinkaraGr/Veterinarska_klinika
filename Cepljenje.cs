using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektnaNaloga
{
    internal class Cepljenje : Intervencija
    {
        public Cepivo Cepivo { get; set; }
        public DateTime DatumPonovitve { get; set; }
        public bool JePrvoCepljenje { get; set; }

        public override double IzracunajCeno()
        {
            if (Cepivo != null)
                return OsnovnaCena + Cepivo.Cena;
            else
                return OsnovnaCena;
        }

        public override string OpisStoritve => $"Cepljenje: {Cepivo?.Naziv}";

        public override async Task Izvedi()
        {
            Status = StatusIntervencije.VTeku;
            Console.WriteLine($"Cepim {Pacient.Ime} s cepivom {Cepivo.Naziv}...");
            await Task.Delay(500);
            Status = StatusIntervencije.Zakljucena;
            Console.WriteLine($"Cepljenje uspešno. Naslednje cepljenje: {DatumPonovitve:dd.MM.yyyy}");
        }
    }
}
