using System;

namespace Asklepios.Core.Models
{
    public abstract class Person
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PESEL { get; set; }
        public string PassportNumber { get; set; }
        public string PassportCode { get; set; }
        public bool HasPolishCitizenship{ get;set; }

        public Person (string name, string surName, long id, string pesel, bool hasPolishCitizenship, string passportNumber,string  passportCode)
        {
            Name = name;
            Surname = surName;
            Id = id;
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
        }
    }
}
