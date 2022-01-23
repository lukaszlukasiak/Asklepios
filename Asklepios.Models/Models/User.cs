using Asklepios.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Core.Models
{
    public class User
    {
        [Key]
        public long Id { get; set; }
        [Required(ErrorMessage = "Proszę podać adres e-mail. Będzie on zarówno nazwą użytkownika.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Adres e-mail")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Proszę podać hasło")]
        [DataType(DataType.Password)]
        [Display(Name = "Haslo (minimum 8 znaków")]
        public string Password { get; set; }
        [Display(Name = "Typ użytkownika")]
        public UserType? UserType { get; set; }
        [Display(Name = "Typ modułu, do którego użytkownik ma dostęp")]
        public WorkerModuleType? WorkerModuleType { get; set; }
        public long PersonId { get; set; }
        public Person Person { get; set; }

        public bool IsValid
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(UserName))
                {
                    if (!string.IsNullOrWhiteSpace(UserName) && UserName.Length>=8)
                    {
                        if (UserType.HasValue)
                        {
                            if (WorkerModuleType.HasValue)
                            {
                                if (PersonId>0)
                                {
                                    if (Person!=null)
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
                return false;
            }
        }
    }
}
