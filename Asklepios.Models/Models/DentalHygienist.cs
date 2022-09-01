using Asklepios.Core.Enums;

namespace Asklepios.Core.Models
{
    public class DentalHygienist : MedicalWorker
    {
        public DentalHygienist(long personId, string professionalNumber) : base(personId)
        {
            ProfessionalNumber = professionalNumber;
            MedicalWorkerType = Enums.MedicalWorkerType.DentalHygienist;
            PersonId = personId;
        }
        public override string ProfessionalTitle => "Higienistka Dentystyczna";
    }
}
