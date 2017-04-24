using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Psiko.Data.Models
{
    public class Kategori
    {
        public int Id { get; set; }
        public int anakategoriID{ get; set; }

        public string kategoriAd { get; set; }

        public int kategoriSira { get; set; }

        public int kategoriAdet { get; set; }

        public string kategoriResim { get; set; }

    }
}
