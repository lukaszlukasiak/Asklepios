using Asklepios.Core.Models;
using Asklepios.Web.Enums;
using Asklepios.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace Asklepios.Web.Areas.AdministrativeArea.Models 
{
    public class ContactMessageViewModel : IContactViewModel
    {
    //    [Required(ErrorMessage = "Proszę podaj swoje imię i nazwisko")]
    //    [Display(Name = "Imię i nazwisko")]
    //    [StringLength(50)]
    //    public string ContactName { get; set; }
    //    [Required(ErrorMessage ="Proszę wprowadź swój adres e-mail")]
    //    [StringLength(50)]
    //    [DataType(DataType.EmailAddress)]
    //    [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
    //ErrorMessage = "Niepoprawny format adresu e-mail")]
    //    public string ContactEMailAddress { get; set; }
    //    [Required(ErrorMessage = "Proszę wprowadź numer telefonu")]
    //    [StringLength(25)]
    //    [DataType(DataType.PhoneNumber)]
    //    [Display(Name = "Numer telefonu")]

    //    public string PhoneNumber { get; set; }
    //    [Required(ErrorMessage = "Proszę wprowadź temat wiadomości")]
    //    [StringLength(50)]

    //    public string Subject { get; set; }
    //    [Required(ErrorMessage = "Proszę wprowadź wiadomość")]
    //    [StringLength(500)]
    //    public string Message { get; set; }
    //    public string AlertMessage { get; set; }
    //    public Core.Enums.UserType UserType { get; set; }
    //    public long UserId { get; set; }
        public bool HasInfoMessage 
        { 
            get
            {
                return !(string.IsNullOrWhiteSpace(AlertMessage));
            }
        }
        public AlertMessageType AlertMessageType { get; set; }
        public Patient SelectedPatient { get; set; }

        //public ContactMessageViewModel()
        //{
        //    UserType = Core.Enums.UserType.Guest;
        //}
        //public ContactMessageViewModel(Core.Models.Patient patient )
        //{
        //    SelectedPatient = patient;
        //    UserType = Core.Enums.UserType.Patient;
        //    ContactName = patient.Person.FullName;
        //    ContactEMailAddress = patient.Person.EmailAddress;
        //    UserId = patient.Id;
        //}
        public ContactMessageViewModel()
        {
        }

        public ContactMessageViewModel(Core.Models.MedicalWorker worker)
        {
            UserType = Core.Enums.UserType.Employee;
            ContactName = worker.Person.FullName;
            ContactEMailAddress = worker.Person.EmailAddress;
            UserId = worker.Id;
        }
        public ContactMessageViewModel(User user)
        {
            UserType = user.UserType;
            ContactName = user.Person.FullName;
            ContactEMailAddress = user.Person.EmailAddress;
            UserId = user.Person.Id;
        }

    }
}
