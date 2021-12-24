﻿using System.Collections.Generic;

namespace Asklepios.Core.Models
{
    public class VisitSummary
    {
        public long Id { get; set; }
        public string MedicalHistory { get; set; }
        public  MedicalTestResult MedicalResult { get; set; }
        public List<Recommendation> Recommendations { get; set; }
        public Prescription Prescription { get; set; }
        public List<ExaminationReferral> ExaminationReferrals { get; set; }
        public VisitReview VisitReview { get; set; }
        public Visit Visit { get; set; }

    }
}