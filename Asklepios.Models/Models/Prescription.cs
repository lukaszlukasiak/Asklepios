using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Asklepios.Core.Models
{
    public class Prescription
    {
        public long Id { get; set; }

        public DateTimeOffset IssueDate { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }
        public Doctor IssuedBy { get; set; }
        public Patient IssuedTo { get; set; }
        public long IssuedToId { get; set; }
        
        public List<IssuedMedicine> IssuedMedicines {get;set;}
        [Required]
        public string AccessCode { get; set; }
        public string IdentificationCode { get; set; }
        public Visit _visit;
        public Visit Visit
        {
            get
            {
                return _visit;
            }
            set
            {
                _visit = value;
                IssuedBy = value.MedicalWorker as Doctor;
                IssuedTo = value.Patient;
                IssueDate = value.DateTimeSince;
            }
        }
        //public VisitSummary VisitSummary { get; set; }
    }
}