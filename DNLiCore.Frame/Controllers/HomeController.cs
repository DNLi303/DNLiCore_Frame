using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using DNLiCore.Service;

namespace DNLiCore_Frame.Controllers
{
    public class HomeController : Controller
    {
       
        

        public IActionResult Index()
        {
            // _test.tesss();

            return View();
        }

       

      
    }
}
