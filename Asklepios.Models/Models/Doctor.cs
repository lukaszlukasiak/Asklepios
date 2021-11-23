using Asklepios.Core.Enums;

namespace Asklepios.Core.Models
{
    public class Doctor : MedicalWorker
    {
        public override string ProfessionalTitle => "Lekarz";

        

        public Doctor(string name, string surName, long id, string pesel, bool hasPolishCitizenship, string passportNumber, string passportCode, string email, Aglomeration aglomeration) : base(name, surName, id, pesel, hasPolishCitizenship, passportNumber, passportCode, email, aglomeration)
        {
            this.MedicalWorkerType = Enums.MedicalWorkerType.Doctor;
        }

        //public Doctor()
        //{
        //}
    }
}