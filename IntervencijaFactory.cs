using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektnaNaloga
{
    public enum IntervencijaTip
    {
        Pregled,
        Operacija,
        Cepljenje,
        NujniPregled
    }

    internal static class IntervencijaFactory
    {
        public static Intervencija UstvariIntervencijo(IntervencijaTip tip)
        {
            Intervencija intervencija = null;

            switch (tip)
            {
                case IntervencijaTip.Pregled:
                    intervencija = new Pregled
                    {
                        OsnovnaCena = Klinika.Instance.CenikStoritev["Pregled"],
                        Opis = "Redni zdravstveni pregled"
                    };
                    break;
                case IntervencijaTip.Operacija:
                    intervencija = new Operacija
                    {
                        OsnovnaCena = Klinika.Instance.CenikStoritev["Operacija"],
                        Opis = "Operacija",
                        Trajanje = TimeSpan.FromHours(1)
                    };
                    break;
                case IntervencijaTip.Cepljenje:
                    intervencija = new Cepljenje
                    {
                        OsnovnaCena = Klinika.Instance.CenikStoritev["Cepljenje"],
                        Opis = "Redno cepljenje",
                        DatumPonovitve = DateTime.Now.AddYears(1),
                        Cepivo = new Cepivo
                        {
                            Id = 1,
                            Naziv = "Cepivo1",
                            Proizvajalec = "MSD",
                            Cena = 18.50,
                            ZaVrstoZivali = "Pes, Mačka",
                            TrajanjeImunitete = TimeSpan.FromDays(365),
                            JeObvezno = true,
                            KolicinaNaZalogi = 50,
                            Enota = "odmerek"
                        }
                    };
                    break;
                case IntervencijaTip.NujniPregled:
                    intervencija = new Pregled
                    {
                        OsnovnaCena = Klinika.Instance.CenikStoritev["Nujni pregled"],
                        Opis = "NUJNI pregled"
                    };
                    if (intervencija is Pregled pregled)
                    {
                        pregled.PreventivniPregled = false;
                    }
                    break;
            }

            return intervencija;
        }
    }
}
