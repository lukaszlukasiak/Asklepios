using Asklepios.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asklepios.Core.Models
{
    public class Patient : Person
    {
        public MedicalPackage MedicalPackage { get; set; }
        public string EmployerName { get; set; }
        public string EmployerNIP { get; set; }
        public NFZUnit NFZUnit { get; set; }
        public List<NotificationFilter> Notifications {get;set;}
        public Patient(string name, string surName, long id, string pesel, bool hasPolishCitizenship, string passportNumber, string passportCode, string email, Aglomeration aglomeration) : base(name, surName, id, pesel, hasPolishCitizenship, passportNumber, passportCode, email, aglomeration)
        {
        }
    }
}
