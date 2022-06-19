using Asklepios.Core.Models;
using Asklepios.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.PatientArea.Models
{
    public class MedicalWorkerViewModel:SearchViewModel, IBaseViewModel
    {
        public MedicalWorker MedicalWorker { get; set; }
        public string UserName { get; set; }
        public List<Notification> Notifications { get; set; }
    }
}
