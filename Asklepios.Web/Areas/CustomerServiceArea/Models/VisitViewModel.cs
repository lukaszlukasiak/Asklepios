using Asklepios.Core.Models;
using Asklepios.Web.Models;

namespace Asklepios.Web.Areas.CustomerServiceArea.Models
{
    public class VisitViewModel: SearchViewModel, IBaseViewModel
    {
        public Visit Visit { get; set; }
        public Patient SelectedPatient { get; set; }

        public VisitViewModel(Patient patient, Visit visit)
        {
            SelectedPatient = patient;
            Visit = visit;
        }
    }
}
