using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Core.Models
{
    public class MedicalReferral
    {
        public long Id { get; set; }

        public MedicalService PrimaryMedicalService { get; set; }
        public MedicalService MinorMedicalService { get; set; }
        public DateTimeOffset IssueDate { get; set; }
        public DateTimeOffset ExpireDate { get; set; }
        public bool IsActive 
        {
            get
            {
                if (HasBeenUsed==false && DateTimeOffset.Now<ExpireDate)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool HasExpired
        {
            get
            {
                if (DateTimeOffset.Now>ExpireDate)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool HasBeenUsed { get; set; }
        public MedicalWorker IssuedBy { get; set; }
        public Patient IssuedTo { get; set; }
        [Display(Name = "Komentarz")]
        [DataType(DataType.Text)]
        public string Comment { get; set; }
        public Visit _visitUsed;
        public Visit VisitWhenUsed
        {
            get
            {
                return _visitUsed;
            }
            set
            {
                _visit = value;
            }
        }

        public Visit _visit;
        public Visit VisitWhenIssued 
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
        //public VisitSummary VisitSummary { get; set; }

    }
}
