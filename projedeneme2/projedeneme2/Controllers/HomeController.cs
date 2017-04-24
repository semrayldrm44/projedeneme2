using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace projedeneme2.Controllers
{
    public class HomeController : Controller
    {
        

        public IActionResult Anasayfa()
        {
            return View();
        }

        public IActionResult cocukPsikoloji()
        {

            return View();
        }

        public IActionResult eriskinPsikoloji()
        {
          
            return View();
        }
        public IActionResult hakkimizda()
        {


            return View();
        }
        public IActionResult soruCevap()
        {


            return View();
        }
        public IActionResult onlineRandevu()
        {


            return View();
        }
        public IActionResult giris()
        {


            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
