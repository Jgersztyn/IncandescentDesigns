using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IncandescentDesigns.Controllers
{
    public class SimulatorController : Controller
    {
        // GET: Simulator
        public ActionResult Index()
        {
            return View();
        }

        // GET: Simulator 2D
        public ActionResult SimulatorTwo()
        {
            return View();
        }

        public string intToHex(int number)
        {
            string hexValue = number.ToString("X");
            return hexValue;
        }


    }
}