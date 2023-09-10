using Asklepios.Core.Models;
using Asklepios.Web.Enums;
using Asklepios.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace Asklepios.Web.Areas.MedicalWorkerArea.Models
{
    public class ContactMessageViewModel: ContactViewModel,IBaseViewModel
    {
        public bool HasInfoMessage 
        { 
            get
            {
                return !(string.IsNullOrWhiteSpace(AlertMessage));
            }
        }
        public AlertMessageType AlertMessageType { get; set; }
        public Patient SelectedPatient { get; set; }
        public string UserName { get;set; }

        public ContactMessageViewModel()
        {
            UserType = Core.Enums.UserType.Guest;
        }
        public ContactMessageViewModel(Core.Models.Patient patient )
        {
            SelectedPatient = patient;
            UserType = Core.Enums.UserType.Patient;
            ContactName = patient.Person.FullName;
            ContactEMailAddress = patient.User.Email;
            UserId = patient.Id;
        }
        //public ContactMessageViewModel(Core.Models.MedicalWorker worker, Core.Models.Patient patient)
        //{
        //    SelectedPatient = patient;
        //    UserType = Core.Enums.UserType.Employee;
        //    ContactName = worker.Person.FullName;
        //    ContactEMailAddress = worker.User.EmailAddress;
        //    UserId = worker.Id;
        //}
        public ContactMessageViewModel(Core.Models.MedicalWorker worker)
        {
            UserType = Core.Enums.UserType.Employee;
            ContactName = worker.Person.FullName;
            //worker.User=_conte
            ContactEMailAddress = worker.User.Email;
            UserId = worker.Id;
        }
        public ContactMessageViewModel(User user)
        {
            UserType = user.UserType.Value;
            ContactName = user.Person.FullName;
            ContactEMailAddress = user.Email;
            UserId = user.Person.Id;
        }
    }
}
