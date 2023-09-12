using Asklepios.Core.Enums;
using Asklepios.Core.Models;
using Asklepios.Core.Extensions;
using Asklepios.Data.DBContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;

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
        [NotMapped]
        public static ICollection<Visit> AvailableVisits
        {
            get
            {
                return FutureVisits.Where(c => c.VisitStatus == Core.Enums.VisitStatus.AvailableNotBooked).ToList();
            }
        }
        [NotMapped]
        public static ICollection<Visit> BookedVisits
        {
            get
            {
                //BookedVisits =
                return FutureVisits.Where(c => c.VisitStatus == Core.Enums.VisitStatus.Booked).ToList();
            }
        }
        [NotMapped]
        public static List<Visit> FutureVisits { get; set; }
        [NotMapped]
        public static List<Visit> HistoricalVisits { get; set; }

        public static List<Location> Locations { get; set; }
        //public Patient Patient { get; set; }

        public static List<MedicalWorker> MedicalWorkers { get; set; }
        public static List<MedicalService> MedicalServices { get; set; }
        public static List<MedicalServiceDiscount> MedicalServiceDiscounts { get; private set; }
        public static List<MedicalService> PrimaryMedicalServices { get; set; }
        public static List<VisitCategory> VisitCategories { get; set; }
        public static List<MedicalPackage> MedicalPackages { get; set; }
        public static List<NFZUnit> NfzUnits { get; set; }
        public static List<Patient> AllPatients { get; set; }
        public static List<MedicalRoom> MedicalRooms { get; set; }
        public static List<Recommendation> Recommendations { get; set; }
        public static List<MedicalReferral> MedicalReferrals { get; set; }
        public static List<Prescription> Prescriptions { get; set; }
        public static List<IssuedMedicine> IssuedMedicines { get; set; }
        public static List<MedicalTestResult> MedicalTestResults { get; set; }
        public static List<Notification> Notifications { get; set; }
        public static List<VisitReview> VisitReviews { get; set; }
        public static List<IdentityRole<long>> IdentityRoles { get; set; }
        public static List<IdentityUserRole<long>> IdentityUserRoles { get; set; }

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
        //private static long _patientId;
        internal static Visit GetBookedVisitById(long currentVisitId)
        {
            Visit visit = BookedVisits.Where(d => d.Id == currentVisitId).FirstOrDefault();
            return visit;
        }

        public static Patient CurrentPatient { get; set; }
        public static List<Person> Persons { get; set; }
        public static List<Visit> AllVisits
        {
            get
            {
                List<Visit> visits = new List<Visit>();
                visits.AddRange(HistoricalVisits);
                visits.AddRange(FutureVisits);
                return visits;
            }
        }
        public static List<MinorServiceToVisit> MinorServicesToVisits { get; set; } //= new List<MinorServiceToVisit>();
        public static bool IsCreated;

        public static void SetData()
        {
            IsCreated = true;
            //Facilities = GetFacilities();
            Persons = GetAllPersons();
            Users = GetAllUsers();
            Notifications = new List<Notification>();
            //Rooms = GetMedicalRooms().ToList();
            NfzUnits = GetNFZUnits().ToList();
            MedicalServices = GetMedicalServices().ToList();
            MedicalServiceDiscounts = GetMedicalServiceDiscounts();
            Recommendations = GetSomeRecommendations();
            MedicalPackages = GetMedicalPackages().ToList();
            AllPatients = GetAllPatients().ToList();
            PrimaryMedicalServices = MedicalServices.Where(c => c.IsPrimaryService == true).ToList();
            VisitCategories = GetVisitCategories().ToList();
            MedicalRooms = GetMedicalRooms().ToList();
            Locations = GetAllLocations().ToList();
            MedicalWorkers = GetMedicalWorkers().ToList();
            MedicalReferrals = GetDummyMedicalReferrals(null, DateTimeOffset.Now);
            VisitReviews = GetDummyMedicalReviews().ToList();
            IssuedMedicines = GetDummyMedicines();
            Prescriptions = GetDummyPrescriptions(DateTimeOffset.Now);
            MedicalTestResults = GetDummyMedicalTestResults();            //AllPatients = GetAllPatients().ToList();
            FutureVisits = GetFutureVisits().ToList();
            HistoricalVisits = GetHistoricalVisits().ToList();

            //FillManyToManyRelationsForVisit();
            PasswordHasher<User> ph = new PasswordHasher<User>();
            Users.ToList().ForEach(c => c.PasswordHash = ph.HashPassword(c, c.PasswordHash));
            Users.ForEach(c => c.NormalizedEmail = c.Email.ToUpper());
            Users.ForEach(c => c.NormalizedUserName = c.UserName.ToUpper());
            Users.ForEach(c => c.SecurityStamp = Guid.NewGuid().ToString("D"));
            Users.ForEach(c => c.ConcurrencyStamp = Guid.NewGuid().ToString("D"));

            //PasswordHasher<User> hasher = new PasswordHasher<User>();
            //users.ForEach(c => c.PasswordHash = hasher.HashPassword(c, c.PasswordHash));
            //users.ForEach(c => c.SecurityStamp = Guid.NewGuid().ToString("D"));

            IdentityRoles = GetIdentityRoles();
            IdentityUserRoles = AddUsersToRoles(Users, IdentityRoles);
            BookRandomVisits();
            AddMinorServicesToVisitsRelations(HistoricalVisits);
        }
        private static List<IdentityUserRole<long>> AddUsersToRoles(List<User> allUsers, List<IdentityRole<long>> allIdentityRoles)
        {
            List<IdentityUserRole<long>> mix = new List<Microsoft.AspNetCore.Identity.IdentityUserRole<long>>();

            List<User> users = allUsers.Where(c => c.UserType == UserType.Patient).ToList();
            IdentityRole<long> identityRole = allIdentityRoles.First(c => c.Name == "Patient");

            foreach (var user in users)
            {
                mix.Add(new Microsoft.AspNetCore.Identity.IdentityUserRole<long>() { RoleId = identityRole.Id, UserId = user.Id });
            }


            users = allUsers.Where(c => c.WorkerModuleType == WorkerModuleType.CustomerServiceModule).ToList();
            identityRole = allIdentityRoles.First(c => c.Name == "CustomerService");

            foreach (var user in users)
            {
                mix.Add(new Microsoft.AspNetCore.Identity.IdentityUserRole<long>() { RoleId = identityRole.Id, UserId = user.Id });
            }
            users = allUsers.Where(c => c.WorkerModuleType == WorkerModuleType.AdministrativeWorkerModule).ToList();
            identityRole = allIdentityRoles.First(c => c.Name == "AdministrativeWorker");

            foreach (var user in users)
            {
                mix.Add(new Microsoft.AspNetCore.Identity.IdentityUserRole<long>() { RoleId = identityRole.Id, UserId = user.Id });
            }
            users = allUsers.Where(c => c.WorkerModuleType == WorkerModuleType.MedicalWorkerModule).ToList();
            identityRole = allIdentityRoles.First(c => c.Name == "MedicalWorker");

            foreach (var user in users)
            {
                mix.Add(new Microsoft.AspNetCore.Identity.IdentityUserRole<long>() { RoleId = identityRole.Id, UserId = user.Id });
            }
            return mix;
        }
        private static List<IdentityRole<long>> GetIdentityRoles()
        {

            string[] roles = new string[] { IdentityRoleTypes.AdministrativeWorker.GetDescription(), IdentityRoleTypes.CustomerService.GetDescription(), IdentityRoleTypes.MedicalWorker.GetDescription(), IdentityRoleTypes.Patient.GetDescription() };
            List<IdentityRole<long>> roleList = new List<IdentityRole<long>>();
            //var roleStore = new RoleStore<IdentityRole>(new AsklepiosDbContext());
            int i = 0;
            foreach (string role in roles)
            {
                roleList.Add(new IdentityRole<long>()
                {
                    Id = ++i,
                    Name = role,
                    NormalizedName = role.ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString("D")
                }
                );
            }
            //new IdentityRole() { Id=new Guid()}
            return roleList;
            //return identityRoles;
        }

        //private static void FillManyToManyRelationsForVisit()
        //{
        //    long id = 0;
        //    for (int j = 0; j < PatientMockDB.AllVisits.Count; j++)
        //    {
        //        Visit v = PatientMockDB.AllVisits[j];

        //        if (v.MinorMedicalServices != null)
        //        {
        //            for (int i = 0; i < v.MinorMedicalServices.Count; i++)
        //            {
        //                MedicalService ms = v.MinorMedicalServices[i];
        //                if (ms == null)
        //                {
        //                    v.MinorMedicalServices.Remove(ms);
        //                }
        //                else
        //                {
        //                    PatientMockDB.MinorServicesToVisits.Add(new MinorServiceToVisit() { Id = ++id, MedicalServiceId = ms.Id, VisitId = v.Id });
        //                    //PatientMockDB.MinorServicesToVisits.Add(new MinorServiceToVisit() {  MedicalServiceId = ms.Id, VisitId = v.Id });

        //                }
        //            }


        //        }
        //    }
        //}

        //private static List<Facility> GetFacilities()
        //{
        //    List<Facility> facilities=new List<Facility>()
        //    {
        //    }
        //}

        internal static void SetDataHome()
        {
            IsCreated = true;
            MedicalServices = GetMedicalServices().ToList();
            PrimaryMedicalServices = MedicalServices.Where(c => c.IsPrimaryService == true).ToList();

            Locations = GetAllLocations().ToList();
        }

        internal static List<Visit> GetAllVisits()
        {
            List<Visit> visits = GetHistoricalVisits().ToList();
            visits.AddRange(GetFutureVisits());
            return visits;
        }

        private static void BookRandomVisits()
        {
            Random rnd = new Random();
            int range = AvailableVisits.Count / 8;
            //FutureVisits= FutureVisits.OrderBy(c=>c.DateTimeSince).ToList();
            List<Visit> visits = AvailableVisits.OrderBy(c => c.DateTimeSince).ToList(); //AvailableVisits.ToList();

            foreach (Patient patient in AllPatients)
            {
                for (int i = 1; i < 6; i++)
                {
                    //int number = (400 * i % AvailableVisits.Count);
                    int number = rnd.Next(range * (i - 1), range * i);

                    visits.ElementAt(number).Patient = patient;
                    visits.ElementAt(number).PatientId = patient.Id;
                    visits.ElementAt(number).VisitStatus = Core.Enums.VisitStatus.Booked;
                }
            }

            foreach (MedicalWorker medicalWorker in MedicalWorkers)
            {
                visits = AvailableVisits.Where(c => c.MedicalWorker.Id == medicalWorker.Id).ToList();
                int divider = 8;


                range = visits.Count / divider;

                int pNumber = 0;
                for (int i = 1; i < divider; i++)
                {
                    pNumber++;

                    int number = rnd.Next(range * (i - 1), range * i);
                    visits.ElementAt(number).Patient = AllPatients.ElementAt(pNumber % AllPatients.Count);
                    visits.ElementAt(number).PatientId = AllPatients.ElementAt(pNumber % AllPatients.Count).Id;
                    visits.ElementAt(number).VisitStatus = Core.Enums.VisitStatus.Booked;

                }
            }
        }

        internal static List<MedicalServiceDiscount> GetMedicalServiceDiscounts()
        {
            List<MedicalServiceDiscount> discounts = new List<MedicalServiceDiscount>();
            long id = 1;
            foreach (MedicalService service in MedicalServices)
            {
                MedicalServiceDiscount discount = new MedicalServiceDiscount() { Discount = new decimal(0.2), MedicalService = service, MedicalPackageId = 1, Id = id++, MedicalServiceId = service.Id };
                discounts.Add(discount);
            }
            foreach (MedicalService service in MedicalServices)
            {
                MedicalServiceDiscount discount = new MedicalServiceDiscount() { Discount = new decimal(0.4), MedicalService = service, MedicalPackageId = 2, Id = id++, MedicalServiceId = service.Id };
                discounts.Add(discount);
            }
            foreach (MedicalService service in MedicalServices)
            {
                MedicalServiceDiscount discount = new MedicalServiceDiscount() { Discount = new decimal(0.6), MedicalService = service, MedicalPackageId = 3, Id = id++, MedicalServiceId = service.Id };
                discounts.Add(discount);
            }
            foreach (MedicalService service in MedicalServices)
            {
                MedicalServiceDiscount discount = new MedicalServiceDiscount() { Discount = new decimal(0.8), MedicalService = service, MedicalPackageId = 4, Id = id++, MedicalServiceId = service.Id };
                discounts.Add(discount);
            }

            return discounts;
        }

        public static Person GetPersonById(long id)
        {
            return Persons.Where(c => c.Id == id).FirstOrDefault();
        }

        internal static List<User> GetAllUsers()
        {
            long id = 0;
            long pid = 0;
            List<User> users = new()
            {
                //medical workers
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordMW!" + id.ToString(), UserName = "MedicalWorker" + id.ToString(), Email = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                //cs workers                                                     
                new User() { Id = ++id, PasswordHash = "PasswordService!1", Email = "sw1@asklepios.com", UserName = "sw1", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.CustomerServiceModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordService!2", Email = "sw2@asklepios.com", UserName = "sw2", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.CustomerServiceModule, PersonId = id },
                //admin workers                     
                new User() { Id = ++id, PasswordHash = "PasswordAdmin!1", Email = "ad1@asklepios.com", UserName = "ad1", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.AdministrativeWorkerModule, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordAdmin!2", Email = "ad2@asklepios.com", UserName = "ad2", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.AdministrativeWorkerModule, PersonId = id },

                //patients
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },

                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },


                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, PasswordHash = "PasswordPatient!" + (++pid).ToString(), UserName = "patient" + pid.ToString(), Email = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },

            };
            //PasswordHasher<User> hasher = new PasswordHasher<User>();
            //users.ForEach(c => c.PasswordHash=hasher.HashPassword(c,c.PasswordHash));
            //users.ForEach(c => c.SecurityStamp = Guid.NewGuid().ToString("D"));
            // users.ForEach(c => c.Id = Guid.NewGuid().ToString("D"));
            return users;
        }

        internal static void RemoveFile(string documentPath)
        {
            if (File.Exists(documentPath))
            {
                File.Delete(documentPath);
            }

        }

        internal static void UpdateMedicalPackage(MedicalPackage newPackage, MedicalPackage oldPackage)
        {
            int index = MedicalPackages.IndexOf(oldPackage);

            MedicalPackages[index] = newPackage;
        }

        internal static void UpdateRoom(MedicalRoom newRoom, MedicalRoom oldRoom)
        {
            int index = MedicalRooms.IndexOf(oldRoom);

            MedicalRooms[index] = newRoom;
        }

        internal static void AddMedicalWorker(MedicalWorker medicalWorker)
        {
            if (medicalWorker.Id == 0)
            {
                medicalWorker.Id = Persons.Max(c => c.Id) + 1;
            }
            MedicalWorkers.Add(medicalWorker);
        }
        internal static void UpdateMedicalWorker(MedicalWorker oldMedicalWorker, MedicalWorker medicalWorker)
        {
            int index = MedicalWorkers.IndexOf(oldMedicalWorker);

            MedicalWorkers[index] = medicalWorker;
        }
        internal static void RemoveMedicalWorkerById(long id)
        {
            MedicalWorker medicalWorker = MedicalWorkers.Where(c => c.Id == id).FirstOrDefault();
            if (medicalWorker != null)
            {
                MedicalWorkers.Remove(medicalWorker);
            }
        }
        internal static void UpdateLocation(Location selectedLocation, Location oldLocation)
        {
            int index = Locations.IndexOf(oldLocation);

            Locations[index] = selectedLocation;
        }


        internal static void UpdatePatient(Patient oldPatient, Patient patient)
        {
            int index = AllPatients.IndexOf(oldPatient);
            AllPatients[index] = patient;
        }

        internal static void RemovePatientById(long id)
        {
            Patient patient = AllPatients.Where(c => c.Id == id).FirstOrDefault();
            if (patient != null)
            {
                AllPatients.Remove(patient);
            }
        }

        internal static void AddPerson(Person person)
        {
            if (person.Id == 0)
            {
                person.Id = Persons.Max(c => c.Id) + 1;
            }
            Persons.Add(person);
        }

        internal static void AddPatient(Patient patient)
        {
            if (patient.Id == 0)
            {
                patient.Id = AllPatients.Max(c => c.Id) + 1;
            }
            AllPatients.Add(patient);
        }

        internal static void AddUser(User user)
        {
            if (user.Id == 0)
            {
                user.Id = Users.Max(c => c.Id) + 1;
            }
            Users.Add(user);
        }

        internal static void RemoveVisitById(long id)
        {
            Visit visit = FutureVisits.Where(c => c.Id == id).FirstOrDefault();
            if (visit != null)
            {
                FutureVisits.Remove(visit);
            }
        }

        public static IEnumerable<Visit> GetFutureVisits()
        {
            DateTimeOffset dateTimeOffset = DateTime.Now;

            List<Visit> availableVisits = new List<Visit>();
            int dayOffset = -1;
            DateTimeOffset start = new DateTimeOffset(dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day, 8, 0, 0, new TimeSpan(0, 0, 0)).AddDays(0);
            long startId = 1;
            int locationsNumber = Locations.Count;
            //7 to za dużo i jest stackoverflow
            for (int i = 0; i <= 5; i++)
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

                //int servicesCounter = medicalWorker.MedicalServices.Where(c => c.IsPrimaryService).Count();
                //int serviceIndex = (servicesCounter-1) % (i+1);
                //MedicalService service = medicalWorker.MedicalServices.Where(c => c.IsPrimaryService).ToList().ElementAt(serviceIndex);
                //List<VisitCategory> categories = VisitCategories.Where(c => c.PrimaryMedicalServices.Any(d => d.Id == service.Id)).ToList();
                //VisitCategory visitCategory = categories[categories.Count % (i + 1)];

                //VisitCategory visitCategory = VisitCategories.Where(c => c.PrimaryMedicalServices.Any(d => d.Id == service.Id)).FirstOrDefault();

                //List<VisitCategory> categories = VisitCategories.Where(c => c.PrimaryMedicalServices.Any(d => d.Id == medicalService.Id)).ToList();

                for (int j = 0; j < MedicalWorkers.Count; j++)
                {
                    MedicalWorker medicalWorker = MedicalWorkers.ElementAt(j);

                    List<MedicalService> primaryServices = medicalWorker.MedicalServices.Where(c => c.IsPrimaryService).ToList();
                    int servicesCounter = primaryServices.Count();
                    minutsOffset = -1;

                    for (int m = 0; m < 12; m++)
                    {


                        int serviceIndex = (m + 1) % (servicesCounter);
                        MedicalService service = primaryServices.ElementAt(serviceIndex);
                        //zrobic tak zeby byly rozne kategorie
                        List<VisitCategory> categories = VisitCategories.Where(c => c.MedicalServices.Any(d => d.Id == service.Id)).ToList();
                        VisitCategory visitCategory = categories[(j + 1) % (categories.Count)];

                        Location location = Locations.ElementAt((j + 1) % (locationsNumber));
                        int roomsCounter = location.MedicalRooms.Count;
                        MedicalRoom room = location.MedicalRooms.ElementAt((j + 1) % (roomsCounter));

                        minutsOffset++;
                        Visit visit = new Visit()
                        {
                            Id = startId++,
                            PrimaryService = service,// PrimaryMedicalServices[0],
                            PrimaryServiceId = service.Id,
                            DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),

                            DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                            Location = location,
                            LocationId = location.Id,
                            VisitStatus = Core.Enums.VisitStatus.AvailableNotBooked,
                            MedicalRoom = room,
                            MedicalRoomId = room.Id,
                            MedicalWorker = medicalWorker,//MedicalWorkers.ElementAt(36),
                            MedicalWorkerId = medicalWorker.Id,
                            VisitCategory = visitCategory,//VisitCategories.ElementAt(0),,
                            VisitCategoryId = visitCategory.Id
                        };
                        availableVisits.Add(visit);
                    }

                }
            }

            //int visitCounter = 70;// 00;
            //for (int i = 0; i < AllPatients.Count; i++)
            //{
            //    AllPatients[i].BookVisit(availableVisits[visitCounter * (1) + 0 + i * 2]);
            //    AllPatients[i].BookVisit(availableVisits[visitCounter * (i + 1) + 100 + i]);
            //    AllPatients[i].BookVisit(availableVisits[visitCounter * (i + 1) + 250 + i]);
            //    AllPatients[i].BookVisit(availableVisits[visitCounter * (i + 1) + 400 + i]);
            //    AllPatients[i].BookVisit(availableVisits[visitCounter * (i + 1) + 550 + i]);
            //    AllPatients[i].BookVisit(availableVisits[visitCounter * (i + 1) + 700 + i]);
            //}
            //AddMinorServicesToVisitsRelations(availableVisits);

            return availableVisits;
        }

        internal static User GetUserById(int parsedId)
        {
            User user = Users.Where(c => c.Id == parsedId).FirstOrDefault();
            if (user != null)
            {
                user.PersonId = user.PersonId;
                user.Person = GetPersonById(user.PersonId.Value);
            }
            return user;
        }

        private static IEnumerable<Visit> GetHistoricalVisits()
        {
            DateTimeOffset dateTimeOffset = new DateTimeOffset(DateTime.Now);
            DateTimeOffset now = DateTime.Now;

            List<VisitReview> reviews = VisitReviews; //GetDummyMedicalReviews();
            List<MedicalReferral> referrals = MedicalReferrals;// GetDummyMedicalReferrals(null, now);
            List<string> medicalHistories = GetDummyMedicalHistories();
            List<Visit> historicalVisits = new List<Visit>();
            //List<Recommendation> recommendations = GetSomeRecommendations();

            Random rnd = new Random();
            long id = FutureVisits.Max(c => c.Id);


            foreach (Patient patient in AllPatients)
            {
                int numberOfVisits = rnd.Next(3, 12);

                for (int i = 0; i < numberOfVisits; i++)
                {
                    int medicalWorkerIndex = rnd.Next(0, MedicalWorkers.Count - 1);
                    MedicalWorker medicalWorker = MedicalWorkers[medicalWorkerIndex];
                    int primaryServicesCounter = medicalWorker.MedicalServices.Where(c => c.IsPrimaryService).Count();
                    int testResultIndex = rnd.Next(-MedicalTestResults.Count, MedicalTestResults.Count - 1);
                    int daysAgo = rnd.Next(0, 20);
                    int hour = rnd.Next(0, 12);
                    int quarter = rnd.Next(0, 3);
                    int medicalLocationIndex = rnd.Next(0, Locations.Count - 1);
                    int roomIndex = rnd.Next(0, Locations[medicalLocationIndex].MedicalRooms.Count - 1);
                    //int medicalReviewIndex = rnd.Next(-reviews.Count, reviews.Count - 1);
                    int prescriptionIndex = rnd.Next(-Prescriptions.Count, Prescriptions.Count - 1);
                    int referralsIndex = rnd.Next(-referrals.Count, referrals.Count - 1);
                    int medicalHistoryIndex = rnd.Next(-2, medicalHistories.Count - 1);
                    int primaryMedicalServiceIndex = rnd.Next(0, primaryServicesCounter - 1);
                    int recommendationIndex = rnd.Next(-4, Recommendations.Count - 1);
                    int reviewIndex = rnd.Next(-reviews.Count, reviews.Count - 1);
                    int reviewDaysOffset = rnd.Next(1, 15);
                    TimeSpan timeSpan1 = new TimeSpan(7, 0, 0);
                    DateTimeOffset dateTime = new DateTimeOffset(now.AddDays(-daysAgo).Date, timeSpan1);

                    TimeSpan timeSpan2 = new TimeSpan(hour, quarter * 15, 0);
                    dateTime = dateTime.Date + timeSpan2;// +timeSpan1;
                    MedicalService medicalService = medicalWorker.MedicalServices.Where(c => c.IsPrimaryService).ElementAt(primaryMedicalServiceIndex);// PrimaryMedicalServices[primaryMedicalServiceIndex];
                    long medicalServiceId = medicalService.Id;
                    //int minorServiceIndex = -1;
                    int minorServiceIndex = medicalService.SubServices == null ? -1 : medicalService.SubServices.Count() - 1;
                    MedicalService minorService = null;
                    List<long> minorMedicalServicesIds = null;//= 

                    if (minorServiceIndex >= 0)
                    {
                        minorServiceIndex = rnd.Next(0, medicalService.SubServices == null ? -1 : medicalService.SubServices.Count() - 1);
                        minorService = medicalService.SubServices.ElementAt(minorServiceIndex);
                        minorMedicalServicesIds = new List<long>() { minorService.Id };

                    }

                    Location location = null;
                    if (medicalLocationIndex >= 0)
                    {
                        location = Locations[medicalLocationIndex];
                    }
                    MedicalRoom medicalRoom = null;
                    if (roomIndex >= 0)
                    {
                        medicalRoom = location.MedicalRooms[roomIndex];
                    }
                    List<MedicalReferral> examinationReferrals = null;
                    List<long> examinationReferralsIds = null;
                    if (referralsIndex >= 0)
                    {
                        long max = referrals.Max(c => c.Id) + 1;
                        MedicalReferral referral = referrals[referralsIndex].MockClone(max);
                        MedicalReferrals.Add(referral);
                        examinationReferrals = new List<MedicalReferral>() { referral };
                        examinationReferralsIds = examinationReferrals.Select(c => c.Id).ToList();
                        // referrals.RemoveAt(referralsIndex);
                    }
                    string history = null;
                    if (medicalHistoryIndex >= 0)
                    {
                        history = medicalHistories[medicalHistoryIndex];
                    }
                    Prescription prescription = null;
                    long prescriptionId = -1;
                    if (prescriptionIndex >= 0)
                    {
                        prescription = Prescriptions[prescriptionIndex];
                        long newId = Prescriptions.Max(c => c.Id) + 1;
                        long newIMId = IssuedMedicines.Max(c => c.Id) + 1;
                        prescription = prescription.MockClone(newId, newIMId);
                        prescriptionId = prescription.Id;
                        Prescriptions.Add(prescription);
                        IssuedMedicines.AddRange(prescription.IssuedMedicines);
                    }
                    List<Recommendation> visitRecommendations = null;
                    List<long> visitRecommendationsIds = null;
                    if (recommendationIndex >= 0)
                    {

                        visitRecommendations = new List<Recommendation>() { Recommendations[recommendationIndex] };
                        long newId = Recommendations.Max(c => c.Id) + 1;

                        for (int j = 0; j < visitRecommendations.Count; j++)
                        {
                            visitRecommendations[j] = visitRecommendations[j].MockClone(newId);
                            newId++;
                        }
                        Recommendations.AddRange(visitRecommendations);

                        visitRecommendationsIds = visitRecommendations.Select(c => c.Id).ToList();
                    }
                    VisitReview review = null;
                    long reviewId = -1;
                    if (reviewIndex >= 0)
                    {
                        review = reviews[reviewIndex];
                        long max = reviews.Max(c => c.Id) + 1;
                        review = review.MockClone(max);
                        review.ReviewDate = dateTime.AddDays(reviewDaysOffset);
                        reviewId = review.Id;
                        review.ReviewerId = patient.Id;
                        review.RevieweeId = medicalWorker.Id;
                        VisitReviews.Add(review);
                    }
                    MedicalTestResult testResult = null;
                    long testResultId = -1;
                    if (testResultIndex >= 0)
                    {
                        testResult = (MedicalTestResult)MedicalTestResults[testResultIndex].Clone();
                        testResult.MedicalWorker = medicalWorker;
                        testResult.Id = MedicalTestResults.Max(c => c.Id) + 1;
                        testResult.MedicalService = medicalService;
                        testResult.MedicalServiceId = medicalService.Id;
                        testResultId = testResult.Id;
                        MedicalTestResults.Add(testResult);
                    }
                    VisitCategory visitCategory = VisitCategories.Where(c => c.MedicalServices.Any(d => d.Id == medicalService.Id)).FirstOrDefault();
                    long visitCategoryId = -1;
                    if (visitCategory != null)
                    {
                        visitCategoryId = visitCategory.Id;
                    }
                    Visit visit = new Visit()
                    {
                        Id = ++id,
                        //Patient = patient,
                        PatientId = patient.Id,
                        //MedicalWorker = medicalWorker,
                        MedicalWorkerId = medicalWorker.Id,
                        DateTimeSince = dateTime,
                        DateTimeTill = dateTime.AddMinutes(15),
                        ///Location = Locations[medicalLocationIndex],
                        LocationId = Locations[medicalLocationIndex].Id,
                        //MedicalRoom = medicalRoom,
                        MedicalRoomId = medicalRoom.Id,
                        ExaminationReferrals = examinationReferrals,
                        //ExaminatinoReferralsIds = examinationReferralsIds,
                        MedicalHistory = history,
                        //Prescription = prescription,
                        PrescriptionId = prescriptionId,
                        Recommendations = visitRecommendations,
                        //RecommendationIds = visitRecommendationsIds,
                        //PrimaryService = medicalService,
                        PrimaryServiceId = medicalServiceId,
                        //MedicalResult = testResult,
                        MedicalTestResultId = testResultId,
                        //VisitCategory = visitCategory,
                        VisitCategoryId = visitCategoryId,
                        MinorMedicalServices = new List<MedicalService>() { minorService },
                        // MinorMedicalServicesIds = minorMedicalServicesIds,
                        // VisitReview = review,
                        VisitReviewId = reviewId
                    };
                    if (testResult != null)
                    {
                        if (medicalWorker is Doctor || medicalWorker is ElectroradiologyTechnician || medicalWorker is Nurse)
                        {
                            testResult.PatientId = patient.Id;
                            testResult.MedicalWorkerId = medicalWorker.Id;
                            testResult.ExamDate = dateTime;
                            testResult.VisitId = visit.Id;
                            //visit.MedicalTestResult = testResult;
                        }
                        else
                        {
                            MedicalTestResults.Remove(testResult);
                            visit.MedicalTestResult = null;
                            visit.MedicalTestResultId = null;
                            //testResult = null;
                        }
                    }
                    if (visitRecommendations != null)
                    {
                        for (int j = 0; j < visitRecommendations.Count; j++)
                        {
                            Recommendation rec = visitRecommendations[j];
                            rec.VisitId = visit.Id;
                        }
                    }
                    if (review != null)
                    {
                        review.VisitId = visit.Id;
                        review.ReviewerId = visit.PatientId.Value;
                        review.RevieweeId = visit.MedicalWorkerId.Value;

                    }
                    if (examinationReferrals != null)
                    {
                        if (medicalWorker is Doctor)
                        {
                            examinationReferrals[0].VisitWhenIssuedId = visit.Id;
                            examinationReferrals[0].IssuedToId = patient.Id;
                            examinationReferrals[0].IssuedById = medicalWorker.Id;
                        }
                        else
                        {
                            foreach (var item in examinationReferrals)
                            {
                                visit.ExaminatinoReferralsIds = null;
                                visit.ExaminationReferrals = null;
                                MedicalReferrals.Remove(item); 
                            }
                            //examinationReferrals = null;
                        }
                    }
                    if (prescription != null)
                    {
                        if (medicalWorker is Doctor || medicalWorker is Physiotherapist || medicalWorker is Nurse)
                        {

                            prescription.IssuedToId = patient.Id;
                            prescription.IssuedById = medicalWorker.Id;
                            prescription.VisitId = visit.Id;
                        }
                        else
                        {
                            foreach (var item in prescription.IssuedMedicines)
                            {
                                IssuedMedicines.Remove(item);
                            }
                            Prescriptions.Remove(prescription);
                            visit.PrescriptionId = null;
                            //prescription = null;
                        }
                        //prescription.IssuedToId = patient.Id;
                        //prescription.
                    }
                    visit.VisitStatus = Core.Enums.VisitStatus.Finished;
                    AddNotificationsOrNot(visit, prescription, testResult);
                    historicalVisits.Add(visit);
                }

            }

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


            MedicalTestResults.RemoveAll(c => c.PatientId == 0);
            Prescriptions.RemoveAll(c => c.IssuedById == 0 || c.IssuedById == null);
            IssuedMedicines.RemoveAll(c => c.PrescriptionId == 0);
            Recommendations.RemoveAll(c => c.VisitId == 0);
            VisitReviews.RemoveAll(c => c.VisitId == 0);
            MedicalReferrals.RemoveAll(c => c.VisitWhenIssuedId == null);
            MedicalTestResults.RemoveAll(c => c.MedicalServiceId == null || c.MedicalServiceId == 0);
            historicalVisits.Where(c => c.LocationId == -1).ToList().ForEach(d => d.LocationId = null);
            historicalVisits.Where(c => c.MedicalTestResultId == -1).ToList().ForEach(d => d.MedicalTestResultId = null);
            historicalVisits.Where(c => c.MedicalRoomId == -1).ToList().ForEach(d => d.MedicalRoomId = null);
            historicalVisits.Where(c => c.MedicalWorkerId == -1).ToList().ForEach(d => d.MedicalWorkerId = null);
            historicalVisits.Where(c => c.PatientId == -1).ToList().ForEach(d => d.PatientId = null);
            historicalVisits.Where(c => c.PrescriptionId == -1).ToList().ForEach(d => d.PrescriptionId = null);
            historicalVisits.Where(c => c.PrimaryServiceId == -1).ToList().ForEach(d => d.PrimaryServiceId = null);
            historicalVisits.Where(c => c.UsedExaminationReferralId == -1).ToList().ForEach(d => d.LocationId = null);
            historicalVisits.Where(c => c.VisitCategoryId == -1).ToList().ForEach(d => d.VisitCategoryId = null);
            historicalVisits.Where(c => c.VisitReviewId == -1).ToList().ForEach(d => d.VisitReviewId = null);

            //AddMinorServicesToVisitsRelations(historicalVisits);

            return historicalVisits;
        }

        private static void AddMinorServicesToVisitsRelations(List<Visit> historicalVisits)
        {
            //long msvId = 0;

            foreach (Visit item in historicalVisits)
            {
                if (item.MinorMedicalServices != null)
                {
                    foreach (MedicalService ser in item.MinorMedicalServices)
                    {
                        if (ser != null)
                        {
                            if (MinorServicesToVisits == null)
                            {
                                MinorServicesToVisits = new List<MinorServiceToVisit>();
                            }
                            //   MinorServicesToVisits.Add(new MinorServiceToVisit() { Id = ++msvId, MedicalService = ser, MedicalServiceId = ser.Id, Visit = item, VisitId = item.Id });
                            MinorServicesToVisits.Add(new MinorServiceToVisit() { MedicalServiceId = ser.Id, VisitId = item.Id });

                        }
                    }
                }

            }
        }

        private static void AddNotificationsOrNot(Visit visit, Prescription prescription, MedicalTestResult testResult)
        {
            if (testResult != null)
            {
                Notification notification = new Notification
                {
                    DateTimeAdded = DateTimeOffset.Now,
                    EventObject = testResult,
                    EventObjectId = testResult.Id,
                    NotificationType = Core.Enums.NotificationType.TestResult,
                    PatientId = visit.PatientId.Value,
                    VisitId = visit.Id
                };
                if (Notifications?.Count > 0)
                {
                    notification.Id = Notifications.Max(c => c.Id) + 1;
                }
                else
                {
                    notification.Id = 1;
                }
                Notifications.Add(notification);
            }
            if (prescription != null)
            {
                Notification notification = new Notification();
                notification.DateTimeAdded = DateTimeOffset.Now;
                notification.EventObject = prescription;
                notification.EventObjectId = prescription.Id;
                notification.NotificationType = Core.Enums.NotificationType.Prescription;
                notification.PatientId = visit.PatientId.Value;
                notification.VisitId = visit.Id;

                if (Notifications?.Count > 0)
                {
                    notification.Id = Notifications.Max(c => c.Id) + 1;
                }
                else
                {
                    notification.Id = 1;
                }
                Notifications.Add(notification);
            }
            if (visit.ExaminationReferrals != null)
            {
                foreach (MedicalReferral item in visit.ExaminationReferrals)
                {
                    Notification notification = new Notification();
                    notification.DateTimeAdded = DateTimeOffset.Now;
                    notification.EventObject = item;
                    notification.EventObjectId = item.Id;
                    notification.NotificationType = Core.Enums.NotificationType.MedicalReferral;
                    notification.PatientId = visit.PatientId.Value;
                    notification.VisitId = visit.Id;

                    if (Notifications?.Count > 0)
                    {
                        notification.Id = Notifications.Max(c => c.Id) + 1;
                    }
                    else
                    {
                        notification.Id = 1;
                    }
                    Notifications.Add(notification);
                }
            }
        }

        private static List<string> GetDummyMedicalHistories()
        {
            List<string> histories = new List<string>()
            {
                "Pacjent skarży się na problemy z układem pokarmowym, nawracające biegunki, gazy, bóle brzucha",
                "Swędzenie skóry, uczucie senności po posiłku",
                 "Pacjent posiada typowe dla łuszczycy zmiany skórne: na skórze głowy oraz pod pachami. Twierdzi, że ma je ok. 3 miesięcy.",
                "Podejrzenie złamania nadgarstka. Pacjent przewrócił się wczoraj na rowerze, od tego czasu odczuwa ból w okolicach nadgarstka, mocno ograniczone ruchy nadgarstka, duża opuchlizna. Skierowanie na rtg oraz zalecenie zakupu ortezy na nadgarstek.",
                "Rehabilitacja nadgarstka złamanego 3 miesiące temu. Orteza noszona przez miesiąc, pacjent uskarża się na sztywność nadgarstka i lekkie bóle podczas wyginania nadgarstka.",
                "Ból lewej, dolnej szóstki od kilku tygodni.",
                "Pacjent skarży się na chroniczne zmęczenie. Wspomina też o tym, że mimo że je tyle samo co wcześniej, to ostatnio sporo przytył. Ma nadwagę, 170 cm wzrostu, 90 kg.",
                "Mocno opuchnięta kostka, pacjent odczuwa ból. Podejrzenie zwichnięcia. Zdarzenie miało miejsce 2 dni temu podczas gry w piłkę.",
                "Poczucie duszności, trudność w oddychaniu i słyszalny świst przy nim, ból w klatce peirsiowej oraz uciążliwy, suchy kaszel. Podejrzenie zatorowości płucnej",
                "Zażółcone spojówki oczu oraz skóra w niektórych miejscach, pacjent skarży się także na świąd skóry. Podejrzenie żółtaczki.",
                "Katar, kaszel, ból gardła oraz lekka gorączka od ok.  3 dni. Przeziębienie.",
                "Pacjent czuje osłabienie, ma regularne bóle brzucha. Potencjalne problemy układu pokarmowego, wątroby, nerek, trzustki. Badania diagnostyczne."
            };
            return histories;
        }

        private static List<VisitReview> GetDummyMedicalReviews()
        {
            DateTimeOffset now = DateTime.Now;
            long id = 0;
            List<VisitReview> visitReviews = new List<VisitReview>();
            for (int i = 0; i < 100; i++)
            {
                visitReviews.AddRange(
                    new List<VisitReview>()
                    {
                        new VisitReview(){Id=++id, AtmosphereRate=1,CompetenceRate=4,GeneralRate=3,ShortDescription="Lekarz w miarę kompetentny, ale chamski gbur"},
                        new VisitReview(){Id=++id,AtmosphereRate=5,CompetenceRate=2,GeneralRate=3,ShortDescription="Miły lekarz, niestety jego zalecenia nic nie pomogły"},
                        new VisitReview(){Id=++id,AtmosphereRate=4,CompetenceRate=4,GeneralRate=4,ShortDescription="Przepisane przez niego medykamenty poprawiły mój stan, ale część objawów się utrzymała."},
                        new VisitReview(){Id=++id,AtmosphereRate=5,CompetenceRate=5,GeneralRate=5,ShortDescription="Super lekarz, pomógł mi, dodatkowo jest bardzo sympatyczny i wszystko mi po kolei wyjaśnił. Lekarz-ideał."},
                        new VisitReview(){Id=++id,AtmosphereRate=2,CompetenceRate=1,GeneralRate=1,ShortDescription="Lekarza nie interesowały wyniki badań, nie interesowało co mówię, jedyne co mi zalecił, to leki przeciwbólowe!."},
                        new VisitReview(){Id=++id,AtmosphereRate=1,CompetenceRate=2,GeneralRate=2,ShortDescription="Bardzo nieprzyjemny, jego leczenie nie przyniosło większej poprawy"},
                        new VisitReview(){Id=++id,AtmosphereRate=5,CompetenceRate=5,GeneralRate=5,ShortDescription="Polecam, 100% zaangażowania w problem z jakim przychodzi się do doktora.Szczegółowo omawia choroba, natychmiastowo zleca dalsze badania i dobiera leczenie."},
                        new VisitReview(){Id=++id,AtmosphereRate=4,CompetenceRate=5,GeneralRate=5,ShortDescription="Doktor sumiennie zajmuje się pacjentem na wizycie. Odpowiada na pytania, doskonale tłumaczy sposób leczenia."},
                        new VisitReview(){Id=++id,AtmosphereRate=5,CompetenceRate=5,GeneralRate=5,ShortDescription="Bardzo dobry lekarz z ogromnym doświadczeniem, oddany pacjentom, przyjazny, można mu zaufać, rzeczowy w wyjaśnieniach." },
                        new VisitReview(){Id=++id,AtmosphereRate=4,CompetenceRate=5,GeneralRate=5,ShortDescription="Bardzo profesjonalne podejście do pacjenta. Wszystko dokładnie wyjaśnione. Serdecznie polecam!"},
                        new VisitReview(){Id=++id,AtmosphereRate=5,CompetenceRate=4,GeneralRate=4,ShortDescription="Doktor jest cudownym człowiekiem. Jest niesamowicie miły i empatyczny. Bardzo zaangażowany w problemy pacjenta. Szczegółowo wyjaśnia wszystkie wątpliwości. Na pacjenta poświęca tyle czasu ile jest niezbędne, a nie tyle ile jest w grafiku. Zaproponowane leczenie okazało sie bardzo skuteczne i znacznie poprawiło mój komfort życia."},
                        new VisitReview(){Id=++id,AtmosphereRate=4,CompetenceRate=5,GeneralRate=4,ShortDescription="Bardzo dobry lekarz, z dużym doświadczeniem. Dokładny wywiad, badania, szybka diagnoza i leczenie. Polecam."},
                        new VisitReview(){Id=++id,AtmosphereRate=4,CompetenceRate=4,GeneralRate=4,ShortDescription="Polecam, 100% zaangażowania w problem z jakim przychodzi się do doktora.Szczegółowo omawia przypadłość, natychmiast zleca dalsze badania i dobiera leczenie." },
                        new VisitReview(){Id=++id,AtmosphereRate=5,CompetenceRate=4,GeneralRate=4,ShortDescription="Bardzo miły i pomocny lekarz :) Wszystko wyjaśnił, o wszystko co istotne zapytał. Polecam!!!" },
                        new VisitReview(){Id=++id,AtmosphereRate=4,CompetenceRate=5,GeneralRate=4,ShortDescription="Było tak, jak chyba każdy pacjent oczekuje. Lekarz elokwentny i ewidentnie zna się na swoim fachu.Polecam." },
                        new VisitReview(){Id=++id,AtmosphereRate=2,CompetenceRate=2,GeneralRate=2,ShortDescription="Miałam teleporade u tego lekarza, która trwała może z 2 min z czego większość czasu mówiłam ja. Żadnego dzień dobry, Lekarz wysłuchał i wystawił zwolnienie i tyle. Zero porady, brak pytań z jego strony oprócz prośby o pesel i pytania co się dzieje. Osobiście nie polecam." },
                        new VisitReview(){Id=++id,AtmosphereRate=1,CompetenceRate=2,GeneralRate=2,ShortDescription="Nieuprzejmy, wręcz opryskliwy doktor, bez szacunku do pacjenta." },
                        new VisitReview(){Id=++id,AtmosphereRate=2,CompetenceRate=3,GeneralRate=2,ShortDescription="Doktor nie słucha, poucza (a nie doradza pacjentowi, który w końcu nie przychodzi na wizytę dla przyjemności).W rezultacie przepisuje leki, które wcześniej samemu sie zażywało bez wizyty u lekarza. Bardzo niemiła i nieprofesjonalna wizyta. Szczerze odradzam, szkoda czasu i zdrowia." },
                        new VisitReview(){Id=++id,AtmosphereRate=3,CompetenceRate=2,GeneralRate=2,ShortDescription="Pan doktor bardziej byl zainteresowany nipem do L4 niz stanem mojego zdrowia. Po skończeniu przepisanego przez Pana doktora antybiotyku nastąpiły powikłania i nawrót choroby." },
                        new VisitReview(){Id=++id,AtmosphereRate=2,CompetenceRate=3,GeneralRate=2,ShortDescription="Mało delikatnie dał mi do zrozumienia, że zabieram mu za dużo czasu, bo mam dwie sprawy a nie jedną." },
                        new VisitReview(){Id=++id,AtmosphereRate=3,CompetenceRate=1,GeneralRate=2,ShortDescription="Podczas wizyty doktor powiedział, że ludzie niepotrzebnie chcą się badać i wykonywanie badań kontrolnych jest bez sensu. Z łaską dostałam skierowanie do specjalisty, u którego okazało się, że pan doktor postawił u mnie błędną diagnozę." },
                        new VisitReview(){Id=++id,AtmosphereRate=2,CompetenceRate=2,GeneralRate=2,ShortDescription="Lekarz podczas wizyty skupione głównie nie na pacjencie, tylko na zabawie ze smartfonem. Zdawkowe odpowiedzi, leczenie nie przyniosło pożądancyh rezultatów." },
                        new VisitReview(){Id=++id,AtmosphereRate=2,CompetenceRate=4,GeneralRate=3,ShortDescription="Lekarz niemiły podczas wizyty, ale zalecone leki wydają się działać." },
                        new VisitReview(){Id=++id,AtmosphereRate=5,CompetenceRate=2,GeneralRate=2,ShortDescription="Doktor przyjazny, dopytywał o wiele rzeczy, niestety zalecone leczenie jedynie pogorszyło mój stan. więc niestety nie mogę polecić." }
                    }
                );
            }
            return visitReviews;
        }
        static List<IssuedMedicine> GetDummyMedicines()
        {
            long medicineId = 0;

            List<IssuedMedicine> issuedMedicines = new List<IssuedMedicine>()
            {
                new IssuedMedicine()
                {
                    //Dosage="Dwa razy dziennie po 1 tabletce",
                    Id = ++medicineId,
                    PackageSize = "60 tabletek",
                    MedicineName = "Metformax",
                    PaymentDiscount = 30,

                },
                        new IssuedMedicine()
                        {
                            Id = ++medicineId,
                            //Dosage="Raz dziennie 2 tabletki",
                            PackageSize = "50 tabletek",
                            MedicineName = "Metformina",
                            PaymentDiscount = 40
                        },
                        new IssuedMedicine()
                        {
                            Id = ++medicineId,
                            //Dosage="Trzy raz dziennie na zmianę skórną",
                            PackageSize = "Buteleczka 100 ml",
                            MedicineName = "Belosalic",
                            PaymentDiscount = 40

                        },
                        new IssuedMedicine(){
                            Id=++medicineId,
                            //Dosage="Dwa razy dziennie po 1 tabletce",
                            PackageSize="20 tabletek",MedicineName="Lakcid",PaymentDiscount=30},
                        new IssuedMedicine() {
                            Id=++medicineId,
                            //Dosage="Dwa razy dziennie po 1 tabletce",
                            PackageSize="30 tabletek",MedicineName="Trilac Plus",PaymentDiscount=0},
                        new IssuedMedicine() {
                            Id=++medicineId,
                            //Dosage="Raz dziennie po 1 tabletce",
                            PackageSize="30 tabletek",MedicineName="Enterol",PaymentDiscount=40},

                                                new IssuedMedicine(){
                            Id=++medicineId,
                            //Dosage="Dwa razy dziennie po 1 tabletce",
                            PackageSize="60 tabletek",MedicineName="Eltroxin",PaymentDiscount=30},
                        new IssuedMedicine() {
                            Id=++medicineId,
                            //Dosage="Dwa razy dziennie po 1 tabletce",
                            PackageSize="40 tabletek",MedicineName="Thyrozol",PaymentDiscount=10},
                        new IssuedMedicine() {
                            Id=++medicineId,
                            //Dosage="Trzy razy dziennie po 2 tabletki",
                            PackageSize="100 tabletek",MedicineName="Metoprolol",PaymentDiscount=40},
                                                new IssuedMedicine(){
                            Id=++medicineId,
                            //Dosage="Dwa razy dziennie po 1 tabletce",
                            PackageSize="100 tabletek",MedicineName="Debretin 100 mg",PaymentDiscount=30},
                        new IssuedMedicine() {
                            Id=++medicineId,
                            //Dosage="Dwa razy dziennie po 1 tabletce",
                            PackageSize="60 tabletek",MedicineName="Duspatalin 200 mg",PaymentDiscount=40},
                                                new IssuedMedicine(){
                            Id=++medicineId,
                            //Dosage="Dwa razy dziennie po 1 tabletce",
                            PackageSize="20 sztuk",MedicineName="Flavamed, 30 mg",PaymentDiscount=30},
                        new IssuedMedicine() {
                            Id=++medicineId,
                            //Dosage="Dwa razy dziennie po 1 tabletce",
                            PackageSize="150 ml",MedicineName="Prospan",PaymentDiscount=20},
                                                new IssuedMedicine(){
                            Id=++medicineId,
                            //Dosage="Dwa razy dziennie po 1 tabletce",
                            PackageSize="28 tabletek",MedicineName="Betaloc ZOK 100 mg",PaymentDiscount=30},
                        new IssuedMedicine() {
                            Id=++medicineId,
                            //Dosage="Dwa razy dziennie po 1 tabletce",
                            PackageSize="60 tabletek",MedicineName="Vivacor 25 mg",PaymentDiscount=40},
                        new IssuedMedicine(){
                            Id=++medicineId,
                            //Dosage="Dwa razy dziennie po 1 tabletce",
                            PackageSize="60 tabletek",MedicineName="Zuccarin",PaymentDiscount=50},
                        new IssuedMedicine() {
                            Id=++medicineId,
                            //Dosage="Dwa razy dziennie po 1 tabletce",
                            PackageSize="30 tabletek",MedicineName="Thionerv 600",PaymentDiscount=60},
                                                new IssuedMedicine(){
                            Id=++medicineId,
                            //Dosage="Dwa razy dziennie po 1 tabletce",
                            PackageSize="10 ampułko-strzykawek",MedicineName="Clexane 60 mg",PaymentDiscount=30},
                        new IssuedMedicine() {
                            Id=++medicineId,
                            //Dosage="Dwa razy dziennie po 1 tabletce",
                            PackageSize="20 ampułek",MedicineName="Nebbud 2 ml",PaymentDiscount=66},

                        new IssuedMedicine(){
                            Id=++medicineId,
                            //Dosage="Dwa razy dziennie po 1 tabletce",
                            PackageSize="100 ml",MedicineName="Iberogast",PaymentDiscount=20},
                        new IssuedMedicine() {
                            Id=++medicineId,
                            //Dosage="Dwa razy dziennie po 1 tabletce",
                            PackageSize="100 tabletek",MedicineName="Espumisan 40 mg",PaymentDiscount=20},
                                                new IssuedMedicine(){
                            Id=++medicineId,
                            //Dosage="Dwa razy dziennie po 1 tabletce",
                            PackageSize="28 tabletek",MedicineName="Nezyr 28 mg",PaymentDiscount=30},
                        new IssuedMedicine() {
                            Id=++medicineId,
                            //Dosage="Dwa razy dziennie po 1 tabletce",
                            PackageSize="30 tabletek",MedicineName="Apo-Pentox 400 SR 400 mg",PaymentDiscount=40},

                    };
            return issuedMedicines;
        }
        internal static List<Prescription> GetDummyPrescriptions(DateTimeOffset dateTimeOffset)
        {
            List<Prescription> prescriptions = new List<Prescription>()
            {
                new Prescription()
                {
                    AccessCode="1561",
                    Id=1,
                    //IssuedBy=(MedicalWorkers.ElementAt(0) as Doctor),
                    IssueDate= dateTimeOffset,
                    ExpirationDate=dateTimeOffset.AddMonths(1),
                    IdentificationCode="sd5f4ads5f4dsa65f46d",
                    IssuedMedicines=new List<IssuedMedicine>(IssuedMedicines.GetRange(0,3).ToList())
                    //{
                    //    //new IssuedMedicine(){
                    //    //    //Dosage="Dwa razy dziennie po 1 tabletce",
                    //    //    Id=++medicineId,
                    //    //    PackageSize="60 tabletek",
                    //    //    MedicineName="Metformax",
                    //    //    PaymentDiscount=30,

                    //    //},
                    //    //new IssuedMedicine() {
                    //    //    Id=++medicineId,
                    //    //    //Dosage="Raz dziennie 2 tabletki",
                    //    //    PackageSize="50 tabletek",
                    //    //    MedicineName="Metformina",
                    //    //    PaymentDiscount=40
                    //    //},
                    //    //new IssuedMedicine() {
                    //    //    Id=++medicineId,
                    //    //    //Dosage="Trzy raz dziennie na zmianę skórną",
                    //    //    PackageSize="Buteleczka 100 ml",
                    //    //    MedicineName="Belosalic",
                    //    //    PaymentDiscount=40
                    //    //}
                    //}
                },
                new Prescription()
                {
                    AccessCode = "7496",
                    Id = 2,
                    //IssuedBy = (MedicalWorkers.ElementAt(1) as Doctor),
                    IssueDate = dateTimeOffset.AddDays(-10),
                    ExpirationDate = dateTimeOffset.AddDays(70),
                                        IdentificationCode="u5y4fg654h6fds54gdfs",

                    IssuedMedicines = new List<IssuedMedicine>(IssuedMedicines.GetRange(3,3)),

                },
                new Prescription()
                {
                    AccessCode = "5555",
                    Id = 3,
                    //IssuedBy = (MedicalWorkers.ElementAt(2) as Doctor),
                    IssueDate = dateTimeOffset.AddDays(-20),
                    ExpirationDate = dateTimeOffset.AddDays(40),
                    IdentificationCode="asd4a5s64d65as4fsd56",

                    IssuedMedicines = new List<IssuedMedicine>(IssuedMedicines.GetRange(6,3))
                    //{

                    //}
                },
                new Prescription()
                {
                    AccessCode = "4564",
                    Id = 4,
                    //IssuedBy = (MedicalWorkers.ElementAt(2) as Doctor),
                    IssueDate = dateTimeOffset.AddDays(-20),
                    ExpirationDate = dateTimeOffset.AddDays(40),
                    IdentificationCode="dsfgdad4sf4ds56af4sd",

                    IssuedMedicines = new List<IssuedMedicine>(IssuedMedicines.GetRange(9,2))
                    //{

                    //}
                },
                new Prescription()
                {
                    AccessCode = "5478",
                    Id = 5,
                    //IssuedBy = (MedicalWorkers.ElementAt(2) as Doctor),
                    IssueDate = dateTimeOffset.AddDays(-20),
                    ExpirationDate = dateTimeOffset.AddDays(40),
                    IdentificationCode="sadsd5f4ds6f4ds65f4m",

                    IssuedMedicines = new List<IssuedMedicine>(IssuedMedicines.GetRange(11,2))
                    //{


                    //}
                },
                new Prescription()
                {
                    AccessCode = "1324",
                    Id = 6,
                    //IssuedBy = (MedicalWorkers.ElementAt(2) as Doctor),
                    IssueDate = dateTimeOffset.AddDays(-20),
                    ExpirationDate = dateTimeOffset.AddDays(40),
                    IdentificationCode="tg4564sda8f7a9f7s9io",

                    IssuedMedicines = new List<IssuedMedicine>(IssuedMedicines.GetRange(13,2))
                    //{

                    //}
                },
                new Prescription()
                {
                    AccessCode = "4123",
                    Id = 7,
                    //IssuedBy = (MedicalWorkers.ElementAt(2) as Doctor),
                    IssueDate = dateTimeOffset.AddDays(-20),
                    ExpirationDate = dateTimeOffset.AddDays(40),
                    IdentificationCode="u8n4nb4v654vs68plnyv",
                    IssuedMedicines=new List<IssuedMedicine>(IssuedMedicines.GetRange(15,2))

                },
                new Prescription()
                {
                    AccessCode = "9646",
                    Id = 8,
                    //IssuedBy = (MedicalWorkers.ElementAt(2) as Doctor),
                    IssueDate = dateTimeOffset.AddDays(-20),
                    ExpirationDate = dateTimeOffset.AddDays(40),
                    IdentificationCode="qasdfs8f97sd946pumba",

                    IssuedMedicines = new List<IssuedMedicine>(IssuedMedicines.GetRange(17,2))
                    {

                    }
                },
                new Prescription()
                {
                    AccessCode = "8521",
                    Id = 9,
                    //IssuedBy = (MedicalWorkers.ElementAt(2) as Doctor),
                    IssueDate = dateTimeOffset.AddDays(-20),
                    ExpirationDate = dateTimeOffset.AddDays(40),
                    IdentificationCode="bnvvb5546df1g32fd4aq",

                    IssuedMedicines = new List<IssuedMedicine>(IssuedMedicines.GetRange(19,2))
                    {

                    }
                },
                new Prescription()
                {
                    AccessCode = "7894",
                    Id = 10,
                    //IssuedBy = (MedicalWorkers.ElementAt(2) as Doctor),
                    IssueDate = dateTimeOffset.AddDays(-20),
                    ExpirationDate = dateTimeOffset.AddDays(40),
                    IdentificationCode="ghjfgh15446df5467vcz",

                    IssuedMedicines = new List<IssuedMedicine>(IssuedMedicines.GetRange(21,2))
                    {
                    }
                }
            };
            return prescriptions;
        }
        public static List<MedicalRoom> UpdateWithIdsAndReturnMedicalRooms(List<MedicalRoom> rooms, long id)
        {
            rooms.ForEach(c => c.LocationId = id);
            return rooms;
        }
        public static IEnumerable<Location> GetAllLocations()
        {
            //IEnumerable<MedicalRoom> roomsCollections = GetMedicalRooms();
            if (MedicalRooms == null)
            {
                MedicalRooms = GetMedicalRooms().ToList();
            }
            long id = 0;
            List<Location> locations = new List<Location>()
            {
                new Location()
                    {
                        City="Warszawa",
                        StreetAndNumber="Jerozolimskie 80",
                        Description="Ośrodek w centrum Warszawy ze świetnym dojazdem z każdej dzielnicy.",
                        //Facilities=new List<string>(){"15 gabinetów ogólno-konsultacyjnych", "Gabinet zabiegowy", "2 gabinety stomatologiczne", "Gabinet higieny jamy ustnej", "Gabinet diagnostyki obrazowej"},
                        Id=++id,
                        Name="Ośrodek Warszawa Jerozolimskie",
                        Services=new List<MedicalService>(){ PrimaryMedicalServices[0],PrimaryMedicalServices[1],PrimaryMedicalServices[2],PrimaryMedicalServices[3],PrimaryMedicalServices[4],PrimaryMedicalServices[5],PrimaryMedicalServices[6],PrimaryMedicalServices[7],PrimaryMedicalServices[8] ,PrimaryMedicalServices[9],PrimaryMedicalServices[10]}, 
                        //new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia"},
                        //VoivodeshipType=Core.Enums.VoivodeshipType.mazowieckie,
                        Aglomeration=Core.Enums.Aglomeration.Warsaw,
                        ImagePath="/img/Locations/loc1.jpeg",
                        PhoneNumber="22 780 421 433",
                        PostalCode="01-111",
                        MedicalRooms=UpdateWithIdsAndReturnMedicalRooms(MedicalRooms.GetRange(0,12),id),//roomsCollections.ElementAt(0)
                        //Latitude=52.219230.ToString(),
                        //Longtitude=20.970020.ToString()
                    },
                new Location()
                    {
                        City="Warszawa",
                        StreetAndNumber="Grójecka 100",
                        Description="Ośrodek w Warszawie w dzielnicy Ochota, z bardzo dobrym dojazdem z zachodniej części Warszawy.",
                        //Facilities=new List<string>(){"12 gabinetów ogólno-konsultacyjnych", "Gabinet zabiegowy", "Gabinet diagnostyki obrazowej", "Gabinek okulistyczny"},
                        Id=++id,
                        Name="Ośrodek Warszawa Ochota",
                        Services=new List<MedicalService>(){ PrimaryMedicalServices[10],PrimaryMedicalServices[11],PrimaryMedicalServices[12],PrimaryMedicalServices[13],PrimaryMedicalServices[14],PrimaryMedicalServices[15],PrimaryMedicalServices[16],PrimaryMedicalServices[17],PrimaryMedicalServices[18] ,PrimaryMedicalServices[19],PrimaryMedicalServices[20] },
                        //Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia"},
                        //VoivodeshipType=Core.Enums.VoivodeshipType.mazowieckie,
                                                Aglomeration=Core.Enums.Aglomeration.Warsaw,

                        ImagePath="/img/Locations/loc2.jpg",
                        PhoneNumber="22 787 477 323",
                        PostalCode="01-211",
                        MedicalRooms=UpdateWithIdsAndReturnMedicalRooms(MedicalRooms.GetRange(12,13),id),//roomsCollections.ElementAt(1)
                        //Latitude=52.210960.ToString(),
                        //Longtitude=20.976140.ToString()
                        },
                new Location()
                    {
                    City="Warszawa",
                        StreetAndNumber="KEN 20",
                        Description="Ośrodek na południu Warszawy ze świetnym dojazdem z południa Warszawy oraz regionów wzdłuż M1 oraz południowych okolic Warszawy.",
                        //Facilities=new List<string>(){"11 gabinetów ogólno-konsultacyjnych", "2 Gabinety zabiegowe", "2 Gabinety ginekologiczne", "2 gabinety stomatologiczne", "Gabinet diagnostyki obrazowej"},
                        Id=++id,
                        Name="Ośrodek Warszawa Ursynów",
                        Services=new List<MedicalService>(){ PrimaryMedicalServices[20],PrimaryMedicalServices[21],PrimaryMedicalServices[22],PrimaryMedicalServices[23],PrimaryMedicalServices[24],PrimaryMedicalServices[25],PrimaryMedicalServices[26],PrimaryMedicalServices[27],PrimaryMedicalServices[28] ,PrimaryMedicalServices[29],PrimaryMedicalServices[30] },
                        //Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia"},
                        //VoivodeshipType=Core.Enums.VoivodeshipType.mazowieckie,
                        Aglomeration=Core.Enums.Aglomeration.Warsaw,
                        ImagePath="/img/Locations/loc3.jpg",
                        PhoneNumber="22 777 600 313",
                        PostalCode="03-055",
                        MedicalRooms=UpdateWithIdsAndReturnMedicalRooms(MedicalRooms.GetRange(25,15),id),//.ForEach(c=>c.LocationId=id),//roomsCollections.ElementAt(2)
                        //Latitude=52.132490.ToString(),
                        //Longtitude=21.065310.ToString()
                    },
                new Location()
                    {
                        City="Warszawa",
                        StreetAndNumber="Malborska 15",
                        Description="Ośrodek na wschodzie Warszawy z dobrym dojazdem ze wschodnich dzielnic Warszawy a także wschodnich okolic Warszawy.",
                        //Facilities=new List<string>(){"15 gabinetów ogólno-konsultacyjnych", "Gabinet zabiegowy", "2 gabinety stomatologiczne", "Gabinet higieny jamy ustnej", "Gabinet diagnostyki obrazowej"},
                        Id=++id,//4,
                        Name="Ośrodek Warszawa Targówek",
                        Services=new List<MedicalService>(){ PrimaryMedicalServices[30],PrimaryMedicalServices[31],PrimaryMedicalServices[32],PrimaryMedicalServices[33],PrimaryMedicalServices[12],PrimaryMedicalServices[5],PrimaryMedicalServices[6],PrimaryMedicalServices[7],PrimaryMedicalServices[8] ,PrimaryMedicalServices[9],PrimaryMedicalServices[0] },

                        //Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia"},
                        //VoivodeshipType=Core.Enums.VoivodeshipType.mazowieckie,
                                                Aglomeration=Core.Enums.Aglomeration.Warsaw,

                        ImagePath="/img/Locations/loc4.jpg",
                        PhoneNumber="22 777 444 333",
                        PostalCode="02-222",
                        MedicalRooms=UpdateWithIdsAndReturnMedicalRooms(MedicalRooms.GetRange(40,12),id),//roomsCollections.ElementAt(3)
                        //Latitude=52.296230.ToString(),
                        //Longtitude=21.051420.ToString()
                        },
                    new Location()
                    {
                        City="Kraków",
                        StreetAndNumber="Paulińska 26",
                        Description="Ośrodek w Krakowie, w świetnie skomunikowanym Kazimierzu",
                        //Facilities=new List<string>(){"15 gabinetów ogólno-konsultacyjnych", "Gabinet zabiegowy", "2 gabinety stomatologiczne", "Gabinet higieny jamy ustnej", "Gabinet diagnostyki obrazowej"},
                        Id=++id,//5,
                        Name="Ośrodek Kraków Pogórze",
                        Services=new List<MedicalService>(){ PrimaryMedicalServices[15],PrimaryMedicalServices[31],PrimaryMedicalServices[32],PrimaryMedicalServices[33],PrimaryMedicalServices[14],PrimaryMedicalServices[25],PrimaryMedicalServices[26],PrimaryMedicalServices[27],PrimaryMedicalServices[28] ,PrimaryMedicalServices[29],PrimaryMedicalServices[11] },

                        //Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia"},
                        //VoivodeshipType=Core.Enums.VoivodeshipType.malopolskie,
                                                Aglomeration=Core.Enums.Aglomeration.Cracow,

                        ImagePath="/img/Locations/loc5.jpg",
                        PhoneNumber="20 300 400 111",
                        PostalCode="80-078",
                        MedicalRooms=UpdateWithIdsAndReturnMedicalRooms(MedicalRooms.GetRange(52,12),id),//roomsCollections.ElementAt(4)
                        //Latitude=50.050052.ToString(),
                        //Longtitude=19.940330.ToString()
                        },
                    new Location()
                    {
                        City="Gdańsk",
                        StreetAndNumber="Chlebnicka 11",
                        Description="Ośrodek w centrum Gdańska na popularnej Wyspie Spichrzów",
                        //Facilities=new List<string>(){"22 gabinety ogólno-konsultacyjne", "Gabinet zabiegowy", "2 gabinety stomatologiczne", "Gabinet higieny jamy ustnej", "Gabinet diagnostyki obrazowej"},
                        Id=++id,//6,
                        Name="Ośrodek Gdańsk Wyspa Spichrzów",
                        Services=new List<MedicalService>(){ PrimaryMedicalServices[18],PrimaryMedicalServices[19],PrimaryMedicalServices[20],PrimaryMedicalServices[21],PrimaryMedicalServices[22],PrimaryMedicalServices[25],PrimaryMedicalServices[26],PrimaryMedicalServices[27],PrimaryMedicalServices[28] ,PrimaryMedicalServices[29],PrimaryMedicalServices[11] },

                        //Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia"},
                        //VoivodeshipType=Core.Enums.VoivodeshipType.pomorskie,
                                                Aglomeration=Core.Enums.Aglomeration.Tricity,

                        ImagePath="/img/Locations/loc6.jpg",
                        PhoneNumber="30 500 500 241",
                        PostalCode="45-100",
                        MedicalRooms=UpdateWithIdsAndReturnMedicalRooms(MedicalRooms.GetRange(64,16),id),//roomsCollections.ElementAt(5)
                        //Latitude=54.349064.ToString(),
                        //Longtitude=18.654018.ToString()
                    },
                    new Location()
                    {
                        City="Poznań",
                        StreetAndNumber="Ogrodowa 10",
                        Description="Ośrodek położony na terenie Galerie Malta Poznań",
                        //Facilities=new List<string>(){"20 gabinetów ogólno-konsultacyjnych", "Gabinet zabiegowy", "2 gabinety stomatologiczne", "Gabinet higieny jamy ustnej", "Gabinet diagnostyki obrazowej"},
                        Id=++id,//7,
                        Name="Ośrodek Poznań Malta",
                        Services=new List<MedicalService>(){ PrimaryMedicalServices[21],PrimaryMedicalServices[22],PrimaryMedicalServices[23],PrimaryMedicalServices[24],PrimaryMedicalServices[25],PrimaryMedicalServices[32],PrimaryMedicalServices[26],PrimaryMedicalServices[27],PrimaryMedicalServices[28] ,PrimaryMedicalServices[29],PrimaryMedicalServices[1] },

                        //Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia", "Okulistyka"},
                        //VoivodeshipType=Core.Enums.VoivodeshipType.pomorskie,
                        Aglomeration=Core.Enums.Aglomeration.Poznan,
                        ImagePath="/img/locations/loc7.jpg",
                        PhoneNumber="30 500 500 241",
                        PostalCode="60-102",
                        MedicalRooms=UpdateWithIdsAndReturnMedicalRooms(MedicalRooms.GetRange(80,10),id),//roomsCollections.ElementAt(1)
                        //Latitude=52.403926.ToString(),
                        //Longtitude=16.925172.ToString()
                    },
                    new Location()
                    {
                        City="Wrocław",
                        StreetAndNumber="Kotlarska 38",
                        Description="Placówka położona nieco na wschód od ścisłego centrum. Łatwo do niej trafić, idąc prosto od strony placu Grunwaldzkiego.",
                        //Facilities=new List<string>(){"15 gabinetów ogólno-konsultacyjnych", "Gabinet zabiegowy", "2 gabinety stomatologiczne", "Gabinet higieny jamy ustnej", "Gabinet diagnostyki obrazowej"},
                        Id=++id,
                        Name="Ośrodek Wrocław Szczytnicka",
                                                Services=new List<MedicalService>(){ PrimaryMedicalServices[15],PrimaryMedicalServices[31],PrimaryMedicalServices[32],PrimaryMedicalServices[33],PrimaryMedicalServices[0],PrimaryMedicalServices[1],PrimaryMedicalServices[2],PrimaryMedicalServices[3] ,PrimaryMedicalServices[4],PrimaryMedicalServices[5] },

                        //Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia"},
                        //VoivodeshipType=Core.Enums.VoivodeshipType.pomorskie,
                        Aglomeration=Core.Enums.Aglomeration.Wroclaw,
                        ImagePath="/img/locations/loc8.jpg",
                        PhoneNumber="71 500 500 241",
                        PostalCode="50-031",
                        MedicalRooms=UpdateWithIdsAndReturnMedicalRooms(MedicalRooms.GetRange(90,14),id),//roomsCollections.ElementAt(3)
                        //Latitude=51.111614.ToString(),
                        //Longtitude=17.033046.ToString()
                    },
                    new Location()
                    {
                        City="Katowice",
                        StreetAndNumber="Teatralna 8",
                        Description="Ośrodek położony w bliskiej okolicy dworca PKP oraz Placu Wolności",
                        //Facilities=new List<string>(){"21 gabinetów ogólno-konsultacyjnych", "Gabinet zabiegowy", "2 gabinety stomatologiczne", "Gabinet higieny jamy ustnej", "Gabinet diagnostyki obrazowej"},
                        Id=++id,
                        Name="Ośrodek Kopalnia Katowice",
                        Services=new List<MedicalService>(){ PrimaryMedicalServices[15],PrimaryMedicalServices[16],PrimaryMedicalServices[17],PrimaryMedicalServices[18],PrimaryMedicalServices[14],PrimaryMedicalServices[25],PrimaryMedicalServices[6],PrimaryMedicalServices[7],PrimaryMedicalServices[8] ,PrimaryMedicalServices[9],PrimaryMedicalServices[11] },
                        //Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia", "Gastrologia"},
                        //VoivodeshipType=Core.Enums.VoivodeshipType.pomorskie,
                        Aglomeration=Core.Enums.Aglomeration.Silesia,
                        ImagePath="/img/locations/loc9.jpg",
                        PhoneNumber="32 500 500 241",
                        PostalCode="40-750",
                        MedicalRooms=UpdateWithIdsAndReturnMedicalRooms(MedicalRooms.GetRange(104,21),id),//roomsCollections.ElementAt(2)
                        //Latitude=50.259811.ToString(),
                        //Longtitude=19.024153.ToString()
                    },
            };

            //long msid = 0;
            //foreach (Location item in locations)
            //{
            //    if (item.Services!=null)
            //    {
            //        foreach (MedicalService ser in item.Services)
            //        {
            //            MedicalServiceToLocation.Add(new Core.Models.MedicalServiceToLocation() { Id = ++msid, Location = item, LocationId = item.Id, MedicalService = ser, MedicalServiceId = ser.Id });
            //        }
            //    }
            //}
            //  locations.ForEach(c=>c.SetRoomsBackReferences());
            return locations;
        }

        public static IEnumerable<MedicalPackage> GetMedicalPackages()
        {
            long id = 0;
            List<MedicalPackage> medicalPackages = new List<MedicalPackage>()
            {
                new MedicalPackage()
                {
                    Id=++id,
                    Name="Podstawowy",
                    Description="Podstawowy pakiet dla osób szukajacych podstawowej opieki zdrowotnej. W cenie pakietu są zawarte bezpłatne konsultacje z 7 specjalizacji oraz podstawowe badania",
                    //ServicesDiscounts=new Dictionary<MedicalService, decimal>(),
                    ServiceDiscounts=MedicalServiceDiscounts.Where(c=>c.MedicalPackageId==id).ToList()
                },
                new MedicalPackage()
                {
                    Id=++id,
                    Name="Srebrny",
                    Description="Srebrny pakiet jest pakietem dla osób szukajacych rozszerzonej opieki zdrowotnej. W ramach abonamentu medycznego są darmowe konsultacje u większości specjalistów, rozszerzony pakiet badań medycznych oraz 3 wizyty rehabilitacyjnE rocznie.",
                    //ServicesDiscounts=new Dictionary<MedicalService, decimal>(),
                                        ServiceDiscounts=MedicalServiceDiscounts.Where(c=>c.MedicalPackageId==id).ToList()

                },
                                new MedicalPackage()
                {
                    Id=++id,
                    Name="Złoty",
                    Description="Złoty pakiet dla osób szukajacych specjalistycznej opieki, w tym opieki dentystycznej oraz rehabilitacji.",
                    //ServicesDiscounts=new Dictionary<MedicalService, decimal>(),
                                        ServiceDiscounts=MedicalServiceDiscounts.Where(c=>c.MedicalPackageId==id).ToList()

                },
                new MedicalPackage()
                {
                    Id=++id,
                    Name="Platynowy",
                    Description="Platynowy pakiet jest pakietem dla osób szukajacych pełnej ochrony zdrowia. Wszystkie oferowane przez nas usługi są oferowane nieodpłatnie. Priorytetowa obsługa w przypadku badań/operacji niecierpiących zwłoki. ",
                    //ServicesDiscounts=new Dictionary<MedicalService, decimal>(),
                                        ServiceDiscounts=MedicalServiceDiscounts.Where(c=>c.MedicalPackageId==id).ToList()

                },
            };

            //Dictionary<MedicalService, decimal> discounts = new Dictionary<MedicalService, decimal>();
            //for (int i = 0; i < MedicalServices.Count; i++)
            //{
            //    MedicalService service = MedicalServices[i];
            //    discounts.Add(service, (decimal)0.2);
            //}
            //Dictionary<MedicalService, decimal> discounts2 = new Dictionary<MedicalService, decimal>();
            //for (int i = 0; i < MedicalServices.Count; i++)
            //{
            //    MedicalService service = MedicalServices[i];
            //    discounts2.Add(service, (decimal)0.5);
            //}
            //Dictionary<MedicalService, decimal> discounts3 = new Dictionary<MedicalService, decimal>();
            //for (int i = 0; i < MedicalServices.Count; i++)
            //{
            //    MedicalService service = MedicalServices[i];
            //    discounts3.Add(service, (decimal)0.75);
            //}
            //Dictionary<MedicalService, decimal> discounts4 = new Dictionary<MedicalService, decimal>();
            //for (int i = 0; i < MedicalServices.Count; i++)
            //{
            //    MedicalService service = MedicalServices[i];
            //    discounts4.Add(service, (decimal)1);
            //}
            //MedicalPackages[0].ServicesDiscounts = discounts;
            //MedicalPackages[1].ServicesDiscounts = discounts2;
            //MedicalPackages[2].ServicesDiscounts = discounts3;
            //MedicalPackages[3].ServicesDiscounts = discounts4;
            //medicalPackages[0].ServiceDiscounts= MedicalServiceDiscounts.Where(c => c.MedicalPackageId == medicalPackages[0].Id).ToList();
            //medicalPackages[1].ServiceDiscounts = MedicalServiceDiscounts.Where(c => c.MedicalPackageId == medicalPackages[1].Id).ToList();
            //medicalPackages[2].ServiceDiscounts = MedicalServiceDiscounts.Where(c => c.MedicalPackageId == medicalPackages[2].Id).ToList();
            //medicalPackages[3].ServiceDiscounts = MedicalServiceDiscounts.Where(c => c.MedicalPackageId == medicalPackages[3].Id).ToList();

            return medicalPackages;
        }

        internal static List<Person> GetAllPersons()
        {
            List<Person> people = new List<Person>()
            { };
            //medical workers
            people.Add(new Person(imagePath: "/img/MW/m/1.jpg", phoneNumber: "777774377", gender: Core.Enums.Gender.Male, id: 1, name: "Mariusz", surName: "Puto", pesel: "77784512598", birthDate: new DateTimeOffset(new DateTime(1977, 7, 8)), hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person1@gmail.com", aglomeration: Core.Enums.Aglomeration.Bialystok));//,
            people.Add(new Person(imagePath: "/img/MW/m/2.jpg", phoneNumber: "715772743", gender: Core.Enums.Gender.Male, id: 2, name: "Witold", surName: "Głąbek", pesel: "65101046546", birthDate: new DateTimeOffset(new DateTime(1965, 10, 10)), hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person2@gmail.com", aglomeration: Core.Enums.Aglomeration.Cracow));//,
            people.Add(new Person(imagePath: "/img/MW/m/3.jpg", phoneNumber: "715772743", gender: Core.Enums.Gender.Male, id: 3, name: "Henryk", surName: "Bąbel", pesel: "87010256123", birthDate: new DateTimeOffset(new DateTime(1987, 1, 2)), hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person3@gmail.com", aglomeration: Core.Enums.Aglomeration.Kielce));//,
            people.Add(new Person(imagePath: "/img/MW/m/4.jpg", phoneNumber: "711272743", gender: Core.Enums.Gender.Male, id: 4, name: "Ferdynand", surName: "Małolepszy", pesel: "56050834534", birthDate: new DateTimeOffset(new DateTime(1956, 05, 08)), hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person4@gmail.com", aglomeration: Core.Enums.Aglomeration.Rzeszów));//,
            people.Add(new Person(imagePath: "/img/MW/m/5.jpg", phoneNumber: "711272743", gender: Core.Enums.Gender.Male, id: 5, name: "Zenon", surName: "Krzywy", pesel: "54020246454", birthDate: new DateTimeOffset(new DateTime(1954, 2, 2)), hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person5@gmail.com", aglomeration: Core.Enums.Aglomeration.Cracow));//,
            people.Add(new Person(imagePath: "/img/MW/m/6.jpg", phoneNumber: "711272743", gender: Core.Enums.Gender.Male, id: 6, name: "Tadeusz", surName: "Nowak", pesel: "65111176546", birthDate: new DateTimeOffset(new DateTime(1965, 11, 11)), hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person6@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/MW/m/7.jpg", phoneNumber: "711272743", gender: Core.Enums.Gender.Male, id: 7, birthDate: new DateTimeOffset(new DateTime(1978, 7, 8)), name: "Tomasz", surName: "Woda", pesel: "78945646312", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person7@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/MW/m/8.jpg", phoneNumber: "711272743", gender: Core.Enums.Gender.Male, id: 8, birthDate: new DateTimeOffset(new DateTime(1975, 7, 8)), name: "Łukasz", surName: "Czekaj", pesel: "75654654646", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person8@gmail.com", aglomeration: Core.Enums.Aglomeration.Tricity));//,
            people.Add(new Person(imagePath: "/img/MW/m/9.jpg", phoneNumber: "711272743", gender: Core.Enums.Gender.Male, id: 9, birthDate: new DateTimeOffset(new DateTime(1961, 7, 8)), name: "Dariusz", surName: "Dzwonek", pesel: "61321234189", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person58@gmail.com", aglomeration: Core.Enums.Aglomeration.Cracow));//,
            people.Add(new Person(imagePath: "/img/MW/m/10.jpg", phoneNumber: "712727717", gender: Core.Enums.Gender.Male, id: 10, birthDate: new DateTimeOffset(new DateTime(1984, 7, 8)), name: "Mateusz", surName: "Chodzień", pesel: "84131321654", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person9@gmail.com", aglomeration: Core.Enums.Aglomeration.Cracow));//,
            people.Add(new Person(imagePath: "/img/MW/m/11.jpg", phoneNumber: "712727717", gender: Core.Enums.Gender.Male, id: 11, birthDate: new DateTimeOffset(new DateTime(1944, 7, 8)), name: "Leszek", surName: "Ancymon", pesel: "44445465456", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person10@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/MW/m/12.jpg", phoneNumber: "712727717", gender: Core.Enums.Gender.Male, id: 12, birthDate: new DateTimeOffset(new DateTime(1975, 7, 8)), name: "Karol", surName: "Szczęsny", pesel: "75321231654", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person11@gmail.com", aglomeration: Core.Enums.Aglomeration.Silesia));//,
            people.Add(new Person(imagePath: "/img/MW/m/13.jpg", phoneNumber: "712727717", gender: Core.Enums.Gender.Male, id: 13, birthDate: new DateTimeOffset(new DateTime(1965, 7, 8)), name: "Remigiusz", surName: "Czystka", pesel: "65421321564", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person12@gmail.com", aglomeration: Core.Enums.Aglomeration.Tricity));//,
            people.Add(new Person(imagePath: "/img/MW/m/14.jpg", phoneNumber: "712727717", gender: Core.Enums.Gender.Male, id: 14, birthDate: new DateTimeOffset(new DateTime(1979, 7, 8)), name: "Robert", surName: "Pawłowski", pesel: "79887987545", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person13@gmail.com", aglomeration: Core.Enums.Aglomeration.Cracow));//,
            people.Add(new Person(imagePath: "/img/MW/m/15.jpg", phoneNumber: "712729717", gender: Core.Enums.Gender.Male, id: 15, birthDate: new DateTimeOffset(new DateTime(1971, 7, 8)), name: "Szymon", surName: "Sosna", pesel: "71123156456", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person14@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/MW/m/16.jpg", phoneNumber: "712729717", gender: Core.Enums.Gender.Male, id: 16, birthDate: new DateTimeOffset(new DateTime(1965, 7, 8)), name: "Sergiusz", surName: "Ząbek", pesel: "65231546456", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person15@gmail.com", aglomeration: Core.Enums.Aglomeration.Tricity));//,
            people.Add(new Person(imagePath: "/img/MW/m/17.jpg", phoneNumber: "712729717", gender: Core.Enums.Gender.Male, id: 17, birthDate: new DateTimeOffset(new DateTime(1964, 7, 8)), name: "Tymoteusz", surName: "Zez", pesel: "64561231564", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person16@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/MW/m/18.jpg", phoneNumber: "712729718", gender: Core.Enums.Gender.Male, id: 18, birthDate: new DateTimeOffset(new DateTime(1945, 7, 8)), name: "Zbigniew", surName: "Korzeń", pesel: "45632132456", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person17@gmail.com", aglomeration: Core.Enums.Aglomeration.Bialystok));//,
            people.Add(new Person(imagePath: "/img/MW/m/19.jpg", phoneNumber: "712729718", gender: Core.Enums.Gender.Male, id: 19, birthDate: new DateTimeOffset(new DateTime(1949, 7, 8)), name: "Zbigniew", surName: "Osiński", pesel: "49987945646", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person18@gmail.com", aglomeration: Core.Enums.Aglomeration.Cracow));//,
            people.Add(new Person(imagePath: "/img/MW/m/20.jpg", phoneNumber: "712729718", gender: Core.Enums.Gender.Male, id: 20, birthDate: new DateTimeOffset(new DateTime(1965, 7, 8)), name: "Michał", surName: "Czosnek", pesel: "65432154656", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person19@gmail.com", aglomeration: Core.Enums.Aglomeration.Tricity));//,
            people.Add(new Person(imagePath: "/img/MW/m/21.jpg", phoneNumber: "715729718", gender: Core.Enums.Gender.Male, id: 21, birthDate: new DateTimeOffset(new DateTime(1980, 7, 8)), name: "Tomasz", surName: "Truteń", pesel: "80121316546", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person20@gmail.com", aglomeration: Core.Enums.Aglomeration.Cracow));//,
            people.Add(new Person(imagePath: "/img/MW/m/22.jpg", phoneNumber: "715729718", gender: Core.Enums.Gender.Male, id: 22, birthDate: new DateTimeOffset(new DateTime(1955, 7, 8)), name: "Bogusław", surName: "Śmiały", pesel: "55465456412", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person21@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/MW/m/23.jpg", phoneNumber: "715729778", gender: Core.Enums.Gender.Male, id: 23, birthDate: new DateTimeOffset(new DateTime(1954, 7, 8)), name: "Jan", surName: "Dutki", pesel: "54654321314", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person22@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/MW/m/24.jpg", phoneNumber: "715729778", gender: Core.Enums.Gender.Male, id: 24, birthDate: new DateTimeOffset(new DateTime(1965, 7, 8)), name: "Jarosław", surName: "Kurczak", pesel: "65461234564", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person23@gmail.com", aglomeration: Core.Enums.Aglomeration.Cracow));//,
            people.Add(new Person(imagePath: "/img/MW/m/25.jpg", phoneNumber: "715729778", gender: Core.Enums.Gender.Male, id: 25, birthDate: new DateTimeOffset(new DateTime(1965, 7, 8)), name: "Grzegorz", surName: "Grześkowiak", pesel: "65487456465", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person24@gmail.com", aglomeration: Core.Enums.Aglomeration.Cracow));//,
            people.Add(new Person(imagePath: "/img/MW/m/26.jpg", phoneNumber: "715729778", gender: Core.Enums.Gender.Male, id: 26, birthDate: new DateTimeOffset(new DateTime(1945, 7, 8)), name: "Gerwazy", surName: "Zasada", pesel: "45612315646", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person25@gmail.com", aglomeration: Core.Enums.Aglomeration.Silesia));//,
            people.Add(new Person(imagePath: "/img/MW/m/27.jpg", phoneNumber: "715729778", gender: Core.Enums.Gender.Male, id: 27, birthDate: new DateTimeOffset(new DateTime(1954, 7, 8)), name: "Czesław", surName: "Wilk", pesel: "54878975646", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person26@gmail.com", aglomeration: Core.Enums.Aglomeration.Cracow));//,
            people.Add(new Person(imagePath: "/img/MW/m/28.jpg", phoneNumber: "715729777", gender: Core.Enums.Gender.Male, id: 28, birthDate: new DateTimeOffset(new DateTime(1964, 7, 8)), name: "Tadeusz", surName: "Gąska", pesel: "64621321564", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person27@gmail.com", aglomeration: Core.Enums.Aglomeration.Bialystok));//,
            people.Add(new Person(imagePath: "/img/MW/m/29.jpg", phoneNumber: "715729777", gender: Core.Enums.Gender.Male, id: 29, birthDate: new DateTimeOffset(new DateTime(1959, 7, 8)), name: "Waldemar", surName: "Kucaj", pesel: "59456123156", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person28@gmail.com", aglomeration: Core.Enums.Aglomeration.Tricity));//,
            people.Add(new Person(imagePath: "/img/MW/m/30.jpg", phoneNumber: "715729777", gender: Core.Enums.Gender.Male, id: 30, birthDate: new DateTimeOffset(new DateTime(1978, 7, 8)), name: "Piotr", surName: "Kuropatwa", pesel: "78946513213", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person29@gmail.com", aglomeration: Core.Enums.Aglomeration.Bialystok));//,
            people.Add(new Person(imagePath: "/img/MW/m/31.jpg", phoneNumber: "715729777", gender: Core.Enums.Gender.Male, id: 31, birthDate: new DateTimeOffset(new DateTime(1978, 10, 8)), name: "Paweł", surName: "Łąkietka", pesel: "78946546549", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person30@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/MW/m/32.jpg", phoneNumber: "715729777", gender: Core.Enums.Gender.Male, id: 32, birthDate: new DateTimeOffset(new DateTime(1945, 7, 8)), name: "Rozmus", surName: "Remus", pesel: "45641341564", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person31@gmail.com", aglomeration: Core.Enums.Aglomeration.Cracow));//,
            people.Add(new Person(imagePath: "/img/MW/m/33.jpg", phoneNumber: "715729777", gender: Core.Enums.Gender.Male, id: 33, birthDate: new DateTimeOffset(new DateTime(1948, 7, 8)), name: "Miłosz", surName: "Ciapek", pesel: "48794564321", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person32@gmail.com", aglomeration: Core.Enums.Aglomeration.Silesia));//,
            people.Add(new Person(imagePath: "/img/MW/k/2.jpg", phoneNumber: "715772977", gender: Core.Enums.Gender.Female, id: 34, birthDate: new DateTimeOffset(new DateTime(1965, 7, 8)), name: "Czesława", surName: "Kret", pesel: "65461231564", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person33@gmail.com", aglomeration: Core.Enums.Aglomeration.Bialystok));//,
            people.Add(new Person(imagePath: "/img/MW/k/3.jpg", phoneNumber: "715772977", gender: Core.Enums.Gender.Female, id: 35, birthDate: new DateTimeOffset(new DateTime(1989, 7, 8)), name: "Marlena", surName: "Bajka", pesel: "89456113215", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person34@gmail.com", aglomeration: Core.Enums.Aglomeration.Tricity));//,
            people.Add(new Person(imagePath: "/img/MW/k/4.jpg", phoneNumber: "715772977", gender: Core.Enums.Gender.Female, id: 36, birthDate: new DateTimeOffset(new DateTime(1954, 7, 8)), name: "Bożena", surName: "Arbuz", pesel: "54564632165", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person35@gmail.com", aglomeration: Core.Enums.Aglomeration.Bialystok));//,
            people.Add(new Person(imagePath: "/img/MW/k/5.jpg", phoneNumber: "715772977", gender: Core.Enums.Gender.Female, id: 37, birthDate: new DateTimeOffset(new DateTime(1980, 7, 8)), name: "Klaudia", surName: "Kąkol", pesel: "80156465465", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person36@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/MW/k/6.jpg", phoneNumber: "715772977", gender: Core.Enums.Gender.Female, id: 38, birthDate: new DateTimeOffset(new DateTime(1986, 7, 8)), name: "Sandra", surName: "Sosna", pesel: "86465456464", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person37@gmail.com", aglomeration: Core.Enums.Aglomeration.Kielce));//,
            people.Add(new Person(imagePath: "/img/MW/k/7.jpg", phoneNumber: "715772977", gender: Core.Enums.Gender.Female, id: 39, birthDate: new DateTimeOffset(new DateTime(1951, 7, 8)), name: "Teodora", surName: "Wiśniowiecka", pesel: "51564894651", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person38@gmail.com", aglomeration: Core.Enums.Aglomeration.Rzeszów));//,
            people.Add(new Person(imagePath: "/img/MW/k/8.jpg", phoneNumber: "715772977", gender: Core.Enums.Gender.Female, id: 40, birthDate: new DateTimeOffset(new DateTime(1966, 7, 8)), name: "Kornelia", surName: "Krasicka", pesel: "66454564654", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person39@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/MW/k/9.jpg", phoneNumber: "715772977", gender: Core.Enums.Gender.Female, id: 41, birthDate: new DateTimeOffset(new DateTime(1975, 7, 8)), name: "Marzena", surName: "Rudnicka", pesel: "75164546546", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person40@gmail.com", aglomeration: Core.Enums.Aglomeration.Tricity));//,
            people.Add(new Person(imagePath: "/img/MW/k/10.jpg", phoneNumber: "715729773", gender: Core.Enums.Gender.Female, id: 42, birthDate: new DateTimeOffset(new DateTime(1961, 7, 8)), name: "Beata", surName: "Bomba", pesel: "61231546546", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person41@gmail.com", aglomeration: Core.Enums.Aglomeration.Silesia));//,
            people.Add(new Person(imagePath: "/img/MW/k/11.jpg", phoneNumber: "715729773", gender: Core.Enums.Gender.Female, id: 43, birthDate: new DateTimeOffset(new DateTime(1971, 7, 8)), name: "Katarzyna", surName: "Łasinkiewicz", pesel: "71123456476", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person42@gmail.com", aglomeration: Core.Enums.Aglomeration.Bialystok));//,
            people.Add(new Person(imagePath: "/img/MW/k/12.jpg", phoneNumber: "715129773", gender: Core.Enums.Gender.Female, id: 44, birthDate: new DateTimeOffset(new DateTime(1981, 7, 8)), name: "Weronika", surName: "Kurzydło", pesel: "81546546546", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person43@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/MW/k/13.jpg", phoneNumber: "715127773", gender: Core.Enums.Gender.Female, id: 45, birthDate: new DateTimeOffset(new DateTime(1978, 7, 8)), name: "Maria", surName: "Kurka", pesel: "78794654616", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person44@gmail.com", aglomeration: Core.Enums.Aglomeration.Cracow));//,
            people.Add(new Person(imagePath: "/img/MW/k/14.jpg", phoneNumber: "715127773", gender: Core.Enums.Gender.Female, id: 46, birthDate: new DateTimeOffset(new DateTime(1949, 7, 8)), name: "Bronisława", surName: "Czesiek", pesel: "49489646146", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person45@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/MW/k/15.jpg", phoneNumber: "715127773", gender: Core.Enums.Gender.Female, id: 47, birthDate: new DateTimeOffset(new DateTime(1965, 7, 8)), name: "Aleksandra", surName: "Ruda", pesel: "65487987446", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person46@gmail.com", aglomeration: Core.Enums.Aglomeration.Tricity));//,
            people.Add(new Person(imagePath: "/img/MW/k/16.jpg", phoneNumber: "715127773", gender: Core.Enums.Gender.Female, id: 48, birthDate: new DateTimeOffset(new DateTime(1978, 7, 8)), name: "Iga", surName: "Bodzio", pesel: "78484654654", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person47@gmail.com", aglomeration: Core.Enums.Aglomeration.Cracow));//,
            people.Add(new Person(imagePath: "/img/MW/k/17.jpg", phoneNumber: "715127773", gender: Core.Enums.Gender.Female, id: 49, birthDate: new DateTimeOffset(new DateTime(1984, 7, 8)), name: "Agnieszka", surName: "Pluto", pesel: "84879486546", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person48@gmail.com", aglomeration: Core.Enums.Aglomeration.Bialystok));//,
            people.Add(new Person(imagePath: "/img/MW/k/18.jpg", phoneNumber: "715127773", gender: Core.Enums.Gender.Female, id: 50, birthDate: new DateTimeOffset(new DateTime(1985, 7, 8)), name: "Karolina", surName: "Majak", pesel: "85641541321", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person49@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/MW/k/19.jpg", phoneNumber: "715127773", gender: Core.Enums.Gender.Female, id: 51, birthDate: new DateTimeOffset(new DateTime(1989, 7, 8)), name: "Karina", surName: "Wąsacz", pesel: "89456411324", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person50@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/MW/k/20.jpg", phoneNumber: "715127373", gender: Core.Enums.Gender.Female, id: 52, birthDate: new DateTimeOffset(new DateTime(1956, 7, 8)), name: "Grażyna", surName: "Rudniewska", pesel: "56413215649", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person51@gmail.com", aglomeration: Core.Enums.Aglomeration.Tricity));//,
            people.Add(new Person(imagePath: "/img/MW/k/21.jpg", phoneNumber: "715127373", gender: Core.Enums.Gender.Female, id: 53, birthDate: new DateTimeOffset(new DateTime(1984, 7, 8)), name: "Marta", surName: "Tracka", pesel: "84651654964", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person52@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/MW/k/22.jpg", phoneNumber: "715127373", gender: Core.Enums.Gender.Female, id: 54, birthDate: new DateTimeOffset(new DateTime(1986, 7, 8)), name: "Marta", surName: "Trąbicka", pesel: "86231165448", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person53@gmail.com", aglomeration: Core.Enums.Aglomeration.Bialystok));//,
            people.Add(new Person(imagePath: "/img/MW/k/23.jpg", phoneNumber: "715127373", gender: Core.Enums.Gender.Female, id: 55, birthDate: new DateTimeOffset(new DateTime(1979, 7, 8)), name: "Sylwia", surName: "Sarna", pesel: "79132131564", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person54@gmail.com", aglomeration: Core.Enums.Aglomeration.Rzeszów));//,
            people.Add(new Person(imagePath: "/img/MW/k/24.jpg", phoneNumber: "715127373", gender: Core.Enums.Gender.Female, id: 56, birthDate: new DateTimeOffset(new DateTime(1975, 7, 8)), name: "Kamila", surName: "Kozera", pesel: "75123165465", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person55@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/MW/k/25.jpg", phoneNumber: "715127373", gender: Core.Enums.Gender.Female, id: 57, birthDate: new DateTimeOffset(new DateTime(1954, 7, 8)), name: "Bogumiła", surName: "Braniewska", pesel: "54878946123", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person56@gmail.com", aglomeration: Core.Enums.Aglomeration.Cracow));//,
            people.Add(new Person(imagePath: "/img/MW/k/26.jpg", phoneNumber: "715127373", gender: Core.Enums.Gender.Female, id: 58, birthDate: new DateTimeOffset(new DateTime(1962, 7, 8)), name: "Teresa", surName: "Winniczek", pesel: "62348979521", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person57@gmail.com", aglomeration: Core.Enums.Aglomeration.Bialystok));//,
            people.Add(new Person(imagePath: "/img/MW/k/27.jpg", phoneNumber: "715127373", gender: Core.Enums.Gender.Female, id: 59, birthDate: new DateTimeOffset(new DateTime(1974, 7, 8)), name: "Daria", surName: "Jaszczur", pesel: "74561213898", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person59@gmail.com", aglomeration: Core.Enums.Aglomeration.Tricity));//,
            people.Add(new Person(imagePath: "/img/MW/k/28.jpg", phoneNumber: "715177373", gender: Core.Enums.Gender.Female, id: 60, birthDate: new DateTimeOffset(new DateTime(1979, 7, 8)), name: "Daria", surName: "Biernacka", pesel: "79123156494", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person60@gmail.com", aglomeration: Core.Enums.Aglomeration.Cracow));//,
            people.Add(new Person(imagePath: "/img/MW/k/29.jpg", phoneNumber: "715177373", gender: Core.Enums.Gender.Female, id: 61, birthDate: new DateTimeOffset(new DateTime(1978, 7, 8)), name: "Maria", surName: "Balon", pesel: "78532154645", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person61@gmail.com", aglomeration: Core.Enums.Aglomeration.Bialystok));//,
            people.Add(new Person(imagePath: "/img/MW/k/30.jpg", phoneNumber: "715177373", gender: Core.Enums.Gender.Female, id: 62, birthDate: new DateTimeOffset(new DateTime(1984, 7, 8)), name: "Anna", surName: "Poranna", pesel: "84561321499", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person62@gmail.com", aglomeration: Core.Enums.Aglomeration.Cracow));//,
            people.Add(new Person(imagePath: "/img/MW/k/31.jpg", phoneNumber: "715177373", gender: Core.Enums.Gender.Female, id: 63, birthDate: new DateTimeOffset(new DateTime(1988, 7, 8)), name: "Anna", surName: "Poletko", pesel: "88456413215", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person63@gmail.com", aglomeration: Core.Enums.Aglomeration.Tricity));//,
            people.Add(new Person(imagePath: "/img/MW/k/32.jpg", phoneNumber: "715177373", gender: Core.Enums.Gender.Female, id: 64, birthDate: new DateTimeOffset(new DateTime(1989, 7, 8)), name: "Agata", surName: "Bosko", pesel: "89561321564", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person64@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/MW/k/33.jpg", phoneNumber: "715177373", gender: Core.Enums.Gender.Female, id: 65, birthDate: new DateTimeOffset(new DateTime(1978, 7, 8)), name: "Agata", surName: "Mińska", pesel: "78465413131", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person65@gmail.com", aglomeration: Core.Enums.Aglomeration.Wroclaw));//,
            people.Add(new Person(imagePath: "/img/MW/k/34.jpg", phoneNumber: "715177373", gender: Core.Enums.Gender.Female, id: 66, birthDate: new DateTimeOffset(new DateTime(1980, 7, 8)), name: "Monika", surName: "Szajka", pesel: "80156467513", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person66@gmail.com", aglomeration: Core.Enums.Aglomeration.Bialystok));//,
            people.Add(new Person(imagePath: "/img/MW/k/35.jpg", phoneNumber: "715177373", gender: Core.Enums.Gender.Female, id: 67, birthDate: new DateTimeOffset(new DateTime(1979, 7, 8)), name: "Mariola", surName: "Kiepska", pesel: "79856461321", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person67@gmail.com", aglomeration: Core.Enums.Aglomeration.Kielce));//,
            people.Add(new Person(imagePath: "/img/MW/k/36.jpg", phoneNumber: "715177377", gender: Core.Enums.Gender.Female, id: 68, birthDate: new DateTimeOffset(new DateTime(1974, 7, 8)), name: "Dorota", surName: "Zawisza", pesel: "74413212649", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person68@gmail.com", aglomeration: Core.Enums.Aglomeration.Silesia));//,
            people.Add(new Person(imagePath: "/img/MW/k/37.jpg", phoneNumber: "715177377", gender: Core.Enums.Gender.Female, id: 69, birthDate: new DateTimeOffset(new DateTime(1988, 7, 8)), name: "Anastasia", surName: "Radczuk", pesel: "88456123134", hasPolishCitizenship: false, passportNumber: "AAAA87946121646", passportCode: "UKR", email: "person69@gmail.com", aglomeration: Core.Enums.Aglomeration.Tricity));//,
            people.Add(new Person(imagePath: "/img/MW/k/38.jpg", phoneNumber: "715777377", gender: Core.Enums.Gender.Female, id: 70, birthDate: new DateTimeOffset(new DateTime(1979, 7, 8)), name: "Karolina", surName: "Kulka", pesel: "79846513215", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person70@gmail.com", aglomeration: Core.Enums.Aglomeration.Rzeszów));//,
            people.Add(new Person(imagePath: "/img/MW/k/39.jpg", phoneNumber: "615772377", gender: Core.Enums.Gender.Female, id: 71, birthDate: new DateTimeOffset(new DateTime(1982, 2, 3)), name: "Sonia", surName: "Czapska", pesel: "82154698713", hasPolishCitizenship: false, passportNumber: "AAAA87946121646", passportCode: "UKR", email: "person71@gmail.com", aglomeration: Core.Enums.Aglomeration.Tricity));//,
            people.Add(new Person(imagePath: "/img/MW/k/40.jpg", phoneNumber: "414577375", gender: Core.Enums.Gender.Female, id: 72, birthDate: new DateTimeOffset(new DateTime(1980, 10, 20)), name: "Nina", surName: "Rączka", pesel: "80846513215", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person72@gmail.com", aglomeration: Core.Enums.Aglomeration.Rzeszów));//,
            people.Add(new Person(imagePath: "/img/MW/k/41.jpg", phoneNumber: "315787311", gender: Core.Enums.Gender.Female, id: 73, birthDate: new DateTimeOffset(new DateTime(1991, 12, 2)), name: "Karina", surName: "Kowalska", pesel: "91846533219", hasPolishCitizenship: true, passportNumber: null, passportCode: "POL", email: "person73@gmail.com", aglomeration: Core.Enums.Aglomeration.Rzeszów));//,


            people.Add(new Person(imagePath: "/img/persons/uk8.jpg", phoneNumber: "715747777", name: "Bożena", surName: "Raj", id: 74, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1949, 11, 18)), pesel: "49111816546", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person74@gmail.com", aglomeration: Core.Enums.Aglomeration.Cracow));//,
            people.Add(new Person(imagePath: "/img/persons/um6.jpg", phoneNumber: "715747793", name: "Fryderyk", surName: "Czyż", id: 75, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1976, 12, 18)), pesel: "76121864984", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person75@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/persons/uk11.jpg", phoneNumber: "715477222", name: "Monika", surName: "Zalewska", id: 76, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1982, 9, 9)), pesel: "82090913215", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person76@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/persons/uk6.jpg", phoneNumber: "715747777", name: "Daria", surName: "Raszpan", id: 77, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1984, 6, 16)), pesel: "84061632131", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person77@gmail.com", aglomeration: Core.Enums.Aglomeration.Bialystok));//,


            people.Add(new Person(imagePath: "/img/persons/um7.jpg", phoneNumber: "715475577", gender: Core.Enums.Gender.Male, id: 78, birthDate: new DateTimeOffset(new DateTime(1987, 7, 8)), name: "Łukasz", surName: "Łuk", pesel: "87101010105", hasPolishCitizenship: true, passportNumber: "484654asd4a5sd4", passportCode: "PL", email: "person78@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,//główny pacjent
            people.Add(new Person(imagePath: "/img/persons/uk1.jpg", phoneNumber: "715746772", name: "Magdalena", surName: "Bomba", id: 79, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1974, 5, 12)), pesel: "74051256121", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person79@gmail.com", aglomeration: Core.Enums.Aglomeration.Cracow));//,
            people.Add(new Person(imagePath: "/img/persons/uk2.jpg", phoneNumber: "715741778", name: "Katarzyna", surName: "Jelitko", id: 80, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1966, 4, 8)), pesel: "66040865456", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person80@gmail.com", aglomeration: Core.Enums.Aglomeration.Kielce));//,
            people.Add(new Person(imagePath: "/img/persons/um3.jpg", phoneNumber: "715741237", name: "Krzysztof", surName: "Kitka", id: 81, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1979, 8, 5)), pesel: "79080546213", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person81@gmail.com", aglomeration: Core.Enums.Aglomeration.Kuyavia));//,
            people.Add(new Person(imagePath: "/img/persons/um2.jpg", phoneNumber: "515747787", name: "Dariusz", surName: "Czapa", id: 82, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1982, 1, 24)), pesel: "82012464695", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person82@gmail.com", aglomeration: Core.Enums.Aglomeration.Poznan));//,
            people.Add(new Person(imagePath: "/img/persons/um5.jpg", phoneNumber: "415747747", name: "Tomasz", surName: "Komar", id: 83, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1981, 6, 7)), pesel: "81060754612", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person83@gmail.com", aglomeration: Core.Enums.Aglomeration.Rzeszów));//,
            people.Add(new Person(imagePath: "/img/persons/um4.jpg", phoneNumber: "315747737", name: "Arkadiusz", surName: "Patka", id: 84, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1979, 10, 20)), pesel: "79102013465", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person84@gmail.com", aglomeration: Core.Enums.Aglomeration.Silesia));//,
            people.Add(new Person(imagePath: "/img/persons/uk3.jpg", phoneNumber: "215747127", name: "Marta", surName: "Rakieta", id: 85, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1991, 2, 12)), pesel: "91021245646", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person85@gmail.com", aglomeration: Core.Enums.Aglomeration.Tricity));//,
            people.Add(new Person(imagePath: "/img/persons/uk4.jpg", phoneNumber: "715741179", name: "Ada", surName: "Ruda", id: 86, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1994, 12, 13)), pesel: "94121321654", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person86@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/persons/uk5.jpg", phoneNumber: "715747771", name: "Genowefa", surName: "Pigwa", id: 87, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1954, 6, 13)), pesel: "54061324651", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person87@gmail.com", aglomeration: Core.Enums.Aglomeration.Wroclaw));//,
            people.Add(new Person(imagePath: "/img/persons/um1.jpg", phoneNumber: "915742775", name: "Wacław", surName: "Kopytko", id: 88, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1955, 3, 13)), pesel: "55031365494", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person88@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/persons/uk9.jpg", phoneNumber: "500365555", name: "Agnieszka", surName: "Pielak", id: 89, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1990, 01, 15)), pesel: "90011515676", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person89@gmail.com", aglomeration: Core.Enums.Aglomeration.Rzeszów));
            people.Add(new Person(imagePath: "/img/persons/uk13.jpg", phoneNumber: "430385094", name: "Monika", surName: "Krasicka", id: 90, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1970, 01, 15)), pesel: "70011515678", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person90@gmail.com", aglomeration: Core.Enums.Aglomeration.Silesia));//,
            people.Add(new Person(imagePath: "/img/persons/uk14.jpg", phoneNumber: "530365915", name: "Maria", surName: "Kostacińska", id: 91, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1988, 01, 15)), pesel: "88011515600", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person91@gmail.com", aglomeration: Core.Enums.Aglomeration.Tricity));//,
            people.Add(new Person(imagePath: "/img/persons/uk15.jpg", phoneNumber: "540315526", name: "Wanda", surName: "Nowak", id: 92, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(2000, 01, 15)), pesel: "00311515644", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person92@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/persons/uk16.jpg", phoneNumber: "530364595", name: "Żaneta", surName: "Zielińska", id: 93, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1995, 01, 15)), pesel: "95211515624", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person93@gmail.com", aglomeration: Core.Enums.Aglomeration.Wroclaw));//,
            people.Add(new Person(imagePath: "/img/persons/uk17.jpg", phoneNumber: "560325595", name: "Renata", surName: "Molska", id: 94, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1966, 01, 15)), pesel: "66111515668", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person94@gmail.com", aglomeration: Core.Enums.Aglomeration.Bialystok));//,
            people.Add(new Person(imagePath: "/img/persons/um8.jpg", phoneNumber: "501325593", name: "Radosław", surName: "Gręda", id: 95, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1977, 01, 15)), pesel: "77013015655", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person95@gmail.com", aglomeration: Core.Enums.Aglomeration.Kielce));//,
            people.Add(new Person(imagePath: "/img/persons/um9.jpg", phoneNumber: "505364573", name: "Robert", surName: "Sapierzyński", id: 96, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1982, 01, 15)), pesel: "82014515633", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person96@gmail.com", aglomeration: Core.Enums.Aglomeration.Kuyavia));//,
            people.Add(new Person(imagePath: "/img/persons/um10.jpg", phoneNumber: "507361553", name: "Paweł", surName: "Tryfon", id: 97, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1980, 01, 15)), pesel: "80011513671", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person97@gmail.com", aglomeration: Core.Enums.Aglomeration.Rzeszów));//,
            people.Add(new Person(imagePath: "/img/persons/um11.jpg", phoneNumber: "601365563", name: "Tomasz", surName: "Oniśk", id: 98, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1991, 01, 15)), pesel: "91011415679", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person98@gmail.com", aglomeration: Core.Enums.Aglomeration.Poznan));//,
            people.Add(new Person(imagePath: "/img/persons/um12.jpg", phoneNumber: "701362551", name: "Mateusz", surName: "Skorupka", id: 99, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1996, 01, 15)), pesel: "96011215631", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person99@gmail.com", aglomeration: Core.Enums.Aglomeration.Poznan));//,
            people.Add(new Person(imagePath: "/img/persons/um13.jpg", phoneNumber: "401361353", name: "Robert", surName: "Krzycki", id: 100, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1972, 01, 15)), pesel: "72011525627", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person100@gmail.com", aglomeration: Core.Enums.Aglomeration.Tricity));//,
            people.Add(new Person(imagePath: "/img/persons/um14.jpg", phoneNumber: "854612314", name: "Tomasz", surName: "Janiga", id: 101, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1997, 01, 15)), pesel: "97011215631", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person101@gmail.com", aglomeration: Core.Enums.Aglomeration.Poznan));//,
            people.Add(new Person(imagePath: "/img/persons/um15.jpg", phoneNumber: "795161304", name: "Rafał", surName: "Fabisiak", id: 102, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1988, 01, 15)), pesel: "88011525627", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person102@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/persons/um16.jpg", phoneNumber: "354612314", name: "Radosław", surName: "Panga", id: 103, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1997, 01, 15)), pesel: "97011215631", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person103@gmail.com", aglomeration: Core.Enums.Aglomeration.Poznan));//,
            people.Add(new Person(imagePath: "/img/persons/um17.jpg", phoneNumber: "495161304", name: "Janusz", surName: "Buc", id: 104, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1988, 01, 15)), pesel: "88011525627", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person104@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
                                                                                                                                                                                                                                                                                                                                                                                                             //administrative workers
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "615475577", gender: Core.Enums.Gender.Male, id: 105, birthDate: new DateTimeOffset(new DateTime(1987, 7, 8)), name: "Łukasz", surName: "Łucznik", pesel: "87201010105", hasPolishCitizenship: true, passportNumber: "484654asd4a5sd4", passportCode: "PL", email: "person178@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,//główny pacjent
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "615746772", name: "Magdalena", surName: "Bombka", id: 106, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1974, 5, 12)), pesel: "74051296121", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person179@gmail.com", aglomeration: Core.Enums.Aglomeration.Cracow));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "615741778", name: "Katarzyna", surName: "Jelito", id: 107, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1966, 4, 8)), pesel: "66040865456", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person180@gmail.com", aglomeration: Core.Enums.Aglomeration.Kielce));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "615741237", name: "Krzysztof", surName: "Kita", id: 108, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1979, 8, 5)), pesel: "79080946213", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person181@gmail.com", aglomeration: Core.Enums.Aglomeration.Kuyavia));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "445747787", name: "Dariusz", surName: "Czapka", id: 109, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1982, 1, 24)), pesel: "82022464695", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person182@gmail.com", aglomeration: Core.Enums.Aglomeration.Poznan));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "335747745", name: "Tomasz", surName: "Mucha", id: 110, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1981, 6, 7)), pesel: "82060794612", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person183@gmail.com", aglomeration: Core.Enums.Aglomeration.Rzeszów));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "775747737", name: "Arkadiusz", surName: "Patka", id: 111, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1979, 10, 20)), pesel: "79202013465", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person184@gmail.com", aglomeration: Core.Enums.Aglomeration.Silesia));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "665747121", name: "Marta", surName: "Miotła", id: 112, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1995, 2, 18)), pesel: "95021845746", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person185@gmail.com", aglomeration: Core.Enums.Aglomeration.Tricity));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "505741179", name: "Ada", surName: "Rudzka", id: 113, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1994, 12, 13)), pesel: "94221321654", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person186@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "505747771", name: "Genowefa", surName: "Piwna", id: 114, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1954, 6, 13)), pesel: "54061324651", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person187@gmail.com", aglomeration: Core.Enums.Aglomeration.Wroclaw));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "885742775", name: "Wacław", surName: "Kopyto", id: 115, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1955, 3, 13)), pesel: "55031365494", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person188@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "440365555", name: "Agnieszka", surName: "Pielna", id: 116, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1990, 01, 15)), pesel: "90021915686", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person189@gmail.com", aglomeration: Core.Enums.Aglomeration.Rzeszów));
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "670385094", name: "Monika", surName: "Krasińska", id: 117, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1970, 01, 15)), pesel: "70021915788", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person190@gmail.com", aglomeration: Core.Enums.Aglomeration.Silesia));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "810365915", name: "Maria", surName: "Kostrzewa", id: 118, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1988, 01, 15)), pesel: "88021915800", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person191@gmail.com", aglomeration: Core.Enums.Aglomeration.Tricity));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "820315526", name: "Wanda", surName: "Nowacka", id: 119, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(2000, 01, 15)), pesel: "00321915944", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person192@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "600364595", name: "Żaneta", surName: "Zielna", id: 120, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1995, 01, 15)), pesel: "95221916024", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person193@gmail.com", aglomeration: Core.Enums.Aglomeration.Wroclaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "710325595", name: "Renata", surName: "Mol", id: 121, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1966, 01, 01)), pesel: "66221916168", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person194@gmail.com", aglomeration: Core.Enums.Aglomeration.Bialystok));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "701325593", name: "Radosław", surName: "Grzęda", id: 122, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1977, 01, 02)), pesel: "77023016255", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person195@gmail.com", aglomeration: Core.Enums.Aglomeration.Kielce));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "405364573", name: "Robert", surName: "Saper", id: 123, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1982, 01, 03)), pesel: "82024916333", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person196@gmail.com", aglomeration: Core.Enums.Aglomeration.Kuyavia));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "555361553", name: "Paweł", surName: "Trefny", id: 124, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1980, 01, 04)), pesel: "80021913681", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person197@gmail.com", aglomeration: Core.Enums.Aglomeration.Rzeszów));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "511365563", name: "Tomasz", surName: "Oniszk", id: 125, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1991, 01, 05)), pesel: "92021416789", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person198@gmail.com", aglomeration: Core.Enums.Aglomeration.Poznan));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "771362551", name: "Mateusz", surName: "Skorupski", id: 126, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1996, 01, 06)), pesel: "96021216831", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person199@gmail.com", aglomeration: Core.Enums.Aglomeration.Poznan));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "421361353", name: "Robert", surName: "Krzyk", id: 127, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1972, 01, 07)), pesel: "72021926928", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person1100@gmail.com", aglomeration: Core.Enums.Aglomeration.Tricity));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "854612314", name: "Tomasz", surName: "Jańczak", id: 128, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1997, 01, 08)), pesel: "97021217031", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person1101@gmail.com", aglomeration: Core.Enums.Aglomeration.Poznan));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "795161304", name: "Rafał", surName: "Fabisiuk", id: 129, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1988, 01, 09)), pesel: "88021927128", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person1102@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "354612312", name: "Radosław", surName: "Para", id: 130, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1997, 01, 10)), pesel: "97021217231", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person1103@gmail.com", aglomeration: Core.Enums.Aglomeration.Poznan));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "495161303", name: "Janusz", surName: "Boczek", id: 131, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1988, 01, 16)), pesel: "88021927328", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person1104@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "777161305", name: "Jagna", surName: "Buc", id: 132, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1988, 01, 17)), pesel: "88021927428", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person1104@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "500161304", name: "Janina", surName: "Bociek", id: 133, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1988, 01, 18)), pesel: "88021927528", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person1104@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "444161304", name: "Julija", surName: "Korczuk", id: 134, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1988, 01, 19)), pesel: null, hasPolishCitizenship: false, passportCode: "UKR", passportNumber: "ASDD4894121", email: "person1104@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,


            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "615475577", gender: Core.Enums.Gender.Male, id: 135, birthDate: new DateTimeOffset(new DateTime(1987, 7, 8)), name: "Łukasz", surName: "Łąkotka", pesel: "81201010105", hasPolishCitizenship: true, passportNumber: "4846ggf56sd4a5sd4", passportCode: "PL", email: "person1782@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,//główny pacjent
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "615746772", name: "Magdalena", surName: "Boruch", id: 136, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1974, 5, 12)), pesel: "79051296121", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person1792@gmail.com", aglomeration: Core.Enums.Aglomeration.Cracow));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "615741778", name: "Katarzyna", surName: "Krasucha", id: 137, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1966, 4, 8)), pesel: "61240865456", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person1802@gmail.com", aglomeration: Core.Enums.Aglomeration.Kielce));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "615741237", name: "Krzysztof", surName: "Kici", id: 138, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1979, 8, 5)), pesel: "77680946213", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person1812@gmail.com", aglomeration: Core.Enums.Aglomeration.Kuyavia));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "445747787", name: "Dariusz", surName: "Czarnecki", id: 139, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1982, 1, 24)), pesel: "81122464695", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person1822@gmail.com", aglomeration: Core.Enums.Aglomeration.Poznan));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "335747745", name: "Tomasz", surName: "Pchełka", id: 140, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1981, 6, 7)), pesel: "80160794612", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person1832@gmail.com", aglomeration: Core.Enums.Aglomeration.Rzeszów));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "775747737", name: "Arkadiusz", surName: "Packa", id: 141, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1979, 10, 20)), pesel: "77802013465", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person1842@gmail.com", aglomeration: Core.Enums.Aglomeration.Silesia));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "665747121", name: "Marta", surName: "Paletka", id: 142, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1991, 2, 12)), pesel: "92721245646", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person1852@gmail.com", aglomeration: Core.Enums.Aglomeration.Tricity));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "505741179", name: "Ada", surName: "Rudzka", id: 143, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1994, 12, 13)), pesel: "95321321654", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person1862@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "505747771", name: "Genowefa", surName: "Winna", id: 144, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1954, 6, 13)), pesel: "57061324651", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person1872@gmail.com", aglomeration: Core.Enums.Aglomeration.Wroclaw));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "885742775", name: "Wacław", surName: "Koń", id: 145, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1955, 3, 13)), pesel: "59231365494", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person1882@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "440365555", name: "Agnieszka", surName: "Galicyjska", id: 146, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1990, 01, 15)), pesel: "91321915686", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person1892@gmail.com", aglomeration: Core.Enums.Aglomeration.Rzeszów));
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "670385094", name: "Monika", surName: "Krośnieńska", id: 147, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1970, 01, 15)), pesel: "74321915788", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person1902@gmail.com", aglomeration: Core.Enums.Aglomeration.Silesia));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "810365915", name: "Maria", surName: "Ostra", id: 148, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1988, 01, 15)), pesel: "85521915800", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person1912@gmail.com", aglomeration: Core.Enums.Aglomeration.Tricity));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "820315526", name: "Wanda", surName: "Nowa", id: 149, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(2000, 01, 15)), pesel: "00121915944", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person1922@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "600364595", name: "Żaneta", surName: "Ziele", id: 150, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1995, 01, 15)), pesel: "92221916024", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person1932@gmail.com", aglomeration: Core.Enums.Aglomeration.Wroclaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "710325595", name: "Renata", surName: "Maska", id: 151, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1966, 01, 15)), pesel: "68921916168", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person1942@gmail.com", aglomeration: Core.Enums.Aglomeration.Bialystok));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "701325593", name: "Radosław", surName: "Giętki", id: 152, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1977, 01, 15)), pesel: "78923016255", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person1952@gmail.com", aglomeration: Core.Enums.Aglomeration.Kielce));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "405364573", name: "Robert", surName: "Saperski", id: 153, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1982, 01, 15)), pesel: "81424916333", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person1962@gmail.com", aglomeration: Core.Enums.Aglomeration.Kuyavia));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "555361553", name: "Paweł", surName: "Trudny", id: 154, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1980, 01, 15)), pesel: "83621913681", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person1972@gmail.com", aglomeration: Core.Enums.Aglomeration.Rzeszów));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "511365563", name: "Tomasz", surName: "Olenderski", id: 155, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1991, 01, 15)), pesel: "93721416789", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person1982@gmail.com", aglomeration: Core.Enums.Aglomeration.Poznan));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "771362551", name: "Mateusz", surName: "Jajo", id: 156, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1996, 01, 15)), pesel: "95221216831", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person1992@gmail.com", aglomeration: Core.Enums.Aglomeration.Poznan));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "421361353", name: "Robert", surName: "Krzywy", id: 157, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1972, 01, 15)), pesel: "79621926928", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person11002@gmail.com", aglomeration: Core.Enums.Aglomeration.Tricity));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "854612314", name: "Tomasz", surName: "Jaki", id: 158, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1997, 01, 15)), pesel: "94721217031", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person11012@gmail.com", aglomeration: Core.Enums.Aglomeration.Poznan));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "795161304", name: "Rafał", surName: "Fabiański", id: 159, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1988, 01, 15)), pesel: "86321927128", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person11022@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "354612312", name: "Radosław", surName: "Pilecki", id: 160, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1997, 01, 15)), pesel: "98521217231", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person11032@gmail.com", aglomeration: Core.Enums.Aglomeration.Poznan));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "495161303", name: "Janusz", surName: "Łącki", id: 161, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1988, 01, 15)), pesel: "83621927328", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person11042@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "777161305", name: "Jagna", surName: "Baćka", id: 162, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1988, 02, 15)), pesel: "89521927428", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person11043@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "500161304", name: "Janina", surName: "Słonina", id: 163, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1988, 03, 15)), pesel: "81221927528", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person11044@gmail.com", aglomeration: Core.Enums.Aglomeration.Wroclaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "444161304", name: "Julija", surName: "Krawczuk", id: 164, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1988, 04, 15)), pesel: null, hasPolishCitizenship: false, passportCode: "UKR", passportNumber: "ASDE5894121", email: "person11045@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "333161305", name: "Mariola", surName: "Kićka", id: 165, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1981, 02, 15)), pesel: "81521927428", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person300@gmail.com", aglomeration: Core.Enums.Aglomeration.Bialystok));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "444161304", name: "Maria", surName: "Baranina", id: 166, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1982, 03, 15)), pesel: "82221927528", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person301@gmail.com", aglomeration: Core.Enums.Aglomeration.Kielce));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "555161304", name: "Katja", surName: "Marczuk", id: 167, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1983, 04, 15)), pesel: null, hasPolishCitizenship: false, passportCode: "UKR", passportNumber: "AUDE5894921", email: "person302@gmail.com", aglomeration: Core.Enums.Aglomeration.Silesia));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "554161305", name: "Bogna", surName: "Sosna", id: 168, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1984, 02, 15)), pesel: "89521927428", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person303@gmail.com", aglomeration: Core.Enums.Aglomeration.Cracow));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "666161304", name: "Weronika", surName: "Kronika", id: 169, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1985, 03, 15)), pesel: "81221927528", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person304@gmail.com", aglomeration: Core.Enums.Aglomeration.Wroclaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "321161304", name: "Maryna", surName: "Funszczyk", id: 170, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1986, 04, 15)), pesel: null, hasPolishCitizenship: false, passportCode: "UKR", passportNumber: "BSDE6874121", email: "person305@gmail.com", aglomeration: Core.Enums.Aglomeration.Kielce));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "741161305", name: "Zofia", surName: "Stonka", id: 171, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1987, 02, 15)), pesel: "89521927428", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person306@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "701161304", name: "Teresa", surName: "Mercedes", id: 172, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1988, 03, 22)), pesel: "81221927528", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person307@gmail.com", aglomeration: Core.Enums.Aglomeration.Wroclaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "606161304", name: "Tania", surName: "Michajłowicz", id: 173, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1989, 04, 15)), pesel: null, hasPolishCitizenship: false, passportCode: "UKR", passportNumber: "BOSD5877123", email: "person308@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "222161305", name: "Jagna", surName: "Baćka", id: 174, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1990, 02, 15)), pesel: "89521927428", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person309@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "489161304", name: "Janina", surName: "Słonina", id: 175, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1991, 03, 01)), pesel: "81221927528", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person310@gmail.com", aglomeration: Core.Enums.Aglomeration.Wroclaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "319161304", name: "Esra", surName: "Melik", id: 176, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1992, 04, 19)), pesel: null, hasPolishCitizenship: false, passportCode: "TUR", passportNumber: "LLUE1894133", email: "person11045@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,


            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "775395571", name: "Łukasz", surName: "Czapa", id: 177, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1987, 7, 8)), pesel: "81201010105", hasPolishCitizenship: true, passportNumber: "4846f56sd4a5sd4", passportCode: "PL", email: "person11782@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,//główny pacjent
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "615936772", name: "Magdalena", surName: "Bambo", id: 178, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1974, 5, 12)), pesel: "79051296121", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person17192@gmail.com", aglomeration: Core.Enums.Aglomeration.Cracow));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "615831778", name: "Katarzyna", surName: "Potok", id: 179, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1966, 4, 8)), pesel: "61240865456", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person18012@gmail.com", aglomeration: Core.Enums.Aglomeration.Kielce));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "615737237", name: "Krzysztof", surName: "Kicka", id: 180, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1979, 8, 5)), pesel: "77680946213", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person18112@gmail.com", aglomeration: Core.Enums.Aglomeration.Kuyavia));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "135732787", name: "Dariusz", surName: "Czarnek", id: 181, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1982, 1, 24)), pesel: "81122464695", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person18122@gmail.com", aglomeration: Core.Enums.Aglomeration.Poznan));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "135733735", name: "Tomasz", surName: "Słoń", id: 182, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1981, 6, 7)), pesel: "80160794612", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person11832@gmail.com", aglomeration: Core.Enums.Aglomeration.Rzeszów));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "775734737", name: "Arkadiusz", surName: "Tacka", id: 183, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1979, 10, 20)), pesel: "77802013465", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person11842@gmail.com", aglomeration: Core.Enums.Aglomeration.Silesia));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "645735121", name: "Marta", surName: "Piłka", id: 184, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1991, 2, 12)), pesel: "92721245646", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person18512@gmail.com", aglomeration: Core.Enums.Aglomeration.Tricity));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "505731176", name: "Ada", surName: "Radzewicz", id: 185, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1994, 12, 13)), pesel: "95321321654", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person18612@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "505737871", name: "Genowefa", surName: "Wino", id: 186, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1954, 6, 13)), pesel: "57061324651", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person18712@gmail.com", aglomeration: Core.Enums.Aglomeration.Wroclaw));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "885732715", name: "Wacław", surName: "Kot", id: 187, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1955, 3, 13)), pesel: "59231365494", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person18282@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "10365545", name: "Agnieszka", surName: "Galicka", id: 188, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1990, 01, 15)), pesel: "91321915686", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person18922@gmail.com", aglomeration: Core.Enums.Aglomeration.Rzeszów));
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "670383093", name: "Monika", surName: "Krosta", id: 189, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1970, 01, 15)), pesel: "74321915788", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person12902@gmail.com", aglomeration: Core.Enums.Aglomeration.Silesia));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "810362915", name: "Maria", surName: "Awaria", id: 190, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1988, 01, 15)), pesel: "85521915800", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person19312@gmail.com", aglomeration: Core.Enums.Aglomeration.Tricity));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "820314526", name: "Wanda", surName: "Juranda", id: 191, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(2000, 01, 15)), pesel: "00121915944", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person19242@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "600367595", name: "Żaneta", surName: "Kareta", id: 192, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1995, 01, 15)), pesel: "92221916024", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person19532@gmail.com", aglomeration: Core.Enums.Aglomeration.Wroclaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "710326595", name: "Renata", surName: "Kareta", id: 193, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1966, 01, 15)), pesel: "68921916168", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person19542@gmail.com", aglomeration: Core.Enums.Aglomeration.Bialystok));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "701325593", name: "Radosław", surName: "Twardy", id: 194, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1977, 01, 15)), pesel: "78923016255", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person15952@gmail.com", aglomeration: Core.Enums.Aglomeration.Kielce));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "405363573", name: "Robert", surName: "Mina", id: 195, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1982, 01, 15)), pesel: "81424916333", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person19652@gmail.com", aglomeration: Core.Enums.Aglomeration.Kuyavia));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "555361593", name: "Paweł", surName: "Apacz", id: 196, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1980, 01, 15)), pesel: "83621913681", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person19752@gmail.com", aglomeration: Core.Enums.Aglomeration.Rzeszów));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "511369563", name: "Tomasz", surName: "Olejarski", id: 197, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1991, 01, 15)), pesel: "93721416789", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person19582@gmail.com", aglomeration: Core.Enums.Aglomeration.Poznan));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "771362581", name: "Mateusz", surName: "Jajko", id: 198, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1996, 01, 15)), pesel: "95221216831", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person15992@gmail.com", aglomeration: Core.Enums.Aglomeration.Poznan));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "421361383", name: "Robert", surName: "Koniuszy", id: 199, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1972, 01, 15)), pesel: "79621926928", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person110502@gmail.com", aglomeration: Core.Enums.Aglomeration.Tricity));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "853612313", name: "Tomasz", surName: "Nijaki", id: 200, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1997, 01, 15)), pesel: "94721217031", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person115012@gmail.com", aglomeration: Core.Enums.Aglomeration.Poznan));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "795161303", name: "Rafał", surName: "Fart", id: 201, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1988, 01, 15)), pesel: "86321927128", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person110522@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "453612312", name: "Radosław", surName: "Pantera", id: 202, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1997, 01, 15)), pesel: "98521217231", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person110532@gmail.com", aglomeration: Core.Enums.Aglomeration.Poznan));//,
            people.Add(new Person(imagePath: "/img/persons/male.svg", phoneNumber: "895161303", name: "Janusz", surName: "Łęcki", id: 203, gender: Core.Enums.Gender.Male, birthDate: new DateTimeOffset(new DateTime(1988, 01, 15)), pesel: "83621927328", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person110542@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "757161305", name: "Jagna", surName: "Kaczka", id: 204, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1988, 02, 15)), pesel: "89521927428", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person110543@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "500161303", name: "Janina", surName: "Malina", id: 205, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1988, 03, 15)), pesel: "81221927528", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person110544@gmail.com", aglomeration: Core.Enums.Aglomeration.Wroclaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "833161303", name: "Julija", surName: "Adamczuk", id: 206, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1988, 04, 15)), pesel: null, hasPolishCitizenship: false, passportCode: "UKR", passportNumber: "UKRE5894121", email: "person110545@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "833161305", name: "Mariola", surName: "Koza", id: 207, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1981, 02, 15)), pesel: "81521927428", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "perso55n300@gmail.com", aglomeration: Core.Enums.Aglomeration.Bialystok));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "383161303", name: "Maria", surName: "Owca", id: 208, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1982, 03, 15)), pesel: "82221927528", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person30551@gmail.com", aglomeration: Core.Enums.Aglomeration.Kielce));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "525161303", name: "Katja", surName: "Zelenski", id: 209, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1983, 04, 15)), pesel: null, hasPolishCitizenship: false, passportCode: "UKR", passportNumber: "BELE5894921", email: "person3502@gmail.com", aglomeration: Core.Enums.Aglomeration.Silesia));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "513161405", name: "Bogna", surName: "Radosna", id: 210, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1984, 02, 15)), pesel: "89521927428", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person30553@gmail.com", aglomeration: Core.Enums.Aglomeration.Cracow));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "666161603", name: "Weronika", surName: "Trygonomika", id: 211, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1985, 03, 15)), pesel: "81221927528", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person5304@gmail.com", aglomeration: Core.Enums.Aglomeration.Wroclaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "321161703", name: "Maryna", surName: "Melnik", id: 212, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1986, 04, 15)), pesel: null, hasPolishCitizenship: false, passportCode: "UKR", passportNumber: "UKRE6874121", email: "person3505@gmail.com", aglomeration: Core.Enums.Aglomeration.Kielce));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "731161805", name: "Zofia", surName: "Kromka", id: 213, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1987, 02, 15)), pesel: "89521927428", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person3065@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "701161903", name: "Teresa", surName: "Ferrari", id: 214, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1988, 03, 22)), pesel: "81221927528", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person3507@gmail.com", aglomeration: Core.Enums.Aglomeration.Wroclaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "606161003", name: "Tania", surName: "Kowalewicz", id: 215, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1989, 04, 15)), pesel: null, hasPolishCitizenship: false, passportCode: "UKR", passportNumber: "UKRSD5877123", email: "person3508@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "222161105", name: "Jagna", surName: "Małopolska", id: 216, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1990, 02, 15)), pesel: "89521927428", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person3059@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "389161203", name: "Janina", surName: "Podlaska", id: 217, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1991, 03, 01)), pesel: "81221927528", hasPolishCitizenship: true, passportCode: null, passportNumber: null, email: "person3150@gmail.com", aglomeration: Core.Enums.Aglomeration.Wroclaw));//,
            people.Add(new Person(imagePath: "/img/persons/female.svg", phoneNumber: "619161303", name: "Esra", surName: "Gunes", id: 218, gender: Core.Enums.Gender.Female, birthDate: new DateTimeOffset(new DateTime(1992, 04, 19)), pesel: null, hasPolishCitizenship: false, passportCode: "TUR", passportNumber: "TURUE1894133", email: "person110455@gmail.com", aglomeration: Core.Enums.Aglomeration.Warsaw));//,

            //customer service workers
            //};
            return people;
        }

        public static IEnumerable<MedicalWorker> GetMedicalWorkers()
        {
            DateTime now = DateTime.Now;

            List<List<VisitReview>> visitRatingsList1 = new List<List<VisitReview>>();
            List<List<VisitReview>> visitRatingsList2 = new List<List<VisitReview>>();
            List<List<VisitReview>> visitRatingsList3 = new List<List<VisitReview>>();

            for (int i = 0; i < 70; i++)
            {
                List<VisitReview> visitRatings1 = new List<VisitReview>()
                {
                    new VisitReview()
                    {
                        AtmosphereRate=1,
                        CompetenceRate=4,
                        GeneralRate=3,
                        Id=1+i*6,
                        ShortDescription="Lekarz w miarę kompetentny, ale chamski gbur",
                        ReviewDate= now.AddDays(-10),
                        //Reviewer=AllPatients[0],
                        ReviewerId=AllPatients[0].Id,

                    },
                    new VisitReview()
                    {
                        AtmosphereRate=5,
                        CompetenceRate=2,
                        GeneralRate=3,
                        Id=2+i*6,
                        ShortDescription="Miły lekarz, niestety jego zalecenia nic nie pomogły",
                        ReviewDate= now.AddDays(-20),
                        //Reviewer=AllPatients[1]
                        ReviewerId=AllPatients[1].Id,

                    }
                };
                List<VisitReview> visitRatings2 = new List<VisitReview>()
                {
                    new VisitReview()
                    {
                        AtmosphereRate=4,
                        CompetenceRate=4,
                        GeneralRate=4,
                        Id=4+i*6,
                        ShortDescription="Przepisane przez niego medykamenty poprawiły mój stan, ale część objawów się utrzymała.",
                        ReviewDate= now.AddDays(-120),
                        //Reviewer=AllPatients[2]
                        ReviewerId=AllPatients[2].Id,
                    },
                    new VisitReview()
                    {
                        AtmosphereRate=5,
                        CompetenceRate=5,
                        GeneralRate=5,
                        Id=3+i*6,
                        ShortDescription="Super lekarz, pomógł mi, dodatkowo jest bardzo sympatyczny i wszystko mi po kolei wyjaśnił. Lekarz-ideał.",
                        ReviewDate= now.AddDays(-100),
                        //Reviewer=AllPatients[3],
                        ReviewerId=AllPatients[3].Id,
                    }
                };
                List<VisitReview> visitRatings3 = new List<VisitReview>()
                {
                    new VisitReview()
                    {
                        AtmosphereRate=2,
                        CompetenceRate=1,
                        GeneralRate=1,
                        Id=5+i*6,
                        ShortDescription="Lekarza nie interesowały wyniki badań, nie interesowało co mówię, jedyne co mi zalecił, to leki przeciwbólowe!.",
                        ReviewDate= now.AddDays(-50),
                        //Reviewer=AllPatients[4],
                        ReviewerId=AllPatients[4].Id,

                    },
                    new VisitReview()
                    {
                        AtmosphereRate=1,
                        CompetenceRate=2,
                        GeneralRate=2,
                        Id=6+i*6,
                        ShortDescription="Bardzo nieprzyjemny, jego leczenie nie przyniosło większej poprawy",
                        ReviewDate= now.AddDays(-55),
                        //Reviewer=AllPatients[5],
                        ReviewerId=AllPatients[5].Id

                    }
                };
            }

            long id = 0;
            int userId = 0;
            List<MedicalWorker> MedicalWorkers =
            new List<MedicalWorker>()
            {
                new Doctor(Persons[0].Id,"IUHIDUASHDI545613216")
                {
                    Id=++id,
                                        UserId=Users[userId++].Id,
                    //User=Users[userId++],

                    Education=UM_1,//new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/MW/m/1.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsActive=true,
                   // VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[0],PrimaryMedicalServices[1]
                    }

                },
                new Doctor(Persons[1].Id, "ASGER51541213")
                {
                    Id=++id,
                    UserId=Users[userId++].Id,
                    //User=Users[userId++],

                    Education=UM_3,// new List<string>() {UM_3,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu praskim",
                    //ImagePath="/img/MW/m/2.jpg",
                    HiredSince=new DateTime(2017,1,1),
                    IsActive=true,
                    //VisitReviews=visitRatings2,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[2],
                        PrimaryMedicalServices[3],
                        PrimaryMedicalServices[34],
                        PrimaryMedicalServices[35]
                    }

                },
                new Physiotherapist(Persons[2].Id, "GVCXDS56151321")
                {
                    Id=++id,
                                                            UserId=Users[userId++].Id,

                    //User=Users[userId++],

                    Education=UM_2,// new List<string>() {UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu MSWiA",
                    //ImagePath="/img/MW/m/3.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsActive=true,
                    //VisitReviews=visitRatings3,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[30],
                        PrimaryMedicalServices[31],
                        PrimaryMedicalServices[32]
                    }

                },
                new Physiotherapist   (Persons[3].Id,"IUJNKJN54321165")
                {
                    Id=++id,
                    UserId=Users[userId++].Id,

                    //User=Users[userId++],

                    Education=UM_2,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu UMK",
                    //ImagePath="/img/MW/m/4.jpg",
                    HiredSince=new DateTime(2020,4,1),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[30],
                        PrimaryMedicalServices[31],
                        PrimaryMedicalServices[32]
                    }

                },
                new Doctor(Persons[4].Id,"IUJKHJK546121646")
                {
                    Id=++id,
                    UserId=Users[userId++].Id,

                    //User=Users[userId++],

                    Education=UM_1,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/MW/m/5.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[5]
                    }
                },
                new Doctor(Persons[5].Id,"OPASDASP54156142313")
                {
                    Id=++id,
                    UserId=Users[userId++].Id,

                    //User=Users[userId++],

                    Education=UM_1,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/MW/m/6.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsActive=true,
                    //VisitReviews=visitRatings2,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[20],
                        PrimaryMedicalServices[21]
                    }
                },
                new Doctor(Persons[6].Id, "IAOSD5161231564")
                {
                    Id=++id,
                    UserId=Users[userId++].Id,

                    //User=Users[userId++],

                    Education=UM_7,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu wrocławskim",
                    //ImagePath="/img/MW/m/7.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsActive=true,
                    //VisitReviews=visitRatings3,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[8]
                    }
                },
                new Doctor(Persons[7].Id, "UNCAJSDS51651323")
                {
                    Id=++id,
                    UserId=Users[userId++].Id,

                    //User=Users[userId++],

                    Education=UM_3,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu podlaskim",
                    //ImagePath="/img/MW/m/8.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsActive=true,
                   // VisitReviews=visitRatings2,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[7],
                        PrimaryMedicalServices[2],
                        PrimaryMedicalServices[34],
                        PrimaryMedicalServices[38]
                    }

                },
                new Doctor(Persons[8].Id, "DFSDFD4654213")
                {
                    Id=++id,
                    UserId=Users[userId++].Id,

                    //User=Users[userId++],

                    Education=UM_4,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/MW/m/9.jpg",
                    HiredSince=new DateTime(2012,1,1),
                    IsActive=true,
                   // VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[4],
                        PrimaryMedicalServices[2],
                        PrimaryMedicalServices[34],
                        PrimaryMedicalServices[36]
                    }
                },
                new Doctor(Persons[9].Id,"IOWNCAS5613245")
                {
                    Id=++id,
                    UserId=Users[userId++].Id,

                    //User=Users[userId++],
                    Education=UM_5,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu suwalskim",
                    //ImagePath="/img/MW/m/10.jpg",
                    HiredSince=new DateTime(2018,1,1),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[20],
                        PrimaryMedicalServices[22]
                    }
                },
                new Doctor(Persons[10].Id,"MNMCXISA561235")
                {
                    Id=++id,
                    UserId=Users[userId++].Id,
                    //User=Users[userId++],
                    Education=UM_9,// new List<string>() {UM_9},
                    Experience="W latach 2008-2019 praca w szpitalu podkarpackim",
                    //ImagePath="/img/MW/m/11.jpg",
                    HiredSince=new DateTime(2017,5,5),
                    IsActive=true,
                   // VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[7],
                        PrimaryMedicalServices[2],
                        PrimaryMedicalServices[34],
                        PrimaryMedicalServices[38]
                    }

                },
                new Doctor(Persons[11].Id,"ASIUDAS5123463")
                {
                    Id=++id,
                    UserId=Users[userId++].Id,

                                    //User=Users[userId++],

                    Education=UM_8,// new List<string>() {UM_8},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/MW/m/12.jpg",
                    HiredSince=new DateTime(2017,1,1),
                    IsActive=true,
                  //  VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[5],
                        PrimaryMedicalServices[37]
                    }

                },
                new ElectroradiologyTechnician (Persons[12].Id,"QPSCS5346448")
                {
                    Id=++id,
                    UserId=Users[userId++].Id,
                                    //User=Users[userId++],
                    Education=UM_7,// new List<string>() {UM_7},
                    Experience="W latach 2005-2020 praca w szpitalu wojskowym",
                    //ImagePath="/img/MW/m/13.jpg",
                    HiredSince=new DateTime(2012,12,12),
                    IsActive=true,
                  //  VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[28],
                        PrimaryMedicalServices[29]

                    }

                },
                new Doctor(Persons[13].Id, "CXCXZS6543215")
                {
                    Id=++id,
                    UserId=Users[userId++].Id,
                                    //User=Users[userId++],
                    Education=UM_6,// new List<string>() {UM_6},
                    Experience="W latach 2010-2019 praca w szpitalu matki i dziecka",
                    //ImagePath="/img/MW/m/14.jpg",
                    HiredSince=new DateTime(2019,4,4),
                    IsActive=true,
                 //   VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[7],
                        PrimaryMedicalServices[34]
                    }

                },
                new Doctor(Persons[14].Id, "PASXCA516164")
                {
                    Id=++id,
                    UserId=Users[userId++].Id,
                    //User=Users[userId++],
                    Education=UM_5,// new List<string>() {UM_5},
                    Experience="W latach 2011-2021 praca w szpitalu zakaźnym",
                    //ImagePath="/img/MW/m/15.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsActive=true,
               //     VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[9],
                        PrimaryMedicalServices[39]
                    }
                },
                new Doctor(Persons[15].Id, "PSADNASJ1564613")
                {
                    Id=++id,
                    UserId=Users[userId++].Id,
                    //User=Users[userId++],
                    Education=UM_4,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2007-2021 praca w szpitalu kujawskim",
                    //ImagePath="/img/MW/m/16.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsActive=true,
           //         VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[8]
                    }
                },
                new Doctor(Persons[16].Id, "AHUHIFDSD18564513")
                {
                    Id=++id,
                    UserId=Users[userId++].Id,

                                    //User=Users[userId++],

                    Education=UM_4,// new List<string>() {UM_4},
                    Experience="W latach 2005-2020 praca w szpitalu łódzkim",
                    //ImagePath="/img/MW/m/17.jpg",
                    HiredSince=new DateTime(2013,3,3),
                    IsActive=true,
          //          VisitReviews=visitRatings3,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[11],PrimaryMedicalServices[12]
                    }
                },
                new Doctor(Persons[17].Id,"UYGSDAS541321")
                {
                    Id=++id,
                    UserId=Users[userId++].Id,

                                    //User=Users[userId++],

                    Education=UM_2,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/MW/m/2.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsActive=true,
                   // VisitReviews=visitRatings2,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[11],PrimaryMedicalServices[13]
                    }

                },
                new Doctor(Persons[18].Id,"JHGDAJSH516145")
                {
                    Id=++id,
                    UserId=Users[userId++].Id,

                    //User=Users[userId++],

                    Education=UM_3,// new List<string>() {UM_3},
                    Experience="W latach 2009-2020 praca w POZ Węgrów.",
                    //ImagePath="/img/MW/m/19.jpg",
                    HiredSince=new DateTime(2018,7,6),
                    IsActive=true,
                //    VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[11],PrimaryMedicalServices[14]
                    }

                },
                new Doctor(Persons[19].Id, "GSFEQWDXA515646")
                {
                    Id=++id,
                    UserId=Users[userId++].Id,

                                    //User=Users[userId++],

                    Education=UM_1,// new List<string>() {UM_1},
                    Experience="W latach 2005-2020 praca w szpitalu miejskim w Krośnie",
                    //ImagePath="/img/MW/m/20.jpg",
                    HiredSince=new DateTime(2020,2,1),
                    IsActive=true,
                 //   VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[11],PrimaryMedicalServices[15]
                    }

                },
                new Doctor(Persons[20].Id, "ISJAD4465132")
                {
                    Id=++id,
                    UserId=Users[userId++].Id,
                    //User=Users[userId++],
                    Education=UM_2,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu tarnowskim",
                    //ImagePath="/img/MW/m/21.jpg",
                    HiredSince=new DateTime(2017,1,1),
                    IsActive=true,
            //       VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[16]
                    }

                },
                new Doctor(Persons[21].Id, "UISDR216443")
                {
                    Id=++id,
                    UserId=Users[userId++].Id,
                    //User=Users[userId++],

                    Education=UM_3,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu miejskim w Zakopanem",
                    //ImagePath="/img/MW/m/22.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsActive=true,
                //    VisitReviews=visitRatings2,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[17]
                    }

                },
                new Doctor(Persons[22].Id, "VASDK5421324")
                {
                    Id=++id,
                    UserId=Users[userId++].Id,
                                    //User=Users[userId++],
                    Education=UM_4,// new List<string>() {UM_7},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/MW/m/23.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsActive=true,
                 //   VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[18]
                    }
                },
                new Doctor(Persons[23].Id, "ASPDUI56321587")
                {
                    Id=++id,
                    UserId=Users[userId++].Id,
                                    //User=Users[userId++],
                    Education=UM_5,// new List<string>() {UM_5},
                    Experience="W latach 2008-2014 praca w szpitalu kardiologicznym",
                    //ImagePath="/img/MW/m/2.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsActive=true,
                 //   VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[19],
                        PrimaryMedicalServices[41]
                    }
                },
                new Doctor(Persons[24].Id, "BVNMXCA4623148")
                {
                    Id=++id,
                    UserId=Users[userId++].Id,
                                    //User=Users[userId++],
                    Education=UM_6,// new List<string>() {UM_5},
                    Experience="W latach 2005-2020 praca w szpitalu w Dębicy",
                    //ImagePath="/img/MW/m/25.jpg",
                    //HiredSince=new DateTime(2015,1,1),
                    IsActive=true,
                //    VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[20],PrimaryMedicalServices[24]
                    }

                },
                new Physiotherapist(Persons[25].Id,"FAHDJ665413215")
                {
                    Id=++id,
                    UserId=Users[userId++].Id,
                    //User=Users[userId++],

                    Education=UM_7,// new List<string>() {UM_4},
                    Experience="W latach 2005-2020 praca w szpitalu powiatowym w Zamościu",
                    //ImagePath="/img/MW/m/26.jpg",
                    //HiredSince=new DateTime(2019,1,1),
                    IsActive=true,
                //    VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[31],
                        PrimaryMedicalServices[32],
                        PrimaryMedicalServices[30]
                    }

                },
                new Doctor(Persons[26].Id,"ALKJSD5461321")
                {
                    Id=++id,
                    UserId=Users[userId++].Id,
                    //User=Users[userId++],

                    Education=UM_8,// new List<string>() {UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu zakaźnym na Woli",
                    //ImagePath="/img/MW/m/27.jpg",
                    //HiredSince=new DateTime(2011,10,11),
                    IsActive=true,
                 //   VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[6],PrimaryMedicalServices[27]
                    }

                },
                new ElectroradiologyTechnician(Persons[27].Id, "HGSDAS545641231")
                {
                    Id=++id,
                    UserId=Users[userId++].Id,

                    //UserId=Users[userId].Id,
                    //User=Users[userId++],

                    Education=UM_9,// new List<string>() {UM_6},
                    Experience="W latach 2006-2019 praca w szpitalu świętokrzyskim",
                    //ImagePath="/img/MW/m/28.jpg",
                    //HiredSince=new DateTime(2020,8,8),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[28],PrimaryMedicalServices[29]
                    }

                },
                new Doctor(Persons[28].Id,"BHJASGDJAS54613254")
                {
                    Id=++id,
                                        UserId=Users[userId++].Id,

                    //UserId=Users[userId].Id,
                    //User=Users[userId++],

                    Education=UM_9,////new List<string>() {UM_8},
                    Experience="W latach 2005-2020 praca w szpitalu akademickim w Białymstoku",
                    //ImagePath="/img/MW/m/29.jpg",
                    //HiredSince=new DateTime(2018,1,1),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[6],PrimaryMedicalServices[27]
                    }
                },
                new Doctor(Persons[29].Id,"OJIHJDAS543156")
                {
                    Id=++id,
                                        UserId=Users[userId++].Id,

                    //User=Users[userId++],
                    //UserId=Users[userId].Id,

                    Education=UM_8,// new List<string>() {UM_6},
                    Experience="W latach 2005-2020 praca w szpitalu miejskim w Słupsku",
                    //ImagePath="/img/MW/m/30.jpg",
                    //HiredSince=new DateTime(2016,4,4),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[7],
                        PrimaryMedicalServices[34]
                    }

                },
                new Doctor(Persons[30].Id,"JHASKDAS65461321")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,

                                        //UserId=Users[userId].Id,

                                        //                User=Users[userId++],

                    Education =UM_7,// new List<string>() {UM_3},
                    Experience="W latach 2005-2012 praca w szpitalu klinicznym w Gnieźnie. Wcześniej pracował w Zielonej górze.",
                    //ImagePath="/img/MW/m/31.jpg",
                    //HiredSince=new DateTime(2011,1,1),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[11],PrimaryMedicalServices[12],PrimaryMedicalServices[13]
                    }
                },
                new Doctor(Persons[31].Id,"JHKSDASD546123")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,

                                        //UserId=Users[userId].Id,

                                        //                User=Users[userId++],

                    Education =UM_6,// new List<string>() {UM_4},
                    Experience="W latach 2005-2020 praca w szpitalu akademickim w Krakowie",
                    //ImagePath="/img/MW/m/32.jpg",
                    //HiredSince=new DateTime(2019,8,1),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[8]
                    }

                },
                new Doctor(Persons[32].Id,"JHASHJDGJA4516354")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,

                                        //UserId=Users[userId].Id,

                                        //                User=Users[userId++],

                    Education = UM_6,//new List<string>() {UM_6},
                    Experience="W latach 2009-2019 praca w szpitalu w Węgrowie",
                    //ImagePath="/img/MW/k/1.jpg",
                    HiredSince=new DateTime(2015,5,5),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[23]
                    }

                },
                new DentalHygienist(Persons[33].Id,"HASDUQ561613")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,

                    //                                    User=Users[userId++],
                    //UserId=Users[userId].Id,

                    Education =UM_1,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2015-2021 praca w szpitalu uniwersyteckim w Poznaniu",
                    //ImagePath="/img/MW/k/2.jpg",
                    HiredSince=new DateTime(2015,10,1),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[25]
                    }

                },
                new Doctor(Persons[34].Id,"JHSAD6564513")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,

                                        //UserId=Users[userId].Id,

                                        //                User=Users[userId++],

                    Education =UM_3,// new List<string>() {UM_3},
                    Experience="W latach 2011-2021 praca w szpitalu miejskim w Łowiczu",
                    //ImagePath="/img/MW/k/3.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[9],
                        PrimaryMedicalServices[39]
                    }

                },
                new Doctor(Persons[35].Id,"GASHJD56441231")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,

                                        //UserId=Users[userId].Id,

                                        //                User=Users[userId++],

                    Education =UM_2,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2008-2020 praca w szpitalu zakaźnym w Krakowie",
                    //ImagePath="/img/mw/k/4.jpg",
                    HiredSince=new DateTime(2018,8,11),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[2],
                        PrimaryMedicalServices[0],
                        PrimaryMedicalServices[34]
                    }

                },
                new Doctor(Persons[36].Id,"HBJASD546132")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,

                    //                                    User=Users[userId++],
                    //UserId=Users[userId].Id,

                    Education =UM_4,// new List<string>() {UM_4},
                    Experience="W latach 2007-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/mw/k/5.jpg",
                    HiredSince=new DateTime(2017,7,7),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[7],
                        PrimaryMedicalServices[34]
                    }

                },
                new Doctor(Persons[37].Id,"BIKDAS5416132")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,

                    //UserId=Users[userId].Id,

                    //                                    User=Users[userId++],

                    Education =UM_4,//  new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/mw/k/6.jpg",
                    HiredSince=new DateTime(2017,4,4),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[2],
                        PrimaryMedicalServices[3],
                        PrimaryMedicalServices[34],
                        PrimaryMedicalServices[35]
                    }

                },
                new Doctor(Persons[38].Id,"HJGASW4654613")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,

                    //UserId=Users[userId].Id,

                    //                                    User=Users[userId++],

                    Education =UM_5,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2012-2020 praca w szpitalu południowym w Warszawie",
                    //ImagePath="/img/mw/k/7.jpg",
                    HiredSince=new DateTime(2015,1,11),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[11],PrimaryMedicalServices[14]
                    }

                },
                new Nurse(Persons[39].Id,"IOSHJD4613245")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,

                    //UserId=Users[userId].Id,

                    //                                    User=Users[userId++],

                    Education =UM_2,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu chorób serca w Gdańsku",
                    //ImagePath="/img/mw/k/8.jpg",
                    HiredSince=new DateTime(2018,8,8),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[26],PrimaryMedicalServices[33]
                    }

                },
                new Nurse(Persons[40].Id,"UGHSDS56134564")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,

                    //UserId=Users[userId].Id,

                    //                                    User=Users[userId++],

                    Education =UM_6,// new List<string>() {UM_6},
                    Experience="W latach 2007-2018 praca w szpitalu praskim w Warszawie",
                    //ImagePath="/img/mw/k/9.jpg",
                    HiredSince=new DateTime(2021,11,1),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[26],PrimaryMedicalServices[33]
                    }

                },
                new Doctor(Persons[41].Id,"USHDKAS744561513")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,

                    //UserId=Users[userId].Id,

                    //                                    User=Users[userId++],
                    Education =UM_8,// new List<string>() {UM_8},
                    Experience="W latach 2009-2019 praca w szpitalu praskim w Warszawie",
                    //ImagePath="/img/mw/k/10.jpg",
                    HiredSince=new DateTime(2012,11,11),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[19],
                        PrimaryMedicalServices[41]
                    }

                    },
                new Doctor(Persons[42].Id,"NMBVDSDA546123")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,

                    //UserId=Users[userId].Id,

                    //                                    User=Users[userId++],

                    Education =UM_5,// new List<string>() {UM_5},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/mw/k/11.jpg",
                    HiredSince=new DateTime(2017,7,9),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[20],PrimaryMedicalServices[21]
                    }
                },
                new Doctor(Persons[43].Id,"LKASJD465315")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,
                    Experience="W latach 2005-2020 praca w szpitalu centralnym w Krakowie",

                    Education =UM_2,// new List<string>() {UM_1,UM_2}, Experience="W latach 2012-2019 praca w szpitalu MSWIA w Warszawie",
                    //ImagePath="/img/mw/k/12.jpg",
                    HiredSince=new DateTime(2019,4,8),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[10],
                        PrimaryMedicalServices[5],
                        PrimaryMedicalServices[37],
                        PrimaryMedicalServices[40]
                    }

                },
                new Doctor(Persons[44].Id,"IOHDSFDS46132456")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,

                    Education =UM_7,// new List<string>() {UM_7},
                    Experience="W latach 2005-2020 praca w szpitalu centralnym w Krakowie",
                    //ImagePath="/img/mw/k/13.jpg",
                    HiredSince=new DateTime(2016,6,6),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[2],
                        PrimaryMedicalServices[30],
                        PrimaryMedicalServices[34]
                    }

                },
                new Doctor(Persons[45].Id,"UHJDSF5645132")
                {
                    Id = ++id,

                                        UserId=Users[userId++].Id,

                    //UserId=Users[userId].Id,  //                                    User=Users[userId++], 
                    Education =UM_1,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2019-2021 praca w szpitalu u Koziołka Matołka w Poznaniu",
                    //ImagePath="/img/mw/k/14.jpg",
                    HiredSince=new DateTime(2015,7,1),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[2],
                        PrimaryMedicalServices[30],
                        PrimaryMedicalServices[34]
                    }

                },
                new DentalHygienist(Persons[46].Id,"SDFJL4654131")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,
                    Education =UM_2 ,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu klinicznym we Wrocławiu",
                    //ImagePath="/img/mw/k/15.jpg",
                    HiredSince=new DateTime(2017,2,11),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[25]
                    }

                },
                new Doctor(Persons[47].Id,"JBNBJHSD45642131")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,

                    Education =UM_3,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2018-2021 praca w szpitalu klinicznym we Wrocławiu",
                    //ImagePath="/img/mw/k/16.jpg",
                    HiredSince=new DateTime(2021,2,1),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[5],
                        PrimaryMedicalServices[37]
                    }
                },
                new Doctor(Persons[48].Id,"JHGFJDS564165412")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,

                    Education =UM_8,// new List<string>() {UM_8},
                    Experience="W latach 2019-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/mw/k/17.jpg",
                    HiredSince=new DateTime(2021,1,9),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[10],
                        PrimaryMedicalServices[40]
                    }

                },
                new Doctor(Persons[49].Id,"JHFDSF4561231")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,

                    Education =UM_4,// new List<string>() {UM_4},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/mw/k/18.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[20],PrimaryMedicalServices[24]
                    }

                },
                new Doctor(Persons[50].Id,"UIFSDF4561321")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,

                    Education =UM_7,// new List<string>() {UM_5,UM_7},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/mw/k/19.jpg",
                    HiredSince=new DateTime(2019,4,4),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[9],
                        PrimaryMedicalServices[39]
                    }

                },
                new ElectroradiologyTechnician(Persons[51].Id,"DHJKFSD4564132")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,


                    Education =UM_9,// new List<string>() {UM_7,UM_9},
                    Experience="Staż odbyła w szpitalu Bródnowskim w Warszawie. Od 2016 roku pracuje w szpitalu Praskim w Warszawie.",
                    //ImagePath="/img/mw/k/20.jpg",
                    HiredSince=new DateTime(2018,9,11),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[28]
                    }

                },
                new Nurse(Persons[52].Id,"HBJKSDF56413215")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,

                    Education =UM_6,// new List<string>() {UM_6,UM_2},
                    Experience="Staż odbyty w szpitalu akademickim w Białymstoku. Od 2018 roku praca w szpitalu powiatowym w Węgrowie",
                    //ImagePath="/img/mw/k/21.jpg",
                    HiredSince=new DateTime(2018,8,8),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[26],PrimaryMedicalServices[33]
                    }

                },
                new Doctor(Persons[53].Id, "RERDSDF2134969")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,

                    Education = UM_4,// new List<string>() {UM_4,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/mw/k/22.jpg",
                    HiredSince=new DateTime(2018,4,6),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[2],
                        PrimaryMedicalServices[18],
                        PrimaryMedicalServices[34]
                    }

                },
                new Nurse(Persons[54].Id,"BNMDSF546123")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,

                    Education =UM_1,// new List<string>() {UM_5},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/mw/k/23.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[33],PrimaryMedicalServices[26]
                    }

                },
                new ElectroradiologyTechnician(Persons[55].Id,"PODBASHJ4454321")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,

                    Education =UM_3,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/mw/k/24.jpg",
                    HiredSince=new DateTime(2019,1,1),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[28],PrimaryMedicalServices[29]
                    }

                },
                new Doctor(Persons[56].Id,"YHBKASD5465123")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,

                    Education =UM_1,// new List<string>() {UM_3},
                    Experience="W latach 2014-2021 praca w szpitalu zielonogórskim",
                    //ImagePath="/img/mw/k/25.jpg",
                    HiredSince=new DateTime(2013,3,3),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[11],PrimaryMedicalServices[14]
                    }

                },
                new Doctor(Persons[57].Id,"OPQEW6546132")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,

                    Education =UM_4,// new List<string>() {UM_5},
                    Experience="W latach 2005-2020 praca w szpitalu wojewódzkim w Olsztynie",
                    //ImagePath="/img/mw/k/26.jpg",
                    HiredSince=new DateTime(2018,4,3),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[9],
                        PrimaryMedicalServices[39]
                    }

                },
                new Doctor(Persons[58].Id,"OPNKWEJR546132")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,
                    Education =UM_6,// new List<string>() {UM_8},
                    Experience="Od 2010 roku pracuje jako ordynator w szpitalu Matki i Dziecka w Warszawie",
                    //ImagePath="/img/mw/k/27.jpg",
                    HiredSince=new DateTime(2018,6,7),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                        MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[5],
                        PrimaryMedicalServices[10],
                        PrimaryMedicalServices[37],
                        PrimaryMedicalServices[40]
                    }

                },
                new Physiotherapist(Persons[59].Id,"GVJDAS54645")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,

                    Education =UM_7,// new List<string>() {UM_9},
                    Experience="W latach 2016-2020 praca w szpitalu miejskim w Grudziądzu",
                    //ImagePath="/img/mw/k/28.jpg",
                    HiredSince=new DateTime(2019,8,11),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                        MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[31],PrimaryMedicalServices[32],PrimaryMedicalServices[30]
                    }

                },
                new Doctor(Persons[60].Id,"UIHDAS546516")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,

                                        Education =UM_9,    // new List<string>() {UM_1,UM_9},
                    Experience="W latach 2009-2020 praca w szpitalu miejskim w Suwałkach",
                    //ImagePath="/img/mw/k/29.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[8]
                    }

                },
                new Nurse(Persons[61].Id,"ADASD46123")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,

                    Education =UM_7,// new List<string>() {UM_7,UM_2},
                    Experience="W latach 2009-2020 praca w szpitalu wojewódzkim w Toruniu",
                    //ImagePath="/img/mw/k/30.jpg",
                    HiredSince=new DateTime(2019,5,4),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[26],PrimaryMedicalServices[33]
                    }

                },
                new ElectroradiologyTechnician(Persons[62].Id,"YUGDSD56131")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,
                    Education =UM_2,// new List<string>() {UM_2,UM_4},
                    Experience="Od 2016 pracuje w szpitalu Bródnowskim",
                    //ImagePath="/img/mw/k/31.jpg",
                    HiredSince=new DateTime(2015,5,1),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[28],PrimaryMedicalServices[29]
                    }
                },
                new Doctor(Persons[63].Id,"YAJHD5461321")
                {
                    Id = ++id,
                    UserId=Users[userId++].Id,
                    Education =UM_5,// new List<string>() {UM_5},
                    Experience="W latach 2009-2021 praca w szpitalu w Przemyślu",
                    //ImagePath="/img/mw/k/32.jpg",
                    HiredSince=new DateTime(2019,9,8),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[20],PrimaryMedicalServices[21]
                    }
                },
                new Doctor(Persons[64].Id,"OOXCZX6541546")
                {
                    Id = ++id,
                    UserId=Users[userId++].Id,
                    //User=Users[userId++],
                    Education =UM_3,// new List<string>() {UM_3},
                    Experience="W latach 2008-2020 praca w szpitalu w Lublinie",
                    //ImagePath="/img/mw/k/33.jpg",
                    HiredSince=new DateTime(2019,4,7),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[16],
                    }
                },
                new Physiotherapist(Persons[65].Id,"FSDRGD54543")
                {
                    Id = ++id,
                    UserId=Users[userId++].Id,

                    Education =UM_2,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/mw/k/34.jpg",
                    HiredSince=new DateTime(2015,9,1),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[31],PrimaryMedicalServices[32],PrimaryMedicalServices[30]
                    }
                },
                new Doctor(Persons[66].Id,"UHJKSAD51321")
                {
                    Id = ++id,
                    UserId=Users[userId++].Id,

                    Education =UM_3,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/mw/k/35.jpg",
                    HiredSince=new DateTime(2019,4,3),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[19],
                        PrimaryMedicalServices[41]
                    }
                },
                new Doctor(Persons[67].Id,"BNSDSA546123")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,

                    Education =UM_5,// new List<string>() {UM_5,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/mw/k/36.jpg",
                    HiredSince=new DateTime(2018,8,1),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[9],
                        PrimaryMedicalServices[39]
                    }
                },
                new ElectroradiologyTechnician(Persons[68].Id,"KLSAD546123")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,

                    Education =UM_1,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2016-2020 praca w szpitalu lwowskim na Ukrainie",
                    //ImagePath="/img/mw/k/37.jpg",
                    HiredSince=new DateTime(2020,8,1),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[28],PrimaryMedicalServices[29]
                    }
                },
                new DentalHygienist(Persons[69].Id,"JHDAS4564231")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,

                    Education =UM_3,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ////ImagePath="/img/MW/k/38.jpg",
                    HiredSince=new DateTime(2015,2,4),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[25]
                    }
                },
                new DentalHygienist(Persons[70].Id,"JHDAS4564231")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,

                    Education =UM_5,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2016-2021 praca w szpitalu centralnym w Łodzi",
                    ////ImagePath="/img/MW/k/38.jpg",
                    HiredSince=new DateTime(2018,8,1),
                    IsActive=true,
                    //VisitReviews=visitRatings3,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[25],
                        //PrimaryMedicalServices[33]
                    }
                },
                new DentalHygienist(Persons[71].Id,"JHDAS4564231")
                {
                    Id = ++id,
                    UserId=Users[userId++].Id,

                    Education =UM_4,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2013-2022 praca w Głównym Szpitalu Śląskim",
                    ////ImagePath="/img/MW/k/38.jpg",
                    HiredSince=new DateTime(2014,8,8),
                    IsActive=true,
                    //VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[25],
                    }
                },
                new DentalHygienist(Persons[72].Id,"HAISDAS465462")
                {
                    Id = ++id,
                                        UserId=Users[userId++].Id,

                    Education =UM_3,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2015-2020 praca w szpitalu Wolskim",
                    ////ImagePath="/img/MW/k/38.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsActive=true,
                    //VisitReviews=visitRatings2,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[25]
                    }
                },
            };

            foreach (MedicalWorker item in MedicalWorkers)
            {
                Users.Where(c => c.Id == item.UserId).First().MedicalWorkerId = item.Id;
            }
            //MedicalServiceToMedicalWorkers = new List<MedicalServiceToMedicalWorker>();
            //long idMsMw = 0;
            //foreach (MedicalWorker mw in MedicalWorkers)
            //{
            //    foreach (MedicalService ms in mw.MedicalServices)
            //    {
            //        MedicalServiceToMedicalWorkers.Add(new MedicalServiceToMedicalWorker() { Id = ++idMsMw, MedicalServiceId = ms.Id, MedicalWorkerId = mw.Id });
            //    }
            //}

            return MedicalWorkers;
        }

        public static IEnumerable<NFZUnit> GetNFZUnits()
        {
            List<NFZUnit> units = new()
            {
                new NFZUnit() { Id = 1, Code = "DLŚ", Description = "Dolnośląski Fundusz Zdrowia", Voivodeship = Core.Enums.VoivodeshipType.dolnoslaskie },
                new NFZUnit() { Id = 2, Code = "KPM", Description = "Kujawsko-Pomorski Fundusz Zdrowia", Voivodeship = Core.Enums.VoivodeshipType.kujawskoPomorskie },
                new NFZUnit() { Id = 3, Code = "LBL", Description = "Lubelski Fundusz Zdrowia", Voivodeship = Core.Enums.VoivodeshipType.lubelskie },
                new NFZUnit() { Id = 4, Code = "LBS", Description = "Lubuski Fundusz Zdrowia", Voivodeship = Core.Enums.VoivodeshipType.lubuskie },
                new NFZUnit() { Id = 5, Code = "ŁDZ", Description = "Łódzki Fundusz Zdrowia", Voivodeship = Core.Enums.VoivodeshipType.lodzkie },
                new NFZUnit() { Id = 6, Code = "MŁP", Description = "Małopolski Fundusz Zdrowia", Voivodeship = Core.Enums.VoivodeshipType.malopolskie },
                new NFZUnit() { Id = 7, Code = "MAZ", Description = "Mazowiecki Fundusz Zdrowia", Voivodeship = Core.Enums.VoivodeshipType.mazowieckie },
                new NFZUnit() { Id = 8, Code = "OPO", Description = "Opolski Fundusz Zdrowia", Voivodeship = Core.Enums.VoivodeshipType.opolskie },
                new NFZUnit() { Id = 9, Code = "PDK", Description = "Podkarpacki Fundusz Zdrowia", Voivodeship = Core.Enums.VoivodeshipType.podkarpackie },
                new NFZUnit() { Id = 10, Code = "PDL", Description = "Podlaski Fundusz Zdrowia", Voivodeship = Core.Enums.VoivodeshipType.podlaskie },
                new NFZUnit() { Id = 11, Code = "POM", Description = "Pomorski Fundusz Zdrowia", Voivodeship = Core.Enums.VoivodeshipType.pomorskie },
                new NFZUnit() { Id = 12, Code = "ŚLĄ", Description = "Śląski Fundusz Zdrowia", Voivodeship = Core.Enums.VoivodeshipType.śląskie },
                new NFZUnit() { Id = 13, Code = "ŚWI", Description = "Świętokrzyski Fundusz Zdrowia", Voivodeship = Core.Enums.VoivodeshipType.swietokrzyskie },
                new NFZUnit() { Id = 14, Code = "WAM", Description = "Warmińsko-Mazurski Fundusz Zdrowia", Voivodeship = Core.Enums.VoivodeshipType.warminskoMazurskie },
                new NFZUnit() { Id = 15, Code = "WLP", Description = "Wielkopolski Fundusz Zdrowia", Voivodeship = Core.Enums.VoivodeshipType.wielkopolskie },
                new NFZUnit() { Id = 16, Code = "ZAP", Description = "Zachodniopomorski Fundusz Zdrowia", Voivodeship = Core.Enums.VoivodeshipType.zachodniopomorskie }
            };
            return units;
        }

        internal static List<MedicalReferral> GetDummyMedicalReferrals(Patient patient, DateTimeOffset now)
        {
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
                    IssueDate=now.AddDays(-9),
                    ExpireDate=now.AddDays(-15).AddMonths(3),
                    IssuedBy=MedicalWorkers.ElementAt(5),
                    IssuedTo=patient,
                    PrimaryMedicalService=PrimaryMedicalServices[23],
                    //VisitSummary=visitSummaries.ElementAt(6)
                },
                new MedicalReferral()
                {
                    Id=7,
                    IssueDate=now.AddDays(-2),
                    ExpireDate=now.AddDays(-15).AddMonths(3),
                    IssuedBy=MedicalWorkers.ElementAt(26),
                    IssuedTo=patient,
                    PrimaryMedicalService=PrimaryMedicalServices[33],
                    //VisitSummary=visitSummaries.ElementAt(7)
                },
                new MedicalReferral()
                {
                    Id=8,
                    IssueDate=now.AddDays(-4),
                    ExpireDate=now.AddDays(-15).AddMonths(3),
                    IssuedBy=MedicalWorkers.ElementAt(26),
                    IssuedTo=patient,
                    PrimaryMedicalService=PrimaryMedicalServices[33],
                    MinorMedicalService=MedicalServices[7],
                    //VisitSummary=visitSummaries.ElementAt(7)
                },
                new MedicalReferral()
                {
                    Id=9,
                    IssueDate=now.AddDays(-6),
                    ExpireDate=now.AddDays(-15).AddMonths(3),
                    IssuedBy=MedicalWorkers.ElementAt(26),
                    IssuedTo=patient,
                    PrimaryMedicalService=PrimaryMedicalServices[33],
                    MinorMedicalService=MedicalServices[8],
                    //VisitSummary=visitSummaries.ElementAt(7)
                },
                new MedicalReferral()
                {
                    Id=10,
                    IssueDate=now.AddDays(-7),
                    ExpireDate=now.AddDays(-15).AddMonths(3),
                    IssuedBy=MedicalWorkers.ElementAt(26),
                    IssuedTo=patient,
                    PrimaryMedicalService=PrimaryMedicalServices[33],
                    MinorMedicalService=MedicalServices[9],
                    //VisitSummary=visitSummaries.ElementAt(7)
                },
                new MedicalReferral()
                {
                    Id=10,
                    IssueDate=now.AddDays(-5),
                    ExpireDate=now.AddDays(-15).AddMonths(3),
                    IssuedBy=MedicalWorkers.ElementAt(26),
                    IssuedTo=patient,
                    PrimaryMedicalService=PrimaryMedicalServices[27],
                },
                new MedicalReferral()
                {
                    Id=10,
                    IssueDate=now.AddDays(-2),
                    ExpireDate=now.AddDays(-15).AddMonths(3),
                    IssuedBy=MedicalWorkers.ElementAt(26),
                    IssuedTo=patient,
                    PrimaryMedicalService=PrimaryMedicalServices[28],
                    //VisitSummary=visitSummaries.ElementAt(7)
                },
                                new MedicalReferral()
                {
                    Id=10,
                    IssueDate=now.AddDays(-2),
                    ExpireDate=now.AddDays(-15).AddMonths(3),
                    IssuedBy=MedicalWorkers.ElementAt(26),
                    IssuedTo=patient,
                    PrimaryMedicalService=PrimaryMedicalServices[23],
                    //VisitSummary=visitSummaries.ElementAt(7)
                },
                                new MedicalReferral()
                {
                    Id=10,
                    IssueDate=now.AddDays(-2),
                    ExpireDate=now.AddDays(-15).AddMonths(3),
                    IssuedBy=MedicalWorkers.ElementAt(26),
                    IssuedTo=patient,
                    PrimaryMedicalService=PrimaryMedicalServices[29],
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

            referrals.Where(c => c.MinorMedicalService != null).ToList().ForEach(c => c.MinorMedicalServiceId = c.MinorMedicalService.Id);
            referrals.ForEach(c => c.PrimaryMedicalServiceId = c.PrimaryMedicalService.Id);
            return referrals;
        }

        private static List<Recommendation> GetSomeRecommendations()
        {
            return new List<Recommendation>()
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
        }

        internal static List<MedicalTestResult> GetDummyMedicalTestResults()
        {
            List<MedicalTestResult> medicalTestResults = new List<MedicalTestResult>()
            {
                new MedicalTestResult()
                {
                    Description="Wyniki rozszerzonych badań krwi",
                    MedicalService=MedicalServices[8],
                  //  Document= Properties.Resources.Badania_krwi_i_moczu                ,  //new PdfSharpCore.Pdf.PdfDocument( new MemoryStream( Properties.Resources.Badania_krwi_i_moczu                )),
                    DocumentPath=@"\Results\1.pdf",
                    Id=1,
                },
                new MedicalTestResult()
                {
                    Id=2,
                    Description="Wyniki podstawowych badań krwi",
                    MedicalService=MedicalServices[7],
                    //Document= Properties.Resources.badania_krwi,// new PdfSharpCore.Pdf.PdfDocument( new MemoryStream( Properties.Resources.badania_krwi                ))
                    DocumentPath=@"\Results\2.pdf",

                },
                new MedicalTestResult()
                {
                    Id=3,
                    Description="Wyniki badań cholesterolu",
                    MedicalService=MedicalServices[50],
                    //Document=Properties.Resources.cholesterol,//  new PdfSharpCore.Pdf.PdfDocument( new MemoryStream( Properties.Resources.cholesterol ))
                    DocumentPath=@"\Results\3.pdf",

                },
                new MedicalTestResult()
                {
                    Id=4,
                    Description="Wyniki ekg serca",
                    MedicalService=MedicalServices[0],
                    Document=Properties.Resources.ekg,//  new PdfSharpCore.Pdf.PdfDocument( new MemoryStream( Properties.Resources.ekg))
                                        DocumentPath=@"\Results\4.pdf",

                },
                new MedicalTestResult()
                {
                    Id=5,
                    Description="RTG nadgarstka z trzech stron",
                    MedicalService=MedicalServices[86],//wybrać ekg serca
                    Document=Properties.Resources.ekg,//new PdfSharpCore.Pdf.PdfDocument(new MemoryStream(Properties.Resources.ekg)),
                    DocumentPath=@"\Results\5.pdf",

                }

            };
            return medicalTestResults;
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
                new MedicalService(){Id=34,Name="Laseroterapia",Description="Laseroterapia", StandardPrice=200, IsPrimaryService=false},
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
                //new MedicalService(){Id=73,Name="Medycyna pracy",Description="Medycyna pracy", StandardPrice=200, IsPrimaryService=true, RequireRefferal=true},

                new MedicalService(){Id=30,Name="Masaż leczniczy",Description="Masaż leczniczy", StandardPrice=300, IsPrimaryService=true, RequireRefferal=true},
                new MedicalService(){Id=31,Name="Zajęcia rehabilitacyjne",Description="Zajęcia rehabilitacyjne", StandardPrice=300, IsPrimaryService=true, RequireRefferal=true},
                new MedicalService(){Id=75,Name="Fizykoterapia",Description="Fizykoterapia", StandardPrice=400, IsPrimaryService=true, RequireRefferal=true} ,
                new MedicalService(){Id=74,Name="Badanie laboratoryjne",Description="Badanie laboratoryjne", StandardPrice=100, IsPrimaryService=true, RequireRefferal=true},
                new MedicalService(){Id=94,Name="Badanie cholesterolu",Description="Badanie cholesterolu", StandardPrice=100, IsPrimaryService=false, RequireRefferal=true},
                new MedicalService(){Id=95, Name="USG Dopplera", Description="USG Dopplera", StandardPrice=150, IsPrimaryService=false,RequireRefferal=true},
                //proktolog
                new MedicalService(){Id=96, Name="Anoskopia", Description="Anoskopia", StandardPrice=100, IsPrimaryService=false,RequireRefferal=true},
                new MedicalService(){Id=97, Name="Rektoskopia", Description="Rektoskopia", StandardPrice=150, IsPrimaryService=false,RequireRefferal=true},
                new MedicalService(){Id=98, Name="Kolanoskopia", Description="Kolanoskopia", StandardPrice=200, IsPrimaryService=false,RequireRefferal=true},


                //new MedicalService(){Id=100,Name="e-Konsultacja gastrologiczna",            Description="e-Konsultacja gastrologiczna", StandardPrice=250, IsPrimaryService=true},
                //new MedicalService(){Id=101,Name="e-Konsultacja proktologiczna",            Description="e-Konsultacja proktologiczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=102,Name="e-Konsultacja internistyczna",            Description="e-Konsultacja internistyczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=103,Name="e-Konsultacja pediatryczna",              Description="e-Konsultacja pediatryczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=104,Name="e-Konsultacja geriatryczna",              Description="e-Konsultacja geriatryczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=105,Name="e-Konsultacja ginekologiczna",            Description="e-Konsultacja ginekologiczna", StandardPrice=200, IsPrimaryService=true},
                //new MedicalService(){Id=106,Name="e-Konsultacja ortopedyczna",              Description="e-Konsultacja ortopedyczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=107,Name="e-Konsultacja kardiologiczna",            Description="e-Konsultacja kardiologiczna", StandardPrice=200, IsPrimaryService=true},
                //new MedicalService(){Id=108,Name="e-Konsultacja okulistyczna",              Description="e-Konsultacja okulistyczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=109,Name="e-Konsultacja dermatologiczna",           Description="e-Konsultacja dermatologiczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=110,Name="e-Konsultacja endokrynologiczna",         Description="e-Konsultacja endokrynologiczna", StandardPrice=200, IsPrimaryService=true},
                //new MedicalService(){Id=111,Name="e-Konsultacja chirurgii ogólnej",         Description="e-Konsultacja chirurgii ogólnej", StandardPrice=200, IsPrimaryService=true},
                //new MedicalService(){Id=112,Name="e-Konsultacja neurochirurgiczna",         Description="e-Konsultacja neurochirurgiczna", StandardPrice=250, IsPrimaryService=true},
                //new MedicalService(){Id=113,Name="e-Konsultacja chirurgii naczyniowej",     Description="e-Konsultacja chirurgii naczyniowej", StandardPrice=250, IsPrimaryService=true},
                //new MedicalService(){Id=114,Name="e-Konsultacja chirurgii plastycznej",     Description="e-Konsultacja chirurgii plastycznej", StandardPrice=300, IsPrimaryService=true},
                //new MedicalService(){Id=115,Name="e-Konsultacja chirurgii onkologicznej",   Description="e-Konsultacja chirurgii onkologicznej", StandardPrice=300, IsPrimaryService=true},
                //new MedicalService(){Id=116,Name="e-Konsultacja laryngologiczna",           Description="e-Konsultacja laryngologiczna", StandardPrice=200, IsPrimaryService=true},
                //new MedicalService(){Id=117,Name="e-Konsultacja neurologiczna",             Description="e-Konsultacja neurologiczna", StandardPrice=200, IsPrimaryService=true},
                //new MedicalService(){Id=118,Name="e-Konsultacja urologiczna",               Description="e-Konsultacja urologiczna", StandardPrice=200, IsPrimaryService=true},
                new MedicalService(){Id=119,Name="e-Konsultacja psychologiczna",            Description="e-Konsultacja psychologiczna", StandardPrice=200, IsPrimaryService=true},

            };

            //chirurgia
            services.Where(d => d.Id == 48).FirstOrDefault().SubServices = new List<MedicalService>(services.GetRange(33, 6));
            services.Where(d => d.Id == 48).FirstOrDefault().SubServices.ForEach(c => c.PrimaryServiceId = 48);

            //services[48].SubServices.Append(services[10]);
            services.Where(d => d.Id == 50).FirstOrDefault().SubServices = new List<MedicalService>() { services.Where(c => c.Id == 95).FirstOrDefault() };
            services.Where(d => d.Id == 50).FirstOrDefault().SubServices.ForEach(c => c.PrimaryServiceId = 50);


            //ortopeda
            MedicalService service = services.Where(d => d.Id == 43).FirstOrDefault();
            MedicalService subService = services.Where(c => c.Id == 85).FirstOrDefault();
            service.SubServices = new List<MedicalService>();
            service.SubServices.Add(subService);
            service.SubServices.Add(services.Where(c => c.Id == 9).FirstOrDefault());
            service.SubServices.ForEach(c => c.PrimaryServiceId = service.Id);

            //gastrologia

            services.Where(c => c.Id == 4).First().SubServices = new List<MedicalService>() { services.Where(c => c.Id == 6).FirstOrDefault(), services.Where(c => c.Id == 8).FirstOrDefault() };
            services.Where(c => c.Id == 4).First().SubServices.ForEach(c => c.PrimaryServiceId = 4);

            //okulista
            service = services.Where(c => c.Id == 45).FirstOrDefault();
            service.SubServices = new List<MedicalService>(services.Where(c => c.Id >= 80 && c.Id <= 85));
            service.SubServices.Add(services.Where(c => c.Id == 5).FirstOrDefault());
            service.SubServices.ForEach(c => c.PrimaryServiceId = 45);


            //laryngologia
            service = services.Where(c => c.Id == 53).First();
            service.SubServices = new List<MedicalService>();
            service.SubServices.Add(services.Where(c => c.Id == 37).First());
            service.SubServices.Add(services.Where(c => c.Id == 7).First());
            service.SubServices.ForEach(c => c.PrimaryServiceId = service.Id);


            //stomatologia
            services.Where(c => c.Id == 62).FirstOrDefault().SubServices = new List<MedicalService>(services.Where(c => c.Id >= 11 && c.Id < 13));
            services.Where(c => c.Id == 58).FirstOrDefault().SubServices = new List<MedicalService>(services.Where(c => c.Id >= 77 && c.Id < 80));
            services.Where(c => c.Id == 59).First().SubServices = new List<MedicalService>(services.Where(c => c.Id >= 13 && c.Id < 17));
            services.Where(c => c.Id == 60).First().SubServices = new List<MedicalService>(services.Where(c => c.Id >= 17 && c.Id < 19));
            services.Where(c => c.Id == 61).First().SubServices = new List<MedicalService>(services.Where(c => c.Id >= 63 && c.Id < 66));
            services.Where(c => c.Id == 57).First().SubServices = new List<MedicalService>(services.Where(c => c.Id >= 19 && c.Id < 24));

            services.Where(c => c.Id == 62).First().SubServices.ForEach(c => c.PrimaryServiceId = 62);
            services.Where(c => c.Id == 58).First().SubServices.ForEach(c => c.PrimaryServiceId = 58);
            services.Where(c => c.Id == 59).First().SubServices.ForEach(c => c.PrimaryServiceId = 59);
            services.Where(c => c.Id == 60).First().SubServices.ForEach(c => c.PrimaryServiceId = 60);
            services.Where(c => c.Id == 61).First().SubServices.ForEach(c => c.PrimaryServiceId = 61);
            services.Where(c => c.Id == 57).First().SubServices.ForEach(c => c.PrimaryServiceId = 57);

            //badania laboratoryjne oraz szczepienia
            services.Where(c => c.Id == 74).First().SubServices = new List<MedicalService>(services.Where(c => c.Id >= 24 && c.Id < 30));
            services.Where(c => c.Id == 74).First().SubServices.Add(services.Where(c => c.Id == 94).First());
            services.Where(c => c.Id == 76).First().SubServices = new List<MedicalService>(services.Where(c => c.Id >= 66 && c.Id < 73));

            services.Where(c => c.Id == 74).First().SubServices.ForEach(c => c.PrimaryServiceId = 74);
            services.Where(c => c.Id == 76).First().SubServices.ForEach(c => c.PrimaryServiceId = 76);

            //fizjoterapia
            services.Where(c => c.Id == 75).First().SubServices = new List<MedicalService>(services.Where(c => c.Id >= 32 && c.Id < 37));
            services.Where(c => c.Id == 75).First().SubServices.ForEach(c => c.PrimaryServiceId = 75);

            //szczepienia

            //proktolog

            service = services.Where(c => c.Id == 38).First();
            service.SubServices = new List<MedicalService>(services.Where(c => c.Id >= 96 && c.Id <= 98));
            service.SubServices.ForEach(c => c.PrimaryServiceId = service.Id);

            //kardiolog
            service = services.Where(c => c.Id == 44).First();
            service.SubServices = new List<MedicalService>(services.Where(c => c.Id >= 91 && c.Id <= 93));
            service.SubServices.ForEach(c => c.PrimaryServiceId = service.Id);

            return services;
        }

        public static IEnumerable<VisitCategory> GetVisitCategories()
        {
            List<VisitCategory> categories = new()
            {
                new VisitCategory() { Id = 1, CategoryName = "Konsultacje stacjonarne", MedicalServices = new List<MedicalService>(PrimaryMedicalServices.GetRange(0, 20)), Type = Core.Enums.VisitCategoryType.Consultations },
                new VisitCategory() { Id = 2, CategoryName = "E-konsultacje", MedicalServices = new List<MedicalService>(PrimaryMedicalServices.Where(c => c.Id >= 100)), Type = Core.Enums.VisitCategoryType.EConsultations },
                new VisitCategory() { Id = 3, CategoryName = "Stomatologia", MedicalServices = new List<MedicalService>(PrimaryMedicalServices.GetRange(20, 6)), Type = Core.Enums.VisitCategoryType.Stomatology },
                new VisitCategory() { Id = 4, CategoryName = "Diagnostyka obrazowa ", MedicalServices = new List<MedicalService>(PrimaryMedicalServices.GetRange(27, 3)), Type = Core.Enums.VisitCategoryType.MedicalImaging },
                new VisitCategory() { Id = 5, CategoryName = "Fizjoterapia", MedicalServices = new List<MedicalService>(PrimaryMedicalServices.GetRange(30, 3)), Type = Core.Enums.VisitCategoryType.Physiotherapy },
                new VisitCategory() { Id = 6, CategoryName = "Gabinet zabiegowy", MedicalServices = new List<MedicalService>() { PrimaryMedicalServices[26], PrimaryMedicalServices[33] }, Type = Core.Enums.VisitCategoryType.TreatmentRoom },
            };
            categories.ForEach(c => c.MedicalServices.ForEach(d => d.VisitCategoryId = c.Id));
            PrimaryMedicalServices.Where(c => c.SubServices != null).ToList().ForEach(d => d.SubServices.ForEach(c => c.VisitCategoryId = d.VisitCategoryId));

            //categories[0].PrimaryMedicalServices.Add(MedicalServices[0]);
            return categories;
        }


        public static Location GetLocationById(long locationId)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<Patient> GetAllPatients()
        {
            int id = 76;
            int patientId = 0;

            List<Patient> patients = new List<Patient>();
            Random rnd = new Random();

            List<string> companies = new List<string>()
            {
                "Agfa sp. jawna",
                "Alfa Bum Sp. z o.o.",
                "Alior Bank S.A.",
                "Api Market sp. z o.o.",
                "Apple Sp. z o.o.",
                "Biedronka sp. z o.o.",
                "CCC Sp. z o.o.",
                "Coca Cola",
                "Compensa z o.o.",
                "Dell Polska S.A.",
                "Henkel S.A.",
                "Kowalski i spółka",
                "Lasy Pańśtwowe",
                "LOT S.A.",
                "Lubaszka wypieki",
                "McKinsey Polska Sp. z o.o.",
                "Microsoft Polska",
                "Nestle Polska",
                "PJATK",
                "PKP Intercity",
                "PKP Informatyka",
                "Samsung Polska",
                "Six Sigma Poland",
                "Styropmin",
                "UM Ząbki",
                "UM Warszawa",
                "Uniwersytet Warszawski",
                "Wirtualna Polska",
                "Wedel sp. z o.o."
            };
            List<string> nips = new List<string>()
            {
                "5223225632",
                "7010322625",
                "1070010731",
                "7010335504",
                "5223027866",
                "5492451709",
                "9542802634",
                "7773032833",
                "7010080346",
                "1080008799",
                "7010426342",
                "1132873168",
                "7272792094",
                "8322085733",
                "5262544258",
                "9542381960",
                "9542187614",
                "5262542704",
                "5262562977",
                "5252621690",
                "5220103934",
                "9552254186",
                "7252309189",
                "8571842707",
                "7280132254",
                "5262649402",
                "5262654142",
                "1132875351",
                "7282844183"
            };

            int maxCompany = companies.Count()-1;
            int maxUnit = NfzUnits.Count()-1;
            int maxPackage = MedicalPackages.Count() - 1;
            int companyIndex = -1;
            int packageIndex = -1;
            int unitIndex = -1;

            for (int i = 0; i < 141; i++)
            {
                companyIndex = rnd.Next(0, maxCompany);
                unitIndex = rnd.Next(0, maxUnit);
                packageIndex = rnd.Next(0, maxPackage);

                Patient patient = new Patient(Persons[++id])
                {
                    Id = ++patientId,
                    EmployerNIP = nips[companyIndex],
                    MedicalPackage = MedicalPackages[packageIndex],
                    NFZUnit = NfzUnits[unitIndex],
                    EmployerName = companies[companyIndex],
                    UserId = Persons[id].Id
                };
                patients.Add(patient);
            }

            patients.ForEach(c => Users.ToList().Where(d => d.Id == c.UserId).FirstOrDefault().PatientId = c.Id);
            patients.ForEach(c => c.MedicalPackageId = c.MedicalPackage.Id);
            patients.ForEach(c => c.NFZUnitId = c.NFZUnit.Id);

            return patients;
        }

        public static Patient GetPatientById(long id)
        {

            Patient patient = AllPatients.Where(c => c.Id == id).FirstOrDefault();
            patient.AllVisits.AddRange(BookedVisits.Where(c => c.Patient.Id == patient.Id).ToList());
            patient.AllVisits.AddRange(HistoricalVisits.Where(c => c.Patient.Id == patient.Id).ToList());
            //patient.BookedVisits = BookedVisits.Where(c => c.Patient.Id == patient.Id).ToList();
            //patient.HistoricalVisits = HistoricalVisits.Where(c => c.Patient.Id == patient.Id).ToList();
            //patient.MedicalReferrals = patient.HistoricalVisits?.Select(c => c.ExaminationReferrals).ToList();
            //patient.Prescriptions= patient.HistoricalVisits?.Select(c => c.ExaminationReferrals).ToList();
            //patient.TestsResults= patient.HistoricalVisits?.Select(c => c.MedicalResult).ToList();

            //return AllPatients.Where(c => c.Id == id).FirstOrDefault();
            return patient;
            //return CurrentPatient;
        }

        public static Visit GetAvailableVisitById(long id)
        {
            Visit visit = AvailableVisits.Where(c => c.Id == id).FirstOrDefault();
            return visit;
        }

        public static MedicalWorker GetMedicalWorkerById(long id)
        {
            return MedicalWorkers.Where(c => c.Id == id).FirstOrDefault();
        }

        //public static Visit GetHistoricalVisitById(long id)
        //{
        //    //Visit visit = .Where(c => c.Id == id).FirstOrDefault();
        //    //return visit;
        //    return new Visit();
        //}

        public static MedicalService GetMedicalServiceById(long id)
        {
            return MedicalServices.Where(c => c.Id == id).FirstOrDefault();
        }

        public static MedicalPackage GetMedicalPackageById(long id)
        {
            MedicalPackage medicalPackage = MedicalPackages.Where(c => c.Id == id).FirstOrDefault();
            return medicalPackage;
        }

        public static NFZUnit GetNFZUnitById(long id)
        {
            NFZUnit unit = NfzUnits.Where(c => c.Id == id).FirstOrDefault();
            return unit;
        }

        public static VisitCategory GetVisitCategoryById(long id)
        {
            VisitCategory category = VisitCategories.Where(c => c.Id == id).FirstOrDefault();
            return category;
        }

        public static IEnumerable<MedicalRoom> GetMedicalRooms()
        {
            //List<List<MedicalRoom>> roomsCollections = new List<List<MedicalRoom>>()
            //{
            long id = 0;
            List<MedicalRoom> rooms = new List<MedicalRoom>()
            {
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Cardiological,
                    Name = "1"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Dental,
                    Name = "2"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "3"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "4"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Gynecological,
                    Name = "5"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Laryngological,
                    Name = "6"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.MedicalImaging,
                    Name = "7"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Neurological,
                    Name = "8"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Ophthalmology,
                    Name = "9"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.OralHygiene,
                    Name = "10"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.OralHygiene,
                    Name = "11"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Rehabilitation,
                    Name = "12"
                },

            //},
            //    new List<MedicalRoom>()
            //{
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=1,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Surgical,
                    Name="1A"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=1,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Treatment,
                    Name="1B"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=1,
                    MedicalRoomType=Core.Enums.MedicalRoomType.General,
                    Name="1C"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=1,
                    MedicalRoomType=Core.Enums.MedicalRoomType.General,
                    Name="1D"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=1,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Laryngological,
                    Name="1E"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=2,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Cardiological,
                    Name="2A"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=2,
                    MedicalRoomType=Core.Enums.MedicalRoomType.MedicalImaging,
                    Name="2B"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=2,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Ophthalmology,
                    Name="2C"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=2,
                    MedicalRoomType=Core.Enums.MedicalRoomType.General,
                    Name="2D"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=3,
                    MedicalRoomType=Core.Enums.MedicalRoomType.General,
                    Name="3A"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=3,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Treatment,
                    Name="3B"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=3,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Cardiological,
                    Name="3C"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=3,
                    MedicalRoomType=Core.Enums.MedicalRoomType.General,
                    Name="3D"
                },


            //},
            //    new List<MedicalRoom>()
            //{
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=4,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Treatment,
                    Name="41"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=4,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Treatment,
                    Name="42"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=4,
                    MedicalRoomType=Core.Enums.MedicalRoomType.General,
                    Name="43"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=4,
                    MedicalRoomType=Core.Enums.MedicalRoomType.MedicalImaging,
                    Name="44"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=4,
                    MedicalRoomType=Core.Enums.MedicalRoomType.General,
                    Name="45"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=4,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Cardiological,
                    Name="46"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=4,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Gynecological ,
                    Name="47"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=5,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Laryngological,
                    Name="51"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=5,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Neurological,
                    Name="52"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=5,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Ophthalmology,
                    Name="53"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=5,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Rehabilitation,
                    Name="54"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=5,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Surgical,
                    Name="55"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=5,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Surgical,
                    Name="56"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=5,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Surgical,
                    Name="57"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=5,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Surgical,
                    Name="58"
                },

            //},
            //    new List<MedicalRoom>()
            //{
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=7,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Surgical,
                    Name="2"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=7,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Dental,
                    Name="3"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=7,
                    MedicalRoomType=Core.Enums.MedicalRoomType.OralHygiene,
                    Name="4"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=7,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Cardiological,
                    Name="5"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=7,
                    MedicalRoomType=Core.Enums.MedicalRoomType.General,
                    Name="6"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=7,
                    MedicalRoomType=Core.Enums.MedicalRoomType.General,
                    Name="7"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=7,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Rehabilitation,
                    Name="8"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=7,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Treatment,
                    Name="9"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=7,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Cardiological,
                    Name="10"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=7,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Neurological,
                    Name="11"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=7,
                    MedicalRoomType=Core.Enums.MedicalRoomType.MedicalImaging,
                    Name="12"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=7,
                    MedicalRoomType=Core.Enums.MedicalRoomType.General,
                    Name="13"
                },

            //},
            //    new List<MedicalRoom>()
            //{
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=2,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Cardiological,
                    Name="A"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=2,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Cardiological,
                    Name="B"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=2,
                    MedicalRoomType=Core.Enums.MedicalRoomType.General,
                    Name="C"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=2,
                    MedicalRoomType=Core.Enums.MedicalRoomType.General,
                    Name="D"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=2,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Gynecological,
                    Name="E"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=2,
                    MedicalRoomType=Core.Enums.MedicalRoomType.MedicalImaging,
                    Name="F"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=3,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Surgical,
                    Name="G"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=3,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Treatment,
                    Name="H"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=3,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Treatment,
                    Name="I"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=3,
                    MedicalRoomType=Core.Enums.MedicalRoomType.General,
                    Name="J"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=3,
                    MedicalRoomType=Core.Enums.MedicalRoomType.Cardiological,
                    Name="K"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber=3,
                    MedicalRoomType=Core.Enums.MedicalRoomType.General,
                    Name="L"
                },
            //},
            //    new List<MedicalRoom>()
            //{
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Cardiological,
                    Name = "1"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Dental,
                    Name = "2"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "3"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "4"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Gynecological,
                    Name = "5"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Laryngological,
                    Name = "6"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.MedicalImaging,
                    Name = "7"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Neurological,
                    Name = "8"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Ophthalmology,
                    Name = "9"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.OralHygiene,
                    Name = "10"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.OralHygiene,
                    Name = "11"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Rehabilitation,
                    Name = "12"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "13"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "14"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "15"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "16"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 1,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "11"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 2,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "23"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 2,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "24"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 2,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "25"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 5,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Cardiological,
                    Name = "51"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 5,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Dental,
                    Name = "52"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 5,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "53"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 5,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "54"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 5,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Gynecological,
                    Name = "55"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 5,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Laryngological,
                    Name = "56"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 5,
                    MedicalRoomType = Core.Enums.MedicalRoomType.MedicalImaging,
                    Name = "57"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 5,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Neurological,
                    Name = "58"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 5,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Ophthalmology,
                    Name = "59"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 6,
                    MedicalRoomType = Core.Enums.MedicalRoomType.OralHygiene,
                    Name = "60"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 6,
                    MedicalRoomType = Core.Enums.MedicalRoomType.OralHygiene,
                    Name = "61"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 6,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Rehabilitation,
                    Name = "62"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 6,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "63"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 6,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "64"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 6,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "65"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 6,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "66"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 6,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "67"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 6,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "68"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 6,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "69"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 6,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "69B"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Cardiological,
                    Name = "A"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Dental,
                    Name = "B"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "C"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "D"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Gynecological,
                    Name = "E"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Laryngological,
                    Name = "F"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.MedicalImaging,
                    Name = "G"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Neurological,
                    Name = "H"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Ophthalmology,
                    Name = "I"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.OralHygiene,
                    Name = "J"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.OralHygiene,
                    Name = "K"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 0,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Rehabilitation,
                    Name = "L"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 1,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "M"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 1,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "N"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 1,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "O"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 1,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "P"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 1,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "R"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 2,
                    MedicalRoomType = Core.Enums.MedicalRoomType.Dental,
                    Name = "S"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 2,
                    MedicalRoomType = Core.Enums.MedicalRoomType.OralHygiene,
                    Name = "T"
                },
                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 2,
                    MedicalRoomType = Core.Enums.MedicalRoomType.General,
                    Name = "U"
                },
                                new MedicalRoom()
                {
                    Id = ++id,
                    FloorNumber = 2,
                    MedicalRoomType = Core.Enums.MedicalRoomType.USG,
                    Name = "W"
                },

            };

            return rooms;
        }

        public static MedicalRoom GetMedicalRoomById(long id)
        {
            MedicalRoom room = MedicalRooms.Where(c => c.Id == id).FirstOrDefault();
            return room;
        }

        public static Visit GetHistoricalVisitById(long id)
        {
            Visit visit = HistoricalVisits.Where(c => c.Id == id).FirstOrDefault();
            return visit;
        }
    }
}
