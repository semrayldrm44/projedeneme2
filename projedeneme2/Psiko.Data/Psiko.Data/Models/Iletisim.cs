using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Psiko.Data.Models
{
    public class Iletisim
    {
        public int Id { get; set; }
        public string iletisimIcerik { get; set; }

        public DateTime iletisimTarih { get; set; }
        public int kullaniciID { get; set; }

    }
}
