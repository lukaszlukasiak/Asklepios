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
        private List<MedicalService> medicalServices { get; set; }
        private List<MedicalService> primaryMedicalServices { get; set; }
        private List<VisitCategory> visitCategories { get; set; }

        public PatientInMemoryContext()
        {
            medicalServices = GetMedicalServices().ToList();
            primaryMedicalServices = medicalServices.Where(c => c.IsPrimaryService == true).ToList();
            visitCategories = GetVisitCategories().ToList();
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
                new MedicalService(){Id=0,Name="Konsultacja gastrologiczna	",Description="Konsultacja gastrologiczna", StandardPrice=250, IsPrimaryService=true},

                new MedicalService(){Id=1,Name="USG",Description="USG", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=2,Name="RTG",Description="RTG", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=3,Name="Rezonans magnetyczny",Description="Rezonans magnetyczny", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=4,Name="EKG spoczynkowe",Description="EKG spoczynkowe", StandardPrice=200, IsPrimaryService=false},
                new MedicalService(){Id=5,Name="Komputerowe pole widzenia",Description="Komputerowe pole widzenia", StandardPrice=200, IsPrimaryService=false},
                new MedicalService(){Id=6,Name="Kolonoskopia",Description="Kolonoskopia", StandardPrice=200, IsPrimaryService=false},
                new MedicalService(){Id=7,Name="Audiometria",Description="Audiometria", StandardPrice=200, IsPrimaryService=false},
                new MedicalService(){Id=8,Name="Gastroskopia",Description="Gastroskopia", StandardPrice=200, IsPrimaryService=false},
                new MedicalService(){Id=9,Name="Założenie gipsu",Description="Założenie gipsu", StandardPrice=200, IsPrimaryService=false},
                new MedicalService(){Id=10,Name="Usunięcie paznokcia",Description="Usunięcie paznokcia", StandardPrice=100, IsPrimaryService=false},


                new MedicalService(){Id=11,Name="Piaskowanie",Description="Piaskowanie", StandardPrice=100, IsPrimaryService=false},
                new MedicalService(){Id=12,Name="Fluoryzacja",Description="Fluoryzacja", StandardPrice=100, IsPrimaryService=false},
                new MedicalService(){Id=13,Name="Usunięcie ósemki",Description="Usunięcie ósemki", StandardPrice=100, IsPrimaryService=false},
                new MedicalService(){Id=14,Name="Usunięcie zęba jednokorzeniowego",Description="Usunięcie zęba jednokorzeniowego", StandardPrice=100, IsPrimaryService=false},
                new MedicalService(){Id=15,Name="Usunięcie zęba jednokorzeniowego wielokorzeniowego",Description="Usunięcie zęba jednokorzeniowego wielokorzeniowego", StandardPrice=100, IsPrimaryService=false},
                new MedicalService(){Id=16,Name="Usunięcie zęba mlecznego",Description="Usunięcie zęba mlecznego", StandardPrice=100, IsPrimaryService=false},
                new MedicalService(){Id=17,Name="Pantomogram zęba",Description="Pantomogram zęba", StandardPrice=100, IsPrimaryService=false},
                new MedicalService(){Id=18,Name="Tomografia komputerowa CBCT",Description="Tomografia komputerowa CBCT", StandardPrice=100, IsPrimaryService=false},
                new MedicalService(){Id=19,Name="Znieczulenie",Description="Znieczulenie", StandardPrice=50, IsPrimaryService=false},
                new MedicalService(){Id=20,Name="Wypełnienie czasowe",Description="Wypełnienie czasowe", StandardPrice=50, IsPrimaryService=false},
                new MedicalService(){Id=21,Name="Wypełnienie kompozytowe",Description="Wypełnienie kompozytowe", StandardPrice=200, IsPrimaryService=false},
                new MedicalService(){Id=22,Name="Odbudowa zęba po leczeniu kanałowym",Description="Odbudowa zęba po leczeniu kanałowym", StandardPrice=400, IsPrimaryService=false},
                new MedicalService(){Id=23,Name="Dewitalizacja",Description="Dewitalizacja", StandardPrice=100, IsPrimaryService=false},
                new MedicalService(){Id=63,Name="Korona porcelanowa",Description="Korona porcelanowa", StandardPrice=800, IsPrimaryService=false},
                new MedicalService(){Id=64,Name="Licówka porcelanowa",Description="Licówka porcelanowa", StandardPrice=1600, IsPrimaryService=false},
                new MedicalService(){Id=65,Name="Korona pełnoceramiczna",Description="Korona pełnoceramiczna", StandardPrice=1600, IsPrimaryService=false},


                new MedicalService(){Id=24,Name="Podstawowe badanie krwi",Description="Podstawowe badanie krwi", StandardPrice=400, IsPrimaryService=false},
                new MedicalService(){Id=25,Name="Rozszerzone badanie krwi",Description="Rozszerzone zęba po leczeniu kanałowym", StandardPrice=400, IsPrimaryService=false},
                new MedicalService(){Id=26,Name="Badanie moczu",Description="Badanie moczu", StandardPrice=400, IsPrimaryService=false},
                new MedicalService(){Id=27,Name="Badanie kału",Description="Badanie kału", StandardPrice=400, IsPrimaryService=false},
                new MedicalService(){Id=28,Name="Test genetyczny COVID-19",Description="Test genetyczny COVID-19", StandardPrice=400, IsPrimaryService=false},
                new MedicalService(){Id=29,Name="Test antygenowy COVID-19",Description="Test antygenowy COVID-19", StandardPrice=400, IsPrimaryService=false},

                new MedicalService(){Id=30,Name="Masaż leczniczy",Description="Masaż leczniczy", StandardPrice=300, IsPrimaryService=true},

                new MedicalService(){Id=31,Name="Zajęcia rehablitacyjne",Description="Zajęcia rehablitacyjne", StandardPrice=300, IsPrimaryService=true},

                new MedicalService(){Id=32,Name="Krioterapia",Description="Krioterapia", StandardPrice=100, IsPrimaryService=false},
                new MedicalService(){Id=33,Name="Elektrostymulacja",Description="Elektrostymulacja", StandardPrice=100, IsPrimaryService=false},
                new MedicalService(){Id=34,Name="Krioterapia",Description="Krioterapia", StandardPrice=200, IsPrimaryService=false},
                new MedicalService(){Id=35,Name="Ultradźwięki",Description="Ultradźwięki", StandardPrice=100, IsPrimaryService=false},
                new MedicalService(){Id=36,Name="Magnetoterapia",Description="Magnetoterapia", StandardPrice=100, IsPrimaryService=false},

                new MedicalService(){Id=37,Name="Płukanie ucha",Description="Płukanie ucha", StandardPrice=50, IsPrimaryService=false},


                new MedicalService(){Id=38,Name="Konsultacja proktologiczna",Description="Konsultacja proktologiczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=39,Name="Konsultacja internistyczna",Description="Konsultacja internistyczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=40,Name="Konsultacja pediatryczna",Description="Konsultacja pediatryczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=41,Name="Konsultacja geriatryczna",Description="Konsultacja geriatryczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=42,Name="Konsultacja ginekologiczna",Description="Konsultacja ginekologiczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=43,Name="Konsultacja ortopedyczna",Description="Konsultacja ortopedyczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=44,Name="Konsultacja kardiologiczna",Description="Konsultacja kardiologiczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=45,Name="Konsultacja okulistyczna",Description="Konsultacja okulistyczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=46,Name="Konsultacja dermatologiczna",Description="Konsultacja dermatologiczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=47,Name="Konsultacja endokrynologiczna",Description="Konsultacja endokrynologiczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=48,Name="Konsultacja chirurgii ogólnej",Description="Konsultacja chirurgii ogólnej", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=49,Name="Konsultacja neurochirurgiczna",Description="Konsultacja neurochirurgiczna", StandardPrice=250, IsPrimaryService=true},
                new MedicalService(){Id=50,Name="Konsultacja chirurgii naczyniowej",Description="Konsultacja chirurgii naczyniowej", StandardPrice=250, IsPrimaryService=true},
                new MedicalService(){Id=51,Name="Konsultacja chirurgii plastycznej",Description="Konsultacja chirurgii plastycznej", StandardPrice=300, IsPrimaryService=true},
                new MedicalService(){Id=52,Name="Konsultacja chirurgii onkologicznej",Description="chirurgii onkologicznej", StandardPrice=300, IsPrimaryService=true},
                new MedicalService(){Id=53,Name="Konsultacja laryngologiczna",Description="Konsultacja laryngologiczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=54,Name="Konsultacja neurologiczna",Description="Konsultacja neurologiczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=55,Name="Konsultacja urologiczna",Description="Konsultacja urologiczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=56,Name="Konsultacja psychologiczna",Description="Konsultacja psychologiczna", StandardPrice=200, IsPrimaryService=true},


                new MedicalService(){Id=57,Name="Stomatologia zachowawcza",Description="Stomatologia zachowawcza", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=58,Name="Ortodoncja",Description="Ortodoncja", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=59,Name="Chirurgia stomatologiczna",Description="Chirurgia stomatologiczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=60,Name="Rentgen stomatologiczny",Description="Rentgen stomatologiczny", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=61,Name="Protetyka",Description="Protetyka", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=62,Name="Profilaktyka stomatologiczna",Description="Profilaktyka stomatologiczna", StandardPrice=200, IsPrimaryService=true,SubServices=new List<MedicalService>(medicalServices.GetRange(11, 2))},



                new MedicalService(){Id=67,Name="Szczepienie na grypę",Description="Szczepienie na grypę", StandardPrice=100, IsPrimaryService=false},
                new MedicalService(){Id=68,Name="Szczepienie na COVID-19",Description="Szczepienie na COVID-19", StandardPrice=200, IsPrimaryService=false},
                new MedicalService(){Id=69,Name="Szczepienie przeciwko wściekliźnie",Description="Szczepienie przeciwko wściekliźnie", StandardPrice=200, IsPrimaryService=false},
                new MedicalService(){Id=70,Name="Szczepienie przeciwko tężcowi",Description="Szczepienie przeciwko tężcowi", StandardPrice=100, IsPrimaryService=false},
                new MedicalService(){Id=71,Name="Szczepienie przeciwko malarii",Description="Szczepienie przeciwko malarii", StandardPrice=500, IsPrimaryService=false},
                new MedicalService(){Id=72,Name="Szczepienie przeciwko cholerze",Description="Szczepienie przeciwko cholerze", StandardPrice=100, IsPrimaryService=false},

                new MedicalService(){Id=73,Name="Medycyna pracy",Description="Medycyna pracy", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=74,Name="Badanie laboratoryjne",Description="Badanie laboratoryjne", StandardPrice=100, IsPrimaryService=true},
                new MedicalService(){Id=75,Name="Fizykoterapia",Description="Fizykoterapia", StandardPrice=400, IsPrimaryService=true} ,

                new MedicalService(){Id=76,Name="Szczepienia",Description="Szczepienia", StandardPrice=100, IsPrimaryService=true},

                new MedicalService(){Id=77,Name="Aparat stały kryształowy",Description="Aparat stały kryształowy", StandardPrice=2500, IsPrimaryService=false},
                new MedicalService(){Id=78,Name="Aparat stały metalowy",Description="Aparat stały metalowy", StandardPrice=100, IsPrimaryService=false},
                new MedicalService(){Id=79,Name="Aparat ruchomy - płytka Schwarza",Description="Aparat ruchomy - płytka Schwarza", StandardPrice=100, IsPrimaryService=false},

                new MedicalService(){Id=80,Name="Topografia rogówki",Description="Topografia rogówki", StandardPrice=100, IsPrimaryService=false},
                new MedicalService(){Id=81,Name="Dobór soczewek kontaktowych",Description="Dobór soczewek kontaktowych", StandardPrice=150, IsPrimaryService=false},
                new MedicalService(){Id=82,Name="Zdjęcie dna oka",Description="Zdjęcie dna oka", StandardPrice=50, IsPrimaryService=false},
                new MedicalService(){Id=83,Name="Pachymetria",Description="Pachymetria", StandardPrice=100, IsPrimaryService=false},
                new MedicalService(){Id=84,Name="Pomiar ciśnienia wewnątrzgałkowego	",Description="Pomiar ciśnienia wewnątrzgałkowego", StandardPrice=50, IsPrimaryService=false},
                new MedicalService() { Id = 85, Name = "Zdjęcie gipsu", Description = "Zdjęcie gipsu", StandardPrice = 100, IsPrimaryService = false },
                new MedicalService() { Id = 86, Name = "Szycie rany", Description = "Szycie rany", StandardPrice = 100, IsPrimaryService = false },
                new MedicalService() { Id = 87, Name = "Założenie szwów", Description = "Założenie szwów", StandardPrice = 100, IsPrimaryService = false },
                new MedicalService() { Id = 89, Name = "Zdjęcie szwów", Description = "Zdjęcie szwów", StandardPrice = 100, IsPrimaryService = false },
                new MedicalService() { Id = 89, Name = "Zabieg usunięcia ciała obcego", Description = "Zabieg usunięcia ciała obcego", StandardPrice = 600, IsPrimaryService = false },


            };

                //chirurgia
            medicalServices[48].SubServices = new List<MedicalService>(medicalServices.GetRange(86, 5));
            medicalServices[48].SubServices.Append(medicalServices[10]);
            

            //ortopeda
            medicalServices[43].SubServices = new List<MedicalService>();
            medicalServices[43].SubServices.Append(medicalServices[85]);
            medicalServices[43].SubServices.Append(medicalServices[9]);

            //gastrologia
            medicalServices[0].SubServices = new List<MedicalService>() { medicalServices[6], medicalServices[8] };

                //okulista
            medicalServices[45].SubServices = new List<MedicalService>(medicalServices.GetRange(80, 5));
            medicalServices[45].SubServices.Append(medicalServices[5]);


            //laryngologia
            medicalServices[53].SubServices = new List<MedicalService>();
            medicalServices[53].SubServices.Append(medicalServices[37]);
            medicalServices[53].SubServices.Append(medicalServices[7]);


            //stomatologia
            medicalServices[62].SubServices = new List<MedicalService>(medicalServices.GetRange(11, 2));
            medicalServices[58].SubServices = new List<MedicalService>(medicalServices.GetRange(77, 3));
            medicalServices[59].SubServices = new List<MedicalService>(medicalServices.GetRange(13, 4));
            medicalServices[60].SubServices = new List<MedicalService>(medicalServices.GetRange(17, 2));
            medicalServices[61].SubServices = new List<MedicalService>(medicalServices.GetRange(63, 3));
            medicalServices[57].SubServices = new List<MedicalService>(medicalServices.GetRange(19, 5));

            //badania laboratoryjne oraz szczepienia
            medicalServices[74].SubServices = new List<MedicalService>(medicalServices.GetRange(24, 6));
            medicalServices[76].SubServices = new List<MedicalService>(medicalServices.GetRange(67, 6));

            //fizjoterapia
            medicalServices[75].SubServices = new List<MedicalService>(medicalServices.GetRange(32, 5));

            return services;
        }

        public IEnumerable<VisitCategory> GetVisitCategories()
        {
            List<VisitCategory> categories = new()
            {
                new VisitCategory() { Id = 1, CategoryName = "Konsultacje", PrimaryMedicalServices = new List<MedicalService>(medicalServices.GetRange(38, 23)) },
                new VisitCategory() { Id = 2, CategoryName = "E-konsultacje" ,PrimaryMedicalServices = new List<MedicalService>(medicalServices.GetRange(38, 23)) { } },
                new VisitCategory() { Id = 3, CategoryName = "Stomatologia", PrimaryMedicalServices = new List<MedicalService>(medicalServices.GetRange(57, 6)) { } },
                new VisitCategory() { Id = 4, CategoryName = "Diagnostyka obrazowa ", PrimaryMedicalServices = new List<MedicalService>(medicalServices.GetRange(1, 3)) { } },
                new VisitCategory() { Id = 5, CategoryName = "Fizjoterapia", PrimaryMedicalServices = new List<MedicalService>(medicalServices.GetRange(31, 6)) { } },
                new VisitCategory() { Id = 6, CategoryName = "Badania laboratoryjne/szczepienia", PrimaryMedicalServices = new List<MedicalService>(medicalServices.GetRange(24, 6)) { } },
            };
            categories[0].PrimaryMedicalServices.Add(medicalServices[0]);
            return categories;
        }

    }
}