using Asklepios.Core.Enums;
using System;

namespace Asklepios.Core.Models
{
    public  class Person
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FullName
        {
            get
            {
                return Name + " " + Surname;
            }
        }
        public string PESEL { get; set; }
        public string PassportNumber { get; set; }
        public string PassportCode { get; set; }
        public bool HasPolishCitizenship{ get;set; }
        public string EmailAddress { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public Aglomeration DefaultAglomeration { get; set; }
        public Person (string name, string surName, long id, string pesel, bool hasPolishCitizenship, string passportNumber,string  passportCode, string email, Aglomeration aglomeration, DateTimeOffset birthDate)
        {
            Name = name;
            Surname = surName;
            Id = id;
            BirthDate = birthDate;
            if (hasPolishCitizenship)
            {
                if (string.IsNullOrWhiteSpace(pesel))
                {
                    throw new TypeInitializationException("Polski obywatel musi mieć podany PESEL!",null);
                }               
            }
            else
            {
                if (string.IsNullOrWhiteSpace(passportNumber)|| string.IsNullOrWhiteSpace(passportCode))
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
    }
}
