using Asklepios.Data.Interfaces;
using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Data.InMemoryContexts
{
    public class PatientInMemoryContext : IPatientModuleRepository
    {
        readonly IEnumerable<Visit> visits;
        readonly IEnumerable<Location> locations;
        readonly Patient patient;
        private readonly IEnumerable<MedicalWorker> medicalWorkers;

        public PatientInMemoryContext()
        {
            visits = GetAvailableVisits();
            locations = GetLocations();
            patient = GetPatientData();
            medicalWorkers = GetMedicalWorkers();
        }

        public IEnumerable<Visit> GetAvailableVisits()
        {
            DateTime today = DateTime.Today;
            return new List<Visit>()
            { new Visit() {Id=1,DateTimeSince=today+new TimeSpan(9,0,0),DateTimeTill=today+new TimeSpan(9,15,0),Location=locations.ElementAt(0)}};

        }

        public IEnumerable<Visit> GetHistoricalVisits()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Location> GetLocations()
        {
            return new List<Location>()
            {
                new Location()
                    {   City="Warszawa",
                        StreetAndNumber="Jerozolimskie 80",
                        Description="Ośrodek w centrum Warszawy ze świetnym dojazdem z każdej dzielnicy.",
                        Facilities=new List<string>(){"15 gabinetów ogólno-konsultacyjnych", "Gabinez zabiegowy", "2 gabinety stomatologiczne", "Gabinet higieny jamy ustnej", "Gabinet diagnostyki obrazowej"},
                        Id=1,
                        Name="Ośrodek Warszawa Jerozolimskie",
                        Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia"},
                        VoivodeshipType=Core.Enums.VoivodeshipType.mazowieckie,
                        ImagePath="/img/locations/loc1.jpeg",
                        PhoneNumber="22 780 421 433",
                        PostalCode="01-111",
                        },
                                new Location()
                    {   City="Warszawa",
                        StreetAndNumber="Grójecka 100",
                        Description="Ośrodek w Warszawie w dzielnicy Ochota, z bardzo dobrym dojazdem z zachodniej części Warszawy.",
                        Facilities=new List<string>(){"12 gabinetów ogólno-konsultacyjnych", "Gabinez zabiegowy", "Gabinet diagnostyki obrazowej", "Gabinek okulistyczny"},
                        Id=2,
                        Name="Ośrodek Warszawa Ochota",
                        Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia"},
                        VoivodeshipType=Core.Enums.VoivodeshipType.mazowieckie,
                        ImagePath="/img/locations/loc2.jpg",
                        PhoneNumber="22 787 477 323",
                        PostalCode="01-211",

                        },
                new Location()
                    {   City="Warszawa",
                        StreetAndNumber="KEN 20",
                        Description="Ośrodek na południu Warszawy ze świetnym dojazdem z południa Warszawy oraz regionów wzdłuż M1 oraz południowych okolic Warszawy.",
                        Facilities=new List<string>(){"11 gabinetów ogólno-konsultacyjnych", "2 Gabinety zabiegowe", "2 Gabinety ginekologiczne", "2 gabinety stomatologiczne", "Gabinet diagnostyki obrazowej"},
                        Id=3,
                        Name="Ośrodek Warszawa Ursynów",
                        Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia"},
                        VoivodeshipType=Core.Enums.VoivodeshipType.mazowieckie,
                        ImagePath="/img/locations/loc3.jpg",
                        PhoneNumber="22 777 600 313",
                        PostalCode="03-055",

                        },
                new Location()
                    {   City="Warszawa",
                        StreetAndNumber="Malborska",
                        Description="Ośrodek na wschodzie Warszawy z dobrym dojazdem ze wschodnich dzielnic Warszawy a także wschodnich okolic Warszawy.",
                        Facilities=new List<string>(){"15 gabinetów ogólno-konsultacyjnych", "Gabinez zabiegowy", "2 gabinety stomatologiczne", "Gabinet higieny jamy ustnej", "Gabinet diagnostyki obrazowej"},
                        Id=4,
                        Name="Ośrodek Warszawa Targówek",
                        Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia"},
                        VoivodeshipType=Core.Enums.VoivodeshipType.mazowieckie,
                        ImagePath="/img/locations/loc4.jpg",
                        PhoneNumber="22 777 444 333",
                        PostalCode="02-222",

                        },
                new Location()
                    {   City="Kraków",
                        StreetAndNumber="Podgórska 14",
                        Description="Ośrodek w Krakowie, w świetnie skomunikowanym Kazimierzu",
                        Facilities=new List<string>(){"15 gabinetów ogólno-konsultacyjnych", "Gabinez zabiegowy", "2 gabinety stomatologiczne", "Gabinet higieny jamy ustnej", "Gabinet diagnostyki obrazowej"},
                        Id=5,
                        Name="Ośrodek Warszawa Jerozolimskie",
                        Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia"},
                        VoivodeshipType=Core.Enums.VoivodeshipType.malopolskie,
                        ImagePath="/img/locations/loc5.jpg",
                        PhoneNumber="20 300 400 111",
                        PostalCode="80-078",

                        },
                new Location()
                    {   City="Gdańsk",
                        StreetAndNumber="Chlebnicka 11",
                        Description="Ośrodek w centrum Gdańska na popularnje Wyspie Spichrzów",
                        Facilities=new List<string>(){"15 gabinetów ogólno-konsultacyjnych", "Gabinez zabiegowy", "2 gabinety stomatologiczne", "Gabinet higieny jamy ustnej", "Gabinet diagnostyki obrazowej"},
                        Id=1,
                        Name="Ośrodek Gdańsk Wyspa Spichrzów",
                        Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia"},
                        VoivodeshipType=Core.Enums.VoivodeshipType.pomorskie,
                        ImagePath="/img/locations/loc6.jpg",
                        PhoneNumber="30 500 500 241",
                        PostalCode="45-100",
                        },
            };

        }

        public IEnumerable<MedicalPackage> GetMedicalPackages()
        {
            throw new NotImplementedException();
        }


        public IEnumerable<MedicalWorker> GetMedicalWorkers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NFZUnit> GetNFZUnits()
        {
            List<NFZUnit> units = new()
            {
                new NFZUnit() { Code = "DLŚ", Description = "Dolnośląski Fundusz Zdrowia" },
                new NFZUnit() { Code = "KPM", Description = "Kujawsko-Pomorski Fundusz Zdrowia" },
                new NFZUnit() { Code = "LBL", Description = "Lubelski Fundusz Zdrowia" },
                new NFZUnit() { Code = "LBS", Description = "Lubuski Fundusz Zdrowia" },
                new NFZUnit() { Code = "ŁDZ", Description = "Łódzki Fundusz Zdrowia" },
                new NFZUnit() { Code = "MŁP", Description = "Małopolski Fundusz Zdrowia" },
                new NFZUnit() { Code = "MAZ", Description = "Mazowiecki Fundusz Zdrowia" },
                new NFZUnit() { Code = "OPO", Description = "Opolski Fundusz Zdrowia" },
                new NFZUnit() { Code = "PDK", Description = "Podkarpacki Fundusz Zdrowia" },
                new NFZUnit() { Code = "PDL", Description = "Podlaski Fundusz Zdrowia" },
                new NFZUnit() { Code = "POM", Description = "Pomorski Fundusz Zdrowia" },
                new NFZUnit() { Code = "ŚLĄ", Description = "Śląski Fundusz Zdrowia" },
                new NFZUnit() { Code = "ŚWI", Description = "Świętokrzyski Fundusz Zdrowia" },
                new NFZUnit() { Code = "WAM", Description = "Warmińsko-Mazurski Fundusz Zdrowia" },
                new NFZUnit() { Code = "WLP", Description = "Wielkopolski Fundusz Zdrowia" },
                new NFZUnit() { Code = "ZAP", Description = "Zachodniopomorski Fundusz Zdrowia" }
            };
            return units;
        }

        public Patient GetPatientData()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<MedicalService> GetMedicalServices()
        {
            List<MedicalService> services = new List<MedicalService>()
            {

                new MedicalService(){Id=1,Name="Konsultacja proktologiczna",Description="Konsultacja proktologiczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=1,Name="Konsultacja internistyczna",Description="Konsultacja internistyczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=1,Name="Konsultacja internistyczna",Description="Konsultacja internistyczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=1,Name="Konsultacja internistyczna",Description="Konsultacja internistyczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=1,Name="Konsultacja internistyczna",Description="Konsultacja internistyczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=1,Name="Konsultacja internistyczna",Description="Konsultacja internistyczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=1,Name="Konsultacja internistyczna",Description="Konsultacja internistyczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=1,Name="Konsultacja internistyczna",Description="Konsultacja internistyczna", StandardPrice=200, IsPrimaryService=true},



                new MedicalService(){Id=1,Name="Konsultacja internistyczna",Description="Konsultacja internistyczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=1,Name="Konsultacja pediatryczna",Description="Konsultacja pediatryczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=1,Name="Konsultacja geriatryczna",Description="Konsultacja geriatryczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=1,Name="Konsultacja ginekologiczna",Description="Konsultacja ginekologiczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=1,Name="Konsultacja ortopedyczna",Description="Konsultacja ortopedyczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=1,Name="Konsultacja kardiologiczna",Description="Konsultacja kardiologiczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=1,Name="Konsultacja okulistyczna",Description="Konsultacja okulistyczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=1,Name="Konsultacja dermatologiczna",Description="Konsultacja dermatologiczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=1,Name="Konsultacja endokrynologiczna",Description="Konsultacja endokrynologiczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=1,Name="Konsultacja chirurgii ogólnej",Description="Konsultacja chirurgii ogólnej", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=1,Name="Konsultacja neurochirurgiczna",Description="Konsultacja neurochirurgiczna", StandardPrice=250, IsPrimaryService=true},
                new MedicalService(){Id=1,Name="Konsultacja chirurgii naczyniowej",Description="Konsultacja chirurgii naczyniowej", StandardPrice=250, IsPrimaryService=true},
                new MedicalService(){Id=1,Name="Konsultacja chirurgii plastycznej",Description="Konsultacja chirurgii plastycznej", StandardPrice=300, IsPrimaryService=true},
                new MedicalService(){Id=1,Name="Konsultacja chirurgii onkologicznej",Description="chirurgii onkologicznej", StandardPrice=300, IsPrimaryService=true},
                new MedicalService(){Id=1,Name="Konsultacja laryngologiczna",Description="Konsultacja laryngologiczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=1,Name="Konsultacja neurologiczna",Description="Konsultacja neurologiczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=1,Name="Konsultacja urologiczna",Description="Konsultacja urologiczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=1,Name="Konsultacja psychologiczna",Description="Konsultacja psychologiczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=1,Name="Konsultacja stomatologiczna",Description="Konsultacja stomatologiczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=1,Name="Fizjoterapia",Description="Fizjoterapia", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=1,Name="Higiena stomatologiczna",Description="Higiena stomatologiczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=1,Name="Medycyna pracy",Description="Medycyna pracy", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=1,Name="Badanie laboratoryjne",Description="Badanie laboratoryjne", StandardPrice=100, IsPrimaryService=true},
                new MedicalService(){Id=1,Name="Diagnostyka obrazowa",Description="Diagnostyka obrazowa", StandardPrice=150, IsPrimaryService=true},


            };
            return services;
        }

        public IEnumerable<VisitCategory> GetVisitCategories()
        {
            List<VisitCategory> categories = new()
            {
                new VisitCategory() { Id = 1, CategoryName = "Konsultacje" }
            };
            return categories;
        }

    }
}