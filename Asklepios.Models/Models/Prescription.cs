using System;
using System.Collections.Generic;

namespace Asklepios.Core.Models
{
    public class Prescription
    {
        public long Id { get; set; }
        public DateTimeOffset IssueDate { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }
        public Doctor Issuer { get; set; }
        public List<IssuedMedicine> IssuedMedicines {get;set;}
        public string AccessCode { get; set; }
        public VisitSummary VisitSummary { get; set; }
    }
}