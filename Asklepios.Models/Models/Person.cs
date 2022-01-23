using Asklepios.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asklepios.Core.Models
{
    public class Person
    {
        public long Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Imię")]
        public string Name { get; set; }
        [Required]
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

        [DataType(DataType.Text)]
        [Display(Name = "PESEL")]

        public string PESEL { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Numer paszportu")]

        public string PassportNumber { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Kod kraju (w paszporcie")]

        public string PassportCode { get; set; }
        [Display(Name = "Czy posiada obywatelstwo")]

        public bool HasPolishCitizenship { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Adres e-mail")]

        public string EmailAddress { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Data urodzenia")]

        public DateTimeOffset? BirthDate { get; set; }
        [Display(Name = "Domyślna aglomeracja")]
        public Aglomeration? DefaultAglomeration { get; set; }
        [NotMapped]
        [Display(Name = "Zdjęcie (300x500 pikseli)")]
        public IFormFile ImageFile { get; set; }
        public string ImageFilePath { get; set; }

        public Person()
        {

        }
        public Person(string name, string surName, long id, string pesel, bool hasPolishCitizenship, string passportNumber, string passportCode, string email, Aglomeration aglomeration, DateTimeOffset birthDate)
        {
            Name = name;
            Surname = surName;
            Id = id;
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
            EmailAddress = email;
            DefaultAglomeration = aglomeration;
        }
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
                        if (!string.IsNullOrWhiteSpace(EmailAddress))
                        {
                            if (BirthDate.HasValue)
                            {
                                if (DefaultAglomeration.HasValue)
                                {
                                    return true;
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
