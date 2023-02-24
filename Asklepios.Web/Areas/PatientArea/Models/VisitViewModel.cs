using Asklepios.Core.Models;
using Asklepios.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.PatientArea.Models
{
    public class VisitViewModel:SearchViewModel, IBaseViewModel
    {
        public Visit Visit { get; set; }
        public string UserName { get; set; }
        public List<Notification> Notifications { get; set; }
        
        public VisitViewModel()
        {
            HasPredefinedMedicalWorker = true;
            HasPredefinedService = true;
            HasPredefinedCategory = true;
        }
    }
}
