using Asklepios.Core.Enums;

namespace Asklepios.Core.Models
{
    public class Doctor : MedicalWorker
    {
        public override string ProfessionalTitle => "Lekarz";
        public override bool CanAddMedicalTestResults => true;
        public override bool CanIssueExamReferral => true;
        public override bool CanIssuePrescription => true;


        public Doctor()
        {

        }
        public Doctor(long personId, string professionalNumber) : base(personId, professionalNumber)
        {
            MedicalWorkerType = Enums.MedicalWorkerType.Doctor;
        }
    }
}