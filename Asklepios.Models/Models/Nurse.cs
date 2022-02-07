using Asklepios.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asklepios.Core.Models
{
    public class Nurse : MedicalWorker
    {
        //public Nurse(string name, string surName, long id, string pesel, bool hasPolishCitizenship, string passportNumber, string passportCode, string pwzNumber, string email, Aglomeration aglomeration) : base(name, surName, id, pesel, hasPolishCitizenship, passportNumber, passportCode, email, aglomeration)
        //{
        //    PWZNumber = pwzNumber;
        //}
        public Nurse(Person person , string profNumber) :base(person)
        {
            ProfessionalNumber = profNumber;
            MedicalWorkerType = Enums.MedicalWorkerType.Nurse;
            PersonId = person.Id;
        }
        public override string ProfessionalTitle => "Pielęgniarka";
        //public string   PWZNumber { get;}
    }

}
