using Asklepios.Core.Enums;
using Asklepios.Core.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace Asklepios.Core.Models
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]

        public long Id { get; set; }
        [Required(ErrorMessage = "Proszę podać imię")]
        [DataType(DataType.Text)]
        [Display(Name = "Imię")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Proszę podać nazwisko")]
        [DataType(DataType.Text)]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }
        public string FullName
        {
            get
            {
                return Name + " " + Surname;
            }
        }
        [StringLength(11, ErrorMessage = "Niepoprawny PESEL, długość musi wynosić 11 znaków", MinimumLength = 11)]
        [DataType(DataType.Text)]
        [Display(Name = "PESEL")]

        public string PESEL { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Numer paszportu")]

        public string PassportNumber { get; set; }

        [StringLength(int.MaxValue, ErrorMessage = "Musisz podać przynajmniej jeden z numerów: PESEL albo numer paszportu", MinimumLength = 1)]

        public string PESELAndPassport
        {
            get
            {
                return PESEL + PassportNumber;
            }
        }
        [StringLength(int.MaxValue, ErrorMessage = "W przypadku obywatela Polski wprowadź numer PESEL", MinimumLength = 1)]
        [Required( ErrorMessage = "W przypadku obywatela Polski wprowadź numer PESEL")]


        public string PESELAndCitizenship
        {
            get
            {
                if (HasPolishCitizenship)
                {
                    return PESEL;
                }
                else
                {
                    return "P";
                }
            }
        }

        [DataType(DataType.Text)]
        [Display(Name = "Kod kraju (w paszporcie)")]

        [StringLength(3, ErrorMessage = "Niepoprawny kod, długość musi wynosić 3 znaki", MinimumLength = 3)]
        public string PassportCode { get; set; }
        [Display(Name = "Czy posiada polskie obywatelstwo?")]

        public bool HasPolishCitizenship { get; set; } = true;
        //[Required(ErrorMessage = "Proszę podać adres e-mail")]
        //[DataType(DataType.EmailAddress)]
        //[Display(Name = "Adres e-mail")]
        //public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Proszę podać number telefonu")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Numer telefonu")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Proszę podać datę urodzenia")]
        [DataType(DataType.Date)]
        [Display(Name = "Data urodzenia")]
        [CustomDateAttribute(ErrorMessage ="Pacjent musi mieć między 0 a 120 lat")]
        public DateTimeOffset? BirthDate { get; set; }
        [Display(Name = "Domyślna aglomeracja")]
        public Aglomeration? DefaultAglomeration { get; set; }
        public string AglomerationDescription
        {
            get
            {
                if (DefaultAglomeration.HasValue)
                {
                    return DefaultAglomeration.Value.GetDescription();
                }
                else
                {
                    return "";
                }
            }
        }
        [Required(ErrorMessage = "Proszę podać płeć")]
        [Display(Name = "Płeć")]
        public Gender? Gender { get; set; }

        [NotMapped]
        [Display(Name = "Zdjęcie (300x500 pikseli)")]
        public IFormFile ImageFile { get; set; }
        public string ImageFilePath { get; set; }
        public string ImageSource
        {
            get
            {
                if (ImageFile != null)
                {
                    if (ImageFile.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            ImageFile.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            string s = Convert.ToBase64String(fileBytes);


                            return string.Format("data:image/jpg;base64,{0}", s);
                            // act on the Base64 data
                        }
                    }

                }
                return ImageFilePath;
            }
        }
        public string ReturnProperImageFilePath(string defaultPathF, string defaultPathM)
        {
            if (string.IsNullOrWhiteSpace(ImageFilePath))
            {
                if (Gender == Enums.Gender.Female)
                {
                    return defaultPathF;
                }
                else
                {
                    return defaultPathM;
                }
            }
            else
            {
                return ImageFilePath;
            }
        }

        public Person()
        {

        }
        public Person(string name, string surName, long id, string pesel, bool hasPolishCitizenship, string passportNumber, string passportCode, string email, Aglomeration aglomeration, DateTimeOffset birthDate, Gender gender, string phoneNumber, string imagePath)
        {
            Name = name;
            Surname = surName;
            Id = id;
            PhoneNumber = phoneNumber;
            Gender = gender;
            ImageFilePath = imagePath;
            BirthDate = birthDate;
            if (hasPolishCitizenship)
            {
                if (string.IsNullOrWhiteSpace(pesel))
                {
                    throw new TypeInitializationException("Polski obywatel musi mieć podany PESEL!", null);
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(passportNumber) || string.IsNullOrWhiteSpace(passportCode))
                {
                    throw new TypeInitializationException("Osoby bez polskiego obywatelstwa muszą mieć podany numer paszportu oraz kod!", null);
                }
            }
            PESEL = pesel;
            HasPolishCitizenship = hasPolishCitizenship;
            PassportNumber = passportNumber;
            PassportCode = passportCode;
            //EmailAddress = email;
            DefaultAglomeration = aglomeration;
        }

        public void UpdateWith(Person person)
        {
            if (person.BirthDate.HasValue)
            {
                this.BirthDate = person.BirthDate;
            }
            if (person.DefaultAglomeration.HasValue)
            {
                this.DefaultAglomeration = person.DefaultAglomeration;
            }
            if (person.Gender.HasValue)
            {
                this.Gender = person.Gender;
            }
            //if (person.HasPolishCitizenship)
            //{
                this.HasPolishCitizenship = person.HasPolishCitizenship;
            //}
            if (!string.IsNullOrWhiteSpace(person.ImageFilePath))
            {
                this.ImageFilePath = person.ImageFilePath;
            }
            if (!string.IsNullOrWhiteSpace( person.Name))
            {
                this.Name = person.Name;
            }
            if (!string.IsNullOrWhiteSpace(person.PassportCode))
            {
                this.PassportCode = person.PassportCode;
            }
            if (!string.IsNullOrWhiteSpace(person.PassportNumber))
            {
                this.PassportNumber = person.PassportNumber;
            }
            if (!string.IsNullOrWhiteSpace(person.PESEL))
            {
                this.PESEL = person.PESEL;
            }
            if (!string.IsNullOrWhiteSpace(person.PhoneNumber))
            {
                this.PhoneNumber = person.PhoneNumber;
            }
            if (!string.IsNullOrWhiteSpace(person.Surname))
            {
                this.Surname = person.Surname;
            }
            
        }
        [NotMapped]
        public string ValidationError { get; set; }
        public bool IsValid
        {
            get
            {
                if (HasPolishCitizenship)
                {
                    if (string.IsNullOrWhiteSpace(PESEL))
                    {
                        ValidationError = "Polski obywatel musi mieć podany PESEL";
                        return false;
                        //throw new TypeInitializationException("Polski obywatel musi mieć podany PESEL!", null);
                    }
                    else
                    {
                        if (PESEL.Length != 11)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(PassportNumber) || string.IsNullOrWhiteSpace(PassportCode))
                    {
                        ValidationError = "Osoby bez polskiego obywatelstwa muszą mieć podany numer paszportu oraz kod!";
                        return false;
                        //throw new TypeInitializationException("Osoby bez polskiego obywatelstwa muszą mieć podany numer paszportu oraz kod!", null);
                    }
                }
                if (!string.IsNullOrWhiteSpace(Name))
                {
                    if (!string.IsNullOrWhiteSpace(Surname))
                    {
                        //if (!string.IsNullOrWhiteSpace(EmailAddress))
                        //{
                        if (BirthDate.HasValue)
                        {
                            if (DefaultAglomeration.HasValue)
                            {
                                if (Gender.HasValue)
                                {
                                    if (!string.IsNullOrWhiteSpace(PhoneNumber))
                                    {
                                        return true;

                                    }
                                }
                            }
                            //}
                        }
                    }
                }
                return false;

            }
        }
    }
}