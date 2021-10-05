using System.Collections.Generic;

namespace Asklepios.Core.Models
{
    public class VisitSummary
    {
        public long Id { get; set; }
        public string Summary { get; set; }
        public List<KeyValuePair< MedicalService, MedicalServiceOutcome>> MedicalResults { get; set; }
        public string Recommendations { get; set; }
        public Prescription Prescription { get; set; }
    }
}