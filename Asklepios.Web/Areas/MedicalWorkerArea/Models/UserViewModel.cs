using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.MedicalWorkerArea.Models
{
    public class UserViewModel : IBaseViewModel
    {
        public string UserName { get; set; }
        public User User { get; set; }
    }
}
