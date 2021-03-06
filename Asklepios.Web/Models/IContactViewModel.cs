using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Proszę podaj swoje imię i nazwisko")]
        [Display(Name = "Imię i nazwisko")]
        [StringLength(50)]
        public string ContactName { get; set; }
        [Required(ErrorMessage = "Proszę wprowadź swój adres e-mail")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
    ErrorMessage = "Niepoprawny format adresu e-mail")]
        public string ContactEMailAddress { get; set; }
        [Required(ErrorMessage = "Proszę wprowadź numer telefonu")]
        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Numer telefonu")]

        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Proszę wprowadź temat wiadomości")]
        [StringLength(50)]

        public string Subject { get; set; }
        [Required(ErrorMessage = "Proszę wprowadź wiadomość")]
        [StringLength(500)]
        public string Message { get; set; }
        public string AlertMessage { get; set; }
        public Core.Enums.UserType UserType { get; set; }
        public long UserId { get; set; }

    }
}
