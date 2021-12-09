using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Core.Models
{
    public class ExaminationReferral
    {
        public long Id { get; set; }

        public MedicalService MedicalService { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool IsActive { get; }
        public bool HasBeenUsed { get; set; }
        public MedicalWorker IssuedBy { get; set; }
        public Patient IssuedTo { get; set; }
        public VisitSummary VisitSummary { get; set; }

    }
}
