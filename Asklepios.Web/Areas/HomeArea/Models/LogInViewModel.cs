using Asklepios.Core.Enums;
using Asklepios.Core.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.HomeArea.Models
{
    public class LogInViewModel
    {
        public User User { get; set; }
        public bool LogInFailed { get; set; }
        public  string LogInFailedAlert = "Nazwa, hasło użytkownika bądź wybrany moduł jest błędny! Spróbuj jeszcze raz!";
        //public string UserName { get; set; }
        //public string Password { get; set; }
        //public UserType UserType { get; set; }
        //public WorkerModuleType WorkerModuleType { get; set; }
    }
}
