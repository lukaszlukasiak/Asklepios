using Asklepios.Data.Interfaces;
using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Data.InMemoryContexts
{
    public class HomeInMemoryContext : IHomeModuleRepository
    {
        private IEnumerable<Location> _locations;
        public HomeInMemoryContext()
        {
            _locations = PatientMockDB.GetAllLocations();

            //_locations = new List<Location>()
            //{
            //    new Location()
            //        {   City="Warszawa",
            //            StreetAndNumber="Jerozolimskie 80",
            //            Description="Ośrodek w centrum Warszawy ze świetnym dojazdem z każdej dzielnicy.",
            //            Facilities=new List<string>(){"15 gabinetów ogólno-konsultacyjnych", "Gabinez zabiegowy", "2 gabinety stomatologiczne", "Gabinet higieny jamy ustnej", "Gabinet diagnostyki obrazowej"},
            //            Id=1,
            //            Name="Ośrodek Warszawa Jerozolimskie",
            //            Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia"},
            //            //VoivodeshipType=Core.Enums.VoivodeshipType.mazowieckie,
            //            Aglomeration=Core.Enums.Aglomeration.Warsaw,
            //            ImagePath="/img/locations/loc1.jpeg",
            //            PhoneNumber="22 780 421 433",
            //            PostalCode="01-111",
            //            },
            //                    new Location()
            //        {   City="Warszawa",
            //            StreetAndNumber="Grójecka 100",
            //            Description="Ośrodek w Warszawie w dzielnicy Ochota, z bardzo dobrym dojazdem z zachodniej części Warszawy.",
            //            Facilities=new List<string>(){"12 gabinetów ogólno-konsultacyjnych", "Gabinez zabiegowy", "Gabinet diagnostyki obrazowej", "Gabinek okulistyczny"},
            //            Id=2,
            //            Name="Ośrodek Warszawa Ochota",
            //            Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia"},
            //            //VoivodeshipType=Core.Enums.VoivodeshipType.mazowieckie,
            //            Aglomeration=Core.Enums.Aglomeration.Warsaw,
            //            ImagePath="/img/locations/loc2.jpg",
            //            PhoneNumber="22 787 477 323",
            //            PostalCode="01-211",

            //            },
            //    new Location()
            //        {   City="Warszawa",
            //            StreetAndNumber="KEN 20",
            //            Description="Ośrodek na południu Warszawy ze świetnym dojazdem z południa Warszawy oraz regionów wzdłuż M1 oraz południowych okolic Warszawy.",
            //            Facilities=new List<string>(){"11 gabinetów ogólno-konsultacyjnych", "2 Gabinety zabiegowe", "2 Gabinety ginekologiczne", "2 gabinety stomatologiczne", "Gabinet diagnostyki obrazowej"},
            //            Id=3,
            //            Name="Ośrodek Warszawa Ursynów",
            //            Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia"},
            //            //VoivodeshipType=Core.Enums.VoivodeshipType.mazowieckie,
            //            Aglomeration=Core.Enums.Aglomeration.Warsaw,
            //            ImagePath="/img/locations/loc3.jpg",
            //            PhoneNumber="22 777 600 313",
            //            PostalCode="03-055",

            //            },
            //    new Location()
            //        {   City="Warszawa",
            //            StreetAndNumber="Malborska",
            //            Description="Ośrodek na wschodzie Warszawy z dobrym dojazdem ze wschodnich dzielnic Warszawy a także wschodnich okolic Warszawy.",
            //            Facilities=new List<string>(){"15 gabinetów ogólno-konsultacyjnych", "Gabinez zabiegowy", "2 gabinety stomatologiczne", "Gabinet higieny jamy ustnej", "Gabinet diagnostyki obrazowej"},
            //            Id=4,
            //            Name="Ośrodek Warszawa Targówek",
            //            Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia"},
            //            //VoivodeshipType=Core.Enums.VoivodeshipType.mazowieckie,
            //            Aglomeration=Core.Enums.Aglomeration.Warsaw,
            //            ImagePath="/img/locations/loc4.jpg",
            //            PhoneNumber="22 777 444 333",
            //            PostalCode="02-222",

            //            },
            //    new Location()
            //        {   City="Kraków",
            //            StreetAndNumber="Podgórska 14",
            //            Description="Ośrodek w Krakowie, w świetnie skomunikowanym Kazimierzu",
            //            Facilities=new List<string>(){"15 gabinetów ogólno-konsultacyjnych", "Gabinez zabiegowy", "2 gabinety stomatologiczne", "Gabinet higieny jamy ustnej", "Gabinet diagnostyki obrazowej"},
            //            Id=5,
            //            Name="Ośrodek Warszawa Jerozolimskie",
            //            Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia"},
            //            //VoivodeshipType=Core.Enums.VoivodeshipType.malopolskie,
            //            Aglomeration=Core.Enums.Aglomeration.Cracow,
            //            ImagePath="/img/locations/loc5.jpg",
            //            PhoneNumber="20 300 400 111",
            //            PostalCode="80-078",

            //            },
            //    new Location()
            //        {   City="Gdańsk",
            //            StreetAndNumber="Chlebnicka 11",
            //            Description="Ośrodek w centrum Gdańska na popularnje Wyspie Spichrzów",
            //            Facilities=new List<string>(){"15 gabinetów ogólno-konsultacyjnych", "Gabinez zabiegowy", "2 gabinety stomatologiczne", "Gabinet higieny jamy ustnej", "Gabinet diagnostyki obrazowej"},
            //            Id=1,
            //            Name="Ośrodek Gdańsk Wyspa Spichrzów",
            //            Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia"},
            //            //VoivodeshipType=Core.Enums.VoivodeshipType.pomorskie,
            //            Aglomeration=Core.Enums.Aglomeration.Tricity,
            //            ImagePath="/img/locations/loc6.jpg",
            //            PhoneNumber="30 500 500 241",
            //            PostalCode="45-100",
            //            },
            //                    new Location()
            //        {   City="Poznań",
            //            StreetAndNumber="Chlebnicka 11",
            //            Description="Ośrodek w centrum Gdańska na popularnje Wyspie Spichrzów",
            //            Facilities=new List<string>(){"15 gabinetów ogólno-konsultacyjnych", "Gabinez zabiegowy", "2 gabinety stomatologiczne", "Gabinet higieny jamy ustnej", "Gabinet diagnostyki obrazowej"},
            //            Id=1,
            //            Name="Ośrodek Gdańsk Wyspa Spichrzów",
            //            Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia"},
            //            //VoivodeshipType=Core.Enums.VoivodeshipType.pomorskie,
            //            Aglomeration=Core.Enums.Aglomeration.Tricity,
            //            ImagePath="/img/locations/loc6.jpg",
            //            PhoneNumber="30 500 500 241",
            //            PostalCode="45-100",
            //            },
            //                                    new Location()
            //        {   City="Wrocław",
            //            StreetAndNumber="Chlebnicka 11",
            //            Description="Ośrodek w centrum Gdańska na popularnje Wyspie Spichrzów",
            //            Facilities=new List<string>(){"15 gabinetów ogólno-konsultacyjnych", "Gabinez zabiegowy", "2 gabinety stomatologiczne", "Gabinet higieny jamy ustnej", "Gabinet diagnostyki obrazowej"},
            //            Id=1,
            //            Name="Ośrodek Gdańsk Wyspa Spichrzów",
            //            Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia"},
            //            //VoivodeshipType=Core.Enums.VoivodeshipType.pomorskie,
            //            Aglomeration=Core.Enums.Aglomeration.Tricity,
            //            ImagePath="/img/locations/loc6.jpg",
            //            PhoneNumber="30 500 500 241",
            //            PostalCode="45-100",
            //            },




            //};
        }
        public IEnumerable<Location> GetAllLocations()
        {
            return _locations;
        }

        public Location GetLocationById(long locationId)
        {
            if (_locations.Where(c=>c.Id==locationId).Count()>0)
            {
                return _locations.Where(c => c.Id == locationId).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public User LogIn(User user)
        {
            List<User> users = PatientMockDB.Users;
            users = users.Where(c => c.UserType == user.UserType).ToList();
            User user1 = users.Where(c => c.UserName == user.UserName).FirstOrDefault();
            if (user1==null)
            {
                return null;
            }
            if (user.Password==user1.Password)
            {
                return user1;
            }
            else
            {
                return null;
            }
            //return PatientMockDB.Users.Where(c => c.UserType == user.UserType && c.UserName == user.UserName && c.Password == user.Password).FirstOrDefault();
        }
    }
}
