using Asklepios.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

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
            PNumber = pNumber;
            MedicalWorkerType= MedicalWorkerType.DentalHygienist;
        }
        public override string ProfessionalTitle => "Higienistka Dentystyczna";
        public string PNumber { get;}
    }

}
