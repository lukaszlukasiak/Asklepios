using Asklepios.Core.Models;

namespace Asklepios.Web.Areas.CustomerServiceArea.Models
{
    public class VisitViewModel:BaseViewModel
    {
        public Visit Visit { get; set; }
        public VisitViewModel(Patient patient, Visit visit)
        {
            SelectedPatient = patient;
            Visit = visit;
        }
    }
}
