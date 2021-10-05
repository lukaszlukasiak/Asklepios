using Asklepios.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.HomeArea.Models
{
    public class LogInViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
        public WorkerModuleType WorkerModuleType { get; set; }
    }
}
