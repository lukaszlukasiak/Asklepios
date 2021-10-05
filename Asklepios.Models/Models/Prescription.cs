using System;
using System.Collections.Generic;

namespace Asklepios.Core.Models
{
    public class Prescription
    {
        public long Id { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Doctor Issuer { get; set; }
        public List<IssuedMedicine> IssuedMedicines {get;set;}
        public string AccessCode { get; set; }
    }
}