using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektnaNaloga
{
    internal interface IStoritev
    {
        string OpisStoritve {  get; }
        double Cena {  get; }

        double IzracunajCeno();
    }
}
