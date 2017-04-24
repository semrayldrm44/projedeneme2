using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace projedeneme2.Controllers
{
    [Route("kullanici")]
    public class KullaniciController : Controller
    {
        private readonly PsikoConfig _options;

        public KullaniciController(IOptions<PsikoConfig> optionsAccessor)
        {
            _options = optionsAccessor.Value;
        }

        [HttpGet("liste")]
        public IActionResult Liste()
        {
            Psiko.Data.Repositories.Kullanici kullaniciRep = new Psiko.Data.Repositories.Kullanici(_options.ConnectionString);
            List<Psiko.Data.Models.Kullanici> kullanicilar = kullaniciRep.GetKullaniciListesi();

            return View(kullanicilar);
        }

        [HttpGet("add")]
        public IActionResult Add()
        {

            return View();
        }

        [HttpPost("kaydet")]
        public IActionResult Kaydet(string AdSoyadi, string Sifre, string tur)
        {
            Psiko.Data.Models.Kullanici kullanici= new Psiko.Data.Models.Kullanici();
            kullanici.AdSoyadi = AdSoyadi;
            kullanici.Sifre = Sifre;
            kullanici.Tur = tur;

            //if (kullanici == null) { return BadRequest("Kullanıcı bılgısı eksık"); }

            Psiko.Data.Repositories.Kullanici kullaniciRep = new Psiko.Data.Repositories.Kullanici(_options.ConnectionString);

            kullanici = kullaniciRep.AddKullanici(kullanici);
            return View("liste");
        }
       


    }
}

