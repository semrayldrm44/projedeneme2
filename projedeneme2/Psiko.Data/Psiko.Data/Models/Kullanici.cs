using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Psiko.Data.Models
{
    public class Kullanici
    {
        public int Id{ get; set; }
        public string AdSoyadi { get; set; }

        public string Sifre { get; set; }

        public string Tur { get; set; }

    }
}
