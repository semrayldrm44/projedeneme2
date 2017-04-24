using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Psiko.Data.Models
{
    public class Makale
    {
        public int Id { get; set; }
        public int kullaniciID { get; set; }
        public string makaleBaslik { get; set; }
        public string makaleOzet { get; set; }
        public string makaleIcerik { get; set; }

        public string makaleResim { get; set; }

        public DateTime makaleTarih { get; set; }
        public int makaleOkunma { get; set; }
        public int makaleYorumSayisi { get; set; }
        public int kategoriID{ get; set; }
       
    }
}
