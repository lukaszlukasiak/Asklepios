using System;
using System.Collections.Generic;
using System.Text;

namespace Asklepios.Core.Models
{
    public class Nurse : MedicalWorker
    {
        public Nurse(string name, string surName, long id, string pesel, bool hasPolishCitizenship, string passportNumber, string passportCode, string pwzNumber) : base(name, surName, id, pesel, hasPolishCitizenship, passportNumber, passportCode)
        {
            PWZNumber = pwzNumber;
        }

        public override string ProfessionalTitle => "Pielęgniarka";
        public string   PWZNumber { get;}
    }

}
