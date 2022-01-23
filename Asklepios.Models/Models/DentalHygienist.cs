using Asklepios.Core.Enums;

namespace Asklepios.Core.Models
{
    public class DentalHygienist : MedicalWorker
    {
        //public DentalHygienist(string name, string surName, long id, string pesel, bool hasPolishCitizenship, string passportNumber, string passportCode, string pNumber, string email, Aglomeration aglomeration) : base(name, surName, id, pesel, hasPolishCitizenship, passportNumber, passportCode, email, aglomeration)
        //{
        //    PNumber = pNumber;
        //}
        //public Person Person { get; set; }
        public DentalHygienist(Person person, string pNumber):base  (person)
        {
            ProfessionalNumber = pNumber;
            MedicalWorkerType= MedicalWorkerType.DentalHygienist;
            PersonId = person.Id;
        }
        public override string ProfessionalTitle => "Higienistka Dentystyczna";
    }

}
