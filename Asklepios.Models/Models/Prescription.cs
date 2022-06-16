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
        public MedicalWorker IssuedBy { get; set; }
        public Patient IssuedTo { get; set; }
        public long IssuedToId { get; set; }

        public List<IssuedMedicine> IssuedMedicines { get; set; } = new List<IssuedMedicine>();
        
        [Display(Name = "Kod dostępu")]
        [Required(ErrorMessage = "Proszę wprowadzić kod dostępu")]
        [DataType(DataType.Text)]
        [StringLength(4, ErrorMessage ="Kod dostępu powinien się składać z 4 cyfr")]
        [Range(0,9999, ErrorMessage = "Kod dostępu powinien się składać z 4 cyfr")]

        public string AccessCode { get; set; }
        [Display(Name = "Numer identyfikacyjny")]
        [Required(ErrorMessage = "Proszę wprowadzić numer identyfikacyjny")]
        [DataType(DataType.Text)]
        [StringLength(20, ErrorMessage ="Numer identyfikacyjny powinien się składać z 20 znaków")]

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
                IssuedBy = value.MedicalWorker;
                IssuedTo = value.Patient;
                IssueDate = value.DateTimeSince;
            }
        }
        public long VisitId { get; set; }
        //public VisitSummary VisitSummary { get; set; }
    }
}