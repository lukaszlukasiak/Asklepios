using Asklepios.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asklepios.Core.Models
{
    public class ElectroradiologyTechnician : MedicalWorker
    {
        //public ElectroradiologyTechnician(string name, string surName, long id, string pesel, bool hasPolishCitizenship, string passportNumber, string passportCode, string email, Aglomeration aglomeration) : base(name, surName, id, pesel, hasPolishCitizenship, passportNumber, passportCode, email, aglomeration)
        //{
        //}

        public ElectroradiologyTechnician(Person person, string profNumber ): base(person)
        {
            ProfessionalNumber = profNumber;
            MedicalWorkerType = Enums.MedicalWorkerType.ElectroriadologyTechnician;
            PersonId = person.Id;
        }
        public override string ProfessionalTitle => "Technik elektroradiolog";
    }
}
