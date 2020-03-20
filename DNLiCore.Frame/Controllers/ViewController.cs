using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DNLiCore_Frame.Controllers
{
    public class ViewController : Controller
    {
        [AllowAnonymous]
        public IActionResult Login()
        {
            // string sd = "";
            //string ddsd= sd.Split(',')[1];
            return View();
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Install()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Error()
        {
            // _test.tesss();

            return View();
        }

    }
}