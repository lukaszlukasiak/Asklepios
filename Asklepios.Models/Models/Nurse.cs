using Asklepios.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asklepios.Core.Models
{
    public class Nurse : MedicalWorker
    {
        public Nurse():base ()
        {

        }
        public Nurse(long personId, string professionalNumber) : base(personId, professionalNumber)
        {
            //ProfessionalNumber = professionalNumber;
            MedicalWorkerType = Enums.MedicalWorkerType.Nurse;
            //PersonId = personId;
        }

        public override string ProfessionalTitle => "Pielęgniarka";
        public override bool CanAddMedicalTestResults => true;
        public override bool CanIssueExamReferral => false    ;
        public override bool CanIssuePrescription => true;

        //public string   PWZNumber { get;}
    }

}
