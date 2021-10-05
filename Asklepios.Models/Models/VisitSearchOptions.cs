using Asklepios.Core.Enums;
using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asklepios.Core.Models
{
    public class VisitSearchOptions
    {
        public Doctor SelectedDoctor { get; set; }
        public Location SelectedLocation { get; set; }
        public VoivodeshipType SelectedVoivodeship { get; set; }
        public VisitCategory SelectedVisitCategory { get; set; }
        public MedicalService SelectedPrimaryService { get; set; }
        
    }
}
