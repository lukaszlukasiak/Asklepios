using Asklepios.Core.Enums;

namespace Asklepios.Core.Models
{
    public class VisitSearchOptions
    {
        public MedicalWorker SelectedDoctor { get; set; }
        public Location SelectedLocation { get; set; }
        public VoivodeshipType? SelectedVoivodeship { get; set; }
        public VisitCategory SelectedVisitCategory { get; set; }
        public MedicalService SelectedPrimaryService { get; set; }
        
    }
}
