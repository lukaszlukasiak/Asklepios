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

        //public ElectroradiologyTechnician(Person person, string profNumber ): base(person)
        //{
        //    ProfessionalNumber = profNumber;
        //    MedicalWorkerType = Enums.MedicalWorkerType.ElectroriadologyTechnician;
        //    PersonId = person.Id;
        //}
        public override bool CanAddMedicalTestResults => true;
        public override bool CanIssueExamReferral => false;
        public override bool CanIssuePrescription => false;

        public ElectroradiologyTechnician()
        {

        }
        public ElectroradiologyTechnician(long personId, string professionalNumber) : base(personId, professionalNumber)
        {
            ProfessionalNumber = professionalNumber;
            MedicalWorkerType = Enums.MedicalWorkerType.ElectroriadologyTechnician;
            //PersonId = personId;
        }

        public override string ProfessionalTitle => "Technik elektroradiolog";
    }
}
