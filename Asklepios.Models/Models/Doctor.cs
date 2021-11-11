namespace Asklepios.Core.Models
{
    public class Doctor : MedicalWorker
    {
        public override string ProfessionalTitle => "Doktor";
        public Doctor(string name, string surName, long id, string pesel, bool hasPolishCitizenship, string passportNumber, string passportCode, string email) : base(name, surName, id, pesel, hasPolishCitizenship, passportNumber, passportCode, email)
        {
        }

    }
}