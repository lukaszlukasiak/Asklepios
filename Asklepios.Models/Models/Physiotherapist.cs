using Asklepios.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asklepios.Core.Models
{
    public class Physiotherapist : MedicalWorker
    {
        public override bool CanAddMedicalTestResults => false;
        public override bool CanIssueExamReferral => false;
        public override bool CanIssuePrescription => true;

        public Physiotherapist():base()
        {

        }
        public Physiotherapist(long personId, string professionalNumber) : base(personId, professionalNumber)
        {
            MedicalWorkerType = Enums.MedicalWorkerType.Physiotherapist;

        }

        public override string ProfessionalTitle => "Fizjoterapeuta";
    }

}
