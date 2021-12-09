using System.Collections.Generic;

namespace Asklepios.Core.Models
{
    public class VisitSummary
    {
        public long Id { get; set; }
        public string MedicalHistory { get; set; }
        public List< MedicalTestResult> MedicalResults { get; set; }
        public List<Recommendation> Recommendations { get; set; }
        public Prescription Prescription { get; set; }
    }
}