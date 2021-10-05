using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.PatientArea.Controllers
{
    [Area("PatientArea")]

    public class PatientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
