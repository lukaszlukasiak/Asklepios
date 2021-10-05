using Asklepios.Core.Enums;
using System.Collections.Generic;

namespace Asklepios.Core.Models
{
    public class NotificationFilter
    {
        public long Id { get; set; }
        public VisitCategory SelectedVisitCategory { get; set; }
        public List<Location> SelectedLocations { get; set; }
        public MedicalWorker SelectedMedicalWorker { get; set; }
        public MedicalService SelectedMedicalService { get; set; }
    }
}