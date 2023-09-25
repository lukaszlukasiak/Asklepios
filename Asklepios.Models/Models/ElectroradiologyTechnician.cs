using Asklepios.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asklepios.Core.Models
{
    public class ElectroradiologyTechnician : MedicalWorker
    {
        public override bool CanAddMedicalTestResults => true;
        public override bool CanIssueExamReferral => false;
        public override bool CanIssuePrescription => false;

        public ElectroradiologyTechnician():base()
        {

        }
        public ElectroradiologyTechnician(long personId, string professionalNumber) : base(personId, professionalNumber)
        {
            ProfessionalNumber = professionalNumber;
            MedicalWorkerType = Enums.MedicalWorkerType.ElectroriadologyTechnician;
        }

        public override string ProfessionalTitle => "Technik elektroradiolog";
    }
}
