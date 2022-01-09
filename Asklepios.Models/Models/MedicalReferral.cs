using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Core.Models
{
    public class MedicalReferral
    {
        public long Id { get; set; }

        public MedicalService MedicalService { get; set; }
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
            }
        }
        //public VisitSummary VisitSummary { get; set; }

    }
}
