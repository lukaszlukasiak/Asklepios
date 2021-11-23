﻿using Asklepios.Data.Interfaces;
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

        const string UM_1 = "Uniwersytet Medyczny w Białymstoku";
        const string UM_2 = "Gdański Uniwersytet Medyczny";
        const string UM_3 = "Śląski Uniwersytet Medyczny";
        const string UM_4 = "Uniwersytet Medyczny w Lublinie";
        const string UM_5 = "Uniwersytet Medyczny w Łodzi";
        const string UM_6 = "Uniwersytet Medyczny im.Karola Marcinkowskiego w Poznaniu";
        const string UM_7 = "Pomorski Uniwersytet Medyczny";
        const string UM_8 = "Warszawski Uniwersytet Medyczny";
        const string UM_9 = "Uniwersytet Medyczny im.Piastów Śląskich we Wrocławiu";

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
            locations = GetAllLocations();
            patient = GetPatientData();
            medicalWorkers = GetMedicalWorkers();
        }

        public IEnumerable<Visit> GetAvailableVisits()
        {
            DateTime today = DateTime.Today;
            //return new List<Visit>()
            //{ new Visit() {Id=1,DateTimeSince=today+new TimeSpan(9,0,0),DateTimeTill=today+new TimeSpan(9,15,0),Location=locations.ElementAt(0)}};
            return new List<Visit>();
        }

        public IEnumerable<Visit> GetHistoricalVisits()
        {
            return new List<Visit>();
        }

        public IEnumerable<Location> GetAllLocations()
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
            return new List<MedicalPackage>();
        }


        public IEnumerable<MedicalWorker> GetMedicalWorkers()
        {

            //List<Person> people = new List<Person>()
            //{
            //    new Person()
            //    {
            //        EmailAddress="person1@gmail.com",
            //        HasPolishCitizenship=true,
            //        Name="Mariusz",
            //        Surname="Puto",
            //        PESEL=77784512598
            //    };
            //}
            //

            List<VisitRating> visitRatings1 = new List<VisitRating>()
            {
                new VisitRating()
                {
                    AtmosphereRate=1,
                    CompetenceRate=4,
                    GeneralRate=3,
                    Id=1,
                    ShortDescription="Lekarz w miarę kompetentny, ale chamski gbur"
                },
                new VisitRating()
                {
                    AtmosphereRate=5,
                    CompetenceRate=2,
                    GeneralRate=3,
                    Id=2,
                    ShortDescription="Miły lekarz, niestety jego zalecenia nic nie pomogły"
                }
            };
            List<VisitRating> visitRatings2 = new List<VisitRating>()
            {
                new VisitRating()
                {
                    AtmosphereRate=4,
                    CompetenceRate=4,
                    GeneralRate=4,
                    Id=4,
                    ShortDescription="Przepisane przez niego medykamenty poprawiły mój stan, ale część objawów się utrzymała."
                },
                new VisitRating()
                {
                    AtmosphereRate=5,
                    CompetenceRate=5,
                    GeneralRate=5,
                    Id=3,
                    ShortDescription="Super lekarz, pomógł mi, dodatkowo jest bardzo sympatyczny i wszystko mi po kolei wyjaśnił. Lekarz-ideał."
                }
            };
            List<VisitRating> visitRatings3 = new List<VisitRating>()
            {
                new VisitRating()
                {
                    AtmosphereRate=2,
                    CompetenceRate=1,
                    GeneralRate=1,
                    Id=3,
                    ShortDescription="Lekarza nie interesowały wyniki badań, nie interesowało co mówię, jedyne co mi zalecił, to leki przeciwbólowe!."
                },
                new VisitRating()
                {
                    AtmosphereRate=1,
                    CompetenceRate=2,
                    GeneralRate=2,
                    Id=6,
                    ShortDescription="Bardzo nieprzyjemny, jego leczenie nie przyniosło większej poprawy"
                }
            };


            List<MedicalWorker> medicalWorkers = new List<MedicalWorker>()
            {
                new Doctor( name:"Mariusz",surName:"Puto",id:1, pesel:"77784512598",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL",email:"person1@gmail.com", aglomeration:Core.Enums.Aglomeration.Bialystok)
                {
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/MW/m/1.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[0],primaryMedicalServices[1]
                    }
                },

                new Doctor(name:"Witold",surName:"Głąbek",id:2,pesel:"156456465465",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person2@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_3,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu praskim",
                    ImagePath="/img/MW/m/2.jpg",
                    HiredSince=new DateTime(2017,1,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings2,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Henryk",surName:"Bąbel",id:3,pesel:"879794561231323",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person3@gmail.com", aglomeration:Core.Enums.Aglomeration.Kielce)
                {
                    Education=new List<string>() {UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu MSWiA",
                    ImagePath="/img/MW/m/3.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings3,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Ferdynand",surName:"Małolepszy",id:4,pesel:"56754334534543",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person4@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu UMK",
                    ImagePath="/img/MW/m/4.jpg",
                    HiredSince=new DateTime(2020,4,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Zenon",surName:"Krzywy",id:5,pesel:"54346546454543",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person5@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/MW/m/5.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Tadeusz",surName:"Nowak",id:6,pesel:"6548797654654654",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person6@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/MW/m/6.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings2,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Tomasz",surName:"Woda",id:7,pesel:"78945646312313",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person7@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu wrocławskim",
                    ImagePath="/img/MW/m/7.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings3,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Łukasz",surName:"Czekaj",id:8,pesel:"756546546466",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person8@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu podlaskim",
                    ImagePath="/img/MW/m/8.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings2,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Mateusz",surName:"Chodzień",id:9,pesel:"841313216546",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person9@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/MW/m/9.jpg",
                    HiredSince=new DateTime(2012,1,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Leszek",surName:"Ancymon",id:10,pesel:"44445465456465",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person10@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu suwalskim",
                    ImagePath="/img/MW/m/10.jpg",
                    HiredSince=new DateTime(2018,1,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Karol",surName:"Szczęsny",id:11,pesel:"7532123165465",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person11@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_9},
                    Experience="W latach 2008-2019 praca w szpitalu podkarpackim",
                    ImagePath="/img/MW/m/11.jpg",
                    HiredSince=new DateTime(2017,5,5),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Remigiusz",surName:"Czystka",id:12,pesel:"654213215649546",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person12@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_8},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/MW/m/12.jpg",
                    HiredSince=new DateTime(2017,1,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Robert",surName:"Pawłowski",id:13,pesel:"798879875456132",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person13@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_7},
                    Experience="W latach 2005-2020 praca w szpitalu wojskowym",
                    ImagePath="/img/MW/m/13.jpg",
                    HiredSince=new DateTime(2012,12,12),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Szymon",surName:"Sosna",id:14,pesel:"71123156456456",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person14@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_6},
                    Experience="W latach 2010-2019 praca w szpitalu matki i dziecka",
                    ImagePath="/img/MW/m/14.jpg",
                    HiredSince=new DateTime(2019,4,4),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Sergiusz",surName:"Ząbek",id:15,pesel:"6523154645633",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person15@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_5},
                    Experience="W latach 2011-2021 praca w szpitalu zakaźnym",
                    ImagePath="/img/MW/m/15.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Tymoteusz",surName:"Zez",id:16,pesel:"64561231564546",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person16@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2007-2021 praca w szpitalu kujawskim",
                    ImagePath="/img/MW/m/16.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Zbigniew",surName:"Korzeń",id:17,pesel:"45632132456486",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person17@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_4},
                    Experience="W latach 2005-2020 praca w szpitalu łódzkim",
                    ImagePath="/img/MW/m/17.jpg",
                    HiredSince=new DateTime(2013,3,3),
                    IsCurrentlyHired=true,VisitRatings=visitRatings3,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Zbigniew",surName:"Osiński",id:18,pesel:"49987945646133",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person18@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/MW/m/2.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings2,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Michał",surName:"Czosnek",id:19,pesel:"654321546563331",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person19@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_3},
                    Experience="W latach 2009-2020 praca w POZ Węgrów.",
                    ImagePath="/img/MW/m/19.jpg",
                    HiredSince=new DateTime(2018,7,6),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Tomasz",surName:"Truteń",id:20,pesel:"8012131654613",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person20@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_1},
                    Experience="W latach 2005-2020 praca w szpitalu miejskim w Krośnie",
                    ImagePath="/img/MW/m/20.jpg",
                    HiredSince=new DateTime(2020,2,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Bogusław",surName:"Śmiały",id:21,pesel:"5546545641231234",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person21@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu tarnowskim",
                    ImagePath="/img/MW/m/21.jpg",
                    HiredSince=new DateTime(2017,1,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Jan",surName:"Dutki",id:22,pesel:"54654321314564",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person22@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu miejskim w Zakopanem",
                    ImagePath="/img/MW/m/22.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings2,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Jarosław",surName:"Kurczak",id:23,pesel:"65461234564546",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person23@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_7},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/MW/m/23.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Grzegorz",surName:"Grześkowiak",id:24,pesel:"6548745646546",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person24@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_5},
                    Experience="W latach 2008-2014 praca w szpitalu kardiologicznym",
                    ImagePath="/img/MW/m/2.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Gerwazy",surName:"Zasada",id:25,pesel:"4561231564654",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person25@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_5},
                    Experience="W latach 2005-2020 praca w szpitalu w Dębicy",
                    ImagePath="/img/MW/m/25.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Czesław",surName:"Wilk",id:26,pesel:"5487897564646",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person26@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_4},
                    Experience="W latach 2005-2020 praca w szpitalu powiatowym w Zamościu",
                    ImagePath="/img/MW/m/26.jpg",
                    HiredSince=new DateTime(2019,1,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Tadeusz",surName:"Gąska",id:27,pesel:"64621321564564",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person27@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu zakaźnym na Woli",
                    ImagePath="/img/MW/m/27.jpg",
                    HiredSince=new DateTime(2011,10,11),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Waldemar",surName:"Kucaj",id:28,pesel:"5945612315645",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person28@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_6},
                    Experience="W latach 2006-2019 praca w szpitalu świętokrzyskim",
                    ImagePath="/img/MW/m/28.jpg",
                    HiredSince=new DateTime(2020,8,8),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Piotr",surName:"Kuropatwa",id:29,pesel:"789465132132",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person29@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_8},
                    Experience="W latach 2005-2020 praca w szpitalu akademickim w Białymstoku",
                    ImagePath="/img/MW/m/29.jpg",
                    HiredSince=new DateTime(2018,1,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Paweł",surName:"Łąkietka",id:30,pesel:"7894654654965",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person30@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_6},
                    Experience="W latach 2005-2020 praca w szpitalu miejskim w Słupsku",
                    ImagePath="/img/MW/m/30.jpg",
                    HiredSince=new DateTime(2016,4,4),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Rozmus",surName:"Remus",id:31,pesel:"4564134156465",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person31@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_3},
                    Experience="W latach 2005-2012 praca w szpitalu klinicznym w Gnieźnie. Wcześniej pracował w Zielonej górze.",
                    ImagePath="/img/MW/m/31.jpg",
                    HiredSince=new DateTime(2011,1,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Miłosz",surName:"Ciapek",id:32,pesel:"487945643213",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person32@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_4},
                    Experience="W latach 2005-2020 praca w szpitalu akademickim w Krakowie",
                    ImagePath="/img/MW/m/32.jpg",
                    HiredSince=new DateTime(2019,8,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Czesława",surName:"Kret",id:33,pesel:"6546123156464",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person33@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_6},
                    Experience="W latach 2009-2019 praca w szpitalu w Węgrowie",
                    ImagePath="/img/MW/k/1.jpg",
                    HiredSince=new DateTime(2015,5,5),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Marlena",surName:"Bajka",id:34,pesel:"894561132156",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person34@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2015-2021 praca w szpitalu uniwersyteckim w Poznaniu",
                    ImagePath="/img/MW/k/2.jpg",
                    HiredSince=new DateTime(2015,10,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Bożena",surName:"Arbuz",id:35,pesel:"5456463216546",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person35@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_3},
                    Experience="W latach 2011-2021 praca w szpitalu miejskim w Łowiczu",
                    ImagePath="/img/MW/k/3.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Klaudia",surName:"Kąkol",id:36,pesel:"8015646546546",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person36@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2008-2020 praca w szpitalu zakaźnym w Krakowie",
                    ImagePath="/img/mw/k/4.jpg",
                    HiredSince=new DateTime(2018,8,11),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Sandra",surName:"Sosna",id:37,pesel:"864654564645",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person37@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_4},
                    Experience="W latach 2007-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/mw/k/5.jpg",
                    HiredSince=new DateTime(2017,7,7),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Teodora",surName:"Wiśniowiecka",id:38,pesel:"515648946513245",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person38@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/mw/k/6.jpg",
                    HiredSince=new DateTime(2017,4,4),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Kornelia",surName:"Krasicka",id:39,pesel:"664545646546546",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person39@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2012-2020 praca w szpitalu południowym w Warszawie",
                    ImagePath="/img/mw/k/7.jpg",
                    HiredSince=new DateTime(2015,1,11),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Marzena",surName:"Rudnicka",id:40,pesel:"7516454654645",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person40@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu chorób serca w Gdańsku",
                    ImagePath="/img/mw/k/8.jpg",
                    HiredSince=new DateTime(2018,8,8),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Beata",surName:"Bomba",id:41,pesel:"61231546546546",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person41@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_6},
                    Experience="W latach 2007-2018 praca w szpitalu praskim w Warszawie",
                    ImagePath="/img/mw/k/9.jpg",
                    HiredSince=new DateTime(2021,11,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Katarzyna",surName:"Łasinkiewicz",id:42,pesel:"7112345647656",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person42@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_8},
                    Experience="W latach 2009-2019 praca w szpitalu praskim w Warszawie",
                    ImagePath="/img/mw/k/10.jpg",
                    HiredSince=new DateTime(2012,11,11),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                    },
                new Doctor(name:"Weronika",surName:"Kurzydło",id:43,pesel:"8154654654656",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person43@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_5},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/mw/k/11.jpg",
                    HiredSince=new DateTime(2017,7,9),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Maria",surName:"Kurka",id:44,pesel:"7879465461654",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person44@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2012-2019 praca w szpitalu MSWIA w Warszawie",
                    ImagePath="/img/mw/k/12.jpg",
                    HiredSince=new DateTime(2019,4,8),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Bronisława",surName:"Czesiek",id:45,pesel:"49489646146546",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person45@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_7},
                    Experience="W latach 2005-2020 praca w szpitalu centralnym w Krakowie",
                    ImagePath="/img/mw/k/13.jpg",
                    HiredSince=new DateTime(2016,6,6),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Aleksandra",surName:"Ruda",id:46,pesel:"65487987446",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person46@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2019-2021 praca w szpitalu u Koziołka Matołka w Poznaniu",
                    ImagePath="/img/mw/k/14.jpg",
                    HiredSince=new DateTime(2015,7,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Iga",surName:"Bodzio",id:47,pesel:"7848465465454",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person47@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu klinicznym we Wrocławiu",
                    ImagePath="/img/mw/k/15.jpg",
                    HiredSince=new DateTime(2017,2,11),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Agnieszka",surName:"Pluto",id:48,pesel:"84879486546548",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person48@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2018-2021 praca w szpitalu klinicznym we Wrocławiu",
                    ImagePath="/img/mw/k/16.jpg",
                    HiredSince=new DateTime(2021,2,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Karolina",surName:"Majak",id:49,pesel:"856415413216",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person49@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_8},
                    Experience="W latach 2019-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/mw/k/17.jpg",
                    HiredSince=new DateTime(2021,1,9),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Karina",surName:"Wąsacz",id:50,pesel:"894564113244",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person50@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_4},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/mw/k/18.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Grażyna",surName:"Rudniewska",id:51,pesel:"5641321564964",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person51@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_5,UM_7},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/mw/k/19.jpg",
                    HiredSince=new DateTime(2019,4,4),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Marta",surName:"Tracka",id:52,pesel:"846516549646411",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person52@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_7,UM_9},
                    Experience="Staż odbyła w szpitalu Bródnowskim w Warszawie. Od 2016 roku pracuje w szpitalu Praskim w Warszawie.",
                    ImagePath="/img/mw/k/20.jpg",
                    HiredSince=new DateTime(2018,9,11),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Marta",surName:"Trąbicka",id:53,pesel:"862311654482631",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person53@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_6,UM_2},
                    Experience="Staż odbyty w szpitalu akademickim w Białymstoku. Od 2018 roku praca w szpitalu powiatowym w Węgrowie",
                    ImagePath="/img/mw/k/21.jpg",
                    HiredSince=new DateTime(2018,8,8),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Sylwia",surName:"Sarna",id:54,pesel:"7913213156465",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person54@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_4,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/mw/k/22.jpg",
                    HiredSince=new DateTime(2018,4,6),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Kamila",surName:"Kozera",id:55,pesel:"751231654654612",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person55@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_5},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/mw/k/23.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Bogumiła",surName:"Braniewsk",id:56,pesel:"548789461231546",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person56@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/mw/k/24.jpg",
                    HiredSince=new DateTime(2019,1,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Teresa",surName:"Winniczek",id:57,pesel:"62348979521",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person57@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_3},
                    Experience="W latach 2014-2021 praca w szpitalu zielonogórskim",
                    ImagePath="/img/mw/k/25.jpg",
                    HiredSince=new DateTime(2013,3,3),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Danuta",surName:"Werys",id:58,pesel:"61321234189",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person58@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_5},
                    Experience="W latach 2005-2020 praca w szpitalu wojewódzkim w Olsztynie",
                    ImagePath="/img/mw/k/26.jpg",
                    HiredSince=new DateTime(2018,4,3),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                    new Doctor(name:"Daria",surName:"Jaszczur",id:59,pesel:"74561213898",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person59@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_8},
                    Experience="Od 2010 roku pracuje jako ordynator w szpitalu Matki i Dziecka w Warszawie",
                    ImagePath="/img/mw/k/27.jpg",
                    HiredSince=new DateTime(2018,6,7),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                        MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                    new Doctor(name:"Daria",surName:"Biernacka",id:60,pesel:"791231564948213",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person60@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_9},
                    Experience="W latach 2016-2020 praca w szpitalu miejskim w Grudziądzu",
                    ImagePath="/img/mw/k/28.jpg",
                    HiredSince=new DateTime(2019,8,11),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                        MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Maria",surName:"Balon",id:61,pesel:"785321546456",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person61@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_1,UM_9},
                    Experience="W latach 2009-2020 praca w szpitalu miejskim w Suwałkach",
                    ImagePath="/img/mw/k/29.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Anna",surName:"Poranna",id:62,pesel:"84561321499476",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person62@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_7,UM_2},
                    Experience="W latach 2009-2020 praca w szpitalu wojewódzkim w Toruniu",
                    ImagePath="/img/mw/k/30.jpg",
                    HiredSince=new DateTime(2019,5,4),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Anna",surName:"Poletko",id:63,pesel:"8845641321546",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person63@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow)
                {
                    Education=new List<string>() {UM_2,UM_4},
                    Experience="Od 2016 pracuje w szpitalu Bródnowskim",
                    ImagePath="/img/mw/k/31.jpg",
                    HiredSince=new DateTime(2015,5,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },

                new Doctor(name:"Agata",surName:"Bosko",id:64,pesel:"8956132156463",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person64@gmail.com", aglomeration:Core.Enums.Aglomeration.Warsaw)
                {
                    Education=new List<string>() {UM_5},
                    Experience="W latach 2009-2021 praca w szpitalu w Przemyślu",
                    ImagePath="/img/mw/k/32.jpg",
                    HiredSince=new DateTime(2019,9,8),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Agata",surName:"Mińska",id:65,pesel:"78465413131468",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person65@gmail.com", aglomeration:Core.Enums.Aglomeration.Wroclaw)
                {
                    Education=new List<string>() {UM_3},
                    Experience="W latach 2008-2020 praca w szpitalu w Lublinie",
                    ImagePath="/img/mw/k/33.jpg",
                    HiredSince=new DateTime(2019,4,7),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Monika",surName:"Szajka",id:66,pesel:"80156467513236",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person66@gmail.com", aglomeration:Core.Enums.Aglomeration.Bialystok)
                {
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/mw/k/34.jpg",
                    HiredSince=new DateTime(2015,9,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Mariola",surName:"Kiepska",id:67,pesel:"798564613216546",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person67@gmail.com", aglomeration:Core.Enums.Aglomeration.Kielce)
                {
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/mw/k/35.jpg",
                    HiredSince=new DateTime(2019,4,3),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Dorota",surName:"Zawisza",id:68,pesel:"7441321264987984",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person68@gmail.com", aglomeration:Core.Enums.Aglomeration.Silesia)
                {
                    Education=new List<string>() {UM_5,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/mw/k/36.jpg",
                    HiredSince=new DateTime(2018,8,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Anastasia",surName:"Radczuk",id:69,pesel:"",hasPolishCitizenship: false,passportNumber: "AAAA87946121646",passportCode:"UKR", email:"person69@gmail.com", aglomeration:Core.Enums.Aglomeration.Tricity)
                {
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2016-2020 praca w szpitalu lwowskim na Ukrainie",
                    ImagePath="/img/mw/k/37.jpg",
                    HiredSince=new DateTime(2020,8,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },
                new Doctor(name:"Karolina",surName:"Kulka",id:70,pesel:"798465132156486",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person70@gmail.com", aglomeration:Core.Enums.Aglomeration.Rzeszów)
                {
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/MW/k/38.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitRatings=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        primaryMedicalServices[2],primaryMedicalServices[1]
                    }

                },

            };

            return medicalWorkers;
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
            return new Patient("Łukasz", "Łukasiak", 1, "8710101010", true, "484654asd4a5sd4", "PL", "terfere@wp.pl", aglomeration: Core.Enums.Aglomeration.Warsaw);
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
                new MedicalService(){Id=52,Name="Konsultacja chirurgii onkologicznej",Description="Konsultacja chirurgii onkologicznej", StandardPrice=300, IsPrimaryService=true},
                new MedicalService(){Id=53,Name="Konsultacja laryngologiczna",Description="Konsultacja laryngologiczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=54,Name="Konsultacja neurologiczna",Description="Konsultacja neurologiczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=55,Name="Konsultacja urologiczna",Description="Konsultacja urologiczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=56,Name="Konsultacja psychologiczna",Description="Konsultacja psychologiczna", StandardPrice=200, IsPrimaryService=true},


                new MedicalService(){Id=57,Name="Stomatologia zachowawcza",Description="Stomatologia zachowawcza", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=58,Name="Ortodoncja",Description="Ortodoncja", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=59,Name="Chirurgia stomatologiczna",Description="Chirurgia stomatologiczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=60,Name="Rentgen stomatologiczny",Description="Rentgen stomatologiczny", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=61,Name="Protetyka",Description="Protetyka", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=62,Name="Profilaktyka stomatologiczna",Description="Profilaktyka stomatologiczna", StandardPrice=200, IsPrimaryService=true},


                new MedicalService(){Id=66,Name="Szczepienie na odrę",Description="Szczepienie na odrę", StandardPrice=100, IsPrimaryService=false},

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
                new MedicalService() { Id = 88, Name = "Zdjęcie szwów", Description = "Zdjęcie szwów", StandardPrice = 100, IsPrimaryService = false },
                new MedicalService() { Id = 89, Name = "Zabieg usunięcia ciała obcego", Description = "Zabieg usunięcia ciała obcego", StandardPrice = 600, IsPrimaryService = false },
                new MedicalService() { Id = 90, Name = "Biopsja otwarta", Description = "Biopsja otwarta", StandardPrice = 600, IsPrimaryService = false },


            };

            //chirurgia
            services[48].SubServices = new List<MedicalService>(services.GetRange(86, 5));
            services[48].SubServices.Append(services[10]);


            //ortopeda
            services[43].SubServices = new List<MedicalService>();
            services[43].SubServices.Append(services[85]);
            services[43].SubServices.Append(services[9]);

            //gastrologia
            services[0].SubServices = new List<MedicalService>() { services[6], services[8] };

            //okulista
            services[45].SubServices = new List<MedicalService>(services.GetRange(80, 5));
            services[45].SubServices.Append(services[5]);


            //laryngologia
            services[53].SubServices = new List<MedicalService>();
            services[53].SubServices.Append(services[37]);
            services[53].SubServices.Append(services[7]);


            //stomatologia
            services[62].SubServices = new List<MedicalService>(services.GetRange(11, 2));
            services[58].SubServices = new List<MedicalService>(services.GetRange(77, 3));
            services[59].SubServices = new List<MedicalService>(services.GetRange(13, 4));
            services[60].SubServices = new List<MedicalService>(services.GetRange(17, 2));
            services[61].SubServices = new List<MedicalService>(services.GetRange(63, 3));
            services[57].SubServices = new List<MedicalService>(services.GetRange(19, 5));

            //badania laboratoryjne oraz szczepienia
            services[74].SubServices = new List<MedicalService>(services.GetRange(24, 6));
            services[76].SubServices = new List<MedicalService>(services.GetRange(66, 7));

            //fizjoterapia
            services[75].SubServices = new List<MedicalService>(services.GetRange(32, 5));

            return services;
        }

        public IEnumerable<VisitCategory> GetVisitCategories()
        {
            List<VisitCategory> categories = new()
            {
                new VisitCategory() { Id = 1, CategoryName = "Konsultacje", PrimaryMedicalServices = new List<MedicalService>(medicalServices.GetRange(38, 23)) },
                new VisitCategory() { Id = 2, CategoryName = "E-konsultacje", PrimaryMedicalServices = new List<MedicalService>(medicalServices.GetRange(38, 23)) { } },
                new VisitCategory() { Id = 3, CategoryName = "Stomatologia", PrimaryMedicalServices = new List<MedicalService>(medicalServices.GetRange(57, 6)) { } },
                new VisitCategory() { Id = 4, CategoryName = "Diagnostyka obrazowa ", PrimaryMedicalServices = new List<MedicalService>(medicalServices.GetRange(1, 3)) { } },
                new VisitCategory() { Id = 5, CategoryName = "Fizjoterapia", PrimaryMedicalServices = new List<MedicalService>(medicalServices.GetRange(31, 6)) { } },
                new VisitCategory() { Id = 6, CategoryName = "Badania laboratoryjne/szczepienia", PrimaryMedicalServices = new List<MedicalService>(medicalServices.GetRange(24, 6)) { } },
            };
            categories[0].PrimaryMedicalServices.Add(medicalServices[0]);
            return categories;
        }


        public Location GetLocationById(long locationId)
        {
            throw new NotImplementedException();
        }
    }
}