﻿using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Data.InMemoryContexts
{
    public static class PatientMockDB
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

        public static IEnumerable<Visit> AvailableVisits;
        //readonly IEnumerable<Visit> historicalVisits;

        public static IEnumerable<Location> Locations;
        //public  Patient Patient { get; set; }
        public static IEnumerable<MedicalWorker> MedicalWorkers;
        public static List<MedicalService> MedicalServices { get; set; }
        public static List<MedicalService> PrimaryMedicalServices { get; set; }
        public static List<VisitCategory> VisitCategories { get; set; }
        public static List<MedicalPackage> MedicalPackages { get; set; }
        public static List<NFZUnit> NfzUnits { get; set; }
        public static List<Patient> AllPatients { get; set; }
        public static List<List<MedicalRoom>> MedicalRooms { get; set; }
        private static List<User> _users;
        public static List<User> Users
        {
            get
            {
                if (_users == null)
                {
                    _users = GetAllUsers();
                    return _users;
                }
                else
                {
                    return _users;
                }
            }
            set
            {
                _users = value;
            }
        }
        public static Patient CurrentPatient { get; set; }
        public static List<Person> Persons { get; internal set; }

        public static bool IsCreated;
        public static void SetData()
        {
            IsCreated = true;
            Persons = GetAllPersons();
            Users = GetAllUsers();
            NfzUnits = GetNFZUnits().ToList();
            MedicalServices = GetMedicalServices().ToList();
            MedicalPackages = GetMedicalPackages().ToList();
            AllPatients = GetAllPatients().ToList();
            PrimaryMedicalServices = MedicalServices.Where(c => c.IsPrimaryService == true).ToList();
            VisitCategories = GetVisitCategories().ToList();
            //medicalRooms = GetMedicalRooms().ToList();
            Locations = GetAllLocations();
            MedicalWorkers = GetMedicalWorkers();
            AvailableVisits = GetAvailableVisits();
            //AllPatients = GetAllPatients().ToList();
            //List<Visit> HistoricalVisits GetHistoricalVisits();
            CurrentPatient = GetPatientData(AllPatients[0]);

        }
        public static Person GetPersonById(long id)
        {
            return Persons.Where(c => c.Id == id).FirstOrDefault();
        }

        private static List<User> GetAllUsers()
        {
            List<User> users = new List<User>()
            {
                new User()
                {
                    Id=1,
                    Password="PasswordPatient1",
                    UserName="patient1@asklepios.com",
                    UserType=Core.Enums.UserType.Patient,
                    WorkerModuleType=null,
                    PersonId=71
                },
                new User()
                {
                    Id=2,
                    Password="PasswordPatient2",
                    UserName="patient2@asklepios.com",
                    UserType=Core.Enums.UserType.Patient,
                    WorkerModuleType=null,
                    PersonId=72
                },
                new User()
                {
                    Id=3,
                    Password="PasswordPatient3",
                    UserName="patient3@asklepios.com",
                    UserType=Core.Enums.UserType.Patient,
                    WorkerModuleType=null,
                    PersonId=73
                },
                new User()
                {
                    Id=4,
                    Password="PasswordService1",
                    UserName="sw1@asklepios.com",
                    UserType=Core.Enums.UserType.Employee,
                    WorkerModuleType=Core.Enums.WorkerModuleType.CustomerServiceModule,
                    PersonId=74
                },
                new User()
                {
                    Id=5,
                    Password="PasswordService2",
                    UserName="sw2@asklepios.com",
                    UserType=Core.Enums.UserType.Employee,
                    WorkerModuleType=Core.Enums.WorkerModuleType.CustomerServiceModule,
                    PersonId=75
                },
                new User()
                {
                    Id=6,
                    Password="PasswordAdmin",
                    UserName="ad1@asklepios.com",
                    UserType=Core.Enums.UserType.Employee,
                    WorkerModuleType=Core.Enums.WorkerModuleType.AdministrativeWorkerModule,
                    PersonId=76
                },
                new User()
                {
                    Id=7,
                    Password="PasswordMedical1",
                    UserName="mw1@asklepios.com",
                    UserType=Core.Enums.UserType.Employee,
                    WorkerModuleType=Core.Enums.WorkerModuleType.MedicalWorkerModule,
                    PersonId=77
                },
                new User()
                {
                    Id=8,
                    Password="PasswordMedical2",
                    UserName="mw2@asklepios.com",
                    UserType=Core.Enums.UserType.Employee,
                    WorkerModuleType=Core.Enums.WorkerModuleType.MedicalWorkerModule,
                    PersonId=78
                }
            };

            return users;
        }

        public static IEnumerable<Visit> GetAvailableVisits()
        {
            DateTimeOffset dateTimeOffset = DateTime.Now;

            List<Visit> availableVisits = new List<Visit>();
            int dayOffset = 0;
            DateTimeOffset start = new DateTimeOffset(dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day, 8, 0, 0, new TimeSpan(0, 0, 0)).AddDays(1);
            long startId = 100;
            for (int i = 0; i <= 3; i++)
            {
                dayOffset++;
                if (start.AddDays(dayOffset).DayOfWeek == DayOfWeek.Saturday)
                {
                    dayOffset += 2;
                }
                else if (start.AddDays(dayOffset).DayOfWeek == DayOfWeek.Sunday)
                {
                    dayOffset++;
                }

                int minutsOffset = -1;

                for (int j = 0; j < 12; j++)
                {
                    minutsOffset++;
                    Visit visit = new Visit(      )
                    {
                        Id = startId++,
                        PrimaryService = PrimaryMedicalServices[0],
                        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                        Location = Locations.ElementAt(3),
                        MedicalRoom = Locations.ElementAt(3).MedicalRooms.ElementAt(4),
                        MedicalWorker = MedicalWorkers.ElementAt(36),
                        VisitCategory = VisitCategories.ElementAt(0),
                    };
                    availableVisits.Add(visit);
                }
                minutsOffset = -1;

                for (int j = 0; j < 12; j++)
                {
                    minutsOffset++;
                    Visit visit = new Visit()
                    {
                        Id = startId++,

                        PrimaryService = PrimaryMedicalServices[9],
                        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                        Location = Locations.ElementAt(1),
                        MedicalRoom = Locations.ElementAt(1).MedicalRooms.ElementAt(6),
                        MedicalWorker = MedicalWorkers.ElementAt(34),
                        VisitCategory = VisitCategories.ElementAt(1),
                    };
                    availableVisits.Add(visit);
                }
                minutsOffset = -1;

                for (int j = 0; j < 12; j++)
                {
                    minutsOffset++;
                    Visit visit = new Visit()
                    {
                        Id = startId++,

                        PrimaryService = PrimaryMedicalServices[23],
                        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                        Location = Locations.ElementAt(1),
                        MedicalRoom = Locations.ElementAt(1).MedicalRooms.ElementAt(3),
                        MedicalWorker = MedicalWorkers.ElementAt(12),
                        VisitCategory = VisitCategories.ElementAt(2),
                    };
                    availableVisits.Add(visit);
                }
                minutsOffset = -1;

                for (int j = 0; j < 12; j++)
                {
                    minutsOffset++;
                    Visit visit = new Visit()
                    {
                        Id = startId++,

                        PrimaryService = PrimaryMedicalServices[2],
                        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                        Location = Locations.ElementAt(5),
                        MedicalRoom = Locations.ElementAt(5).MedicalRooms.ElementAt(3),
                        MedicalWorker = MedicalWorkers.ElementAt(37),
                        VisitCategory = VisitCategories.ElementAt(1),
                    };
                    availableVisits.Add(visit);
                }
                minutsOffset = -1;

                for (int j = 0; j < 12; j++)
                {
                    minutsOffset++;
                    Visit visit = new Visit()
                    {
                        Id = startId++,

                        PrimaryService = PrimaryMedicalServices[16],
                        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                        Location = Locations.ElementAt(4),
                        MedicalRoom = Locations.ElementAt(5).MedicalRooms.ElementAt(2),
                        MedicalWorker = MedicalWorkers.ElementAt(20),
                        VisitCategory = VisitCategories.ElementAt(0),
                    };
                    availableVisits.Add(visit);
                }
                minutsOffset = -1;

                for (int j = 0; j < 12; j++)
                {
                    minutsOffset++;
                    Visit visit = new Visit()
                    {
                        Id = startId++,

                        PrimaryService = PrimaryMedicalServices[28],
                        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                        Location = Locations.ElementAt(1),
                        MedicalRoom = Locations.ElementAt(1).MedicalRooms.ElementAt(3),
                        MedicalWorker = MedicalWorkers.ElementAt(55),
                        VisitCategory = VisitCategories.ElementAt(3),
                    };
                    availableVisits.Add(visit);
                }
                minutsOffset = -1;

                for (int j = 0; j < 12; j++)
                {
                    minutsOffset++;
                    Visit visit = new Visit()
                    {
                        Id = startId++,

                        PrimaryService = PrimaryMedicalServices[20],
                        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                        Location = Locations.ElementAt(4),
                        MedicalRoom = Locations.ElementAt(5).MedicalRooms.ElementAt(1),
                        MedicalWorker = MedicalWorkers.ElementAt(5),
                        VisitCategory = VisitCategories.ElementAt(2),
                    };
                    availableVisits.Add(visit);
                }
                minutsOffset = -1;

                for (int j = 0; j < 12; j++)
                {
                    minutsOffset++;
                    Visit visit = new Visit()
                    {
                        Id = startId++,

                        PrimaryService = PrimaryMedicalServices[32],
                        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                        Location = Locations.ElementAt(0),
                        MedicalRoom = Locations.ElementAt(0).MedicalRooms.ElementAt(11),
                        MedicalWorker = MedicalWorkers.ElementAt(2),
                        VisitCategory = VisitCategories.ElementAt(4),
                    };
                    availableVisits.Add(visit);
                }
                minutsOffset = -1;

                for (int j = 0; j < 12; j++)
                {
                    minutsOffset++;
                    Visit visit = new Visit()
                    {
                        Id = startId++,

                        PrimaryService = PrimaryMedicalServices[34],
                        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                        Location = Locations.ElementAt(1),
                        MedicalRoom = Locations.ElementAt(1).MedicalRooms.ElementAt(1),
                        MedicalWorker = MedicalWorkers.ElementAt(61),
                        VisitCategory = VisitCategories.ElementAt(5),
                    };
                    availableVisits.Add(visit);
                }
                minutsOffset = -1;

                for (int j = 0; j < 12; j++)
                {
                    minutsOffset++;

                    Visit visit = new Visit()
                    {
                        Id = startId++,
                        PrimaryService = PrimaryMedicalServices[9],
                        MedicalRoom = Locations.ElementAt(0).MedicalRooms.ElementAt(0),
                        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                        Location = Locations.ElementAt(0),
                        MedicalWorker = MedicalWorkers.ElementAt(50),
                        VisitCategory = VisitCategories.ElementAt(0),
                    };
                    availableVisits.Add(visit);
                }
                minutsOffset = -1;

                for (int j = 0; j < 12; j++)
                {
                    minutsOffset++;

                    Visit visit = new Visit()
                    {
                        Id = 1,
                        PrimaryService = PrimaryMedicalServices[7],
                        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                        Location = Locations.ElementAt(0),
                        MedicalRoom = Locations.ElementAt(0).MedicalRooms.ElementAt(1),
                        MedicalWorker = MedicalWorkers.ElementAt(29),
                        VisitCategory = VisitCategories.ElementAt(0),
                    };
                    availableVisits.Add(visit);

                }
                minutsOffset = -1;

                for (int j = 0; j < 12; j++)
                {
                    minutsOffset++;

                    Visit visit = new Visit()
                    {
                        Id = 2,
                        PrimaryService = PrimaryMedicalServices[0],
                        MedicalRoom = Locations.ElementAt(0).MedicalRooms.ElementAt(0),
                        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                        Location = Locations.ElementAt(0),
                        MedicalWorker = MedicalWorkers.ElementAt(0),
                        VisitCategory = VisitCategories.ElementAt(0),
                    };
                    availableVisits.Add(visit);



                }
                minutsOffset = -1;

                for (int j = 0; j < 12; j++)
                {
                    minutsOffset++;

                    Visit visit = new Visit()
                    {
                        Id = 3,
                        PrimaryService = PrimaryMedicalServices[28],
                        MedicalRoom = Locations.ElementAt(0).MedicalRooms.ElementAt(0),
                        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                        Location = Locations.ElementAt(0),
                        MedicalWorker = MedicalWorkers.ElementAt(51),
                        VisitCategory = VisitCategories.ElementAt(3),
                    };
                    availableVisits.Add(visit);
                }
                minutsOffset = -1;

                for (int j = 0; j < 12; j++)
                {
                    minutsOffset++;

                    Visit visit = new Visit()
                    {
                        Id = 5,
                        PrimaryService = PrimaryMedicalServices[6],
                        MedicalRoom = Locations.ElementAt(0).MedicalRooms.ElementAt(0),
                        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                        Location = Locations.ElementAt(0),
                        MedicalWorker = MedicalWorkers.ElementAt(26),
                        VisitCategory = VisitCategories.ElementAt(0),
                    };
                    availableVisits.Add(visit);
                }
                minutsOffset = -1;

                for (int j = 0; j < 12; j++)
                {
                    minutsOffset++;

                    Visit visit = new Visit()
                    {
                        Id = 6,
                        PrimaryService = PrimaryMedicalServices[32],
                        MedicalRoom = Locations.ElementAt(0).MedicalRooms.ElementAt(0),
                        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                        Location = Locations.ElementAt(0),
                        MedicalWorker = MedicalWorkers.ElementAt(3),
                        VisitCategory = VisitCategories.ElementAt(1),
                    };
                    availableVisits.Add(visit);
                }
                minutsOffset = -1;

                for (int j = 0; j < 12; j++)
                {
                    minutsOffset++;

                    Visit visit = new Visit()
                    {
                        Id = 7,
                        PrimaryService = PrimaryMedicalServices[20],
                        MinorMedicalServices = new List<MedicalService>() { MedicalServices[48], MedicalServices[49], MedicalServices[52] },
                        MedicalRoom = Locations.ElementAt(0).MedicalRooms.ElementAt(1),
                        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                        Location = Locations.ElementAt(0),
                        MedicalWorker = MedicalWorkers.ElementAt(9),
                        VisitCategory = VisitCategories.ElementAt(2),
                    };
                    availableVisits.Add(visit);
                }
                minutsOffset = -1;

                for (int j = 0; j < 12; j++)
                {
                    minutsOffset++;

                    Visit visit = new Visit()
                    {
                        Id = 8,
                        PrimaryService = PrimaryMedicalServices[34],
                        MinorMedicalServices = new List<MedicalService>() { MedicalServices[7], MedicalServices[9] },
                        MedicalRoom = Locations.ElementAt(0).MedicalRooms.ElementAt(1),
                        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                        Location = Locations.ElementAt(3),
                        MedicalWorker = MedicalWorkers.ElementAt(39),
                        VisitCategory = VisitCategories.ElementAt(5),

                    };
                    availableVisits.Add(visit);
                }
                for (int j = 0; j < 12; j++)
                {
                    minutsOffset++;

                    Visit visit = new Visit()
                    {
                        Id = 9,
                        PrimaryService = PrimaryMedicalServices[2],
                        MedicalRoom = Locations.ElementAt(0).MedicalRooms.ElementAt(1),
                        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                        Location = Locations.ElementAt(3),
                        MedicalWorker = MedicalWorkers.ElementAt(35),
                        VisitCategory = VisitCategories.ElementAt(0),
                        MedicalHistory = "Pacjent skarży się na chroniczne zmęczenie. Wspomina też o tym, że mimo że je tyle samo co wcześniej, to ostatnio sporo przytył. Ma nadwagę, 170 cm wzrostu, 90 kg.",
                    };
                    availableVisits.Add(visit);
                }
            }
            //{
            //    new Visit()
            //    {
            //        Id=6,
            //        BookedMedicalServices=new List<MedicalService>(){ PrimaryMedicalServices[0], PrimaryMedicalServices[5] },
            //        DateTimeSince=dateTimeOffset.AddDays(10),
            //        DateTimeTill=dateTimeOffset.AddDays(10).AddMinutes(15),
            //        Location=Locations.ElementAt(3),
            //        MedicalRoom=Locations.ElementAt(3).MedicalRooms.ElementAt(6),
            //        MedicalWorker=MedicalWorkers.ElementAt(36),
            //        VisitCategory=VisitCategories.ElementAt(3),
            //    },
            //    new Visit()
            //    {
            //        Id=7,
            //        BookedMedicalServices=new List<MedicalService>(){ PrimaryMedicalServices[0], PrimaryMedicalServices[5] },
            //        DateTimeSince=dateTimeOffset.AddDays(14),
            //        DateTimeTill=dateTimeOffset.AddDays(14).AddMinutes(15),
            //        Location=Locations.ElementAt(2),
            //        MedicalRoom=Locations.ElementAt(2).MedicalRooms.ElementAt(6),
            //        MedicalWorker=MedicalWorkers.ElementAt(40),
            //        VisitCategory=VisitCategories.ElementAt(4),
            //    },
            //    new Visit()
            //    {
            //        Id=8,
            //        BookedMedicalServices=new List<MedicalService>(){ PrimaryMedicalServices[0], PrimaryMedicalServices[5] },
            //        DateTimeSince=dateTimeOffset.AddDays(20),
            //        DateTimeTill=dateTimeOffset.AddDays(20).AddMinutes(15),
            //        Location=Locations.ElementAt(4),
            //        MedicalRoom=Locations.ElementAt(4).MedicalRooms.ElementAt(6),
            //        MedicalWorker=MedicalWorkers.ElementAt(30),
            //        VisitCategory=VisitCategories.ElementAt(3),
            //    },
            //    new Visit()
            //    {
            //        Id=9,
            //        BookedMedicalServices=new List<MedicalService>(){ PrimaryMedicalServices[0], PrimaryMedicalServices[5] },
            //        DateTimeSince=dateTimeOffset.AddDays(15),
            //        DateTimeTill=dateTimeOffset.AddDays(15).AddMinutes(30),
            //        Location=Locations.ElementAt(4),
            //        MedicalRoom=Locations.ElementAt(4).MedicalRooms.ElementAt(6),
            //        MedicalWorker=MedicalWorkers.ElementAt(30),
            //        VisitCategory=VisitCategories.ElementAt(2),
            //    },
            //};

            return availableVisits;
        }

        internal static User GetUserById(int parsedId)
        {
            User user = Users.Where(c => c.Id == parsedId).FirstOrDefault();
            if (user != null)
            {
                user.PersonId = user.PersonId;
                user.Person = GetPersonById(user.PersonId);
            }
            return user;
        }

        public static IEnumerable<Visit> GetHistoricalVisits()
        {
            DateTimeOffset dateTimeOffset = DateTime.Now;

            //IEnumerable<Visit> historicalVisits=new List<Visit>     ()
            //{
            //    new Visit()
            //    {
            //        Id=1,
            //        BookedMedicalServices=new List<MedicalService>(){ PrimaryMedicalServices[0], MedicalServices[1] },
            //        MedicalRoom=Locations.ElementAt(0).MedicalRooms.ElementAt(0),
            //        DateTimeSince=dateTimeOffset.AddDays(-20),
            //        DateTimeTill=dateTimeOffset.AddDays(-20).AddMinutes(15),
            //        Location=Locations.ElementAt(0),
            //        MedicalWorker=MedicalWorkers.ElementAt(0),
            //        Patient=patient,
            //        VisitCategory=VisitCategories.ElementAt(0),
            //        VisitSummary=new VisitSummary()
            //        {
            //            Id=1,
            //            MedicalHistory="Pacjent skarżył się na senność i chroniczne zmęczenie. Zostały zlecone badania moczu i krwi, by sprawdzić czy nie ma cukrzycy albo problemów z trzustką/wątrobą/nerkami",
            //            Recommendations=recommendations
            //        }

            //    }

            //}

            return new List<Visit>();
        }

        public static IEnumerable<Location> GetAllLocations()
        {
            IEnumerable<IEnumerable<MedicalRoom>> roomsCollections = GetMedicalRooms();

            return new List<Location>()
            {
                new Location()
                    {   City="Warszawa",
                        StreetAndNumber="Jerozolimskie 80",
                        Description="Ośrodek w centrum Warszawy ze świetnym dojazdem z każdej dzielnicy.",
                        Facilities=new List<string>(){"15 gabinetów ogólno-konsultacyjnych", "Gabinet zabiegowy", "2 gabinety stomatologiczne", "Gabinet higieny jamy ustnej", "Gabinet diagnostyki obrazowej"},
                        Id=1,
                        Name="Ośrodek Warszawa Jerozolimskie",
                        Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia"},
                        //VoivodeshipType=Core.Enums.VoivodeshipType.mazowieckie,
                         Aglomeration=Core.Enums.Aglomeration.Warsaw,

                        ImagePath="/img/Locations/loc1.jpeg",
                        PhoneNumber="22 780 421 433",
                        PostalCode="01-111",
                       MedicalRooms=roomsCollections.ElementAt(0)
                        },
                new Location()
                    {
                    City="Warszawa",
                        StreetAndNumber="Grójecka 100",
                        Description="Ośrodek w Warszawie w dzielnicy Ochota, z bardzo dobrym dojazdem z zachodniej części Warszawy.",
                        Facilities=new List<string>(){"12 gabinetów ogólno-konsultacyjnych", "Gabinet zabiegowy", "Gabinet diagnostyki obrazowej", "Gabinek okulistyczny"},
                        Id=2,
                        Name="Ośrodek Warszawa Ochota",
                        Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia"},
                        //VoivodeshipType=Core.Enums.VoivodeshipType.mazowieckie,
                                                Aglomeration=Core.Enums.Aglomeration.Warsaw,

                        ImagePath="/img/Locations/loc2.jpg",
                        PhoneNumber="22 787 477 323",
                        PostalCode="01-211",
                        MedicalRooms=roomsCollections.ElementAt(1)
                        },
                new Location()
                    {   City="Warszawa",
                        StreetAndNumber="KEN 20",
                        Description="Ośrodek na południu Warszawy ze świetnym dojazdem z południa Warszawy oraz regionów wzdłuż M1 oraz południowych okolic Warszawy.",
                        Facilities=new List<string>(){"11 gabinetów ogólno-konsultacyjnych", "2 Gabinety zabiegowe", "2 Gabinety ginekologiczne", "2 gabinety stomatologiczne", "Gabinet diagnostyki obrazowej"},
                        Id=3,
                        Name="Ośrodek Warszawa Ursynów",
                        Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia"},
                        //VoivodeshipType=Core.Enums.VoivodeshipType.mazowieckie,
                                                Aglomeration=Core.Enums.Aglomeration.Warsaw,

                        ImagePath="/img/Locations/loc3.jpg",
                        PhoneNumber="22 777 600 313",
                        PostalCode="03-055",
                        MedicalRooms=roomsCollections.ElementAt(2)

                        },
                new Location()
                    {   City="Warszawa",
                        StreetAndNumber="Malborska",
                        Description="Ośrodek na wschodzie Warszawy z dobrym dojazdem ze wschodnich dzielnic Warszawy a także wschodnich okolic Warszawy.",
                        Facilities=new List<string>(){"15 gabinetów ogólno-konsultacyjnych", "Gabinet zabiegowy", "2 gabinety stomatologiczne", "Gabinet higieny jamy ustnej", "Gabinet diagnostyki obrazowej"},
                        Id=4,
                        Name="Ośrodek Warszawa Targówek",
                        Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia"},
                        //VoivodeshipType=Core.Enums.VoivodeshipType.mazowieckie,
                                                Aglomeration=Core.Enums.Aglomeration.Warsaw,

                        ImagePath="/img/Locations/loc4.jpg",
                        PhoneNumber="22 777 444 333",
                        PostalCode="02-222",
                        MedicalRooms=roomsCollections.ElementAt(3)

                        },
                new Location()
                    {   City="Kraków",
                        StreetAndNumber="Podgórska 14",
                        Description="Ośrodek w Krakowie, w świetnie skomunikowanym Kazimierzu",
                        Facilities=new List<string>(){"15 gabinetów ogólno-konsultacyjnych", "Gabinet zabiegowy", "2 gabinety stomatologiczne", "Gabinet higieny jamy ustnej", "Gabinet diagnostyki obrazowej"},
                        Id=5,
                        Name="Ośrodek Kraków Pogórze",
                        Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia"},
                        //VoivodeshipType=Core.Enums.VoivodeshipType.malopolskie,
                                                Aglomeration=Core.Enums.Aglomeration.Cracow,

                        ImagePath="/img/Locations/loc5.jpg",
                        PhoneNumber="20 300 400 111",
                        PostalCode="80-078",
                        MedicalRooms=roomsCollections.ElementAt(4)

                        },
                new Location()
                    {   City="Gdańsk",
                        StreetAndNumber="Chlebnicka 11",
                        Description="Ośrodek w centrum Gdańska na popularnej Wyspie Spichrzów",
                        Facilities=new List<string>(){"22 gabinety ogólno-konsultacyjne", "Gabinet zabiegowy", "2 gabinety stomatologiczne", "Gabinet higieny jamy ustnej", "Gabinet diagnostyki obrazowej"},
                        Id=6,
                        Name="Ośrodek Gdańsk Wyspa Spichrzów",
                        Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia"},
                        //VoivodeshipType=Core.Enums.VoivodeshipType.pomorskie,
                                                Aglomeration=Core.Enums.Aglomeration.Tricity,

                        ImagePath="/img/Locations/loc6.jpg",
                        PhoneNumber="30 500 500 241",
                        PostalCode="45-100",
                        MedicalRooms=roomsCollections.ElementAt(5)

                        },
                                                new Location()
                    {   City="Poznań",
                        StreetAndNumber="Maltańska 1",
                        Description="Ośrodek położony na terenie Galerie Malta Poznań",
                        Facilities=new List<string>(){"20 gabinetów ogólno-konsultacyjnych", "Gabinet zabiegowy", "2 gabinety stomatologiczne", "Gabinet higieny jamy ustnej", "Gabinet diagnostyki obrazowej"},
                        Id=1,
                        Name="Ośrodek Poznań Malta",
                        Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia", "Okulistyka"},
                        //VoivodeshipType=Core.Enums.VoivodeshipType.pomorskie,
                        Aglomeration=Core.Enums.Aglomeration.Poznan,
                        ImagePath="/img/locations/loc7.jpg",
                        PhoneNumber="30 500 500 241",
                        PostalCode="60-102",
                        MedicalRooms=roomsCollections.ElementAt(1)

                        },
                                                new Location()
                    {   City="Wrocław",
                        StreetAndNumber="Szczytnicka 11",
                        Description="Placówka położona nieco na wschód od ścisłego centrum. Łatwo do niej trafić, idąc prosto od strony placu Grunwaldzkiego.",
                        Facilities=new List<string>(){"15 gabinetów ogólno-konsultacyjnych", "Gabinet zabiegowy", "2 gabinety stomatologiczne", "Gabinet higieny jamy ustnej", "Gabinet diagnostyki obrazowej"},
                        Id=1,
                        Name="Ośrodek Wrocław Szczytnicka",
                        Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia"},
                        //VoivodeshipType=Core.Enums.VoivodeshipType.pomorskie,
                        Aglomeration=Core.Enums.Aglomeration.Wroclaw,
                        ImagePath="/img/locations/loc8.jpg",
                        PhoneNumber="71 500 500 241",
                        PostalCode="50-031",
                        MedicalRooms=roomsCollections.ElementAt(2)

                        },
                                                            new Location()
                                 {
                    City="Katowice",
                        StreetAndNumber="Młyńska 23",
                        Description="Ośrodek położonyw  bliskiej okolicy dworca PKP oraz Placu Wolności",
                        Facilities=new List<string>(){"21 gabinetów ogólno-konsultacyjnych", "Gabinet zabiegowy", "2 gabinety stomatologiczne", "Gabinet higieny jamy ustnej", "Gabinet diagnostyki obrazowej"},
                        Id=1,
                        Name="Ośrodek Gdańsk Wyspa Spichrzów",
                        Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia", "Gastrologia"},
                        //VoivodeshipType=Core.Enums.VoivodeshipType.pomorskie,
                        Aglomeration=Core.Enums.Aglomeration.Silesia,
                        ImagePath="/img/locations/loc9.jpg",
                        PhoneNumber="32 500 500 241",
                        PostalCode="40-750",
                        MedicalRooms=roomsCollections.ElementAt(3)

                        },


            };

        }

        public static IEnumerable<MedicalPackage> GetMedicalPackages()
        {
            List<MedicalPackage> MedicalPackages = new List<MedicalPackage>()
            {
                new MedicalPackage()
                {
                    Id=1,
                    Name="Podstawowy",
                    Description="Podstawowy pakiet dla osób szukajacych podstawowej opieki zdrowotnej. W cenie pakietu są zawarte bezpłatne konsultacje z 7 specjalizacji oraz podstawowe badania",
                    //ServicesDiscounts=new Dictionary<MedicalService, decimal>(),
                },
                new MedicalPackage()
                {
                    Id=2,
                    Name="Srebrny",
                    Description="Srebrny pakiet jest pakietem dla osób szukajacych rozszerzonej opieki zdrowotnej. W ramach abonamentu medycznego są darmowe konsultacje u większości specjalistów, rozszerzony pakiet badań medycznych oraz 3 wizyty rehabilitacyjnE rocznie.",
                    //ServicesDiscounts=new Dictionary<MedicalService, decimal>(),
                },
                                new MedicalPackage()
                {
                                    Id=3,
                    Name="Złoty",
                    Description="Srebrny pakiet dla osób szukajacych specjalistycznej opieki, w tym opieki dentystycznej oraz rehabilitacji.",
                    //ServicesDiscounts=new Dictionary<MedicalService, decimal>(),
                },
                new MedicalPackage()
                {
                    Id=4,
                    Name="Platynowy",
                    Description="Platynowy pakiet jest pakietem dla osób szukajacych pełnej ochrony zdrowia. Wszystkie oferowane przez nas usługi są oferowane nieodpłatnie. Priorytetowa obsługa w przypadku badań/operacji niecierpiących zwłoki. ",
                    //ServicesDiscounts=new Dictionary<MedicalService, decimal>(),
                },
            };

            Dictionary<MedicalService, decimal> discounts = new Dictionary<MedicalService, decimal>();
            for (int i = 0; i < MedicalServices.Count; i++)
            {
                MedicalService service = MedicalServices[i];
                discounts.Add(service, (decimal)0.2);
            }
            Dictionary<MedicalService, decimal> discounts2 = new Dictionary<MedicalService, decimal>();
            for (int i = 0; i < MedicalServices.Count; i++)
            {
                MedicalService service = MedicalServices[i];
                discounts2.Add(service, (decimal)0.5);
            }
            Dictionary<MedicalService, decimal> discounts3 = new Dictionary<MedicalService, decimal>();
            for (int i = 0; i < MedicalServices.Count; i++)
            {
                MedicalService service = MedicalServices[i];
                discounts3.Add(service, (decimal)0.75);
            }
            Dictionary<MedicalService, decimal> discounts4 = new Dictionary<MedicalService, decimal>();
            for (int i = 0; i < MedicalServices.Count; i++)
            {
                MedicalService service = MedicalServices[i];
                discounts4.Add(service, (decimal)1);
            }
            MedicalPackages[0].ServicesDiscounts = discounts;
            MedicalPackages[1].ServicesDiscounts = discounts2;
            MedicalPackages[2].ServicesDiscounts = discounts3;
            MedicalPackages[3].ServicesDiscounts = discounts4;

            return MedicalPackages;
        }

        private static List<Person> GetAllPersons()
        {
            List<Person> people = new List<Person>()
            {
                new Person(id:1, name:"Mariusz",surName:"Puto",pesel:"77784512598",birthDate:new DateTimeOffset(new DateTime(1977,7,8)) ,hasPolishCitizenship: true,passportNumber: null,passportCode:"POL",email:"person1@gmail.com", aglomeration:Core.Enums.Aglomeration.Bialystok),
                new Person(id:2, name:"Witold",surName:"Głąbek",pesel:"651010465465",birthDate:new DateTimeOffset(new DateTime(1965,10,10)),hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person2@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow),
                new Person(id:3, name:"Henryk",surName:"Bąbel",pesel:"870102561231323",birthDate:new DateTimeOffset(new DateTime(1987,1,2)), hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person3@gmail.com", aglomeration:Core.Enums.Aglomeration.Kielce),
                new Person(id:4, name:"Ferdynand",surName:"Małolepszy",pesel:"56050834534543",birthDate:new DateTimeOffset(new DateTime(1956,05,08)),hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person4@gmail.com", aglomeration:Core.Enums.Aglomeration.Rzeszów),
                new Person(id:5, name:"Zenon",surName:"Krzywy",pesel:"54020246454543",birthDate:new DateTimeOffset(new DateTime(1954,2,2)),hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person5@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow),
                new Person(id:6, name:"Tadeusz",surName:"Nowak",pesel:"6511117654654654",birthDate:new DateTimeOffset(new DateTime(1965,11,11)),hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person6@gmail.com", aglomeration:Core.Enums.Aglomeration.Warsaw),
                new Person(id:7,birthDate:new DateTimeOffset(new DateTime(1978,7,8)) , name:"Tomasz",surName:"Woda",pesel:"78945646312313",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person7@gmail.com", aglomeration:Core.Enums.Aglomeration.Warsaw),
                new Person(id:8,birthDate:new DateTimeOffset(new DateTime(1975,7,8)) , name:"Łukasz",surName:"Czekaj",pesel:"756546546466",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person8@gmail.com", aglomeration:Core.Enums.Aglomeration.Tricity),
                new Person(id:9,birthDate:new DateTimeOffset(new DateTime(1961,7,8)) , name:"Danuta",surName:"Werys",pesel:"61321234189",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person58@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow),
                new Person(id:10,birthDate:new DateTimeOffset(new DateTime(1984,7,8)) , name:"Mateusz",surName:"Chodzień",pesel:"841313216546",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person9@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow),
                new Person(id:11,birthDate:new DateTimeOffset(new DateTime(1944,7,8)) , name:"Leszek",surName:"Ancymon",pesel:"44445465456465",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person10@gmail.com", aglomeration:Core.Enums.Aglomeration.Warsaw),
                new Person(id:12,birthDate:new DateTimeOffset(new DateTime(1975,7,8)) , name:"Karol",surName:"Szczęsny",pesel:"7532123165465",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person11@gmail.com", aglomeration:Core.Enums.Aglomeration.Silesia),
                new Person(id:13,birthDate:new DateTimeOffset(new DateTime(1965,7,8)) , name:"Remigiusz",surName:"Czystka",pesel:"654213215649546",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person12@gmail.com", aglomeration:Core.Enums.Aglomeration.Tricity),
                new Person(id:14,birthDate:new DateTimeOffset(new DateTime(1979,7,8)) , name:"Robert",surName:"Pawłowski",pesel:"798879875456132",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person13@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow),
                new Person(id:15,birthDate:new DateTimeOffset(new DateTime(1971,7,8)) , name:"Szymon",surName:"Sosna",pesel:"71123156456456",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person14@gmail.com", aglomeration:Core.Enums.Aglomeration.Warsaw),
                new Person(id:16,birthDate:new DateTimeOffset(new DateTime(1965,7,8)) , name:"Sergiusz",surName:"Ząbek",pesel:"6523154645633",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person15@gmail.com", aglomeration:Core.Enums.Aglomeration.Tricity),
                new Person(id:17,birthDate:new DateTimeOffset(new DateTime(1964,7,8)) , name:"Tymoteusz",surName:"Zez",pesel:"64561231564546",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person16@gmail.com", aglomeration:Core.Enums.Aglomeration.Warsaw),
                new Person(id:18,birthDate:new DateTimeOffset(new DateTime(1945,7,8)) , name:"Zbigniew",surName:"Korzeń",pesel:"45632132456486",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person17@gmail.com", aglomeration:Core.Enums.Aglomeration.Bialystok),
                new Person(id:19,birthDate:new DateTimeOffset(new DateTime(1949,7,8)) , name:"Zbigniew",surName:"Osiński",pesel:"49987945646133",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person18@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow),
                new Person(id:20,birthDate:new DateTimeOffset(new DateTime(1965,7,8)) , name:"Michał",surName:"Czosnek",pesel:"654321546563331",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person19@gmail.com", aglomeration:Core.Enums.Aglomeration.Tricity),
                new Person(id:21,birthDate:new DateTimeOffset(new DateTime(1980,7,8)) , name:"Tomasz",surName:"Truteń",pesel:"8012131654613",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person20@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow),
                new Person(id:22,birthDate:new DateTimeOffset(new DateTime(1955,7,8)) , name:"Bogusław",surName:"Śmiały",pesel:"5546545641231234",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person21@gmail.com", aglomeration:Core.Enums.Aglomeration.Warsaw),
                new Person(id:23,birthDate:new DateTimeOffset(new DateTime(1954,7,8)) , name:"Jan",surName:"Dutki",pesel:"54654321314564",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person22@gmail.com", aglomeration:Core.Enums.Aglomeration.Warsaw),
                new Person(id:24,birthDate:new DateTimeOffset(new DateTime(1965,7,8)) , name:"Jarosław",surName:"Kurczak",pesel:"65461234564546",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person23@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow),
                new Person(id:25,birthDate:new DateTimeOffset(new DateTime(1965,7,8)) , name:"Grzegorz",surName:"Grześkowiak",pesel:"6548745646546",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person24@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow),
                new Person(id:26,birthDate:new DateTimeOffset(new DateTime(1945,7,8)) , name:"Gerwazy",surName:"Zasada",pesel:"4561231564654",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person25@gmail.com", aglomeration:Core.Enums.Aglomeration.Silesia),
                new Person(id:27,birthDate:new DateTimeOffset(new DateTime(1954,7,8)) , name:"Czesław",surName:"Wilk",pesel:"5487897564646",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person26@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow),
                new Person(id:28,birthDate:new DateTimeOffset(new DateTime(1964,7,8)) , name:"Tadeusz",surName:"Gąska",pesel:"64621321564564",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person27@gmail.com", aglomeration:Core.Enums.Aglomeration.Bialystok),
                new Person(id:29,birthDate:new DateTimeOffset(new DateTime(1959,7,8)) , name:"Waldemar",surName:"Kucaj",pesel:"5945612315645",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person28@gmail.com", aglomeration:Core.Enums.Aglomeration.Tricity),
                new Person(id:30,birthDate:new DateTimeOffset(new DateTime(1978,7,8)) , name:"Piotr",surName:"Kuropatwa",pesel:"789465132132",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person29@gmail.com", aglomeration:Core.Enums.Aglomeration.Bialystok),
                new Person(id:31,birthDate:new DateTimeOffset(new DateTime(1978,10,8)) , name:"Paweł",surName:"Łąkietka",pesel:"7894654654965",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person30@gmail.com", aglomeration:Core.Enums.Aglomeration.Warsaw),
                new Person(id:32,birthDate:new DateTimeOffset(new DateTime(1945,7,8)) , name:"Rozmus",surName:"Remus",pesel:"4564134156465",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person31@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow),
                new Person(id:33,birthDate:new DateTimeOffset(new DateTime(1948,7,8)) , name:"Miłosz",surName:"Ciapek",pesel:"487945643213",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person32@gmail.com", aglomeration:Core.Enums.Aglomeration.Silesia),
                new Person(id:34,birthDate:new DateTimeOffset(new DateTime(1965,7,8)) , name:"Czesława",surName:"Kret",pesel:"6546123156464",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person33@gmail.com", aglomeration:Core.Enums.Aglomeration.Bialystok),
                new Person(id:35,birthDate:new DateTimeOffset(new DateTime(1989,7,8)) , name:"Marlena",surName:"Bajka",pesel:"894561132156", hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person34@gmail.com", aglomeration:Core.Enums.Aglomeration.Tricity),
                new Person(id:36,birthDate:new DateTimeOffset(new DateTime(1954,7,8)) , name:"Bożena",surName:"Arbuz",pesel:"5456463216546",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person35@gmail.com", aglomeration:Core.Enums.Aglomeration.Bialystok),
                new Person(id:37,birthDate:new DateTimeOffset(new DateTime(1980,7,8)) , name:"Klaudia",surName:"Kąkol",pesel:"8015646546546",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person36@gmail.com", aglomeration:Core.Enums.Aglomeration.Warsaw),
                new Person(id:38,birthDate:new DateTimeOffset(new DateTime(1986,7,8)) , name:"Sandra",surName:"Sosna",pesel:"864654564645",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person37@gmail.com", aglomeration:Core.Enums.Aglomeration.Kielce),
                new Person(id:39,birthDate:new DateTimeOffset(new DateTime(1951,7,8)) , name:"Teodora",surName:"Wiśniowiecka",pesel:"515648946513245",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person38@gmail.com", aglomeration:Core.Enums.Aglomeration.Rzeszów),
                new Person(id:40,birthDate:new DateTimeOffset(new DateTime(1966,7,8)) , name:"Kornelia",surName:"Krasicka",pesel:"664545646546546",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person39@gmail.com", aglomeration:Core.Enums.Aglomeration.Warsaw),
                new Person(id:41,birthDate:new DateTimeOffset(new DateTime(1975,7,8)) , name:"Marzena",surName:"Rudnicka",pesel:"7516454654645", hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person40@gmail.com", aglomeration:Core.Enums.Aglomeration.Tricity),
                new Person(id:42,birthDate:new DateTimeOffset(new DateTime(1961,7,8)) , name:"Beata",surName:"Bomba",pesel:"61231546546546",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person41@gmail.com", aglomeration:Core.Enums.Aglomeration.Silesia),
                new Person(id:43,birthDate:new DateTimeOffset(new DateTime(1971,7,8)) , name:"Katarzyna",surName:"Łasinkiewicz",pesel:"7112345647656",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person42@gmail.com", aglomeration:Core.Enums.Aglomeration.Bialystok),
                new Person(id:44,birthDate:new DateTimeOffset(new DateTime(1981,7,8)) , name:"Weronika",surName:"Kurzydło",pesel:"8154654654656",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person43@gmail.com", aglomeration:Core.Enums.Aglomeration.Warsaw),
                new Person(id:45,birthDate:new DateTimeOffset(new DateTime(1978,7,8)) , name:"Maria",surName:"Kurka",pesel:"7879465461654",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person44@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow),
                new Person(id:46,birthDate:new DateTimeOffset(new DateTime(1949,7,8)) , name:"Bronisława",surName:"Czesiek",pesel:"49489646146546",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person45@gmail.com", aglomeration:Core.Enums.Aglomeration.Warsaw),
                new Person(id:47,birthDate:new DateTimeOffset(new DateTime(1965,7,8)) , name:"Aleksandra",surName:"Ruda",pesel:"65487987446",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person46@gmail.com", aglomeration:Core.Enums.Aglomeration.Tricity),
                new Person(id:48,birthDate:new DateTimeOffset(new DateTime(1978,7,8)) , name:"Iga",surName:"Bodzio",pesel:"7848465465454", hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person47@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow),
                new Person(id:49,birthDate:new DateTimeOffset(new DateTime(1984,7,8)) , name:"Agnieszka",surName:"Pluto",pesel:"84879486546548",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person48@gmail.com", aglomeration:Core.Enums.Aglomeration.Bialystok),
                new Person(id:50,birthDate:new DateTimeOffset(new DateTime(1985,7,8)) , name:"Karolina",surName:"Majak",pesel:"856415413216",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person49@gmail.com", aglomeration:Core.Enums.Aglomeration.Warsaw),
                new Person(id:51,birthDate:new DateTimeOffset(new DateTime(1989,7,8)) , name:"Karina",surName:"Wąsacz",pesel:"894564113244",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person50@gmail.com", aglomeration:Core.Enums.Aglomeration.Warsaw),
                new Person(id:52,birthDate:new DateTimeOffset(new DateTime(1956,7,8)) , name:"Grażyna",surName:"Rudniewska",pesel:"5641321564964",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person51@gmail.com", aglomeration:Core.Enums.Aglomeration.Tricity),
                new Person(id:53,birthDate:new DateTimeOffset(new DateTime(1984,7,8)) , name:"Marta",surName:"Tracka",pesel:"846516549646411",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person52@gmail.com", aglomeration:Core.Enums.Aglomeration.Warsaw),
                new Person(id:54,birthDate:new DateTimeOffset(new DateTime(1986,7,8)) , name:"Marta",surName:"Trąbicka",pesel:"862311654482631", hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person53@gmail.com", aglomeration:Core.Enums.Aglomeration.Bialystok),
                new Person(id:55,birthDate:new DateTimeOffset(new DateTime(1979,7,8)) , name:"Sylwia",surName:"Sarna",pesel:"7913213156465",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person54@gmail.com", aglomeration:Core.Enums.Aglomeration.Rzeszów),
                new Person(id:56,birthDate:new DateTimeOffset(new DateTime(1975,7,8)) , name:"Kamila",surName:"Kozera",pesel:"751231654654612", hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person55@gmail.com", aglomeration:Core.Enums.Aglomeration.Warsaw),
                new Person(id:57,birthDate:new DateTimeOffset(new DateTime(1954,7,8)) , name:"Bogumiła",surName:"Braniewska",pesel:"548789461231546",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person56@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow),
                new Person(id:58,birthDate:new DateTimeOffset(new DateTime(1962,7,8)) , name:"Teresa",surName:"Winniczek",pesel:"62348979521",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person57@gmail.com", aglomeration:Core.Enums.Aglomeration.Bialystok),
                new Person(id:59,birthDate:new DateTimeOffset(new DateTime(1974,7,8)) , name:"Daria",surName:"Jaszczur",pesel:"74561213898",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person59@gmail.com", aglomeration:Core.Enums.Aglomeration.Tricity),
                new Person(id:60,birthDate:new DateTimeOffset(new DateTime(1979,7,8)) , name:"Daria",surName:"Biernacka",pesel:"791231564948213", hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person60@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow),
                new Person(id:61,birthDate:new DateTimeOffset(new DateTime(1978,7,8)) , name:"Maria",surName:"Balon",pesel:"785321546456",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person61@gmail.com", aglomeration:Core.Enums.Aglomeration.Bialystok),
                new Person(id:62,birthDate:new DateTimeOffset(new DateTime(1984,7,8)) , name:"Anna",surName:"Poranna",pesel:"84561321499476",hasPolishCitizenship: true, passportNumber: null,passportCode:"POL", email:"person62@gmail.com", aglomeration:Core.Enums.Aglomeration.Cracow),
                new Person(id:63,birthDate:new DateTimeOffset(new DateTime(1988,7,8)) , name:"Anna",surName:"Poletko",pesel:"8845641321546",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person63@gmail.com", aglomeration:Core.Enums.Aglomeration.Tricity),
                new Person(id:64,birthDate:new DateTimeOffset(new DateTime(1989,7,8)) , name:"Agata",surName:"Bosko",pesel:"8956132156463",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person64@gmail.com", aglomeration:Core.Enums.Aglomeration.Warsaw),
                new Person(id:65,birthDate:new DateTimeOffset(new DateTime(1978,7,8)) , name:"Agata",surName:"Mińska",pesel:"78465413131468",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person65@gmail.com", aglomeration:Core.Enums.Aglomeration.Wroclaw),
                new Person(id:66,birthDate:new DateTimeOffset(new DateTime(1980,7,8)) , name:"Monika",surName:"Szajka",pesel:"80156467513236", hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person66@gmail.com", aglomeration:Core.Enums.Aglomeration.Bialystok),
                new Person(id:67,birthDate:new DateTimeOffset(new DateTime(1979,7,8)) , name:"Mariola",surName:"Kiepska",pesel:"798564613216546",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person67@gmail.com", aglomeration:Core.Enums.Aglomeration.Kielce),
                new Person(id:68,birthDate:new DateTimeOffset(new DateTime(1974,7,8)) , name:"Dorota",surName:"Zawisza",pesel:"7441321264987984",hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person68@gmail.com", aglomeration:Core.Enums.Aglomeration.Silesia),
                new Person(id:69,birthDate:new DateTimeOffset(new DateTime(1988,7,8)) , name:"Anastasia",surName:"Radczuk",pesel:"",hasPolishCitizenship: false,passportNumber: "AAAA87946121646",passportCode:"UKR", email:"person69@gmail.com", aglomeration:Core.Enums.Aglomeration.Tricity),
                new Person(id:70,birthDate:new DateTimeOffset(new DateTime(1979,7,8)) , name:"Karolina",surName:"Kulka",pesel:"798465132156486", hasPolishCitizenship: true,passportNumber: null,passportCode:"POL", email:"person70@gmail.com", aglomeration:Core.Enums.Aglomeration.Rzeszów),
                new Person(id:71,birthDate:new DateTimeOffset(new DateTime(1987,7,8)) , name:"Łukasz", surName:"Łukasiak",pesel:"871010101051", hasPolishCitizenship: true, passportNumber:"484654asd4a5sd4", passportCode:"PL", email:"s11437@pjwstk.edu.pl", aglomeration: Core.Enums.Aglomeration.Warsaw),//główny pacjent

            new Person(name: "Magdalena",
                    surName: "Bomba",
                    id: 72,
                    birthDate:new DateTimeOffset(new DateTime(1974,5,12)) ,
                    pesel: "7405125612164",
                    hasPolishCitizenship: true,
                    passportCode: null,
                    passportNumber: null,
                    email: "patient2@live.com",

                    aglomeration: Core.Enums.Aglomeration.Cracow
),
            new Person(name: "Katarzyna",
                    surName: "Jelitko",
                    id: 73,
                    birthDate:new DateTimeOffset(new DateTime(1966,4,8)) ,
                    pesel: "6604086545649",
                    hasPolishCitizenship: true,
                    passportCode: null,
                    passportNumber: null,
                    email: "patient3@live.com",
                    aglomeration: Core.Enums.Aglomeration.Kielce
),
            new Person(name: "Krzysztof",
                    surName: "Kitka",
                    id: 74,
                    birthDate:new DateTimeOffset(new DateTime(1979,8,5)) ,
                    pesel: "790805462134",
                    hasPolishCitizenship: true,
                    passportCode: null,
                    passportNumber: null,
                    email: "patient4@live.com",
                    aglomeration: Core.Enums.Aglomeration.Kuyavia
),
            new Person(name: "Dariusz",
                    surName: "Czapa",
                    id: 75,
                    birthDate:new DateTimeOffset(new DateTime(1982,1,24)) ,
                    pesel: "820124646951234",
                    hasPolishCitizenship: true,
                    passportCode: null,
                    passportNumber: null,
                    email: "patient6@live.com",
                    aglomeration: Core.Enums.Aglomeration.Poznan
                    ),
            new Person(name: "Tomasz",
                    surName: "Komar",
                    id: 76,
                    birthDate:new DateTimeOffset(new DateTime(1981,6,7)) ,
                    pesel: "8106075461233",
                    hasPolishCitizenship: true,
                    passportCode: null,
                    passportNumber: null,
                    email: "patient5@live.com",
                    aglomeration: Core.Enums.Aglomeration.Rzeszów
),
            new Person(name: "Arkadiusz",
                    surName: "Patka",
                    id: 77,
                    birthDate:new DateTimeOffset(new DateTime(1979,10,20)) ,
                    pesel: "791020134654",
                    hasPolishCitizenship: true,
                    passportCode: null,
                    passportNumber: null,
                    email: "patient6@live.com",
                    aglomeration: Core.Enums.Aglomeration.Silesia
),
            new Person(name: "Marta",
                    surName: "Rakieta",
                    id: 78,
                    birthDate:new DateTimeOffset(new DateTime(1991,2,12)) ,
                    pesel: "910212456461",
                    hasPolishCitizenship: true,
                    passportCode: null,
                    passportNumber: null,
                    email: "patient7@live.com",
                    aglomeration: Core.Enums.Aglomeration.Tricity
),
            new Person(name: "Ada",
                    surName: "Ruda",
                    id: 79,
                    birthDate:new DateTimeOffset(new DateTime(1994,12,13)) ,
                    pesel: "941213216541",
                    hasPolishCitizenship: true,
                    passportCode: null,
                    passportNumber: null,
                    email: "patient8@live.com",
                    aglomeration: Core.Enums.Aglomeration.Warsaw
),
            new Person(name: "Genowefa",
                    surName: "Pigwa",
                    id: 80,
                    birthDate:new DateTimeOffset(new DateTime(1954,6,13)) ,
                    pesel: "54061324651322",
                    hasPolishCitizenship: true,
                    passportCode: null,
                    passportNumber: null,
                    email: "patient9@live.com",
                    aglomeration: Core.Enums.Aglomeration.Wroclaw
),
            new Person(name: "Wacław",
                    surName: "Kopytko",
                    id: 81,
                    birthDate:new DateTimeOffset(new DateTime(1955,3,13)) ,
                    pesel: "5503136549494",
                    hasPolishCitizenship: true,
                    passportCode: null,
                    passportNumber: null,
                    email: "patient11@live.com",
                    aglomeration: Core.Enums.Aglomeration.Warsaw
),
            new Person(name: "Bożena",
                    surName: "Raj",
                    id: 82,
                    birthDate:new DateTimeOffset(new DateTime(1949,11,18)) ,
                    pesel: "49111816546513",
                    hasPolishCitizenship: true,
                    passportCode: null,
                    passportNumber: null,
                    email: "patient12@live.com",
                    aglomeration: Core.Enums.Aglomeration.Cracow
),

            new Person(name: "Fryderek",
                    surName: "Czyż",
                    id: 83,
                    birthDate:new DateTimeOffset(new DateTime(1956,12,18)) ,
                    pesel: "56121864984561",
                    hasPolishCitizenship: true,
                    passportCode: null,
                    passportNumber: null,
                    email: "patient13@live.com",
                    aglomeration: Core.Enums.Aglomeration.Warsaw
),
            new Person(name: "Monika",
                    surName: "Zalewska",
                    id: 84,
                    birthDate:new DateTimeOffset(new DateTime(1982,9,9)) ,
                    pesel: "820909132156462",
                    hasPolishCitizenship: true,
                    passportCode: null,
                    passportNumber: null,
                    email: "patient14@live.com",
                    aglomeration: Core.Enums.Aglomeration.Warsaw
),
                    new Person(name: "Daria",
                    surName: "Raszpan",
                    id: 85,
                    birthDate:new DateTimeOffset(new DateTime(1984,6,16)) ,
                    pesel: "840616321316342",
                    hasPolishCitizenship: true,
                    passportCode: null,
                    passportNumber: null,
                    email: "patient1@live.com",
                    aglomeration: Core.Enums.Aglomeration.Bialystok),

        };
            return people;
        }

        public static IEnumerable<MedicalWorker> GetMedicalWorkers()
        {

            DateTime now = DateTime.Now;

            List<VisitReview> visitRatings1 = new List<VisitReview>()
            {
                new VisitReview()
                {
                    AtmosphereRate=1,
                    CompetenceRate=4,
                    GeneralRate=3,
                    Id=1,
                    ShortDescription="Lekarz w miarę kompetentny, ale chamski gbur",
                    ReviewDate= now.AddDays(-10),
                    Reviewer=AllPatients[0],

                },
                new VisitReview()
                {
                    AtmosphereRate=5,
                    CompetenceRate=2,
                    GeneralRate=3,
                    Id=2,
                    ShortDescription="Miły lekarz, niestety jego zalecenia nic nie pomogły",
                    ReviewDate= now.AddDays(-20),
                    Reviewer=AllPatients[1]
                }
            };
            List<VisitReview> visitRatings2 = new List<VisitReview>()
            {
                new VisitReview()
                {
                    AtmosphereRate=4,
                    CompetenceRate=4,
                    GeneralRate=4,
                    Id=4,
                    ShortDescription="Przepisane przez niego medykamenty poprawiły mój stan, ale część objawów się utrzymała.",
                    ReviewDate= now.AddDays(-120),
                    Reviewer=AllPatients[2]
                },
                new VisitReview()
                {
                    AtmosphereRate=5,
                    CompetenceRate=5,
                    GeneralRate=5,
                    Id=3,
                    ShortDescription="Super lekarz, pomógł mi, dodatkowo jest bardzo sympatyczny i wszystko mi po kolei wyjaśnił. Lekarz-ideał.",
                    ReviewDate= now.AddDays(-100),
                    Reviewer=AllPatients[3]
                }
            };
            List<VisitReview> visitRatings3 = new List<VisitReview>()
            {
                new VisitReview()
                {
                    AtmosphereRate=2,
                    CompetenceRate=1,
                    GeneralRate=1,
                    Id=3,
                    ShortDescription="Lekarza nie interesowały wyniki badań, nie interesowało co mówię, jedyne co mi zalecił, to leki przeciwbólowe!.",
                    ReviewDate= now.AddDays(-50),
                    Reviewer=AllPatients[4]
                },
                new VisitReview()
                {
                    AtmosphereRate=1,
                    CompetenceRate=2,
                    GeneralRate=2,
                    Id=6,
                    ShortDescription="Bardzo nieprzyjemny, jego leczenie nie przyniosło większej poprawy",
                    ReviewDate= now.AddDays(-55),
                    Reviewer=AllPatients[5]
                }
            };

            //Person person = new Person(name: "Mariusz", surName: "Puto", id: 1, pesel: "77784512598", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person1@gmail.com", aglomeration: Core.Enums.Aglomeration.Bialystok);
            long id = 0;
            List<MedicalWorker> MedicalWorkers = new List<MedicalWorker>()
            {
                new Doctor(Persons[0],"IUHIDUASHDI545613216")
                {
                    Id=++id,
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/MW/m/1.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[0],PrimaryMedicalServices[1]
                    }
                },

                new Doctor(Persons[1], "ASGER51541213")
                {
                    Id=++id,
                    Education=new List<string>() {UM_3,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu praskim",
                    ImagePath="/img/MW/m/2.jpg",
                    HiredSince=new DateTime(2017,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings2,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[2],PrimaryMedicalServices[3]
                    }

                },
                new Physiotherapist(Persons[2], "GVCXDS56151321")
                {
                    Id=++id,
                    Education=new List<string>() {UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu MSWiA",
                    ImagePath="/img/MW/m/3.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings3,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[33],                        PrimaryMedicalServices[31],                        PrimaryMedicalServices[32]
                    }

                },
                new Physiotherapist   (Persons[3],"IUJNKJN54321165")
                {Id=++id,
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu UMK",
                    ImagePath="/img/MW/m/4.jpg",
                    HiredSince=new DateTime(2020,4,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[33],                        PrimaryMedicalServices[31],                        PrimaryMedicalServices[32]
                    }

                },
                new Doctor(Persons[4],"IUJKHJK546121646")
                {Id=++id,
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/MW/m/5.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[5]
                    }
                },
                new Doctor(Persons[5],"OPASDASP54156142313")
                {Id=++id,
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/MW/m/6.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings2,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[20],PrimaryMedicalServices[21]
                    }
                },
                new Doctor(Persons[6], "IAOSD5161231564")
                {Id=++id,
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu wrocławskim",
                    ImagePath="/img/MW/m/7.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings3,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[8]
                    }
                },
                new Doctor(Persons[7], "UNCAJSDS51651323")
                {Id=++id,
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu podlaskim",
                    ImagePath="/img/MW/m/8.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings2,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[7],PrimaryMedicalServices[2]
                    }

                },
                new Doctor(Persons[8], "DFSDFD4654213")
                {Id=++id,
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/MW/m/9.jpg",
                    HiredSince=new DateTime(2012,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[4],PrimaryMedicalServices[2]
                    }
                },
                new Doctor(Persons[9],"IOWNCAS5613245")
                {Id=++id,
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu suwalskim",
                    ImagePath="/img/MW/m/10.jpg",
                    HiredSince=new DateTime(2018,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[20],PrimaryMedicalServices[22]
                    }
                },
                new Doctor(Persons[10],"MNMCXISA561235")
                {Id=++id,
                    Education=new List<string>() {UM_9},
                    Experience="W latach 2008-2019 praca w szpitalu podkarpackim",
                    ImagePath="/img/MW/m/11.jpg",
                    HiredSince=new DateTime(2017,5,5),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[7],PrimaryMedicalServices[2]
                    }

                },
                new Doctor(Persons[11],"ASIUDAS5123463")
                {Id=++id,
                    Education=new List<string>() {UM_8},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/MW/m/12.jpg",
                    HiredSince=new DateTime(2017,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[5]
                    }

                },
                new ElectroradiologyTechnician (Persons[12],"QPSCS5346448")
                {Id=++id,
                    Education=new List<string>() {UM_7},
                    Experience="W latach 2005-2020 praca w szpitalu wojskowym",
                    ImagePath="/img/MW/m/13.jpg",
                    HiredSince=new DateTime(2012,12,12),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[28],                        PrimaryMedicalServices[29]

                    }

                },
                new Doctor(Persons[13], "CXCXZS6543215")
                {Id=++id,
                    Education=new List<string>() {UM_6},
                    Experience="W latach 2010-2019 praca w szpitalu matki i dziecka",
                    ImagePath="/img/MW/m/14.jpg",
                    HiredSince=new DateTime(2019,4,4),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[7]}

                },
                new Doctor(Persons[14], "PASXCA516164")
                {Id=++id,
                    Education=new List<string>() {UM_5},
                    Experience="W latach 2011-2021 praca w szpitalu zakaźnym",
                    ImagePath="/img/MW/m/15.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[9]
                    }

                },
                new Doctor(Persons[15], "PSADNASJ1564613")
                {Id=++id,
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2007-2021 praca w szpitalu kujawskim",
                    ImagePath="/img/MW/m/16.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[8]
                    }
                },
                new Doctor(Persons[16], "AHUHIFDSD18564513")
                {Id=++id,
                    Education=new List<string>() {UM_4},
                    Experience="W latach 2005-2020 praca w szpitalu łódzkim",
                    ImagePath="/img/MW/m/17.jpg",
                    HiredSince=new DateTime(2013,3,3),
                    IsCurrentlyHired=true,VisitReviews=visitRatings3,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[11],PrimaryMedicalServices[12]
                    }

                },
                new Doctor(Persons[17],"UYGSDAS541321")
                {Id=++id,
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/MW/m/2.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings2,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[11],PrimaryMedicalServices[13]
                    }

                },
                new Doctor(Persons[18],"JHGDAJSH516145")
                {Id=++id,
                    Education=new List<string>() {UM_3},
                    Experience="W latach 2009-2020 praca w POZ Węgrów.",
                    ImagePath="/img/MW/m/19.jpg",
                    HiredSince=new DateTime(2018,7,6),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[11],PrimaryMedicalServices[14]
                    }

                },
                new Doctor(Persons[19], "GSFEQWDXA515646")
                {Id=++id,
                    Education=new List<string>() {UM_1},
                    Experience="W latach 2005-2020 praca w szpitalu miejskim w Krośnie",
                    ImagePath="/img/MW/m/20.jpg",
                    HiredSince=new DateTime(2020,2,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[11],PrimaryMedicalServices[15]
                    }

                },
                new Doctor(Persons[20], "ISJAD4465132")
                {Id=++id,
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu tarnowskim",
                    ImagePath="/img/MW/m/21.jpg",
                    HiredSince=new DateTime(2017,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[16]
                    }

                },
                new Doctor(Persons[21], "UISDR216443")
                {Id=++id,
                    Education=new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu miejskim w Zakopanem",
                    ImagePath="/img/MW/m/22.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings2,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[17]
                    }

                },
                new Doctor(Persons[22], "VASDK5421324")
                {Id=++id,
                    Education=new List<string>() {UM_7},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/MW/m/23.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[18]
                    }
                },
                new Doctor(Persons[23], "ASPDUI56321587")
                {Id=++id,
                    Education=new List<string>() {UM_5},
                    Experience="W latach 2008-2014 praca w szpitalu kardiologicznym",
                    ImagePath="/img/MW/m/2.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[19]
                    }
                },
                new Doctor(Persons[24], "BVNMXCA4623148")
                {Id=++id,
                    Education=new List<string>() {UM_5},
                    Experience="W latach 2005-2020 praca w szpitalu w Dębicy",
                    ImagePath="/img/MW/m/25.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[20],PrimaryMedicalServices[24]
                    }

                },
                new Physiotherapist(Persons[25],"FAHDJ665413215")
                {Id=++id,
                    Education=new List<string>() {UM_4},
                    Experience="W latach 2005-2020 praca w szpitalu powiatowym w Zamościu",
                    ImagePath="/img/MW/m/26.jpg",
                    HiredSince=new DateTime(2019,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[31],PrimaryMedicalServices[32],PrimaryMedicalServices[33]
                    }

                },
                new Doctor(Persons[26],"ALKJSD5461321")
                {Id=++id,
                    Education=new List<string>() {UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu zakaźnym na Woli",
                    ImagePath="/img/MW/m/27.jpg",
                    HiredSince=new DateTime(2011,10,11),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[6],PrimaryMedicalServices[27]
                    }

                },
                new ElectroradiologyTechnician(Persons[27], "HGSDAS545641231")
                {Id=++id,
                    Education=new List<string>() {UM_6},
                    Experience="W latach 2006-2019 praca w szpitalu świętokrzyskim",
                    ImagePath="/img/MW/m/28.jpg",
                    HiredSince=new DateTime(2020,8,8),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[28],PrimaryMedicalServices[29]
                    }

                },
                new Doctor(Persons[28],"BHJASGDJAS54613254")
                {
                    Id=++id,
                    Education=new List<string>() {UM_8},
                    Experience="W latach 2005-2020 praca w szpitalu akademickim w Białymstoku",
                    ImagePath="/img/MW/m/29.jpg",
                    HiredSince=new DateTime(2018,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[6],PrimaryMedicalServices[27]
                    }
                },
                new Doctor(Persons[29],"OJIHJDAS543156")
                {
                Id=++id,
                    Education=new List<string>() {UM_6},
                    Experience="W latach 2005-2020 praca w szpitalu miejskim w Słupsku",
                    ImagePath="/img/MW/m/30.jpg",
                    HiredSince=new DateTime(2016,4,4),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[7]
                    }

                },
                new Doctor(Persons[30],"JHASKDAS65461321")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_3},
                    Experience="W latach 2005-2012 praca w szpitalu klinicznym w Gnieźnie. Wcześniej pracował w Zielonej górze.",
                    ImagePath="/img/MW/m/31.jpg",
                    HiredSince=new DateTime(2011,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[11],PrimaryMedicalServices[12],PrimaryMedicalServices[13]
                    }
                },
                new Doctor(Persons[31],"JHKSDASD546123")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_4},
                    Experience="W latach 2005-2020 praca w szpitalu akademickim w Krakowie",
                    ImagePath="/img/MW/m/32.jpg",
                    HiredSince=new DateTime(2019,8,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[8]
                    }

                },
                new Doctor(Persons[32],"JHASHJDGJA4516354")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_6},
                    Experience="W latach 2009-2019 praca w szpitalu w Węgrowie",
                    ImagePath="/img/MW/k/1.jpg",
                    HiredSince=new DateTime(2015,5,5),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[23]
                    }

                },
                new DentalHygienist(Persons[33],"HASDUQ561613")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_1,UM_2},
                    Experience="W latach 2015-2021 praca w szpitalu uniwersyteckim w Poznaniu",
                    ImagePath="/img/MW/k/2.jpg",
                    HiredSince=new DateTime(2015,10,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[25]
                    }

                },
                new Doctor(Persons[34],"JHSAD6564513")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_3},
                    Experience="W latach 2011-2021 praca w szpitalu miejskim w Łowiczu",
                    ImagePath="/img/MW/k/3.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[9]
                    }

                },
                new Doctor(Persons[35],"GASHJD56441231")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_1,UM_2},
                    Experience="W latach 2008-2020 praca w szpitalu zakaźnym w Krakowie",
                    ImagePath="/img/mw/k/4.jpg",
                    HiredSince=new DateTime(2018,8,11),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[2],PrimaryMedicalServices[0]
                    }

                },
                new Doctor(Persons[36],"HBJASD546132")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_4},
                    Experience="W latach 2007-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/mw/k/5.jpg",
                    HiredSince=new DateTime(2017,7,7),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[7]
                    }

                },
                new Doctor(Persons[37],"BIKDAS5416132")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/mw/k/6.jpg",
                    HiredSince=new DateTime(2017,4,4),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[2],PrimaryMedicalServices[3]
                    }

                },
                new Doctor(Persons[38],"HJGASW4654613")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_1,UM_2},
                    Experience="W latach 2012-2020 praca w szpitalu południowym w Warszawie",
                    ImagePath="/img/mw/k/7.jpg",
                    HiredSince=new DateTime(2015,1,11),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[11],PrimaryMedicalServices[14]
                    }

                },
                new Nurse(Persons[39],"IOSHJD4613245")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu chorób serca w Gdańsku",
                    ImagePath="/img/mw/k/8.jpg",
                    HiredSince=new DateTime(2018,8,8),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[26],PrimaryMedicalServices[34]
                    }

                },
                new Nurse(Persons[40],"UGHSDS56134564")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_6},
                    Experience="W latach 2007-2018 praca w szpitalu praskim w Warszawie",
                    ImagePath="/img/mw/k/9.jpg",
                    HiredSince=new DateTime(2021,11,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[26],PrimaryMedicalServices[34]
                    }

                },
                new Doctor(Persons[41],"USHDKAS744561513")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_8},
                    Experience="W latach 2009-2019 praca w szpitalu praskim w Warszawie",
                    ImagePath="/img/mw/k/10.jpg",
                    HiredSince=new DateTime(2012,11,11),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[19]
                    }

                    },
                new Doctor(Persons[42],"NMBVDSDA546123")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_5},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/mw/k/11.jpg",
                    HiredSince=new DateTime(2017,7,9),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[20],PrimaryMedicalServices[21]
                    }
                },
                new Doctor(Persons[43],"LKASJD465315")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_1,UM_2},
                    Experience="W latach 2012-2019 praca w szpitalu MSWIA w Warszawie",
                    ImagePath="/img/mw/k/12.jpg",
                    HiredSince=new DateTime(2019,4,8),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[10],PrimaryMedicalServices[5]
                    }

                },
                new Doctor(Persons[44],"IOHDSFDS46132456")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_7},
                    Experience="W latach 2005-2020 praca w szpitalu centralnym w Krakowie",
                    ImagePath="/img/mw/k/13.jpg",
                    HiredSince=new DateTime(2016,6,6),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[2],PrimaryMedicalServices[30]
                    }

                },
                new Doctor(Persons[45],"UHJDSF5645132")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_1,UM_2},
                    Experience="W latach 2019-2021 praca w szpitalu u Koziołka Matołka w Poznaniu",
                    ImagePath="/img/mw/k/14.jpg",
                    HiredSince=new DateTime(2015,7,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[2],PrimaryMedicalServices[30]
                    }

                },
                new DentalHygienist(Persons[46],"SDFJL4654131")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu klinicznym we Wrocławiu",
                    ImagePath="/img/mw/k/15.jpg",
                    HiredSince=new DateTime(2017,2,11),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[25]
                    }

                },
                new Doctor(Persons[47],"JBNBJHSD45642131")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_1,UM_2},
                    Experience="W latach 2018-2021 praca w szpitalu klinicznym we Wrocławiu",
                    ImagePath="/img/mw/k/16.jpg",
                    HiredSince=new DateTime(2021,2,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[5]
                    }
                },
                new Doctor(Persons[48],"JHGFJDS564165412")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_8},
                    Experience="W latach 2019-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/mw/k/17.jpg",
                    HiredSince=new DateTime(2021,1,9),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[10]
                    }

                },
                new Doctor(Persons[49],"JHFDSF4561231")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_4},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/mw/k/18.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[20],PrimaryMedicalServices[24]
                    }

                },
                new Doctor(Persons[50],"UIFSDF4561321")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_5,UM_7},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/mw/k/19.jpg",
                    HiredSince=new DateTime(2019,4,4),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[9]
                    }

                },
                new ElectroradiologyTechnician(Persons[51],"DHJKFSD4564132")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_7,UM_9},
                    Experience="Staż odbyła w szpitalu Bródnowskim w Warszawie. Od 2016 roku pracuje w szpitalu Praskim w Warszawie.",
                    ImagePath="/img/mw/k/20.jpg",
                    HiredSince=new DateTime(2018,9,11),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[28]
                    }

                },
                new Nurse(Persons[52],"HBJKSDF56413215")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_6,UM_2},
                    Experience="Staż odbyty w szpitalu akademickim w Białymstoku. Od 2018 roku praca w szpitalu powiatowym w Węgrowie",
                    ImagePath="/img/mw/k/21.jpg",
                    HiredSince=new DateTime(2018,8,8),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[26],PrimaryMedicalServices[34]
                    }

                },
                new Doctor(Persons[53], "RERDSDF2134969")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_4,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/mw/k/22.jpg",
                    HiredSince=new DateTime(2018,4,6),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[2],PrimaryMedicalServices[18]
                    }

                },
                new Nurse(Persons[54],"BNMDSF546123")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_5},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/mw/k/23.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[34],PrimaryMedicalServices[26]
                    }

                },
                new ElectroradiologyTechnician(Persons[55],"PODBASHJ4454321")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/mw/k/24.jpg",
                    HiredSince=new DateTime(2019,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[28],PrimaryMedicalServices[29]
                    }

                },
                new Doctor(Persons[56],"YHBKASD5465123")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_3},
                    Experience="W latach 2014-2021 praca w szpitalu zielonogórskim",
                    ImagePath="/img/mw/k/25.jpg",
                    HiredSince=new DateTime(2013,3,3),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[11],PrimaryMedicalServices[14]
                    }

                },
                new Doctor(Persons[57],"OPQEW6546132")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_5},
                    Experience="W latach 2005-2020 praca w szpitalu wojewódzkim w Olsztynie",
                    ImagePath="/img/mw/k/26.jpg",
                    HiredSince=new DateTime(2018,4,3),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[9]
                    }

                },
                new Doctor(Persons[58],"OPNKWEJR546132")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_8},
                    Experience="Od 2010 roku pracuje jako ordynator w szpitalu Matki i Dziecka w Warszawie",
                    ImagePath="/img/mw/k/27.jpg",
                    HiredSince=new DateTime(2018,6,7),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                        MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[5],PrimaryMedicalServices[10]
                    }

                },
                new Physiotherapist(Persons[59],"GVJDAS54645")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_9},
                    Experience="W latach 2016-2020 praca w szpitalu miejskim w Grudziądzu",
                    ImagePath="/img/mw/k/28.jpg",
                    HiredSince=new DateTime(2019,8,11),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                        MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[31],PrimaryMedicalServices[32],PrimaryMedicalServices[33]
                    }

                },
                new Doctor(Persons[60],"UIHDAS546516")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_1,UM_9},
                    Experience="W latach 2009-2020 praca w szpitalu miejskim w Suwałkach",
                    ImagePath="/img/mw/k/29.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[8]
                    }

                },
                new Nurse(Persons[61],"ADASD46123")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_7,UM_2},
                    Experience="W latach 2009-2020 praca w szpitalu wojewódzkim w Toruniu",
                    ImagePath="/img/mw/k/30.jpg",
                    HiredSince=new DateTime(2019,5,4),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[26],PrimaryMedicalServices[34]
                    }

                },
                new ElectroradiologyTechnician(Persons[62],"YUGDSD56131")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_2,UM_4},
                    Experience="Od 2016 pracuje w szpitalu Bródnowskim",
                    ImagePath="/img/mw/k/31.jpg",
                    HiredSince=new DateTime(2015,5,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[28],PrimaryMedicalServices[29]
                    }

                },
                new Doctor(Persons[63],"YAJHD5461321")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_5},
                    Experience="W latach 2009-2021 praca w szpitalu w Przemyślu",
                    ImagePath="/img/mw/k/32.jpg",
                    HiredSince=new DateTime(2019,9,8),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[20],PrimaryMedicalServices[21]
                    }

                },
                new Doctor(Persons[64],"OOXCZX6541546")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_3},
                    Experience="W latach 2008-2020 praca w szpitalu w Lublinie",
                    ImagePath="/img/mw/k/33.jpg",
                    HiredSince=new DateTime(2019,4,7),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[16],
                    }

                },
                new Physiotherapist(Persons[65],"FSDRGD54543")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/mw/k/34.jpg",
                    HiredSince=new DateTime(2015,9,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[31],PrimaryMedicalServices[32],PrimaryMedicalServices[33]
                    }

                },
                new Doctor(Persons[66],"UHJKSAD51321")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/mw/k/35.jpg",
                    HiredSince=new DateTime(2019,4,3),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[19]
                    }

                },
                new Doctor(Persons[67],"BNSDSA546123")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_5,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/mw/k/36.jpg",
                    HiredSince=new DateTime(2018,8,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[9]
                    }

                },
                new ElectroradiologyTechnician(Persons[68],"KLSAD546123")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_1,UM_2},
                    Experience="W latach 2016-2020 praca w szpitalu lwowskim na Ukrainie",
                    ImagePath="/img/mw/k/37.jpg",
                    HiredSince=new DateTime(2020,8,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[28],PrimaryMedicalServices[29]
                    }

                },
                new DentalHygienist(Persons[69],"JHDAS4564231")
                {
                    Id = ++id,
                    Education =new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ImagePath="/img/MW/k/38.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[25]
                    }

                },

            };

            return MedicalWorkers;
        }

        public static IEnumerable<NFZUnit> GetNFZUnits()
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

        public static Patient GetPatientData(Patient patient)
        {
            //Patient patient = AllPatients[1];//new Patient(); //new Patient("Łukasz", "Łukasiak", 1, "8710101010", true, "484654asd4a5sd4", "PL", "s11437@pjwstk.edu.pl", aglomeration: Core.Enums.Aglomeration.Warsaw);
            DateTimeOffset dateTimeOffset = new DateTimeOffset(DateTime.Now);
            DateTimeOffset now = DateTime.Now;

            List<Prescription> prescriptions = new List<Prescription>()
            {

                new Prescription()
                {
                    AccessCode="156134",
                    Id=1,
                    //IssuedBy=(MedicalWorkers.ElementAt(0) as Doctor),
                    IssueDate= dateTimeOffset,
                    ExpirationDate=dateTimeOffset.AddMonths(1),
                    IdentificationCode="sd5f4ads5f4dsa65f46dsa54f6",
                    IssuedMedicines=new List<IssuedMedicine>()
                    {
                        new IssuedMedicine(){
                            //Dosage="Dwa razy dziennie po 1 tabletce",
                            PackageSize="60 tabletek",
                            MedicineName="Metformax",
                            PaymentDiscount=30,

                        },
                        new IssuedMedicine() {
                            //Dosage="Raz dziennie 2 tabletki",
                            PackageSize="50 tabletek",
                            MedicineName="Metformina",
                            PaymentDiscount=40
                        },
                        new IssuedMedicine() {
                            //Dosage="Trzy raz dziennie na zmianę skórną",
                            PackageSize="Buteleczka 100 ml",
                            MedicineName="Belosalic",
                            PaymentDiscount=40
                        }
                    }
                },
                new Prescription()
                {
                    AccessCode = "749643216",
                    Id = 2,
                    //IssuedBy = (MedicalWorkers.ElementAt(1) as Doctor),
                    IssueDate = dateTimeOffset.AddDays(-10),
                    ExpirationDate = dateTimeOffset.AddDays(70),
                                        IdentificationCode="u5y4fg654h6fds54gdfs56g46df",

                    IssuedMedicines = new List<IssuedMedicine>()
                    {
                        new IssuedMedicine(){
                            //Dosage="Dwa razy dziennie po 1 tabletce",
                            PackageSize="60 tabletek",MedicineName="Lakcid",PaymentDiscount=30},
                        new IssuedMedicine() {
                            //Dosage="Dwa razy dziennie po 1 tabletce",
                            PackageSize="30 tabletek",MedicineName="Trilac Plus",PaymentDiscount=0},
                        new IssuedMedicine() {
                            //Dosage="Raz dziennie po 1 tabletce",
                            PackageSize="30 tabletek",MedicineName="Enterol",PaymentDiscount=40},

                    }
                },
                new Prescription()
                {
                    AccessCode = "55554654646",
                    Id = 3,
                    //IssuedBy = (MedicalWorkers.ElementAt(2) as Doctor),
                    IssueDate = dateTimeOffset.AddDays(-20),
                    ExpirationDate = dateTimeOffset.AddDays(40),
                    IdentificationCode="asd4a5s64d65as4fsd564f65sd4f",

                    IssuedMedicines = new List<IssuedMedicine>()
                    {
                        new IssuedMedicine(){
                            //Dosage="Dwa razy dziennie po 1 tabletce",
                            PackageSize="60 tabletek",MedicineName="Eltroxin",PaymentDiscount=30},
                        new IssuedMedicine() {
                            //Dosage="Dwa razy dziennie po 1 tabletce",
                            PackageSize="40 tabletek",MedicineName="Thyrozol",PaymentDiscount=10},
                        new IssuedMedicine() {
                            //Dosage="Trzy razy dziennie po 2 tabletki",
                            PackageSize="100 tabletek",MedicineName="Metoprolol",PaymentDiscount=40},

                    }
                },
                new Prescription()
                {
                    AccessCode = "45641345695",
                    Id = 4,
                    //IssuedBy = (MedicalWorkers.ElementAt(2) as Doctor),
                    IssueDate = dateTimeOffset.AddDays(-20),
                    ExpirationDate = dateTimeOffset.AddDays(40),
                    IdentificationCode="dsfgdad4sf4ds56af4sd6f4",

                    IssuedMedicines = new List<IssuedMedicine>()
                    {
                        new IssuedMedicine(){
                            //Dosage="Dwa razy dziennie po 1 tabletce",
                            PackageSize="100 tabletek",MedicineName="Debretin 100 mg",PaymentDiscount=30},
                        new IssuedMedicine() {
                            //Dosage="Dwa razy dziennie po 1 tabletce",
                            PackageSize="60 tabletek",MedicineName="Duspatalin 200 mg",PaymentDiscount=40},

                    }
                },
            new Prescription()  //kardio
                {
                    AccessCode = "45641345695",
                    Id = 5,
                    //IssuedBy = (MedicalWorkers.ElementAt(2) as Doctor),
                    IssueDate = dateTimeOffset.AddDays(-20),
                    ExpirationDate = dateTimeOffset.AddDays(40),
                    IdentificationCode="dsfgdad4sf4ds56af4sd6f4",

                    IssuedMedicines = new List<IssuedMedicine>()
                    {
                        new IssuedMedicine(){
                            //Dosage="Dwa razy dziennie po 1 tabletce",
                            PackageSize="28 tabletek",MedicineName="Betaloc ZOK 100 mg",PaymentDiscount=30},
                        new IssuedMedicine() {
                            //Dosage="Dwa razy dziennie po 1 tabletce",
                            PackageSize="60 tabletek",MedicineName="Vivacor 25 mg",PaymentDiscount=40},

                    }
                }



            };

            List<MedicalTestResult> medicalTestResults = new List<MedicalTestResult>()
            {
                new MedicalTestResult()
                {
                    Descritpion="Wyniki rozszerzonych badań krwi",
                    MedicalService=MedicalServices[8],
                    PdfDocument= Properties.Resources.Badania_krwi_i_moczu                ,  //new PdfSharpCore.Pdf.PdfDocument( new MemoryStream( Properties.Resources.Badania_krwi_i_moczu                )),
                    Id=1,
                },
                new MedicalTestResult()
                {
                    Id=2,
                    Descritpion="Wyniki podstawowych badań krwi",
                    MedicalService=MedicalServices[7],
                    PdfDocument= Properties.Resources.badania_krwi,// new PdfSharpCore.Pdf.PdfDocument( new MemoryStream( Properties.Resources.badania_krwi                ))
                },
                new MedicalTestResult()
                {
                    Id=3,
                    Descritpion="Wyniki badań cholesterolu",
                    MedicalService=MedicalServices[50],
                    PdfDocument=Properties.Resources.cholesterol,//  new PdfSharpCore.Pdf.PdfDocument( new MemoryStream( Properties.Resources.cholesterol ))
                },
                new MedicalTestResult()
                {
                    Id=4,
                    Descritpion="Wyniki ekg serca",
                    MedicalService=MedicalServices[0],
                    PdfDocument=Properties.Resources.ekg,//  new PdfSharpCore.Pdf.PdfDocument( new MemoryStream( Properties.Resources.ekg))
                },
                new MedicalTestResult()
                {
                    Id=5,
                    Descritpion="RTG nadgarstka z trzech stron",
                    MedicalService=MedicalServices[86],//wybrać ekg serca
                    PdfDocument=Properties.Resources.ekg,//new PdfSharpCore.Pdf.PdfDocument(new MemoryStream(Properties.Resources.ekg)),
                }

            };

            List<Core.Models.Recommendation> recommendations = new List<Recommendation>()
            {
                new Recommendation()
                {
                    Id=2,
                    Title="Chroniczne zmęczenie, tycie, badania kontrolne",
                    Description="Proszę wdrożyć dietę nisko-tłuszczową o nisko-cukrową oraz wykonać zlecone badania krwii i moczu"
                },

                new Recommendation()
                {
                    Id=1,
                    Title="Cukrzyca - podejrzenie",
                    Description="Proszę ograniczyć spożywanie słodkich napojów, słodyczy. Proszę zrobić badanie krwi.",
                },
                new Recommendation()
                {
                    Id=3,
                    Title="Podejrzenie miażdzycy i wysokiego cholesterolu",
                    Description="Proszę wdrożyć niskotłuszczową dietę. Wysokie ciśnienie 160/100, podejrzenie miażdzycy oraz podwyższonego cholesterolu."
                },
                new Recommendation()
                {
                    Id=4,
                    Title="Szybkie męczenie u wysportowanej osoby",
                    Description="Proszę unikać w najblizszym czasie dużego wysiłku oraz wykonać badanie ekg"
                },
                new Recommendation()
                {
                    Id=5,
                    Title="Zmiany skórne typowe dla łuszczycy",
                    Description="Proszę stosować belosalic na zmiany skórne."
                },

            };
            List<MedicalReferral> referrals = new List<MedicalReferral>()
            {
                new MedicalReferral()
                {
                    Id=1,
                    IssueDate=now.AddDays(-15),
                    ExpireDate=now.AddDays(-15).AddMonths(3),
                    IssuedBy=MedicalWorkers.ElementAt(1),
                    IssuedTo=patient,
                    PrimaryMedicalService=PrimaryMedicalServices[7],
                    MinorMedicalService=MedicalServices[0],
                    //VisitSummary=visitSummaries.ElementAt(0)
                },
                new MedicalReferral()
                {
                    Id=2,
                    IssueDate=now.AddDays(-18),
                    ExpireDate=now.AddDays(-15).AddMonths(3),
                    IssuedBy=MedicalWorkers.ElementAt(2),
                    IssuedTo=patient,
                    PrimaryMedicalService=PrimaryMedicalServices[7],
                    MinorMedicalService=MedicalServices[1],
                    //VisitSummary=visitSummaries.ElementAt(1)

                },
                new MedicalReferral()
                {
                    Id=3,
                    IssueDate=now.AddDays(-20),
                    ExpireDate=now.AddDays(-15).AddMonths(3),
                    IssuedBy=MedicalWorkers.ElementAt(3),
                    IssuedTo=patient,
                    PrimaryMedicalService=PrimaryMedicalServices[0],
                    MinorMedicalService=MedicalServices[3],
                    //VisitSummary=visitSummaries.ElementAt(2)

                },
                new MedicalReferral()
                {
                    Id=4,
                    IssueDate=now.AddDays(-21),
                    ExpireDate=now.AddDays(-15).AddMonths(3),
                    IssuedBy=MedicalWorkers.ElementAt(4),
                    IssuedTo=patient,
                    PrimaryMedicalService=PrimaryMedicalServices[0],
                    MinorMedicalService=MedicalServices[4],
                    //VisitSummary=visitSummaries.ElementAt(4)

                },
                new MedicalReferral()
                {
                    Id=5,
                    IssueDate=now.AddDays(-22),
                    ExpireDate=now.AddDays(-15).AddMonths(3),
                    IssuedBy=MedicalWorkers.ElementAt(26),
                    IssuedTo=patient,
                    PrimaryMedicalService=PrimaryMedicalServices[32],
                },
                new MedicalReferral()
                {
                    Id=6,
                    IssueDate=now.AddDays(-23),
                    ExpireDate=now.AddDays(-15).AddMonths(3),
                    IssuedBy=MedicalWorkers.ElementAt(5),
                    IssuedTo=patient,
                    PrimaryMedicalService=PrimaryMedicalServices[23],
                    //VisitSummary=visitSummaries.ElementAt(6)

                },
                new MedicalReferral()
                {
                    Id=7,
                    IssueDate=now.AddDays(-24),
                    ExpireDate=now.AddDays(-15).AddMonths(3),
                    IssuedBy=MedicalWorkers.ElementAt(26),
                    IssuedTo=patient,
                    PrimaryMedicalService=PrimaryMedicalServices[33],
                    //VisitSummary=visitSummaries.ElementAt(7)

                },
                new MedicalReferral()
                {
                    Id=8,
                    IssueDate=now.AddDays(-24),
                    ExpireDate=now.AddDays(-15).AddMonths(3),
                    IssuedBy=MedicalWorkers.ElementAt(26),
                    IssuedTo=patient,
                    PrimaryMedicalService=PrimaryMedicalServices[34],
                    MinorMedicalService=MedicalServices[7],
                    //VisitSummary=visitSummaries.ElementAt(7)

                },
                        new MedicalReferral()
                {
                    Id=9,
                    IssueDate=now.AddDays(-24),
                    ExpireDate=now.AddDays(-15).AddMonths(3),
                    IssuedBy=MedicalWorkers.ElementAt(26),
                    IssuedTo=patient,
                    PrimaryMedicalService=PrimaryMedicalServices[34],
                    MinorMedicalService=MedicalServices[8],
                    //VisitSummary=visitSummaries.ElementAt(7)

                },
                new MedicalReferral()
                {
                    Id=10,
                    IssueDate=now.AddDays(-24),
                    ExpireDate=now.AddDays(-15).AddMonths(3),
                    IssuedBy=MedicalWorkers.ElementAt(26),
                    IssuedTo=patient,
                    PrimaryMedicalService=PrimaryMedicalServices[34],
                    MinorMedicalService=MedicalServices[9],
                    //VisitSummary=visitSummaries.ElementAt(7)

                },

                //new ExaminationReferral()
                //{
                //    Id=8,
                //    IssueDate=now.AddDays(-28),
                //    ExpireDate=now.AddDays(-15).AddMonths(3),
                //    IssuedBy=MedicalWorkers.ElementAt(10),
                //    IssuedTo=patient,
                //    MedicalService=MedicalServices[8],
                //    //VisitSummary=visitSummaries.ElementAt(9)

                //},
                //new ExaminationReferral()
                //{
                //    Id=9,
                //    IssueDate=now.AddDays(0),
                //    ExpireDate=now.AddDays(-15).AddMonths(3),
                //    IssuedBy=MedicalWorkers.ElementAt(10),
                //    IssuedTo=patient,
                //    //MedicalService=MedicalServices[10],

                //},
                //new ExaminationReferral()
                //{
                //    Id=10,
                //    IssueDate=now.AddDays(-2),
                //    ExpireDate=now.AddDays(-2).AddMonths(3),
                //    IssuedBy=MedicalWorkers.ElementAt(10),
                //    IssuedTo=patient,
                //    //MedicalService=MedicalServices[10],
                //},
            };

            List<Visit> patientHistoricalVisits = new List<Visit>()
            {

                new Visit()
                {
                    Id=1,
                    PrimaryService=PrimaryMedicalServices[7],
                    MinorMedicalServices=new List<MedicalService>(){MedicalServices[20]},
                    DateTimeSince=now.AddDays(-20).AddHours(5).AddMinutes(0),
                    DateTimeTill=now.AddDays(-20).AddHours(5).AddMinutes(15),
                    Location=Locations.ElementAt(0),
                    MedicalRoom=Locations.ElementAt(0).MedicalRooms.ElementAt(1),
                    MedicalWorker=MedicalWorkers.ElementAt(29),
                    Patient=patient,
                    VisitCategory=VisitCategories.ElementAt(0),
                                        MedicalHistory="Pacjent skarży się na problemy z układem pokarmowym, nawracające biegunki, gazy, bóle brzucha",
                    Prescription=prescriptions[4],
                    Recommendations= new List<Recommendation>   (){ recommendations[0] , recommendations[4]},
                    ExaminationReferrals=new List<MedicalReferral>(){referrals[0], referrals[1]}
                },
                new Visit()
                {
                    Id=2,
                    PrimaryService=PrimaryMedicalServices[0],
                    MinorMedicalServices=new List<MedicalService>(){MedicalServices[3], MedicalServices[5]},
                    MedicalRoom=Locations.ElementAt(0).MedicalRooms.ElementAt(0),
                    DateTimeSince=dateTimeOffset.AddDays(-20),
                    DateTimeTill=dateTimeOffset.AddDays(-20).AddMinutes(15),
                    Location=Locations.ElementAt(0),
                    MedicalWorker=MedicalWorkers.ElementAt(0),
                    Patient=patient,
                    VisitCategory=VisitCategories.ElementAt(0),
                    MedicalHistory="Swędzenie skóry, uczucie senności po posiłku",
                    Prescription=prescriptions[1],
                    Recommendations=new List<Recommendation>(){ recommendations[1] },
                    ExaminationReferrals=new List<MedicalReferral>(){referrals[2], referrals[3], referrals[8] }
                },
                new Visit()
                {
                    Id=3,
                    PrimaryService=PrimaryMedicalServices[28],
                    MinorMedicalServices=new List<MedicalService>(){ MedicalServices[20], MedicalServices[21] },
                    MedicalRoom=Locations.ElementAt(0).MedicalRooms.ElementAt(0),
                    DateTimeSince=dateTimeOffset.AddDays(-20),
                    DateTimeTill=dateTimeOffset.AddDays(-20).AddMinutes(15),
                    Location=Locations.ElementAt(0),
                    MedicalWorker=MedicalWorkers.ElementAt(51),
                    Patient=patient,
                    VisitCategory=VisitCategories.ElementAt(3),
                    MedicalHistory="RTG nadgarstka",
                    MedicalResult=  medicalTestResults[4] ,
                },
                new Visit()
                {
                    Id=4,
                    PrimaryService=PrimaryMedicalServices[9],
                    MedicalRoom=Locations.ElementAt(0).MedicalRooms.ElementAt(0),
                    DateTimeSince=dateTimeOffset.AddDays(-20),
                    DateTimeTill=dateTimeOffset.AddDays(-20).AddMinutes(15),
                    Location=Locations.ElementAt(0),
                    MedicalWorker=MedicalWorkers.ElementAt(50),
                    Patient=patient,
                    VisitCategory=VisitCategories.ElementAt(0),
                    MedicalHistory="Pacjent posiada typowe dla łuszczycy zmiany skórne: na skórze głowy oraz pod pachami. Twierdzi, że ma je ok. 3 miesięcy.",
                    Prescription=prescriptions[0],
                    Recommendations=new List<Recommendation>(){ recommendations[4] },
                },
                new Visit()
                {
                    Id=5,
                    PrimaryService=PrimaryMedicalServices[6],
                    MinorMedicalServices=new List<MedicalService>(){ MedicalServices[57] },
                    MedicalRoom=Locations.ElementAt(0).MedicalRooms.ElementAt(0),
                    DateTimeSince=dateTimeOffset.AddDays(-20),
                    DateTimeTill=dateTimeOffset.AddDays(-20).AddMinutes(15),
                    Location=Locations.ElementAt(0),
                    MedicalWorker=MedicalWorkers.ElementAt(26),
                    Patient=patient,
                    VisitCategory=VisitCategories.ElementAt(0),
                                        MedicalHistory="Podejrzenie złamania nadgarstka. Pacjent przewrócił się wczoraj na rowerze, od tego czasu odczuwa ból w okolicach nadgarstka, mocno ograniczone ruchy nadgarstka, duża opuchlizna. Skierowanie na rtg oraz zalecenie zakupu ortezy na nadgarstek.",
                    Prescription=null,
                    Recommendations=new List<Recommendation>(){ recommendations[3] }, //wybrać stomatologię zachowawczą
                                        ExaminationReferrals=new List<MedicalReferral>(){referrals[4], referrals[6]}
                },
                new Visit()
                {
                    Id=6,
                    PrimaryService=PrimaryMedicalServices[32],
                    //MinorMedicalServices=new List<MedicalService>(){ MedicalServices[21], MedicalServices[22] },
                    MedicalRoom=Locations.ElementAt(0).MedicalRooms.ElementAt(0),
                    DateTimeSince=dateTimeOffset.AddDays(-20),
                    DateTimeTill=dateTimeOffset.AddDays(-20).AddMinutes(15),
                    Location=Locations.ElementAt(0),
                    MedicalWorker=MedicalWorkers.ElementAt(3),
                    Patient=patient,
                    VisitCategory=VisitCategories.ElementAt(1),
            MedicalHistory = "Rehabilitacja nadgarstka złamanego 3 miesiące temu. Orteza noszona przez miesiąc, pacjent uskarża się na sztywność nadgarstka i lekkie bóle podczas wyginania nadgarstka.",
                },
                new Visit()
                {
                    Id=7,
                    PrimaryService=PrimaryMedicalServices[20],
                    MinorMedicalServices=new List<MedicalService>(){ MedicalServices[48], MedicalServices[49], MedicalServices[52]},
                    MedicalRoom=Locations.ElementAt(0).MedicalRooms.ElementAt(1),
                    DateTimeSince=dateTimeOffset.AddDays(-20),
                    DateTimeTill=dateTimeOffset.AddDays(-20).AddMinutes(15),
                    Location=Locations.ElementAt(0),
                    MedicalWorker=MedicalWorkers.ElementAt(9),
                    Patient=patient,
                    VisitCategory=VisitCategories.ElementAt(2),
            MedicalHistory = "Ból lewej, dolnej szóstki od kilku tygodni.",
            ExaminationReferrals=new List<MedicalReferral>() {referrals[5]},
                },
                new Visit()
                {
                    Id=8,
                    PrimaryService=PrimaryMedicalServices[34],
                    MinorMedicalServices=new List<MedicalService>(){ MedicalServices[7],MedicalServices[9]},
                    MedicalRoom=Locations.ElementAt(0).MedicalRooms.ElementAt(1),
                    DateTimeSince=dateTimeOffset.AddDays(-10),
                    DateTimeTill=dateTimeOffset.AddDays(-10).AddMinutes(15),
                    Location=Locations.ElementAt(3),
                    MedicalWorker=MedicalWorkers.ElementAt(39),
                    Patient=patient,
                    VisitCategory=VisitCategories.ElementAt(5),
                    MedicalResult=medicalTestResults[1],

                },
                                new Visit()
                {
                    Id=9,
                    PrimaryService=PrimaryMedicalServices[2],
                    MedicalRoom=Locations.ElementAt(0).MedicalRooms.ElementAt(1),
                    DateTimeSince=dateTimeOffset.AddDays(-5),
                    DateTimeTill=dateTimeOffset.AddDays(-5).AddMinutes(15),
                    Location=Locations.ElementAt(3),
                    MedicalWorker=MedicalWorkers.ElementAt(35),
                    Patient=patient,
                    VisitCategory=VisitCategories.ElementAt(0),
                    MedicalHistory="Pacjent skarży się na chroniczne zmęczenie. Wspomina też o tym, że mimo że je tyle samo co wcześniej, to ostatnio sporo przytył. Ma nadwagę, 170 cm wzrostu, 90 kg.",
                    ExaminationReferrals=new List<MedicalReferral>(){referrals[7], referrals[9] }
                },


            };
            prescriptions[0].Visit = patientHistoricalVisits.ElementAt(3);
            prescriptions[1].Visit = patientHistoricalVisits.ElementAt(1);
            //prescriptions[2].Visit = patientHistoricalVisits.ElementAt(2);
            prescriptions[4].Visit = patientHistoricalVisits.ElementAt(0);

            referrals[0].Visit = patientHistoricalVisits.ElementAt(0);
            referrals[1].Visit = patientHistoricalVisits.ElementAt(0);
            referrals[2].Visit = patientHistoricalVisits.ElementAt(1);
            referrals[3].Visit = patientHistoricalVisits.ElementAt(1);
            referrals[4].Visit = patientHistoricalVisits.ElementAt(4);
            referrals[5].Visit = patientHistoricalVisits.ElementAt(6);
            referrals[6].Visit = patientHistoricalVisits.ElementAt(4);
            referrals[7].Visit = patientHistoricalVisits.ElementAt(8);
            referrals[8].Visit = patientHistoricalVisits.ElementAt(1);
            referrals[9].Visit = patientHistoricalVisits.ElementAt(8);
            //////wyniki badan krwi i moczu
            //     medicalTestResults[0].Visit = patientHistoricalVisits.ElementAt(0);
            //krwi
            medicalTestResults[1].Visit = patientHistoricalVisits.ElementAt(7);
            //cholesterolu
            //medicalTestResults[2].Visit = patientHistoricalVisits.ElementAt(2);
            //ekg serca
            // medicalTestResults[3].Visit = patientHistoricalVisits.ElementAt(3);
            //rtg nadgarstka
            medicalTestResults[4].Visit = patientHistoricalVisits.ElementAt(2);


            List<Visit> plannedVisits = new List<Visit>()
            {
                new Visit()
                {
                    Id=10,
                    PrimaryService=PrimaryMedicalServices[0],
                    //MinorMedicalServices=new List<MedicalService>(){ MedicalServices[4] },
                    DateTimeSince=dateTimeOffset.AddDays(10),
                    DateTimeTill=dateTimeOffset.AddDays(10).AddMinutes(15),
                    Location=Locations.ElementAt(3),
                    MedicalRoom=Locations.ElementAt(3).MedicalRooms.ElementAt(6),
                    MedicalWorker=MedicalWorkers.ElementAt(36),
                    Patient=CurrentPatient,
                    VisitCategory=VisitCategories.ElementAt(0),
                },
                new Visit()
                {
                    Id=11,
                    PrimaryService=PrimaryMedicalServices[34],
                    MinorMedicalServices=new List<MedicalService>(){ MedicalServices[14], MedicalServices[15] },
                    DateTimeSince=dateTimeOffset.AddDays(14),
                    DateTimeTill=dateTimeOffset.AddDays(14).AddMinutes(15),
                    Location=Locations.ElementAt(2),
                    MedicalRoom=Locations.ElementAt(2).MedicalRooms.ElementAt(6),
                    MedicalWorker=MedicalWorkers.ElementAt(54),
                    Patient=CurrentPatient,
                    VisitCategory=VisitCategories.ElementAt(5),

                },
                new Visit()
                {
                    Id=12,
                    PrimaryService=PrimaryMedicalServices[32],
                    MinorMedicalServices=new List<MedicalService>(){ MedicalServices[0], MedicalServices[5] },
                    DateTimeSince=dateTimeOffset.AddDays(20),
                    DateTimeTill=dateTimeOffset.AddDays(20).AddMinutes(15),
                    Location=Locations.ElementAt(4),
                    MedicalRoom=Locations.ElementAt(4).MedicalRooms.ElementAt(6),
                    MedicalWorker=MedicalWorkers.ElementAt(25),
                    Patient=CurrentPatient,
                    VisitCategory=VisitCategories.ElementAt(4),
                },
                new Visit()
                {
                    Id=13,
                    PrimaryService=PrimaryMedicalServices[11],
                    //BookedMedicalServices=new List<MedicalService>(){ PrimaryMedicalServices[0], PrimaryMedicalServices[5] },
                    DateTimeSince=dateTimeOffset.AddDays(15),
                    DateTimeTill=dateTimeOffset.AddDays(15).AddMinutes(30),
                    Location=Locations.ElementAt(4),
                    MedicalRoom=Locations.ElementAt(4).MedicalRooms.ElementAt(6),
                    MedicalWorker=MedicalWorkers.ElementAt(30),
                    Patient=CurrentPatient,
                    VisitCategory=VisitCategories.ElementAt(0),
                },
            };

            patient.BookedVisits = plannedVisits;
            patient.HistoricalVisits = patientHistoricalVisits;
            patient.MedicalPackage = MedicalPackages[0];

            return patient;
        }
        //        VisitSummary="Pacjent skarży się na swędzenie skóry, mam lekką nadwagę, bywa śpiący po większym posiłku. W rodzinie są cukrzycy. Podejrzenie cykrzycy, zlecone badania"

        public static IEnumerable<MedicalService> GetMedicalServices()
        {
            List<MedicalService> services = new List<MedicalService>()
            {

                //kardiolog
                new MedicalService(){Id=91,Name="EKG spoczynkowe",Description="EKG spoczynkowe", StandardPrice=200, IsPrimaryService=false,},
                new MedicalService(){Id=92,Name="EKG wysiłkowe",Description="EKG wysiłkowe", StandardPrice=200, IsPrimaryService=false},
                new MedicalService(){Id=93,Name="Echo serca",Description="Echo serca", StandardPrice=200, IsPrimaryService=false},

                //gastrolog
                new MedicalService(){Id=6,Name="Kolonoskopia",Description="Kolonoskopia", StandardPrice=200, IsPrimaryService=false},
                new MedicalService(){Id=8,Name="Gastroskopia",Description="Gastroskopia", StandardPrice=200, IsPrimaryService=false},

                //zęby higiena
                new MedicalService(){Id=11,Name="Piaskowanie",Description="Piaskowanie", StandardPrice=100, IsPrimaryService=false},
                new MedicalService(){Id=12,Name="Fluoryzacja",Description="Fluoryzacja", StandardPrice=100, IsPrimaryService=false},


                //gabinet zabiegowy
                new MedicalService(){Id=24,Name="Podstawowe badanie krwi",Description="Podstawowe badanie krwi", StandardPrice=400, IsPrimaryService=false},
                new MedicalService(){Id=25,Name="Rozszerzone badanie krwi",Description="Rozszerzone badanie krwi", StandardPrice=400, IsPrimaryService=false},
                new MedicalService(){Id=26,Name="Badanie moczu",Description="Badanie moczu", StandardPrice=400, IsPrimaryService=false},
                new MedicalService(){Id=27,Name="Badanie kału",Description="Badanie kału", StandardPrice=400, IsPrimaryService=false},
                new MedicalService(){Id=28,Name="Test genetyczny COVID-19",Description="Test genetyczny COVID-19", StandardPrice=400, IsPrimaryService=false},
                new MedicalService(){Id=29,Name="Test antygenowy COVID-19",Description="Test antygenowy COVID-19", StandardPrice=400, IsPrimaryService=false},


                //fizykoterapia
                new MedicalService(){Id=32,Name="Krioterapia",Description="Krioterapia", StandardPrice=100, IsPrimaryService=false},
                new MedicalService(){Id=33,Name="Elektrostymulacja",Description="Elektrostymulacja", StandardPrice=100, IsPrimaryService=false},
                new MedicalService(){Id=34,Name="Laseroterpaia",Description="Laseroterpaia", StandardPrice=200, IsPrimaryService=false},
                new MedicalService(){Id=35,Name="Ultradźwięki",Description="Ultradźwięki", StandardPrice=100, IsPrimaryService=false},
                new MedicalService(){Id=36,Name="Magnetoterapia",Description="Magnetoterapia", StandardPrice=100, IsPrimaryService=false},

                //laryngolog
                new MedicalService(){Id=37,Name="Płukanie ucha",Description="Płukanie ucha", StandardPrice=50, IsPrimaryService=false},
                new MedicalService(){Id=7,Name="Audiometria",Description="Audiometria", StandardPrice=200, IsPrimaryService=false},


                //szczepienia
                new MedicalService(){Id=66,Name="Szczepienie na odrę",Description="Szczepienie na odrę", StandardPrice=100, IsPrimaryService=false},
                new MedicalService(){Id=67,Name="Szczepienie na grypę",Description="Szczepienie na grypę", StandardPrice=100, IsPrimaryService=false},
                new MedicalService(){Id=68,Name="Szczepienie na COVID-19",Description="Szczepienie na COVID-19", StandardPrice=200, IsPrimaryService=false},
                new MedicalService(){Id=69,Name="Szczepienie przeciwko wściekliźnie",Description="Szczepienie przeciwko wściekliźnie", StandardPrice=200, IsPrimaryService=false},
                new MedicalService(){Id=70,Name="Szczepienie przeciwko tężcowi",Description="Szczepienie przeciwko tężcowi", StandardPrice=100, IsPrimaryService=false},
                new MedicalService(){Id=71,Name="Szczepienie przeciwko malarii",Description="Szczepienie przeciwko malarii", StandardPrice=500, IsPrimaryService=false},
                new MedicalService(){Id=72,Name="Szczepienie przeciwko cholerze",Description="Szczepienie przeciwko cholerze", StandardPrice=100, IsPrimaryService=false},


                //okulistyka
                new MedicalService(){Id=80,Name="Topografia rogówki",Description="Topografia rogówki", StandardPrice=100, IsPrimaryService=false},
                new MedicalService(){Id=81,Name="Dobór soczewek kontaktowych",Description="Dobór soczewek kontaktowych", StandardPrice=150, IsPrimaryService=false},
                new MedicalService(){Id=82,Name="Zdjęcie dna oka",Description="Zdjęcie dna oka", StandardPrice=50, IsPrimaryService=false},
                new MedicalService(){Id=83,Name="Pachymetria",Description="Pachymetria", StandardPrice=100, IsPrimaryService=false},
                new MedicalService(){Id=84,Name="Pomiar ciśnienia wewnątrzgałkowego	",Description="Pomiar ciśnienia wewnątrzgałkowego", StandardPrice=50, IsPrimaryService=false},
                new MedicalService(){Id=5,Name="Komputerowe pole widzenia",Description="Komputerowe pole widzenia", StandardPrice=200, IsPrimaryService=false},


                //chirurg
                new MedicalService() { Id = 86, Name = "Szycie rany", Description = "Szycie rany", StandardPrice = 100, IsPrimaryService = false },
                new MedicalService() { Id = 87, Name = "Założenie szwów", Description = "Założenie szwów", StandardPrice = 100, IsPrimaryService = false },
                new MedicalService() { Id = 88, Name = "Zdjęcie szwów", Description = "Zdjęcie szwów", StandardPrice = 100, IsPrimaryService = false },
                new MedicalService() { Id = 89, Name = "Zabieg usunięcia ciała obcego", Description = "Zabieg usunięcia ciała obcego", StandardPrice = 600, IsPrimaryService = false },
                new MedicalService() { Id = 90, Name = "Biopsja otwarta", Description = "Biopsja otwarta", StandardPrice = 600, IsPrimaryService = false },
                new MedicalService(){Id=10,Name="Usunięcie paznokcia",Description="Usunięcie paznokcia", StandardPrice=100, IsPrimaryService=false},

                                //kategoria stomatologia
                //ortodoncja
                new MedicalService(){Id=77,Name="Aparat stały kryształowy",Description="Aparat stały kryształowy", StandardPrice=2500, IsPrimaryService=false},
                new MedicalService(){Id=78,Name="Aparat stały metalowy",Description="Aparat stały metalowy", StandardPrice=100, IsPrimaryService=false},
                new MedicalService(){Id=79,Name="Aparat ruchomy - płytka Schwarza",Description="Aparat ruchomy - płytka Schwarza", StandardPrice=100, IsPrimaryService=false},

                //chirurgia stomatologiczna
                new MedicalService(){Id=13,Name="Usunięcie ósemki",Description="Usunięcie ósemki", StandardPrice=100, IsPrimaryService=false},
                new MedicalService(){Id=14,Name="Usunięcie zęba jednokorzeniowego",Description="Usunięcie zęba jednokorzeniowego", StandardPrice=100, IsPrimaryService=false},
                new MedicalService(){Id=15,Name="Usunięcie zęba jednokorzeniowego wielokorzeniowego",Description="Usunięcie zęba jednokorzeniowego wielokorzeniowego", StandardPrice=100, IsPrimaryService=false},
                new MedicalService(){Id=16,Name="Usunięcie zęba mlecznego",Description="Usunięcie zęba mlecznego", StandardPrice=100, IsPrimaryService=false},
                //Stomatologiczna diagnostyka obrazowa
                new MedicalService(){Id=17,Name="Pantomogram zęba",Description="Pantomogram zęba", StandardPrice=100, IsPrimaryService=false},
                new MedicalService(){Id=18,Name="Tomografia komputerowa CBCT",Description="Tomografia komputerowa CBCT", StandardPrice=100, IsPrimaryService=false},
                //stomatologia zachowawcza
                new MedicalService(){Id=19,Name="Znieczulenie",Description="Znieczulenie", StandardPrice=50, IsPrimaryService=false},
                new MedicalService(){Id=20,Name="Wypełnienie czasowe",Description="Wypełnienie czasowe", StandardPrice=50, IsPrimaryService=false},
                new MedicalService(){Id=21,Name="Wypełnienie kompozytowe",Description="Wypełnienie kompozytowe", StandardPrice=200, IsPrimaryService=false},
                new MedicalService(){Id=22,Name="Odbudowa zęba po leczeniu kanałowym",Description="Odbudowa zęba po leczeniu kanałowym", StandardPrice=400, IsPrimaryService=false},
                new MedicalService(){Id=23,Name="Dewitalizacja",Description="Dewitalizacja", StandardPrice=100, IsPrimaryService=false},
                //protetyka
                new MedicalService(){Id=63,Name="Korona porcelanowa",Description="Korona porcelanowa", StandardPrice=800, IsPrimaryService=false},
                new MedicalService(){Id=64,Name="Licówka porcelanowa",Description="Licówka porcelanowa", StandardPrice=1600, IsPrimaryService=false},
                new MedicalService(){Id=65,Name="Korona pełnoceramiczna",Description="Korona pełnoceramiczna", StandardPrice=1600, IsPrimaryService=false},
                                //ortopeda
                new MedicalService() { Id = 85, Name = "Zdjęcie gipsu", Description = "Zdjęcie gipsu", StandardPrice = 100, IsPrimaryService = false },
                new MedicalService(){Id=9,Name="Założenie gipsu",Description="Założenie gipsu", StandardPrice=200, IsPrimaryService=false},


                new MedicalService(){Id=4,Name="Konsultacja gastrologiczna",Description="Konsultacja gastrologiczna", StandardPrice=250, IsPrimaryService=true},
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
                new MedicalService(){Id=58,Name="Ortodoncja",Description="Ortodoncja", StandardPrice=200, IsPrimaryService=true, SubServices=new List<MedicalService>(){ } },
                new MedicalService(){Id=59,Name="Chirurgia stomatologiczna",Description="Chirurgia stomatologiczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=60,Name="Stomatologiczna diagnostyka obrazowa",Description="Rentgen stomatologiczny", StandardPrice=200, IsPrimaryService=true, RequireRefferal=true},
                new MedicalService(){Id=61,Name="Protetyka",Description="Protetyka", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=62,Name="Profilaktyka stomatologiczna",Description="Profilaktyka stomatologiczna", StandardPrice=200, IsPrimaryService=true},

                new MedicalService(){Id=76,Name="Szczepienia",Description="Szczepienia", StandardPrice=100, IsPrimaryService=true },

                new MedicalService(){Id=1,Name="USG",Description="USG", StandardPrice=200, IsPrimaryService=true, RequireRefferal=true},
                new MedicalService(){Id=2,Name="RTG",Description="RTG", StandardPrice=200, IsPrimaryService=true, RequireRefferal=true},
                new MedicalService(){Id=3,Name="Rezonans magnetyczny",Description="Rezonans magnetyczny", StandardPrice=200, IsPrimaryService=true, RequireRefferal=true},
                new MedicalService(){Id=73,Name="Medycyna pracy",Description="Medycyna pracy", StandardPrice=200, IsPrimaryService=true, RequireRefferal=true},

                new MedicalService(){Id=30,Name="Masaż leczniczy",Description="Masaż leczniczy", StandardPrice=300, IsPrimaryService=true, RequireRefferal=true},
                new MedicalService(){Id=31,Name="Zajęcia rehabilitacyjne",Description="Zajęcia rehabilitacyjne", StandardPrice=300, IsPrimaryService=true, RequireRefferal=true},
                new MedicalService(){Id=75,Name="Fizykoterapia",Description="Fizykoterapia", StandardPrice=400, IsPrimaryService=true, RequireRefferal=true} ,
                new MedicalService(){Id=74,Name="Badanie laboratoryjne",Description="Badanie laboratoryjne", StandardPrice=100, IsPrimaryService=true, RequireRefferal=true},
                new MedicalService(){Id=94,Name="Badanie cholesterolu",Description="Badanie cholesterolu", StandardPrice=100, IsPrimaryService=false, RequireRefferal=true},

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
            //szczepienia

            return services;
        }

        public static IEnumerable<VisitCategory> GetVisitCategories()
        {
            List<VisitCategory> categories = new()
            {
                new VisitCategory() { Id = 1, CategoryName = "Konsultacje stacjonarne", PrimaryMedicalServices = new List<MedicalService>(MedicalServices.GetRange(38, 23)) },
                new VisitCategory() { Id = 2, CategoryName = "E-konsultacje", PrimaryMedicalServices = new List<MedicalService>(MedicalServices.GetRange(38, 23)) { } },
                new VisitCategory() { Id = 3, CategoryName = "Stomatologia", PrimaryMedicalServices = new List<MedicalService>(MedicalServices.GetRange(57, 6)) { } },
                new VisitCategory() { Id = 4, CategoryName = "Diagnostyka obrazowa ", PrimaryMedicalServices = new List<MedicalService>(MedicalServices.GetRange(1, 3)) { } },
                new VisitCategory() { Id = 5, CategoryName = "Fizjoterapia", PrimaryMedicalServices = new List<MedicalService>(MedicalServices.GetRange(31, 6)) { } },
                new VisitCategory() { Id = 6, CategoryName = "Gabinet zabiegowy", PrimaryMedicalServices = new List<MedicalService>(MedicalServices.GetRange(24, 6)) { } },
            };
            categories[0].PrimaryMedicalServices.Add(MedicalServices[0]);
            return categories;
        }


        public static Location GetLocationById(long locationId)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<Patient> GetAllPatients()
        {

            List<Patient> patients = new List<Patient>()
            {
                new Patient(Persons[70])
                {
                    Id=1,
                    EmployerNIP="845465154654",
                    MedicalPackage=MedicalPackages[0],
                    NFZUnit=NfzUnits[0]
                },
                new Patient (Persons[71]                    )
                {
                    Id=2,
                    EmployerNIP="7777742132152",
                    MedicalPackage=MedicalPackages[3],
                    NFZUnit=NfzUnits[15]
                },

                new Patient (Persons[72]                    )
                {
                    Id=3,
                    EmployerNIP="549642132152",
                    MedicalPackage=MedicalPackages[0],
                    NFZUnit=NfzUnits[0]
                },
                new Patient (Persons[73]                    )
                {
                    Id=4,
                    EmployerNIP="549642132152",
                    MedicalPackage=MedicalPackages[1],
                    NFZUnit=NfzUnits[1]
                },
                new Patient (Persons[74]                    )
                {
                    Id=5,
                    EmployerNIP="7777742132152",
                    MedicalPackage=MedicalPackages[2],
                    NFZUnit=NfzUnits[2]
                },
                new Patient (Persons[75]                    )
                {
                    Id=6,
                    EmployerNIP="7777742132152",
                    MedicalPackage=MedicalPackages[3],
                    NFZUnit=NfzUnits[3]
                },
                new Patient (Persons[76])
                {
                    Id=7,
                    EmployerNIP="7777742132152",
                    MedicalPackage=MedicalPackages[1],
                    NFZUnit=NfzUnits[4]
                },
                new Patient (Persons[77]                    )
                {
                    Id=8,
                    EmployerNIP="7777742132152",
                    MedicalPackage=MedicalPackages[0],
                    NFZUnit=NfzUnits[5]
                },
                new Patient (Persons[78]                    )
                {
                    Id=9,
                    EmployerNIP="7777742132152",
                    MedicalPackage=MedicalPackages[2],
                    NFZUnit=NfzUnits[2]
                },
                new Patient (Persons[79]                    )
                {
                    Id=10,
                    EmployerNIP="7777742132152",
                    MedicalPackage=MedicalPackages[3],
                    NFZUnit=NfzUnits[7]
                },
                new Patient (Persons[80]                    )
                {
                    Id=11,
                    EmployerNIP="984891621654",
                    MedicalPackage=MedicalPackages[2],
                    NFZUnit=NfzUnits[8]
                },
                new Patient (Persons[81]                    )
                {
                    Id=12,
                    EmployerNIP="54646516465",
                    MedicalPackage=MedicalPackages[2],
                    NFZUnit=NfzUnits[8]
                },
                new Patient (Persons[82])
                {
                    Id=13,
                    EmployerNIP="7777742132152",
                    MedicalPackage=MedicalPackages[1],
                    NFZUnit=NfzUnits[7]
                },
                new Patient (Persons[83]                    )
                {
                    Id=14,
                    EmployerNIP="54646516465",
                    MedicalPackage=MedicalPackages[1],
                    NFZUnit=NfzUnits[2]
                },
                new Patient (Persons[84]                    )
                {
                    Id=15,
                    EmployerNIP="7777742132152",
                    MedicalPackage=MedicalPackages[0],
                    NFZUnit=NfzUnits[10]
                },

            };

            return patients;


        }

        public static Patient GetPatientById(long id)
        {
            //return AllPatients.Where(c => c.Id == id).FirstOrDefault(); 
            return CurrentPatient;
        }

        public static Visit GetAvailableVisitById(long id)
        {
            throw new NotImplementedException();
        }

        public static MedicalWorker GetMedicalWorkerById(long id)
        {
            return MedicalWorkers.Where(c => c.Id == id).FirstOrDefault();
        }

        public static Visit GetHistoricalVisitById()
        {
            throw new NotImplementedException();
        }

        public static MedicalService GetMedicalServiceById(long id)
        {
            throw new NotImplementedException();
        }

        public static MedicalPackage GetMedicalPackageById(long id)
        {
            throw new NotImplementedException();
        }

        public static NFZUnit GetNFZUnitById(long id)
        {
            throw new NotImplementedException();
        }

        public static VisitCategory GetVisitCategoryById(long id)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<IEnumerable<MedicalRoom>> GetMedicalRooms()
        {
            List<List<MedicalRoom>> roomsCollections = new List<List<MedicalRoom>>()
            {
                new List<MedicalRoom>()
            {
                new MedicalRoom()
                {
                    Id = 1,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Cardiological,
                    Name = "1"
                },
                new MedicalRoom()
                {
                    Id = 2,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Dental,
                    Name = "2"
                },
                new MedicalRoom()
                {
                    Id = 3,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "3"
                },
                new MedicalRoom()
                {
                    Id = 4,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "4"
                },
                new MedicalRoom()
                {
                    Id = 5,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Gynecological,
                    Name = "5"
                },
                new MedicalRoom()
                {
                    Id = 6,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Laryngological,
                    Name = "6"
                },
                new MedicalRoom()
                {
                    Id = 7,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.MedicalImaging,
                    Name = "7"
                },
                new MedicalRoom()
                {
                    Id = 8,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Neurological,
                    Name = "8"
                },
                new MedicalRoom()
                {
                    Id = 9,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Ophthalmology,
                    Name = "9"
                },
                new MedicalRoom()
                {
                    Id = 10,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.OralHygiene,
                    Name = "10"
                },
                new MedicalRoom()
                {
                    Id = 11,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.OralHygiene,
                    Name = "11"
                },
                new MedicalRoom()
                {
                    Id = 12,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Rehabilitation,
                    Name = "12"
                },

            },
                new List<MedicalRoom>()
            {
                new MedicalRoom()
                {
                    Id=13,
                    FloorNumber=1,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Surgical,
                    Name="1A"
                },
                new MedicalRoom()
                {
                    Id=14,
                    FloorNumber=1,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Treatment,
                    Name="1B"
                },
                new MedicalRoom()
                {
                    Id=15,
                    FloorNumber=1,
                    MedicalRoomType=Core.Enums.MedicalRoomType.General,
                    Name="1C"
                },
                new MedicalRoom()
                {
                    Id=16,
                    FloorNumber=1,
                    MedicalRoomType=Core.Enums.MedicalRoomType.General,
                    Name="1D"
                },
                new MedicalRoom()
                {
                    Id=17,
                    FloorNumber=1,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Laryngological,
                    Name="1E"
                },
                new MedicalRoom()
                {
                    Id=18,
                    FloorNumber=2,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Cardiological,
                    Name="2A"
                },
                new MedicalRoom()
                {
                    Id=19,
                    FloorNumber=2,
                    MedicalRoomType=Core.Enums.MedicalRoomType.MedicalImaging,
                    Name="2B"
                },
                new MedicalRoom()
                {
                    Id=20,
                    FloorNumber=2,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Ophthalmology,
                    Name="2C"
                },
                new MedicalRoom()
                {
                    Id=21,
                    FloorNumber=2,
                    MedicalRoomType=Core.Enums.MedicalRoomType.General,
                    Name="2D"
                },
                new MedicalRoom()
                {
                    Id=22,
                    FloorNumber=3,
                    MedicalRoomType=Core.Enums.MedicalRoomType.General,
                    Name="3A"
                },
                new MedicalRoom()
                {
                    Id=23,
                    FloorNumber=3,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Treatment,
                    Name="3B"
                },
                new MedicalRoom()
                {
                    Id=24,
                    FloorNumber=3,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Cardiological,
                    Name="3C"
                },
                new MedicalRoom()
                {
                    Id=64,
                    FloorNumber=3,
                    MedicalRoomType=Core.Enums.MedicalRoomType.General,
                    Name="3C"
                },


            },
                new List<MedicalRoom>()
            {
                new MedicalRoom()
                {
                    Id=25,
                    FloorNumber=4,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Treatment,
                    Name="41"
                },
                new MedicalRoom()
                {
                    Id=26,
                    FloorNumber=1,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Treatment,
                    Name="42"
                },
                                new MedicalRoom()
                {
                Id=27,
                    FloorNumber=1,
                    MedicalRoomType=Core.Enums.MedicalRoomType.General,
                    Name="43"
                },
                new MedicalRoom()
                {
                    Id=28,
                    FloorNumber=1,
                    MedicalRoomType=Core.Enums.MedicalRoomType.MedicalImaging,
                    Name="44"
                },
                new MedicalRoom()
                {
                    Id=29,
                    FloorNumber=1,
                    MedicalRoomType=Core.Enums.MedicalRoomType.General,
                    Name="45"
                },
                new MedicalRoom()
                {
                    Id=30,
                    FloorNumber=1,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Cardiological,
                    Name="46"
                },
                new MedicalRoom()
                {
                    Id=31,
                    FloorNumber=1,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Gynecological ,
                    Name="47"
                },
                new MedicalRoom()
                {
                    Id=32,
                    FloorNumber=1,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Laryngological,
                    Name="51"
                },
                new MedicalRoom()
                {
                    Id=33,
                    FloorNumber=1,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Neurological,
                    Name="52"
                },
                new MedicalRoom()
                {
                    Id=34,
                    FloorNumber=1,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Ophthalmology,
                    Name="53"
                },
                new MedicalRoom()
                {
                    Id=35,
                    FloorNumber=1,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Rehabilitation,
                    Name="54"
                },
                new MedicalRoom()
                {
                    Id=36,
                    FloorNumber=1,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Surgical,
                    Name="55"
                },
                                new MedicalRoom()
                {
                    Id=61,
                    FloorNumber=1,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Surgical,
                    Name="56"
                },
                new MedicalRoom()
                {
                    Id=62,
                    FloorNumber=1,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Surgical,
                    Name="57"
                },
                new MedicalRoom()
                {
                    Id=63,
                    FloorNumber=1,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Surgical,
                    Name="58"
                },

            },
                new List<MedicalRoom>()
            {
                new MedicalRoom()
                {
                    Id=37,
                    FloorNumber=7,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Surgical,
                    Name="2"
                },
                new MedicalRoom()
                {
                    Id=38,
                    FloorNumber=7,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Dental,
                    Name="3"
                },
                                new MedicalRoom()
                {
                    Id=39,
                    FloorNumber=7,
                    MedicalRoomType=Core.Enums.MedicalRoomType.OralHygiene,
                    Name="4"
                },
                new MedicalRoom()
                {
                    Id=40,
                    FloorNumber=7,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Cardiological,
                    Name="5"
                },
                new MedicalRoom()
                {
                    Id=41,
                    FloorNumber=7,
                    MedicalRoomType=Core.Enums.MedicalRoomType.General,
                    Name="6"
                },
                new MedicalRoom()
                {
                    Id=42,
                    FloorNumber=7,
                    MedicalRoomType=Core.Enums.MedicalRoomType.General,
                    Name="7"
                },
                new MedicalRoom()
                {
                    Id=43,
                    FloorNumber=7,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Rehabilitation,
                    Name="8"
                },
                new MedicalRoom()
                {
                    Id=44,
                    FloorNumber=7,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Treatment,
                    Name="9"
                },
                new MedicalRoom()
                {
                    Id=45,
                    FloorNumber=7,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Cardiological,
                    Name="10"
                },
                new MedicalRoom()
                {
                    Id=46,
                    FloorNumber=7,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Neurological,
                    Name="11"
                },
                new MedicalRoom()
                {
                    Id=47,
                    FloorNumber=7,
                    MedicalRoomType=Core.Enums.MedicalRoomType.MedicalImaging,
                    Name="12"
                },
                new MedicalRoom()
                {
                    Id=48,
                    FloorNumber=7,
                    MedicalRoomType=Core.Enums.MedicalRoomType.General,
                    Name="13"
                },

            },
                new List<MedicalRoom>()
            {
                new MedicalRoom()
                {
                    Id=49,
                    FloorNumber=2,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Cardiological,
                    Name="A"
                },
                new MedicalRoom()
                {
                    Id=50,
                    FloorNumber=2,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Cardiological,
                    Name="B"
                },
                                new MedicalRoom()
                {
                    Id=51,
                    FloorNumber=2,
                    MedicalRoomType=Core.Enums.MedicalRoomType.General,
                    Name="C"
                },
                new MedicalRoom()
                {
                    Id=52,
                    FloorNumber=2,
                    MedicalRoomType=Core.Enums.MedicalRoomType.General,
                    Name="D"
                },
                new MedicalRoom()
                {
                    Id=53,
                    FloorNumber=2,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Gynecological,
                    Name="E"
                },
                new MedicalRoom()
                {
                    Id=54,
                    FloorNumber=2,
                    MedicalRoomType=Core.Enums.MedicalRoomType.MedicalImaging,
                    Name="F"
                },
                new MedicalRoom()
                {
                    Id=55,
                    FloorNumber=3,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Surgical,
                    Name="G"
                },
                new MedicalRoom()
                {
                    Id=56,
                    FloorNumber=3,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Treatment,
                    Name="H"
                },
                new MedicalRoom()
                {
                    Id=57,
                    FloorNumber=3,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Treatment,
                    Name="I"
                },
                new MedicalRoom()
                {
                    Id=58,
                    FloorNumber=3,
                    MedicalRoomType=Core.Enums.MedicalRoomType.General,
                    Name="J"
                },
                new MedicalRoom()
                {
                    Id=59,
                    FloorNumber=3,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Cardiological,
                    Name="K"
                },
                new MedicalRoom()
                {
                    Id=60,
                    FloorNumber=3,
                    MedicalRoomType=Core.Enums.MedicalRoomType.General,
                    Name="L"
                },
            },
                new List<MedicalRoom>()
            {
                new MedicalRoom()
                {
                    Id = 65,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Cardiological,
                    Name = "1"
                },
                new MedicalRoom()
                {
                    Id = 66,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Dental,
                    Name = "2"
                },
                new MedicalRoom()
                {
                    Id = 67,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "3"
                },
                new MedicalRoom()
                {
                    Id = 68,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "4"
                },
                new MedicalRoom()
                {
                    Id = 69,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Gynecological,
                    Name = "5"
                },
                new MedicalRoom()
                {
                    Id = 70,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Laryngological,
                    Name = "6"
                },
                new MedicalRoom()
                {
                    Id = 71,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.MedicalImaging,
                    Name = "7"
                },
                new MedicalRoom()
                {
                    Id = 72,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Neurological,
                    Name = "8"
                },
                new MedicalRoom()
                {
                    Id = 73,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Ophthalmology,
                    Name = "9"
                },
                new MedicalRoom()
                {
                    Id = 74,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.OralHygiene,
                    Name = "10"
                },
                new MedicalRoom()
                {
                    Id = 75,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.OralHygiene,
                    Name = "11"
                },
                new MedicalRoom()
                {
                    Id = 76,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Rehabilitation,
                    Name = "12"
                },
                new MedicalRoom()
                {
                    Id = 77,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "13"
                },
                new MedicalRoom()
                {
                    Id = 78,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "14"
                },
                new MedicalRoom()
                {
                    Id = 79,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "15"
                },
                new MedicalRoom()
                {
                    Id = 80,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "16"
                },

            },
            };

            return roomsCollections;
        }

        public static MedicalRoom GetMedicalRoomById()
        {
            throw new NotImplementedException();
        }

        public static Visit GetHistoricalVisitById(long id)
        {
            Visit visit = CurrentPatient.HistoricalVisits.Where(c => c.Id == id).FirstOrDefault();
            return visit;
        }
    }
}
