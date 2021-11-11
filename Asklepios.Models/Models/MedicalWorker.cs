using Asklepios.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asklepios.Core.Models
{
    public abstract class MedicalWorker : Person
    {
        public MedicalWorker(string name, string surName, long id, string pesel, bool hasPolishCitizenship, string passportNumber, string passportCode, string email) : base(name, surName, id, pesel, hasPolishCitizenship, passportNumber, passportCode, email)
        {
        }

        public DateTime HiredSince { get; set; }
        public DateTime HiredUntil { get; set; }
        public bool IsCurrentlyHired { get; set; }
        public abstract string ProfessionalTitle { get; }
        public List<Visit> FutureVisits { get; set; }
        public List<Visit> PastVisits { get; set; }
        public MedicalWorkerType MedicalWorkerType { get;set;}

    }
}
