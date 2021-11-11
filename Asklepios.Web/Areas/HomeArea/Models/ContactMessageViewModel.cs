using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.HomeArea.Models
{
    public class ContactMessageViewModel
    {
        public string ContactName { get; set; }
        public string ContactEMailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string AlertMessage { get; set; }
        public Core.Enums.UserType UserType { get; set; }
        public long UserId { get; set; }

        public ContactMessageViewModel()
        {
            UserType = Core.Enums.UserType.Guest;
        }
        public ContactMessageViewModel(Core.Models.Patient patient )
        {
            UserType = Core.Enums.UserType.Patient;
            ContactName = patient.FullName;
            ContactEMailAddress = patient.EmailAddress;
            UserId = patient.Id;
        }
        public ContactMessageViewModel(Core.Models.MedicalWorker worker)
        {
            UserType = Core.Enums.UserType.Employee;
            ContactName = worker.FullName;
            ContactEMailAddress = worker.EmailAddress;

            UserId = worker.Id;
        }
    }
}
