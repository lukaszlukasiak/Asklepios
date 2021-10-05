using Asklepios.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asklepios.Core.Models
{
    public class Visit
    {
        public long Id { get; set; }
        public VisitCategory VisitCategory { get; set; }
        public Patient Patient { get; set; }
        public MedicalWorker MedicalWorker { get; set; }
        public DateTime DateTimeSince { get; set; }
        public DateTime DateTimeTill { get; set; }
        public List<MedicalService> BookedMedicalServices { get; set; }
        public Location Location { get; set; }
        public MedicalRoom MedicalRoom { get; set; }
        public VisitRating VisitRate { get; set; }
        public VisitSummary VisitSummary { get;set;}
    }
}
