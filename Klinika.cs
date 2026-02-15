using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektnaNaloga
{
    internal sealed class Klinika
    {
        private static Klinika _instance = null;
        private static readonly object _lock = new object();

        private Generik<Zival> _zivali = new Generik<Zival>();
        private Generik<Lastnik> _lastniki = new Generik<Lastnik>();
        private Generik<Zaposleni> _zaposleni = new Generik<Zaposleni>();
        private List<Intervencija> _intervencije = new List<Intervencija>();
        private List<Racun> _racuni = new List<Racun>();


        private Obvescevalec _obvescevalec = new Obvescevalec();

        public string Ime { get; set; }
        public string Naslov {  get; set; }
        public string TelefonskaStevilka { get; set; }
        public string Email { get; set; }
        public TimeSpan DelovniCasOd { get; set; } = new TimeSpan(8,0,0);
        public TimeSpan DelovniCasDo {  get; set; } = new TimeSpan(16,0,0);
        public Dictionary<string, double> CenikStoritev { get; set; } = new Dictionary<string, double>();

        private Klinika()
        {
            NaloziPrivezeteNastavitve();
        }

        public static Klinika Instance
        {
            get
            {
                lock (_lock)
                {
                    if(_instance == null)
                    {
                        _instance = new Klinika();
                    }
                    return _instance;
                }
            }
        }



        public void PrijaviNaObvestila(IObserver observer)
        {
            _obvescevalec.Prijavi(observer);
        }

        public void OdjaviZObvestil(IObserver observer)
        {
            _obvescevalec.Odjavi(observer);
        }



        private void NaloziPrivezeteNastavitve()
        {
            Ime = "Veterinarska Klinika";
            Naslov = "Ulica 20, 1000 Ljubljana";
            TelefonskaStevilka = "01 234 56 78";
            Email = "info@veterina.si";

            CenikStoritev.Add("Pregled", 35.00);
            CenikStoritev.Add("Cepljenje", 25.00);
            CenikStoritev.Add("Operacija", 150.00);
            CenikStoritev.Add("Nujni pregled", 60.00);
        }

        public double IzracunajCenoZDDV(double osnovnaCena)
        {
            return osnovnaCena * 1.22;
        }

        public void SprejmiZival(Zival zival, Lastnik lastnik)
        {
            var obstojeciLastnik = _lastniki.Najdi(l => l.Id == lastnik.Id);

            if (obstojeciLastnik == null)
            {
                _lastniki.Dodaj(lastnik);
                Console.WriteLine($"\nNov lastnik dodan: {lastnik.Ime} {lastnik.Priimek}");
                PrijaviNaObvestila(new LastnikObserver(lastnik));
            }
            else
            {
                lastnik = obstojeciLastnik;
            }

            _zivali.Dodaj(zival);
            lastnik.DodajZival(zival);

            Console.WriteLine($"Žival ({zival.Ime} - {zival.Vrsta}) sprejeta v kliniko.");
        }

        public void ObvestiOTerminuZaIzvedbo(Intervencija intervencija)
        {
            if (intervencija?.Pacient == null) return;

            string sporocilo = $"Opomnik: {intervencija.OpisStoritve} za {intervencija.Pacient.Ime} " +
                               $"ob {intervencija.DatumInUra:dd.MM.yyyy HH:mm}";

            _obvescevalec.ObvestiLastnikeZivali(intervencija.Pacient, sporocilo);
        }

        public void ObvestiOZakljucku(Intervencija intervencija)
        {
            if (intervencija?.Pacient == null) return;

            string sporocilo = $"Intervencija ({intervencija.OpisStoritve}) za {intervencija.Pacient.Ime} je zaključena.";

            _obvescevalec.ObvestiLastnikeZivali(intervencija.Pacient, sporocilo);
        }


        public Zival NajdiZival(int id)
        {
            return _zivali.Najdi(z => z.Id == id);
        }

        public Zival NajdiZival(string ime)
        {
            return _zivali.Najdi(z => z.Ime.ToLower() == ime.ToLower());
        }

        public List<Zival> VseZivali()
        {
            return _zivali.VsiElementi();
        }

        public List<Zival> ZivaliPoVrsti(string vrsta)
        {
            return _zivali.VsiElementi().Where(z => z.Vrsta.ToLower() == vrsta.ToLower()).ToList();
        }



        public Lastnik NajdiLastnika(int id)
        {
            return _lastniki.Najdi(l => l.Id == id);
        }

        public List<Lastnik> VsiLastniki()
        {
            return _lastniki.VsiElementi();
        }

        public List<Zival> ZivaliLastnika(int lastnikId)
        {
            var lastnik = NajdiLastnika(lastnikId);
            return lastnik?.Ljubljencki ?? new List<Zival>();
        }



        public void Zaposli(Zaposleni zaposleni)
        {
            _zaposleni.Dodaj(zaposleni);
            Console.WriteLine($"Nov zaposleni: {zaposleni.Ime} {zaposleni.Priimek} - {zaposleni.DelovnoMesto}");
        }

        public List<Zaposleni> VsiZaposleni()
        {
            return _zaposleni.VsiElementi();
        }

        public List<Veterinar> VsiVeterinarji()
        {
            return _zaposleni.VsiElementi()
                .OfType<Veterinar>()
                .ToList();
        }

        public Veterinar NajdiVeterinarja(int id)
        {
            return _zaposleni
                .VsiElementi()
                .OfType<Veterinar>()
                .FirstOrDefault(v => v.Id == id);
        }




        public void DodajIntervencijo(Intervencija intervencija)
        {
            intervencija.Status = Intervencija.StatusIntervencije.Planirana;
            _intervencije.Add(intervencija);

            Console.WriteLine($"Intervencija shranjena (ID: {_intervencije.Count})");
        }

        public List<Intervencija> VseIntervencije()
        {
            return _intervencije;
        }

        public List<Intervencija> PlaniraneIntervencije()
        {
            return _intervencije
                .Where(i => i.Status == Intervencija.StatusIntervencije.Planirana)
                .ToList();
        }



        public void DodajRacun(Racun racun)
        {
            _racuni.Add(racun);
            Console.WriteLine($"Račun #{racun.Id} shranjen v sistem.");
        }

        public List<Racun> VsiRacuni()
        {
            return _racuni;
        }



        public void OKliniki()
        {
            Console.WriteLine($"Naziv: {Ime}");
            Console.WriteLine($"Naslov: {Naslov}");
            Console.WriteLine($"Število živali: {_zivali.SteviloElementov}");
            Console.WriteLine($"Število lastnikov: {_lastniki.SteviloElementov}");
            Console.WriteLine($"Število zaposlenih: {_zaposleni.SteviloElementov}");

            var veterinarji = VsiVeterinarji();
            Console.WriteLine($"- od tega veterinarjev: {veterinarji.Count}");

            Console.WriteLine($"Delovni čas: {DelovniCasOd:hh\\:mm} - {DelovniCasDo:hh\\:mm}");

            if (_zivali.SteviloElementov > 0)
            {
                Console.WriteLine("\nŽivali po vrstah:");
                var zivali = _zivali.VsiElementi();
                var poVrstah = zivali.GroupBy(z => z.Vrsta);

                foreach (var skupina in poVrstah)
                {
                    Console.WriteLine($"- {skupina.Key}: {skupina.Count()}");
                }
            }
        }
    }
}
