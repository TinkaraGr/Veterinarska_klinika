namespace ProjektnaNaloga
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var klinika = Klinika.Instance;

            bool deluje = true;

            while (deluje)
            {
                IzpisiMeni();
                Console.Write("\nIzberi možnost: ");
                string izbira = Console.ReadLine();

                switch (izbira)
                {
                    case "1":
                        DodajLastnikaInZival(klinika);
                        break;

                    case "2":
                        ZaposliZaposlenega(klinika);
                        break;

                    case "3":
                        UstvariIntervencijo(klinika);
                        break;

                    case "4":
                        await IzvediIntervencijo(klinika);
                        break;

                    case "5":
                        PrikaziPlaniraneIntervencije(klinika);
                        break;

                    case "6":
                        IzpisZaposlenih(klinika);
                        break;

                    case "7":
                        klinika.OKliniki();
                        break;

                    case "8":
                        NapolniTestnePodatke(klinika);
                        break;

                    case "0":
                        deluje = false;
                        break;

                    default:
                        Console.WriteLine("Neveljavna izbira.");
                        break;
                }

                Console.WriteLine("\nPritisni tipko za nadaljevanje...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static void IzpisiMeni()
        {
            Console.WriteLine("═════════════════════════════");
            Console.WriteLine("    VETERINARSKA KLINIKA     ");
            Console.WriteLine("═════════════════════════════");
            Console.WriteLine("1 - Dodaj lastnika in žival");
            Console.WriteLine("2 - Zaposli zaposlenega");
            Console.WriteLine("3 - Ustvari intervencijo");
            Console.WriteLine("4 - Izvedi intervencijo");
            Console.WriteLine("5 - Prikaži planirane intervencije");
            Console.WriteLine("6 - Izpis zaposlenih");
            Console.WriteLine("7 - Pregled klinike");
            Console.WriteLine("8 - Vnesi testne podatke");
            Console.WriteLine("0 - Izhod");
        }

        static void DodajLastnikaInZival(Klinika klinika)
        {
            Console.WriteLine("\n=== DODAJANJE LASTNIKA ===");

            Console.Write("ID lastnika: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Ime lastnika: ");
            string ime = Console.ReadLine();

            Console.Write("Priimek: ");
            string priimek = Console.ReadLine();

            string email;
            while (true)
            {
                Console.Write("Email: ");
                email = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(email) && email.Contains("@"))
                    break;

                Console.WriteLine("Neveljaven email. Poskusi znova.");
            }

            Console.Write("Telefon: ");
            string telefon = Console.ReadLine();

            var lastnik = new Lastnik
            {
                Id = id,
                Ime = ime,
                Priimek = priimek,
                Email = email,
                Telefon = telefon
            };

            Console.WriteLine("\n=== DODAJANJE ŽIVALI ===");

            Console.Write("ID živali: ");
            int idZivali = int.Parse(Console.ReadLine());

            Console.Write("Ime živali: ");
            string imeZivali = Console.ReadLine();

            Console.Write("Vrsta: ");
            string vrsta = Console.ReadLine();

            Console.Write("Starost: ");
            int starost = int.Parse(Console.ReadLine());

            var zival = new Zival
            {
                Id = idZivali,
                Ime = imeZivali,
                Vrsta = vrsta,
                Starost = starost
            };

            klinika.SprejmiZival(zival, lastnik);
        }

        static void ZaposliZaposlenega(Klinika klinika)
        {
            Console.WriteLine("\n=== ZAPOSLOVANJE ===");
            Console.WriteLine("1 - Veterinar");
            Console.WriteLine("2 - Asistent");

            Console.Write("Izberi tip zaposlenega: ");
            string tip = Console.ReadLine();

            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Ime: ");
            string ime = Console.ReadLine();

            Console.Write("Priimek: ");
            string priimek = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Telefon: ");
            string telefon = Console.ReadLine();

            Console.Write("Plača: ");
            double placa = double.Parse(Console.ReadLine());

            if (tip == "1")
            {
                Console.Write("Specializacija: ");
                string specializacija = Console.ReadLine();

                var veterinar = new Veterinar
                {
                    Id = id,
                    Ime = ime,
                    Priimek = priimek,
                    Email = email,
                    Telefon = telefon,
                    Placa = placa,
                    DatumZaposlitve = DateTime.Now,
                    DelovnoMesto = "Veterinar",
                    Specializacija = specializacija
                };

                klinika.Zaposli(veterinar);
            }
            else if (tip == "2")
            {
                var asistent = new Asistent
                {
                    Id = id,
                    Ime = ime,
                    Priimek = priimek,
                    Email = email,
                    Telefon = telefon,
                    Placa = placa,
                    DatumZaposlitve = DateTime.Now,
                    DelovnoMesto = "Asistent"
                };

                klinika.Zaposli(asistent);
            }
            else
            {
                Console.WriteLine("Neveljavna izbira.");
            }
        }

        static void UstvariIntervencijo(Klinika klinika)
        {
            Console.WriteLine("\n=== USTVARJANJE INTERVENCIJE ===");

            Console.WriteLine("1 - Pregled");
            Console.WriteLine("2 - Operacija");
            Console.WriteLine("3 - Cepljenje");

            string tip = Console.ReadLine();

            IntervencijaTip tipEnum;

            switch (tip)
            {
                case "1":
                    tipEnum = IntervencijaTip.Pregled;
                    break;
                case "2":
                    tipEnum = IntervencijaTip.Operacija;
                    break;
                case "3":
                    tipEnum = IntervencijaTip.Cepljenje;
                    break;
                default:
                    tipEnum = IntervencijaTip.Pregled;
                    break;
            }

            var intervencija = IntervencijaFactory.UstvariIntervencijo(tipEnum);

            Console.Write("ID živali: ");
            int idZivali = int.Parse(Console.ReadLine());

            var zival = klinika.NajdiZival(idZivali);

            if (zival == null)
            {
                Console.WriteLine("Žival ne obstaja.");
                return;
            }

            Console.Write("ID veterinarja: ");
            int idVeterinarja = int.Parse(Console.ReadLine());

            var veterinar = klinika.NajdiVeterinarja(idVeterinarja);

            if (veterinar == null)
            {
                Console.WriteLine("Veterinar ne obstaja.");
                return;
            }

            intervencija.Pacient = zival;
            intervencija.Izvajalec = veterinar;
            intervencija.DatumInUra = DateTime.Now;
            intervencija.Status = Intervencija.StatusIntervencije.Planirana;

            klinika.DodajIntervencijo(intervencija);
            veterinar.DodajVCakalnico(intervencija);
            klinika.ObvestiOTerminuZaIzvedbo(intervencija);

            Console.WriteLine("Intervencija ustvarjena in dodana v čakalnico.");
        }

        static async Task IzvediIntervencijo(Klinika klinika)
        {
            Console.Write("ID veterinarja: ");
            int idVeterinarja = int.Parse(Console.ReadLine());

            var veterinar = klinika.NajdiVeterinarja(idVeterinarja);

            if (veterinar == null)
            {
                Console.WriteLine("Veterinar ne obstaja.");
                return;
            }

            var intervencija = veterinar.NaslednjaIntervencija();

            if (intervencija == null)
            {
                Console.WriteLine("Ni intervencij v čakalnici.");
                return;
            }

            Console.WriteLine($"Izvajam intervencijo za {intervencija.Pacient.Ime}");

            await intervencija.Izvedi();

            klinika.ObvestiOZakljucku(intervencija);

            UstvariRacunZaIntervencijo(klinika, intervencija);
        }

        static void UstvariRacunZaIntervencijo(Klinika klinika, Intervencija intervencija)
        {
            var racun = new Racun
            {
                Id = new Random().Next(1000, 9999),
                DatumIzdaje = DateTime.Now,
                Lastnik = intervencija.Pacient.Lastnik,
                Zival = intervencija.Pacient,
                Status = StatusRacuna.Neplacan
            };

            racun.Storitve.Add(intervencija);

            if (intervencija is IZdravljenje zdravljenje)
            {
                var zdravila = zdravljenje.PridobiPredpisanaZdravila();
                racun.Zdravila.AddRange(zdravila);
            }

            klinika.DodajRacun(racun);

            Console.WriteLine($"\n=== RAČUN {racun.Id} ===");
            Console.WriteLine($"Lastnik: {racun.Lastnik.Ime} {racun.Lastnik.Priimek}");
            Console.WriteLine($"Žival: {racun.Zival.Ime}");
            Console.WriteLine($"Storitev: {intervencija.OpisStoritve}");
            Console.WriteLine($"Skupaj brez DDV: {racun.IzracunajSkupnoCeno()} EUR");
            double ddv = Math.Round(racun.IzracunajCenoZDDV(), 2, MidpointRounding.AwayFromZero);
            Console.WriteLine($"Skupaj z DDV: {ddv} EUR");
        }

        static void PrikaziPlaniraneIntervencije(Klinika klinika)
        {
            var veterinarji = klinika.VsiVeterinarji();

            bool imaIntervencije = false;

            Console.WriteLine("\n=== PLANIRANE INTERVENCIJE ===");

            foreach (var v in veterinarji)
            {
                if (v.Cakalnica.Count == 0) continue;

                Console.WriteLine($"\nVeterinar: {v.Ime} {v.Priimek} - Specializacija: {v.Specializacija}");
                Console.WriteLine($"Število intervencij v čakalnici: {v.Cakalnica.Count}");

                foreach (var intervencija in v.Cakalnica)
                {
                    Console.WriteLine($"- Žival: {intervencija.Pacient.Ime}");
                    Console.WriteLine($"  Tip: {intervencija.OpisStoritve}");
                    Console.WriteLine($"  Datum in ura: {intervencija.DatumInUra:dd.MM.yyyy HH:mm}");
                    Console.WriteLine($"  Status: {intervencija.Status}");
                }

                imaIntervencije = true;
            }

            if (!imaIntervencije)
            {
                Console.WriteLine("Trenutno ni planiranih intervencij.");
            }
        }

        static void IzpisZaposlenih(Klinika klinika)
        {
            var zaposleni = klinika.VsiZaposleni();

            if (zaposleni.Count == 0)
            {
                Console.WriteLine("Ni zaposlenih.");
                return;
            }

            Console.WriteLine("\n=== ZAPOSLENI ===");

            foreach (var z in zaposleni)
            {
                Console.WriteLine($"ID: {z.Id}");
                Console.WriteLine($"Ime: {z.Ime} {z.Priimek}");
                Console.WriteLine($"Delovno mesto: {z.DelovnoMesto}");
                Console.WriteLine($"Plača: {z.Placa} EUR");

                if (z is Veterinar v)
                {
                    Console.WriteLine($"Specializacija: {v.Specializacija}");
                    Console.WriteLine($"Število intervencij v čakalnici: {v.Cakalnica.Count}");
                }

                if (z is Asistent a)
                {
                    Console.WriteLine($"Nadrejen veterinar: {a.NadrejeniVeterinar?.Ime ?? "Ni določen"}");
                }

                Console.WriteLine("----------------------------");
            }
        }

        static void NapolniTestnePodatke(Klinika klinika)
        {
            Console.WriteLine("\n=== POLNJENJE TESTNIH PODATKOV ===");

            var lastnik1 = new Lastnik
            {
                Id = 1,
                Ime = "Janez",
                Priimek = "Novak",
                Email = "janez@gmail.com"
            };

            var lastnik2 = new Lastnik
            {
                Id = 2,
                Ime = "Marija",
                Priimek = "Kovač",
                Email = "marija@gmail.com"
            };

            var zival1 = new Zival
            {
                Id = 1,
                Ime = "Rex",
                Vrsta = "Pes",
                Starost = 3
            };

            var zival2 = new Zival
            {
                Id = 2,
                Ime = "Micka",
                Vrsta = "Mačka",
                Starost = 2
            };

            var zival3 = new Zival
            {
                Id = 3,
                Ime = "Polly",
                Vrsta = "Papiga",
                Starost = 1
            };

            klinika.SprejmiZival(zival1, lastnik1);
            klinika.SprejmiZival(zival2, lastnik2);
            klinika.SprejmiZival(zival3, lastnik2);

            var veterinar1 = new Veterinar
            {
                Id = 1,
                Ime = "Ana",
                Priimek = "Kovač",
                DelovnoMesto = "Veterinar",
                Specializacija = "Male živali"
            };

            var veterinar2 = new Veterinar
            {
                Id = 2,
                Ime = "Marko",
                Priimek = "Zupančič",
                DelovnoMesto = "Veterinar",
                Specializacija = "Kirurgija"
            };

            var asistent = new Asistent
            {
                Id = 3,
                Ime = "Peter",
                Priimek = "Hribar",
                DelovnoMesto = "Asistent"
            };

            klinika.Zaposli(veterinar1);
            klinika.Zaposli(veterinar2);
            klinika.Zaposli(asistent);

            var pregled = IntervencijaFactory.UstvariIntervencijo(IntervencijaTip.Pregled);
            pregled.Pacient = zival3;
            pregled.Izvajalec = veterinar1;
            pregled.DatumInUra = DateTime.Now.AddHours(2);
            klinika.DodajIntervencijo(pregled);
            veterinar1.DodajVCakalnico(pregled);

            var operacija = IntervencijaFactory.UstvariIntervencijo(IntervencijaTip.Operacija) as Operacija;
            operacija.Pacient = zival1;
            operacija.Izvajalec = veterinar2;
            operacija.DatumInUra = DateTime.Now.AddDays(1);
            operacija.Trajanje = TimeSpan.FromHours(2);
            operacija.JeNujna = true;
            klinika.DodajIntervencijo(operacija);
            veterinar2.DodajVCakalnico(operacija);

            var cepljenje = IntervencijaFactory.UstvariIntervencijo(IntervencijaTip.Cepljenje) as Cepljenje;
            cepljenje.Pacient = zival2;
            cepljenje.Izvajalec = veterinar1;
            cepljenje.DatumInUra = DateTime.Now.AddHours(1);
            cepljenje.Cepivo = new Cepivo
            {
                Naziv = "Cepivo",
                Cena = 19.90
            };
            klinika.DodajIntervencijo(cepljenje);
            veterinar1.DodajVCakalnico(cepljenje);

            var nujni = IntervencijaFactory.UstvariIntervencijo(IntervencijaTip.NujniPregled) as Pregled;
            nujni.Pacient = zival2;
            nujni.Izvajalec = veterinar2;
            nujni.DatumInUra = DateTime.Now;
            nujni.Diagnoza = "Akutno stanje";
            klinika.DodajIntervencijo(nujni);
            veterinar2.DodajVCakalnico(nujni);
        }
    }
}
