using Asklepios.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asklepios.Core.Models
{
    public class Physiotherapist : MedicalWorker
    {
        //public Physiotherapist(string name, string surName, long id, string pesel, bool hasPolishCitizenship, string passportNumber, string passportCode, string npwzNumber, string email, Aglomeration aglomeration) : base(name, surName, id, pesel, hasPolishCitizenship, passportNumber, passportCode, email, aglomeration)
        //{
        //    NPWZNumber = npwzNumber;
        //}
        public Physiotherapist(Person person, string profNumber):base(person)
        {
            ProfessionalNumber = profNumber;
            MedicalWorkerType = Enums.MedicalWorkerType.Physiotherapist;
            PersonId = person.Id;
        }
        public override string ProfessionalTitle => "Fizjoterapeuta";
        //public string   NPWZNumber { get;}
    }

}
