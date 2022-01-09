using System;

namespace Asklepios.Core.Models
{
    public class VisitReview
    {
        public long Id { get; set; }
        public float GeneralRate { get; set; }
        public float CompetenceRate { get; set; }
        public float AtmosphereRate { get; set; }
        public string ShortDescription { get; set; }
        public DateTimeOffset ReviewDate { get; set; }
        public MedicalWorker Reviewee { get; set; }
        public Patient Reviewer { get; set; }
        public Visit Visit { get; set; }
    }
}