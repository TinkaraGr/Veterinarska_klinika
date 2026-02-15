using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektnaNaloga
{
    internal class Cepivo : Zdravilo
    {
        public string ZaVrstoZivali {  get; set; }
        public TimeSpan TrajanjeImunitete { get; set; }
        public bool JeObvezno { get; set; }
    }
}
