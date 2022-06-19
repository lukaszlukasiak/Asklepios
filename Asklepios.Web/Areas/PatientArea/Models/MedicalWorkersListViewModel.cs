using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.PatientArea.Models
{
    public class MedicalWorkersListViewModel : IBaseViewModel
    {
        public List<MedicalWorker> MedicalWorkers { get; set; }
        public string UserName { get; set; }
        public List<Notification> Notifications { get; set; }
    }
}
