using Asklepios.Core.Enums;

namespace Asklepios.Core.Models
{
    public class DentalHygienist : MedicalWorker
    {

        public DentalHygienist() :base()
        {

        }
        public DentalHygienist(long personId, string professionalNumber) : base(personId, professionalNumber)
        {
            MedicalWorkerType = Enums.MedicalWorkerType.DentalHygienist;
        }
        public override string ProfessionalTitle => "Higienistka Dentystyczna";
        public override bool CanAddMedicalTestResults => false;
        public override bool CanIssueExamReferral => false;
        public override bool CanIssuePrescription => false;

    }
}
