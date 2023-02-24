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
        //public Nurse(Person person , string profNumber) :base(person)
        //{
        //    ProfessionalNumber = profNumber;
        //    MedicalWorkerType = Enums.MedicalWorkerType.Nurse;
        //    PersonId = person.Id;
        //}
        public Nurse()
        {

        }
        public Nurse(long personId, string professionalNumber) : base(personId, professionalNumber)
        {
            //ProfessionalNumber = professionalNumber;
            MedicalWorkerType = Enums.MedicalWorkerType.Nurse;
            //PersonId = personId;
        }

        public override string ProfessionalTitle => "Pielęgniarka";
        public override bool CanAddMedicalTestResults => true;
        public override bool CanIssueExamReferral => false    ;
        public override bool CanIssuePrescription => true;

        //public string   PWZNumber { get;}
    }

}
