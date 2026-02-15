using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektnaNaloga
{
    internal interface IZdravljenje
    {
        void DodajZdravilo(Zdravilo zdravilo);
        List<Zdravilo> PridobiPredpisanaZdravila();
        bool JePotrebnaHospitalizacija();
    }
}
