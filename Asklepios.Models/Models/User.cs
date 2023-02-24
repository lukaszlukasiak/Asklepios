using Asklepios.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Core.Models
{
    public class User: IdentityUser<long>
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Required]
        //public long Id { get; set; }
        [Required(ErrorMessage = "Proszę podać adres e-mail")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Adres e-mail")]
        public override string Email { get; set; }
        [Required(ErrorMessage = "Proszę podać hasło")]
        [DataType(DataType.Password)]
        [Display(Name = "Haslo (minimum 8 znaków)")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Proszę podać hasło o długości  minimum 8 znaków (i maksymalnie 100)")]
        public override string PasswordHash { get; set; }
        [Display(Name = "Typ użytkownika")]
        public UserType? UserType { get; set; }
        [Display(Name = "Typ modułu, do którego użytkownik ma dostęp")]
        public WorkerModuleType? WorkerModuleType { get; set; }
        public long?      PersonId { get; set; }
        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }

        public long? MedicalWorkerId { get; set; }
        //[ForeignKey("MedicalWorkerId")]

        public virtual MedicalWorker MedicalWorker { get; set; }
        public long? PatientId { get; set; }
        //[ForeignKey("PatientId")]

        public virtual Patient Patient { get; set; }
        //public virtual worker Person { get; set; }



        public bool IsValid
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Email))
                {
                    if (!string.IsNullOrWhiteSpace(PasswordHash) && PasswordHash.Length >= 8)
                    {
                        if (UserType.HasValue)
                        {
                            if (UserType == Enums.UserType.Employee)
                            {
                                if (WorkerModuleType.HasValue)
                                {
                                    if (!string.IsNullOrWhiteSpace( UserName))
                                    {
                                        return true;

                                    }
                                    //if (Person != null)
                                    //{
                                    //}
                                }
                            }
                            else
                            {
                                //if (Person != null)
                                //{
                                    return true;
                                //}
                            }
                        }
                    }
                }
                return false;
            }
        }
        public void UpdateWith(User user)
        {
            if (!string.IsNullOrWhiteSpace(user.Email))
            {
                this.Email = user.Email;
            }
            if (!string.IsNullOrWhiteSpace(user.PasswordHash))
            {
                this.PasswordHash = user.PasswordHash;
            }
            if (user.Person!=null)
            {
                this.Person = user.Person;
            }
            if (user.UserType.HasValue)
            {
                this.UserType = user.UserType;
            }
            if (user.WorkerModuleType.HasValue)
            {
                this.WorkerModuleType = user.WorkerModuleType;
            }
        }
    }
}
