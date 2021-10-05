using System;
using System.Collections.Generic;
using System.Text;

namespace Asklepios.Core.Models
{
    public class ElectroradiologyTechnician : MedicalWorker
    {
        public ElectroradiologyTechnician(string name, string surName, long id, string pesel, bool hasPolishCitizenship, string passportNumber, string passportCode) : base(name, surName, id, pesel, hasPolishCitizenship, passportNumber, passportCode)
        {
        }

        public override string ProfessionalTitle => "Technik elektroradiolog";
    }
}
