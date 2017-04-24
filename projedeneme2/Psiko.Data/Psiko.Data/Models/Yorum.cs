using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Psiko.Data.Models
{
    public class Yorum
    {
        public int Id { get; set; }
        public string yorumIcerik { get; set; }
        public DateTime yorumTarih { get; set; }
        public bool yorumOnay { get; set; }
        public string yorumResim { get; set; }
        public int makaleID { get; set; }
        public int kullaniciID { get; set; }



    }
}
