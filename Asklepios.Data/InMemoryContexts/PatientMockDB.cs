using Asklepios.Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

        public static ICollection<Visit> AvailableVisits
        {
            get
            {
                return FutureVisits.Where(c => c.VisitStatus == Core.Enums.VisitStatus.AvailableNotBooked).ToList();
            }
        }
        public static ICollection<Visit> BookedVisits
        {
            get
            {
                //BookedVisits =
                return FutureVisits.Where(c => c.VisitStatus == Core.Enums.VisitStatus.Booked).ToList();
            }
        }
        public static List<Visit> FutureVisits { get; set; }
        public static List<Visit> HistoricalVisits { get; set; }

        public static List<Location> Locations { get; set; }
        //public  Patient Patient { get; set; }
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

        internal static Visit GetBookedVisitById(long currentVisitId)
        {
            Visit visit = BookedVisits.Where(d => d.Id == currentVisitId).FirstOrDefault();
            return visit;
        }

        public static Patient CurrentPatient { get; set; }
        public static List<Person> Persons { get; internal set; }
        //public static List<MedicalRoom> Rooms { get; set; }

        public static bool IsCreated;
        public static void SetData()
        {
            IsCreated = true;
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
            IssuedMedicines = GetDummyMedicines();
            Prescriptions = GetDummyPrescriptions(DateTimeOffset.Now);
            MedicalTestResults = GetSomeMedicalTestResults();            //AllPatients = GetAllPatients().ToList();
            FutureVisits = GetFutureVisits().ToList();
            HistoricalVisits = GetHistoricalVisits().ToList();
            CurrentPatient = GetPatientData(AllPatients[0]);
            BookRandomVisits();
        }

        private static void BookRandomVisits()
        {
            foreach (Patient patient in AllPatients)
            {
                for (int i = 1; i < 4; i++)
                {
                    int number = (333 * i % AvailableVisits.Count);
                    AvailableVisits.ElementAt(number).Patient = patient;
                    AvailableVisits.ElementAt(number).PatientId = patient.Id;

                }
            }

            foreach (MedicalWorker medicalWorker in MedicalWorkers)
            {
                List<Visit> visits = AvailableVisits.Where(c => c.MedicalWorker.Id == medicalWorker.Id).ToList();
                int pNumber = 0;
                for (int i = 1; i < 4; i++)
                {
                    pNumber++;

                    int number = (333 * i % BookedVisits.Count);
                    BookedVisits.ElementAt(number).Patient = AllPatients.ElementAt(pNumber % AllPatients.Count);
                    BookedVisits.ElementAt(number).PatientId = AllPatients.ElementAt(pNumber % AllPatients.Count).Id;
                }
            }
        }

        internal static List<MedicalServiceDiscount> GetMedicalServiceDiscounts()
        {
            List<MedicalServiceDiscount> discounts = new List<MedicalServiceDiscount>();
            long id = 1;
            foreach (MedicalService service in MedicalServices)
            {
                MedicalServiceDiscount discount = new MedicalServiceDiscount() { Discount = new decimal(0.2), MedicalService = service, MedicalPackageId = 1, Id = id++ };
                discounts.Add(discount);
            }
            foreach (MedicalService service in MedicalServices)
            {
                MedicalServiceDiscount discount = new MedicalServiceDiscount() { Discount = new decimal(0.4), MedicalService = service, MedicalPackageId = 2, Id = id++ };
                discounts.Add(discount);
            }
            foreach (MedicalService service in MedicalServices)
            {
                MedicalServiceDiscount discount = new MedicalServiceDiscount() { Discount = new decimal(0.6), MedicalService = service, MedicalPackageId = 3, Id = id++ };
                discounts.Add(discount);
            }
            foreach (MedicalService service in MedicalServices)
            {
                MedicalServiceDiscount discount = new MedicalServiceDiscount() { Discount = new decimal(0.8), MedicalService = service, MedicalPackageId = 4, Id = id++ };
                discounts.Add(discount);
            }

            return discounts;
        }

        //public static void SetHomeData()
        //{
        //    IsCreated = true;
        //    MedicalServices = GetMedicalServices().ToList();
        //    PrimaryMedicalServices = MedicalServices.Where(c => c.IsPrimaryService == true).ToList();
        //    Locations = GetAllLocations();
        //    //CurrentPatient = GetPatientData(AllPatients[0]);

        //}

        public static Person GetPersonById(long id)
        {
            return Persons.Where(c => c.Id == id).FirstOrDefault();
        }

        private static List<User> GetAllUsers()
        {
            long id = 0;
            long pid = 0;
            List<User> users = new()
            {
                //medical workers
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordMedicalWorker" + id.ToString(), EmailAddress = "MedicalWorker" + id.ToString() + "@Asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule, PersonId = id },
                //cs workers                                                     
                new User() { Id = ++id, Password = "PasswordService1", EmailAddress = "sw1@asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.CustomerServiceModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordService2", EmailAddress = "sw2@asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.CustomerServiceModule, PersonId = id },
                //admin workers                     
                new User() { Id = ++id, Password = "PasswordAdmin1", EmailAddress = "ad1@asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.AdministrativeWorkerModule, PersonId = id },
                new User() { Id = ++id, Password = "PasswordAdmin2", EmailAddress = "ad2@asklepios.com", UserType = Core.Enums.UserType.Employee, WorkerModuleType = Core.Enums.WorkerModuleType.AdministrativeWorkerModule, PersonId = id },

                //patients
                new User() { Id = ++id, Password = "PasswordPatient" + (++pid).ToString(), EmailAddress = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, Password = "PasswordPatient" + (++pid).ToString(), EmailAddress = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, Password = "PasswordPatient" + (++pid).ToString(), EmailAddress = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, Password = "PasswordPatient" + (++pid).ToString(), EmailAddress = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, Password = "PasswordPatient" + (++pid).ToString(), EmailAddress = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, Password = "PasswordPatient" + (++pid).ToString(), EmailAddress = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, Password = "PasswordPatient" + (++pid).ToString(), EmailAddress = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, Password = "PasswordPatient" + (++pid).ToString(), EmailAddress = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, Password = "PasswordPatient" + (++pid).ToString(), EmailAddress = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, Password = "PasswordPatient" + (++pid).ToString(), EmailAddress = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, Password = "PasswordPatient" + (++pid).ToString(), EmailAddress = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, Password = "PasswordPatient" + (++pid).ToString(), EmailAddress = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, Password = "PasswordPatient" + (++pid).ToString(), EmailAddress = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, Password = "PasswordPatient" + (++pid).ToString(), EmailAddress = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, Password = "PasswordPatient" + (++pid).ToString(), EmailAddress = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, Password = "PasswordPatient" + (++pid).ToString(), EmailAddress = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, Password = "PasswordPatient" + (++pid).ToString(), EmailAddress = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, Password = "PasswordPatient" + (++pid).ToString(), EmailAddress = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, Password = "PasswordPatient" + (++pid).ToString(), EmailAddress = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, Password = "PasswordPatient" + (++pid).ToString(), EmailAddress = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, Password = "PasswordPatient" + (++pid).ToString(), EmailAddress = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, Password = "PasswordPatient" + (++pid).ToString(), EmailAddress = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, Password = "PasswordPatient" + (++pid).ToString(), EmailAddress = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, Password = "PasswordPatient" + (++pid).ToString(), EmailAddress = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, Password = "PasswordPatient" + (++pid).ToString(), EmailAddress = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, Password = "PasswordPatient" + (++pid).ToString(), EmailAddress = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },
                new User() { Id = ++id, Password = "PasswordPatient" + (++pid).ToString(), EmailAddress = "patient" + pid.ToString() + "@asklepios.com", UserType = Core.Enums.UserType.Patient, WorkerModuleType = null, PersonId = id },

            };


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
            MedicalWorkers.Append(medicalWorker);
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
            long startId = 100;
            int locationsNumber = Locations.Count;

            for (int i = 0; i <= 7; i++)
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
                    minutsOffset = -1;
                    MedicalWorker medicalWorker = MedicalWorkers.ElementAt(j);

                    int servicesCounter = medicalWorker.MedicalServices.Where(c => c.IsPrimaryService).Count();
                    int serviceIndex = (servicesCounter - 1) % (i + 1);
                    MedicalService service = medicalWorker.MedicalServices.Where(c => c.IsPrimaryService).ToList().ElementAt(serviceIndex);
                    List<VisitCategory> categories = VisitCategories.Where(c => c.PrimaryMedicalServices.Any(d => d.Id == service.Id)).ToList();
                    VisitCategory visitCategory = categories[(categories.Count-1) % (i + 1)];
                    Location location = Locations.ElementAt((locationsNumber -1)% (j + 1));
                    int roomsCounter = location.MedicalRooms.Count;
                    MedicalRoom room = location.MedicalRooms.ElementAt((roomsCounter-1) % (j + 1));

                    for (int m = 0; m < 12; m++)
                    {
                        minutsOffset++;
                        Visit visit = new Visit()
                        {
                            Id = startId++,
                            PrimaryService = service,// PrimaryMedicalServices[0],
                            PrimaryServiceId=service.Id,
                            DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),

                            DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                            Location = location,
                            
                            MedicalRoom = room,
                            MedicalWorker = medicalWorker,//MedicalWorkers.ElementAt(36),
                            VisitCategory = visitCategory//VisitCategories.ElementAt(0),
                        };
                        availableVisits.Add(visit);
                    }

                }

                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;
                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,
                //        PrimaryService = service,// PrimaryMedicalServices[0],
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                //        Location = Locations.ElementAt(3),
                //        MedicalRoom = Locations.ElementAt(3).MedicalRooms.ElementAt(4),
                //        MedicalWorker = medicalWorker,//MedicalWorkers.ElementAt(36),
                //        VisitCategory = visitCategory//VisitCategories.ElementAt(0),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;

                //medicalWorker = MedicalWorkers.ElementAt(34);
                //servicesCounter = medicalWorker.MedicalServices.Where(c => c.IsPrimaryService).Count();
                //serviceIndex = (servicesCounter - 1) % (i + 1);
                //service = medicalWorker.MedicalServices.Where(c => c.IsPrimaryService).ToList().ElementAt(serviceIndex);
                //categories = VisitCategories.Where(c => c.PrimaryMedicalServices.Any(d => d.Id == service.Id)).ToList();
                //visitCategory = categories[categories.Count % (i + 1)];

                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;
                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,

                //        PrimaryService = service,
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                //        Location = Locations.ElementAt(1),
                //        MedicalRoom = Locations.ElementAt(1).MedicalRooms.ElementAt(6),
                //        MedicalWorker = medicalWorker,
                //        VisitCategory = visitCategory,
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;

                //medicalWorker = MedicalWorkers.ElementAt(12);
                //servicesCounter = medicalWorker.MedicalServices.Where(c => c.IsPrimaryService).Count();
                //serviceIndex = (servicesCounter - 1) % (i + 1);
                //service = medicalWorker.MedicalServices.Where(c => c.IsPrimaryService).ToList().ElementAt(serviceIndex);
                //categories = VisitCategories.Where(c => c.PrimaryMedicalServices.Any(d => d.Id == service.Id)).ToList();
                //visitCategory = categories[categories.Count % (i + 1)];

                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;
                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,

                //        PrimaryService = PrimaryMedicalServices[23],
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                //        Location = Locations.ElementAt(1),
                //        MedicalRoom = Locations.ElementAt(1).MedicalRooms.ElementAt(3),
                //        MedicalWorker = MedicalWorkers.ElementAt(12),
                //        VisitCategory = VisitCategories.ElementAt(2),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;

                //medicalWorker = MedicalWorkers.ElementAt(37);
                //servicesCounter = medicalWorker.MedicalServices.Where(c => c.IsPrimaryService).Count();
                //serviceIndex = (servicesCounter - 1) % (i + 1);
                //service = medicalWorker.MedicalServices.Where(c => c.IsPrimaryService).ToList().ElementAt(serviceIndex);
                //categories = VisitCategories.Where(c => c.PrimaryMedicalServices.Any(d => d.Id == service.Id)).ToList();
                //visitCategory = categories[categories.Count % (i + 1)];

                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;
                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,

                //        PrimaryService = PrimaryMedicalServices[3],
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                //        Location = Locations.ElementAt(5),
                //        MedicalRoom = Locations.ElementAt(5).MedicalRooms.ElementAt(3),
                //        MedicalWorker = MedicalWorkers.ElementAt(37),
                //        VisitCategory = VisitCategories.ElementAt(0),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;

                //medicalWorker = MedicalWorkers.ElementAt(55);
                //servicesCounter = medicalWorker.MedicalServices.Where(c => c.IsPrimaryService).Count();
                //serviceIndex = (servicesCounter - 1) % (i + 1);
                //service = medicalWorker.MedicalServices.Where(c => c.IsPrimaryService).ToList().ElementAt(serviceIndex);
                //categories = VisitCategories.Where(c => c.PrimaryMedicalServices.Any(d => d.Id == service.Id)).ToList();
                //visitCategory = categories[categories.Count % (i + 1)];

                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;
                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,

                //        PrimaryService = PrimaryMedicalServices[28],
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                //        Location = Locations.ElementAt(1),
                //        MedicalRoom = Locations.ElementAt(1).MedicalRooms.ElementAt(3),
                //        MedicalWorker = MedicalWorkers.ElementAt(55),
                //        VisitCategory = VisitCategories.ElementAt(3),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;

                //medicalWorker = MedicalWorkers.ElementAt(61);
                //servicesCounter = medicalWorker.MedicalServices.Where(c => c.IsPrimaryService).Count();
                //serviceIndex = (servicesCounter - 1) % (i + 1);
                //service = medicalWorker.MedicalServices.Where(c => c.IsPrimaryService).ToList().ElementAt(serviceIndex);
                //categories = VisitCategories.Where(c => c.PrimaryMedicalServices.Any(d => d.Id == service.Id)).ToList();
                //visitCategory = categories[categories.Count % (i + 1)];


                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;
                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,

                //        PrimaryService = PrimaryMedicalServices[33],
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                //        Location = Locations.ElementAt(1),
                //        MedicalRoom = Locations.ElementAt(1).MedicalRooms.ElementAt(1),
                //        MedicalWorker = MedicalWorkers.ElementAt(61),
                //        VisitCategory = VisitCategories.ElementAt(5),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;

                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;

                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,
                //        PrimaryService = PrimaryMedicalServices[9],
                //        MedicalRoom = Locations.ElementAt(0).MedicalRooms.ElementAt(0),
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                //        Location = Locations.ElementAt(0),
                //        MedicalWorker = MedicalWorkers.ElementAt(50),
                //        VisitCategory = VisitCategories.ElementAt(0),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;

                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;

                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,
                //        PrimaryService = PrimaryMedicalServices[7],
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                //        Location = Locations.ElementAt(0),
                //        MedicalRoom = Locations.ElementAt(0).MedicalRooms.ElementAt(1),
                //        MedicalWorker = MedicalWorkers.ElementAt(29),
                //        VisitCategory = VisitCategories.ElementAt(0),
                //    };
                //    availableVisits.Add(visit);

                //}
                //minutsOffset = -1;

                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;

                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,
                //        PrimaryService = PrimaryMedicalServices[1],
                //        MedicalRoom = Locations.ElementAt(0).MedicalRooms.ElementAt(0),
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                //        Location = Locations.ElementAt(0),
                //        MedicalWorker = MedicalWorkers.ElementAt(0),
                //        VisitCategory = VisitCategories.ElementAt(0),
                //    };
                //    availableVisits.Add(visit);

                //}
                //minutsOffset = -1;

                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;

                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,
                //        PrimaryService = PrimaryMedicalServices[28],
                //        MedicalRoom = Locations.ElementAt(0).MedicalRooms.ElementAt(0),
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                //        Location = Locations.ElementAt(0),
                //        MedicalWorker = MedicalWorkers.ElementAt(51),
                //        VisitCategory = VisitCategories.ElementAt(3),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;

                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;

                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,
                //        PrimaryService = PrimaryMedicalServices[6],
                //        MedicalRoom = Locations.ElementAt(0).MedicalRooms.ElementAt(0),
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                //        Location = Locations.ElementAt(0),
                //        MedicalWorker = MedicalWorkers.ElementAt(26),
                //        VisitCategory = VisitCategories.ElementAt(0),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;

                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;

                //    Visit visit = new()
                //    {
                //        Id = startId++,
                //        PrimaryService = PrimaryMedicalServices[32],
                //        MedicalRoom = Locations.ElementAt(0).MedicalRooms.ElementAt(0),
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                //        Location = Locations.ElementAt(0),
                //        MedicalWorker = MedicalWorkers.ElementAt(3),
                //        VisitCategory = VisitCategories.ElementAt(4),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;


                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;

                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,
                //        PrimaryService = PrimaryMedicalServices[33],
                //        MedicalRoom = Locations.ElementAt(0).MedicalRooms.ElementAt(1),
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                //        Location = Locations.ElementAt(3),
                //        MedicalWorker = MedicalWorkers.ElementAt(39),
                //        VisitCategory = VisitCategories.ElementAt(5),

                //    };
                //    availableVisits.Add(visit);
                //}
                ////interna
                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;

                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,
                //        PrimaryService = PrimaryMedicalServices[2],
                //        MedicalRoom = Locations.ElementAt(0).MedicalRooms.ElementAt(1),
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                //        Location = Locations.ElementAt(3),
                //        MedicalWorker = MedicalWorkers.ElementAt(35),
                //        VisitCategory = VisitCategories.ElementAt(1),
                //    };
                //    availableVisits.Add(visit);
                //}
                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;

                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,
                //        PrimaryService = PrimaryMedicalServices[2],
                //        MedicalRoom = Locations.ElementAt(7).MedicalRooms.ElementAt(4),
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                //        Location = Locations.ElementAt(7),
                //        MedicalWorker = MedicalWorkers.ElementAt(44),
                //        VisitCategory = VisitCategories.ElementAt(0),
                //    };
                //    availableVisits.Add(visit);
                //}
                ////


                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;

                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,
                //        PrimaryService = PrimaryMedicalServices[4],
                //        MedicalRoom = Locations.ElementAt(3).MedicalRooms.ElementAt(6),
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                //        Location = Locations.ElementAt(3),
                //        MedicalWorker = MedicalWorkers.ElementAt(8),
                //        VisitCategory = VisitCategories.ElementAt(0),
                //    };
                //    availableVisits.Add(visit);
                //}
                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;

                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,
                //        PrimaryService = PrimaryMedicalServices[5],
                //        MedicalRoom = Locations.ElementAt(6).MedicalRooms.ElementAt(2),
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                //        Location = Locations.ElementAt(6),
                //        MedicalWorker = MedicalWorkers.ElementAt(4),
                //        VisitCategory = VisitCategories.ElementAt(0),
                //    };
                //    availableVisits.Add(visit);
                //}
                ////okulistyczna
                //for (int j = 0; j < 8; j++)
                //{
                //    minutsOffset++;

                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,
                //        PrimaryService = PrimaryMedicalServices[8],
                //        MedicalRoom = Locations.ElementAt(7).MedicalRooms.ElementAt(7),
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(60 + (minutsOffset * 15)),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(60 + (minutsOffset * 15) + 15),
                //        Location = Locations.ElementAt(7),
                //        MedicalWorker = MedicalWorkers.ElementAt(15),
                //        VisitCategory = VisitCategories.ElementAt(0),
                //    };
                //    availableVisits.Add(visit);
                //}
                //for (int j = 0; j < 10; j++)
                //{
                //    minutsOffset++;

                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,
                //        PrimaryService = PrimaryMedicalServices[8],
                //        MedicalRoom = Locations.ElementAt(8).MedicalRooms.ElementAt(7),
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(90 + (minutsOffset * 15)),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(90 + (minutsOffset * 15) + 15),
                //        Location = Locations.ElementAt(8),
                //        MedicalWorker = MedicalWorkers.ElementAt(31),
                //        VisitCategory = VisitCategories.ElementAt(0),
                //    };
                //    availableVisits.Add(visit);
                //}
                ////endokrynologia
                //for (int j = 0; j < 10; j++)
                //{
                //    minutsOffset++;

                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,
                //        PrimaryService = PrimaryMedicalServices[10],
                //        MedicalRoom = Locations.ElementAt(8).MedicalRooms.ElementAt(9),
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(90 + (minutsOffset * 15)),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(90 + (minutsOffset * 15) + 15),
                //        Location = Locations.ElementAt(8),
                //        MedicalWorker = MedicalWorkers.ElementAt(43),
                //        VisitCategory = VisitCategories.ElementAt(1),
                //    };
                //    availableVisits.Add(visit);
                //}
                //for (int j = 0; j < 10; j++)
                //{
                //    minutsOffset++;

                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,
                //        PrimaryService = PrimaryMedicalServices[10],
                //        MedicalRoom = Locations.ElementAt(0).MedicalRooms.ElementAt(9),
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(90 + (minutsOffset * 15)),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(90 + (minutsOffset * 15) + 15),
                //        Location = Locations.ElementAt(0),
                //        MedicalWorker = MedicalWorkers.ElementAt(48),
                //        VisitCategory = VisitCategories.ElementAt(0),
                //    };
                //    availableVisits.Add(visit);
                //}
                ////chirurgia ogólna  // chirurdzy 16-19, kolejno
                //for (int j = 0; j < 10; j++)
                //{
                //    minutsOffset++;

                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,
                //        PrimaryService = PrimaryMedicalServices[11],
                //        MedicalRoom = Locations.ElementAt(1).MedicalRooms.ElementAt(2),
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes((minutsOffset * 15)),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes((minutsOffset * 15) + 15),
                //        Location = Locations.ElementAt(1),
                //        MedicalWorker = MedicalWorkers.ElementAt(30),
                //        VisitCategory = VisitCategories.ElementAt(0),
                //    };
                //    availableVisits.Add(visit);
                //}
                //for (int j = 0; j < 10; j++)
                //{
                //    minutsOffset++;

                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,
                //        PrimaryService = PrimaryMedicalServices[12],
                //        MedicalRoom = Locations.ElementAt(1).MedicalRooms.ElementAt(2),
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(180 + (minutsOffset * 15)),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(180 + (minutsOffset * 15) + 15),
                //        Location = Locations.ElementAt(1),
                //        MedicalWorker = MedicalWorkers.ElementAt(30),
                //        VisitCategory = VisitCategories.ElementAt(0),
                //    };
                //    availableVisits.Add(visit);
                //}
                //for (int j = 0; j < 10; j++)
                //{
                //    minutsOffset++;

                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,
                //        PrimaryService = PrimaryMedicalServices[11],
                //        MedicalRoom = Locations.ElementAt(3).MedicalRooms.ElementAt(3),
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes((minutsOffset * 15)),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes((minutsOffset * 15) + 15),
                //        Location = Locations.ElementAt(3),
                //        MedicalWorker = MedicalWorkers.ElementAt(16),
                //        VisitCategory = VisitCategories.ElementAt(0),
                //    };
                //    availableVisits.Add(visit);
                //}
                //for (int j = 0; j < 10; j++)
                //{
                //    minutsOffset++;

                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,
                //        PrimaryService = PrimaryMedicalServices[12],
                //        MedicalRoom = Locations.ElementAt(3).MedicalRooms.ElementAt(3),
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(180 + (minutsOffset * 15)),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(180 + (minutsOffset * 15) + 15),
                //        Location = Locations.ElementAt(3),
                //        MedicalWorker = MedicalWorkers.ElementAt(16),
                //        VisitCategory = VisitCategories.ElementAt(0),
                //    };
                //    availableVisits.Add(visit);
                //}

                //for (int j = 0; j < 24; j++)
                //{
                //    minutsOffset++;

                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,
                //        PrimaryService = PrimaryMedicalServices[13],
                //        MedicalRoom = Locations.ElementAt(1).MedicalRooms.ElementAt(3),
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(180 + (minutsOffset * 15)),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(180 + (minutsOffset * 15) + 15),
                //        Location = Locations.ElementAt(1),
                //        MedicalWorker = MedicalWorkers.ElementAt(17),
                //        VisitCategory = VisitCategories.ElementAt(0),
                //    };
                //    availableVisits.Add(visit);
                //}
                //for (int j = 0; j < 24; j++)
                //{
                //    minutsOffset++;

                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,
                //        PrimaryService = PrimaryMedicalServices[14],
                //        MedicalRoom = Locations.ElementAt(2).MedicalRooms.ElementAt(3),
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(180 + (minutsOffset * 15)),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(180 + (minutsOffset * 15) + 15),
                //        Location = Locations.ElementAt(2),
                //        MedicalWorker = MedicalWorkers.ElementAt(18),
                //        VisitCategory = VisitCategories.ElementAt(0),
                //    };
                //    availableVisits.Add(visit);
                //}
                //for (int j = 0; j < 24; j++)
                //{
                //    minutsOffset++;

                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,
                //        PrimaryService = PrimaryMedicalServices[15],
                //        MedicalRoom = Locations.ElementAt(4).MedicalRooms.ElementAt(3),
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(180 + (minutsOffset * 15)),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(180 + (minutsOffset * 15) + 15),
                //        Location = Locations.ElementAt(4),
                //        MedicalWorker = MedicalWorkers.ElementAt(19),
                //        VisitCategory = VisitCategories.ElementAt(0),
                //    };
                //    availableVisits.Add(visit);
                //}


                ////laryngologia
                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;
                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,

                //        PrimaryService = PrimaryMedicalServices[16],
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                //        Location = Locations.ElementAt(4),
                //        MedicalRoom = Locations.ElementAt(5).MedicalRooms.ElementAt(2),
                //        MedicalWorker = MedicalWorkers.ElementAt(20),
                //        VisitCategory = VisitCategories.ElementAt(0),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;

                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;
                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,

                //        PrimaryService = PrimaryMedicalServices[16],
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                //        Location = Locations.ElementAt(6),
                //        MedicalRoom = Locations.ElementAt(6).MedicalRooms.ElementAt(6),
                //        MedicalWorker = MedicalWorkers.ElementAt(64),
                //        VisitCategory = VisitCategories.ElementAt(0),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;

                ////neurologia

                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;
                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,

                //        PrimaryService = PrimaryMedicalServices[17],
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                //        Location = Locations.ElementAt(6),
                //        MedicalRoom = Locations.ElementAt(6).MedicalRooms.ElementAt(7),
                //        MedicalWorker = MedicalWorkers.ElementAt(21),
                //        VisitCategory = VisitCategories.ElementAt(0),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;
                ////urologia

                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;
                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,

                //        PrimaryService = PrimaryMedicalServices[18],
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                //        Location = Locations.ElementAt(6),
                //        MedicalRoom = Locations.ElementAt(6).MedicalRooms.ElementAt(8),
                //        MedicalWorker = MedicalWorkers.ElementAt(22),
                //        VisitCategory = VisitCategories.ElementAt(0),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;
                ////stomatologia zachowawcza
                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;
                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,

                //        PrimaryService = PrimaryMedicalServices[20],
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 30),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 30 + 30),
                //        Location = Locations.ElementAt(4),
                //        MedicalRoom = Locations.ElementAt(5).MedicalRooms.ElementAt(1),
                //        MedicalWorker = MedicalWorkers.ElementAt(5),
                //        VisitCategory = VisitCategories.ElementAt(2),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;

                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;

                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,
                //        PrimaryService = PrimaryMedicalServices[20],
                //        MinorMedicalServices = new List<MedicalService>() { MedicalServices[48], MedicalServices[49], MedicalServices[52] },
                //        MedicalRoom = Locations.ElementAt(0).MedicalRooms.ElementAt(1),
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 30),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 30 + 30),
                //        Location = Locations.ElementAt(0),
                //        MedicalWorker = MedicalWorkers.ElementAt(63),
                //        VisitCategory = VisitCategories.ElementAt(2),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;

                ////ortodoncja
                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;
                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,
                //        PrimaryService = PrimaryMedicalServices[21],
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 30),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 30 + 30),
                //        Location = Locations.ElementAt(7),
                //        MedicalRoom = Locations.ElementAt(7).MedicalRooms.ElementAt(8),
                //        MedicalWorker = MedicalWorkers.ElementAt(63),
                //        VisitCategory = VisitCategories.ElementAt(2),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;

                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;
                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,
                //        PrimaryService = PrimaryMedicalServices[21],
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 30),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 30 + 30),
                //        Location = Locations.ElementAt(8),
                //        MedicalRoom = Locations.ElementAt(8).MedicalRooms.ElementAt(8),
                //        MedicalWorker = MedicalWorkers.ElementAt(42),
                //        VisitCategory = VisitCategories.ElementAt(2),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;
                ////chiruriga sotmatologiczna
                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;
                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,
                //        PrimaryService = PrimaryMedicalServices[22],
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 30),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 30 + 30),
                //        Location = Locations.ElementAt(0),
                //        MedicalRoom = Locations.ElementAt(0).MedicalRooms.ElementAt(10),
                //        MedicalWorker = MedicalWorkers.ElementAt(9),
                //        VisitCategory = VisitCategories.ElementAt(2),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;

                ////protetyka
                //for (int j = 0; j < 16; j++)
                //{
                //    minutsOffset++;
                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,
                //        PrimaryService = PrimaryMedicalServices[24],
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 30),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 30 + 30),
                //        Location = Locations.ElementAt(0),
                //        MedicalRoom = Locations.ElementAt(0).MedicalRooms.ElementAt(11),
                //        MedicalWorker = MedicalWorkers.ElementAt(24),
                //        VisitCategory = VisitCategories.ElementAt(2),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;

                ////higiena

                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;
                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,
                //        PrimaryService = PrimaryMedicalServices[25],
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 30),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 30 + 30),
                //        Location = Locations.ElementAt(1),
                //        MedicalRoom = Locations.ElementAt(1).MedicalRooms.ElementAt(11),
                //        MedicalWorker = MedicalWorkers.ElementAt(33),
                //        VisitCategory = VisitCategories.ElementAt(2),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;

                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;
                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,
                //        PrimaryService = PrimaryMedicalServices[25],
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 30),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 30 + 30),
                //        Location = Locations.ElementAt(2),
                //        MedicalRoom = Locations.ElementAt(2).MedicalRooms.ElementAt(11),
                //        MedicalWorker = MedicalWorkers.ElementAt(46),
                //        VisitCategory = VisitCategories.ElementAt(2),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;

                ////usg

                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;
                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,
                //        PrimaryService = PrimaryMedicalServices[27],
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                //        Location = Locations.ElementAt(8),
                //        MedicalRoom = Locations.ElementAt(8).MedicalRooms.ElementAt(11),
                //        MedicalWorker = MedicalWorkers.ElementAt(26),
                //        VisitCategory = VisitCategories.ElementAt(3),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;
                ////rezonans magnetyczny

                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;
                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,
                //        PrimaryService = PrimaryMedicalServices[29],
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                //        Location = Locations.ElementAt(8),
                //        MedicalRoom = Locations.ElementAt(8).MedicalRooms.ElementAt(5),
                //        MedicalWorker = MedicalWorkers.ElementAt(68),
                //        VisitCategory = VisitCategories.ElementAt(3),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;

                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;
                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,
                //        PrimaryService = PrimaryMedicalServices[29],
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                //        Location = Locations.ElementAt(2),
                //        MedicalRoom = Locations.ElementAt(2).MedicalRooms.ElementAt(9),
                //        MedicalWorker = MedicalWorkers.ElementAt(62),
                //        VisitCategory = VisitCategories.ElementAt(3),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;

                ////fizjo :  masaż + fizykoterapia

                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;
                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,
                //        PrimaryService = PrimaryMedicalServices[31],
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                //        Location = Locations.ElementAt(2),
                //        MedicalRoom = Locations.ElementAt(2).MedicalRooms.ElementAt(5),
                //        MedicalWorker = MedicalWorkers.ElementAt(62),
                //        VisitCategory = VisitCategories.ElementAt(3),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;

                ////gabinet zab: 1 labo, 2 szczepienia
                //for (int j = 0; j < 10; j++)
                //{
                //    minutsOffset++;
                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,

                //        PrimaryService = PrimaryMedicalServices[31],
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 30),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 30 + 30),
                //        Location = Locations.ElementAt(0),
                //        MedicalRoom = Locations.ElementAt(0).MedicalRooms.ElementAt(11),
                //        MedicalWorker = MedicalWorkers.ElementAt(2),
                //        VisitCategory = VisitCategories.ElementAt(4),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;

                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;
                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,

                //        PrimaryService = PrimaryMedicalServices[32],
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                //        Location = Locations.ElementAt(1),
                //        MedicalRoom = Locations.ElementAt(1).MedicalRooms.ElementAt(9),
                //        MedicalWorker = MedicalWorkers.ElementAt(3),
                //        VisitCategory = VisitCategories.ElementAt(4),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;


                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;
                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,

                //        PrimaryService = PrimaryMedicalServices[32],
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                //        Location = Locations.ElementAt(2),
                //        MedicalRoom = Locations.ElementAt(2).MedicalRooms.ElementAt(6),
                //        MedicalWorker = MedicalWorkers.ElementAt(25),
                //        VisitCategory = VisitCategories.ElementAt(4),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;


                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;
                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,

                //        PrimaryService = PrimaryMedicalServices[33],
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                //        Location = Locations.ElementAt(3),
                //        MedicalRoom = Locations.ElementAt(3).MedicalRooms.ElementAt(3),
                //        MedicalWorker = MedicalWorkers.ElementAt(59),
                //        VisitCategory = VisitCategories.ElementAt(4),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;


                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;
                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,

                //        PrimaryService = PrimaryMedicalServices[33],
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 15 + 15),
                //        Location = Locations.ElementAt(4),
                //        MedicalRoom = Locations.ElementAt(4).MedicalRooms.ElementAt(11),
                //        MedicalWorker = MedicalWorkers.ElementAt(65),
                //        VisitCategory = VisitCategories.ElementAt(4),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;

                ////gabinet pielegniarski
                ////szczepienia


                ////39,40,52,61,

                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;
                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,

                //        PrimaryService = PrimaryMedicalServices[26],
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 10),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 10 + 10),
                //        Location = Locations.ElementAt(2),
                //        MedicalRoom = Locations.ElementAt(5).MedicalRooms.ElementAt(2),
                //        MedicalWorker = MedicalWorkers.ElementAt(39),
                //        VisitCategory = VisitCategories.ElementAt(4),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;

                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;
                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,

                //        PrimaryService = PrimaryMedicalServices[26],
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 10),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 10 + 10),
                //        Location = Locations.ElementAt(2),
                //        MedicalRoom = Locations.ElementAt(5).MedicalRooms.ElementAt(2),
                //        MedicalWorker = MedicalWorkers.ElementAt(39),
                //        VisitCategory = VisitCategories.ElementAt(4),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;

                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;
                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,

                //        PrimaryService = PrimaryMedicalServices[26],
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 10),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 10 + 10),
                //        Location = Locations.ElementAt(2),
                //        MedicalRoom = Locations.ElementAt(5).MedicalRooms.ElementAt(2),
                //        MedicalWorker = MedicalWorkers.ElementAt(39),
                //        VisitCategory = VisitCategories.ElementAt(4),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;
                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;
                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,

                //        PrimaryService = PrimaryMedicalServices[26],
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 10),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 10 + 10),
                //        Location = Locations.ElementAt(2),
                //        MedicalRoom = Locations.ElementAt(5).MedicalRooms.ElementAt(2),
                //        MedicalWorker = MedicalWorkers.ElementAt(39),
                //        VisitCategory = VisitCategories.ElementAt(4),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;
                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;
                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,

                //        PrimaryService = PrimaryMedicalServices[26],
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 10),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 10 + 10),
                //        Location = Locations.ElementAt(2),
                //        MedicalRoom = Locations.ElementAt(5).MedicalRooms.ElementAt(2),
                //        MedicalWorker = MedicalWorkers.ElementAt(39),
                //        VisitCategory = VisitCategories.ElementAt(4),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;

                //for (int j = 0; j < 12; j++)
                //{
                //    minutsOffset++;
                //    Visit visit = new Visit()
                //    {
                //        Id = startId++,

                //        PrimaryService = PrimaryMedicalServices[26],
                //        DateTimeSince = start.AddDays(dayOffset).AddMinutes(minutsOffset * 10),
                //        DateTimeTill = start.AddDays(dayOffset).AddMinutes(minutsOffset * 10 + 10),
                //        Location = Locations.ElementAt(2),
                //        MedicalRoom = Locations.ElementAt(5).MedicalRooms.ElementAt(2),
                //        MedicalWorker = MedicalWorkers.ElementAt(39),
                //        VisitCategory = VisitCategories.ElementAt(4),
                //    };
                //    availableVisits.Add(visit);
                //}
                //minutsOffset = -1;


            }

            int visitCounter = 100;
            for (int i = 0; i < AllPatients.Count; i++)
            {
                availableVisits[visitCounter * (1) + 0 + i * 2].Patient = AllPatients[i];
                availableVisits[visitCounter * (i + 1) + 100 + i].Patient = AllPatients[i];
                availableVisits[visitCounter * (i + 1) + 200 + i].Patient = AllPatients[i];
                availableVisits[visitCounter * (i + 1) + 300 + i].Patient = AllPatients[i];
                availableVisits[visitCounter * (i + 1) + 400 + i].Patient = AllPatients[i];
                availableVisits[visitCounter * (i + 1) + 500 + i].Patient = AllPatients[i];

            }

            //for (int i = 100; i < availableVisits.Count; i+=10)
            //{

            //}
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

        private static IEnumerable<Visit> GetHistoricalVisits()
        {
            DateTimeOffset dateTimeOffset = new DateTimeOffset(DateTime.Now);
            DateTimeOffset now = DateTime.Now;

            List<VisitReview> reviews = GetDummyMedicalReviews();
            List<MedicalReferral> referrals = GetDummyMedicalReferrals(null, now);
            List<string> medicalHistories = GetDummyMedicalHistories();
            List<Visit> historicalVisits = new List<Visit>();
            //List<Recommendation> recommendations = GetSomeRecommendations();

            Random rnd = new Random();
            long id = FutureVisits.Max(c => c.Id);


            foreach (Patient patient in AllPatients)
            {
                int numberOfVisits = rnd.Next(3, 15);

                for (int i = 0; i < numberOfVisits; i++)
                {
                    int medicalWorkerIndex = rnd.Next(0, MedicalWorkers.Count - 1);
                    MedicalWorker medicalWorker = MedicalWorkers[medicalWorkerIndex];
                    int primaryServicesCounter = medicalWorker.MedicalServices.Where(c => c.IsPrimaryService).Count();
                    int testResultIndex = rnd.Next(-MedicalTestResults.Count, MedicalTestResults.Count - 1);
                    int daysAgo = rnd.Next(0, 100);
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
                    //List<MedicalService> minorServices = null;
                    //if (minorService != null)
                    //{
                    //    new List<MedicalService>() { minorService };
                    //    minorMedicalServicesIds= new List<long>() { minorService.Id };
                    //}

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
                        examinationReferrals = new List<MedicalReferral>() { referrals[referralsIndex] };
                        examinationReferralsIds = examinationReferrals.Select(c => c.Id).ToList();
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
                        prescriptionId = prescription.Id;
                    }
                    List<Recommendation> visitRecommendations = null;
                    List<long> visitRecommendationsIds = null;
                    if (recommendationIndex >= 0)
                    {
                        visitRecommendations = new List<Recommendation>() { Recommendations[recommendationIndex] };
                        visitRecommendationsIds = visitRecommendations.Select(c => c.Id).ToList();
                    }
                    VisitReview review = null;
                    long reviewId = -1;
                    if (reviewIndex >= 0)
                    {
                        review = reviews[reviewIndex];
                        review.ReviewDate = dateTime.AddDays(reviewDaysOffset);
                        reviewId = review.Id;
                    }
                    MedicalTestResult testResult = null;
                    long testResultId = -1;
                    if (testResultIndex>=0)
                    {
                        testResult = (MedicalTestResult)MedicalTestResults[testResultIndex].Clone();
                        testResult.MedicalWorker = medicalWorker;
                        testResult.Id = MedicalTestResults.Max(c => c.Id) + 1;
                        testResult.MedicalService = medicalService;
                        testResultId = testResult.Id;
                        MedicalTestResults.Add(testResult);
                    }
                    VisitCategory visitCategory = VisitCategories.Where(c => c.PrimaryMedicalServices.Any(d => d.Id == medicalService.Id)).FirstOrDefault();
                    long visitCategoryId = -1;
                    if (visitCategory!=null)
                    {
                        visitCategoryId = visitCategory.Id;
                    }
                    Visit visit = new Visit()
                    {
                        Id = ++id,
                        Patient = patient,
                        PatientId=patient.Id,
                        MedicalWorker = medicalWorker,
                        MedicalWorkerId=medicalWorker.Id,
                        DateTimeSince = dateTime,
                        DateTimeTill = dateTime.AddMinutes(15),
                        Location = Locations[medicalLocationIndex],
                        LocationId= Locations[medicalLocationIndex].Id,
                        MedicalRoom = medicalRoom,
                        MedicalRoomId=medicalRoom.Id,
                        ExaminationReferrals = examinationReferrals,
                        ExaminatinoReferralsIds= examinationReferralsIds,
                        MedicalHistory = history,
                        Prescription = prescription,
                        PrescriptionId=prescriptionId,
                        Recommendations = visitRecommendations,
                        RecommendationIds= visitRecommendationsIds,
                        PrimaryService = medicalService,
                        PrimaryServiceId=medicalServiceId,
                        MedicalResult = testResult,
                        MedicalResultId= testResultId,
                        VisitCategory = visitCategory,
                        VisitCategoryId= visitCategoryId,
                        MinorMedicalServices = new List<MedicalService>() { minorService},
                        MinorMedicalServicesIds= minorMedicalServicesIds,
                        VisitReview = review,
                        VisitReviewId=reviewId
                    };
                    AddNotificationsOrNot(visit);
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

            return historicalVisits;

        }

        private static void AddNotificationsOrNot(Visit visit)
        {
            if (visit.MedicalResult != null)
            {
                Notification notification = new Notification();
                notification.DateTimeAdded = DateTimeOffset.Now;
                notification.EventObject = visit.MedicalResult;
                notification.EventObjectId = visit.MedicalResult.Id;
                notification.NotificationType = Core.Enums.NotificationType.TestResult;
                notification.Patient = visit.Patient;
                notification.PatientId = visit.Patient.Id;
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
            if (visit.Prescription != null)
            {
                Notification notification = new Notification();
                notification.DateTimeAdded = DateTimeOffset.Now;
                notification.EventObject = visit.Prescription;
                notification.EventObjectId = visit.Prescription.Id;
                notification.NotificationType = Core.Enums.NotificationType.Prescription;
                notification.Patient = visit.Patient;
                notification.PatientId = visit.Patient.Id;
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
                    notification.Patient = visit.Patient;
                    notification.PatientId = visit.Patient.Id;
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
            List<VisitReview> visitReviews = new List<VisitReview>()
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

            };

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
        static List<Prescription> GetDummyPrescriptions(DateTimeOffset dateTimeOffset)
        {
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
                    AccessCode = "749643216",
                    Id = 2,
                    //IssuedBy = (MedicalWorkers.ElementAt(1) as Doctor),
                    IssueDate = dateTimeOffset.AddDays(-10),
                    ExpirationDate = dateTimeOffset.AddDays(70),
                                        IdentificationCode="u5y4fg654h6fds54gdfs56g46df",

                    IssuedMedicines = new List<IssuedMedicine>(IssuedMedicines.GetRange(3,3)),

                },
                new Prescription()
                {
                    AccessCode = "55554654646",
                    Id = 3,
                    //IssuedBy = (MedicalWorkers.ElementAt(2) as Doctor),
                    IssueDate = dateTimeOffset.AddDays(-20),
                    ExpirationDate = dateTimeOffset.AddDays(40),
                    IdentificationCode="asd4a5s64d65as4fsd564f65sd4f",

                    IssuedMedicines = new List<IssuedMedicine>(IssuedMedicines.GetRange(6,3))
                    //{

                    //}
                },
                new Prescription()
                {
                    AccessCode = "45641345695",
                    Id = 4,
                    //IssuedBy = (MedicalWorkers.ElementAt(2) as Doctor),
                    IssueDate = dateTimeOffset.AddDays(-20),
                    ExpirationDate = dateTimeOffset.AddDays(40),
                    IdentificationCode="dsfgdad4sf4ds56af4sd6f4",

                    IssuedMedicines = new List<IssuedMedicine>(IssuedMedicines.GetRange(9,2))
                    //{

                    //}
                },
                new Prescription()
                {
                    AccessCode = "547897946213",
                    Id = 5,
                    //IssuedBy = (MedicalWorkers.ElementAt(2) as Doctor),
                    IssueDate = dateTimeOffset.AddDays(-20),
                    ExpirationDate = dateTimeOffset.AddDays(40),
                    IdentificationCode="sadsd5f4ds6f4ds65f4",

                    IssuedMedicines = new List<IssuedMedicine>(IssuedMedicines.GetRange(11,2))
                    //{


                    //}
                },
                new Prescription()
                {
                    AccessCode = "132469798456",
                    Id = 6,
                    //IssuedBy = (MedicalWorkers.ElementAt(2) as Doctor),
                    IssueDate = dateTimeOffset.AddDays(-20),
                    ExpirationDate = dateTimeOffset.AddDays(40),
                    IdentificationCode="tg4564sda8f7a9f7s9",

                    IssuedMedicines = new List<IssuedMedicine>(IssuedMedicines.GetRange(13,2))
                    //{

                    //}
                },
                new Prescription()
                {
                    AccessCode = "45641345695",
                    Id = 7,
                    //IssuedBy = (MedicalWorkers.ElementAt(2) as Doctor),
                    IssueDate = dateTimeOffset.AddDays(-20),
                    ExpirationDate = dateTimeOffset.AddDays(40),
                    IdentificationCode="u8n4nb4v654vs68",
                    IssuedMedicines=new List<IssuedMedicine>(IssuedMedicines.GetRange(15,2))

                },
                new Prescription()
                {
                    AccessCode = "964654697",
                    Id = 8,
                    //IssuedBy = (MedicalWorkers.ElementAt(2) as Doctor),
                    IssueDate = dateTimeOffset.AddDays(-20),
                    ExpirationDate = dateTimeOffset.AddDays(40),
                    IdentificationCode="qasdfs8f97sd946",

                    IssuedMedicines = new List<IssuedMedicine>(IssuedMedicines.GetRange(17,2))
                    {

                    }
                },
                new Prescription()
                {
                    AccessCode = "852134864",
                    Id = 9,
                    //IssuedBy = (MedicalWorkers.ElementAt(2) as Doctor),
                    IssueDate = dateTimeOffset.AddDays(-20),
                    ExpirationDate = dateTimeOffset.AddDays(40),
                    IdentificationCode="bnvvb5546df1g32fd4",

                    IssuedMedicines = new List<IssuedMedicine>(IssuedMedicines.GetRange(19,2))
                    {

                    }
                },
                new Prescription()
                {
                    AccessCode = "78945134687",
                    Id = 10,
                    //IssuedBy = (MedicalWorkers.ElementAt(2) as Doctor),
                    IssueDate = dateTimeOffset.AddDays(-20),
                    ExpirationDate = dateTimeOffset.AddDays(40),
                    IdentificationCode="ghjfgh15446df546",

                    IssuedMedicines = new List<IssuedMedicine>(IssuedMedicines.GetRange(21,2))
                    {
                    }
                }
            };
            return prescriptions;
        }

        public static IEnumerable<Location> GetAllLocations()
        {
            //IEnumerable<MedicalRoom> roomsCollections = GetMedicalRooms();
            if (MedicalRooms == null)
            {
                MedicalRooms = GetMedicalRooms().ToList();
            }
            return new List<Location>()
            {
                new Location()
                    {
                        City="Warszawa",
                        StreetAndNumber="Jerozolimskie 80",
                        Description="Ośrodek w centrum Warszawy ze świetnym dojazdem z każdej dzielnicy.",
                        Facilities=new List<string>(){"15 gabinetów ogólno-konsultacyjnych", "Gabinet zabiegowy", "2 gabinety stomatologiczne", "Gabinet higieny jamy ustnej", "Gabinet diagnostyki obrazowej"},
                        Id=1,
                        Name="Ośrodek Warszawa Jerozolimskie",
                        Services=new List<MedicalService>(){ PrimaryMedicalServices[0],PrimaryMedicalServices[1],PrimaryMedicalServices[2],PrimaryMedicalServices[3],PrimaryMedicalServices[4],PrimaryMedicalServices[5],PrimaryMedicalServices[6],PrimaryMedicalServices[7],PrimaryMedicalServices[8] ,PrimaryMedicalServices[9],PrimaryMedicalServices[10]}, 
                        //new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia"},
                        //VoivodeshipType=Core.Enums.VoivodeshipType.mazowieckie,
                        Aglomeration=Core.Enums.Aglomeration.Warsaw,
                        ImagePath="/img/Locations/loc1.jpeg",
                        PhoneNumber="22 780 421 433",
                        PostalCode="01-111",
                        MedicalRooms=MedicalRooms.GetRange(0,12),//roomsCollections.ElementAt(0)
                    },
                new Location()
                    {
                        City="Warszawa",
                        StreetAndNumber="Grójecka 100",
                        Description="Ośrodek w Warszawie w dzielnicy Ochota, z bardzo dobrym dojazdem z zachodniej części Warszawy.",
                        Facilities=new List<string>(){"12 gabinetów ogólno-konsultacyjnych", "Gabinet zabiegowy", "Gabinet diagnostyki obrazowej", "Gabinek okulistyczny"},
                        Id=2,
                        Name="Ośrodek Warszawa Ochota",
                        Services=new List<MedicalService>(){ PrimaryMedicalServices[10],PrimaryMedicalServices[11],PrimaryMedicalServices[12],PrimaryMedicalServices[13],PrimaryMedicalServices[14],PrimaryMedicalServices[15],PrimaryMedicalServices[16],PrimaryMedicalServices[17],PrimaryMedicalServices[18] ,PrimaryMedicalServices[19],PrimaryMedicalServices[20] },
                        //Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia"},
                        //VoivodeshipType=Core.Enums.VoivodeshipType.mazowieckie,
                                                Aglomeration=Core.Enums.Aglomeration.Warsaw,

                        ImagePath="/img/Locations/loc2.jpg",
                        PhoneNumber="22 787 477 323",
                        PostalCode="01-211",
                        MedicalRooms=MedicalRooms.GetRange(12,13),//roomsCollections.ElementAt(1)
                        },
                new Location()
                    {
                    City="Warszawa",
                        StreetAndNumber="KEN 20",
                        Description="Ośrodek na południu Warszawy ze świetnym dojazdem z południa Warszawy oraz regionów wzdłuż M1 oraz południowych okolic Warszawy.",
                        Facilities=new List<string>(){"11 gabinetów ogólno-konsultacyjnych", "2 Gabinety zabiegowe", "2 Gabinety ginekologiczne", "2 gabinety stomatologiczne", "Gabinet diagnostyki obrazowej"},
                        Id=3,
                        Name="Ośrodek Warszawa Ursynów",
                        Services=new List<MedicalService>(){ PrimaryMedicalServices[20],PrimaryMedicalServices[21],PrimaryMedicalServices[22],PrimaryMedicalServices[23],PrimaryMedicalServices[24],PrimaryMedicalServices[25],PrimaryMedicalServices[26],PrimaryMedicalServices[27],PrimaryMedicalServices[28] ,PrimaryMedicalServices[29],PrimaryMedicalServices[30] },
                        //Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia"},
                        //VoivodeshipType=Core.Enums.VoivodeshipType.mazowieckie,
                        Aglomeration=Core.Enums.Aglomeration.Warsaw,
                        ImagePath="/img/Locations/loc3.jpg",
                        PhoneNumber="22 777 600 313",
                        PostalCode="03-055",
                        MedicalRooms=MedicalRooms.GetRange(25,15),//roomsCollections.ElementAt(2)

                    },
                new Location()
                    {
                        City="Warszawa",
                        StreetAndNumber="Malborska",
                        Description="Ośrodek na wschodzie Warszawy z dobrym dojazdem ze wschodnich dzielnic Warszawy a także wschodnich okolic Warszawy.",
                        Facilities=new List<string>(){"15 gabinetów ogólno-konsultacyjnych", "Gabinet zabiegowy", "2 gabinety stomatologiczne", "Gabinet higieny jamy ustnej", "Gabinet diagnostyki obrazowej"},
                        Id=4,
                        Name="Ośrodek Warszawa Targówek",
                        Services=new List<MedicalService>(){ PrimaryMedicalServices[30],PrimaryMedicalServices[31],PrimaryMedicalServices[32],PrimaryMedicalServices[33],PrimaryMedicalServices[33],PrimaryMedicalServices[5],PrimaryMedicalServices[6],PrimaryMedicalServices[7],PrimaryMedicalServices[8] ,PrimaryMedicalServices[9],PrimaryMedicalServices[0] },

                        //Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia"},
                        //VoivodeshipType=Core.Enums.VoivodeshipType.mazowieckie,
                                                Aglomeration=Core.Enums.Aglomeration.Warsaw,

                        ImagePath="/img/Locations/loc4.jpg",
                        PhoneNumber="22 777 444 333",
                        PostalCode="02-222",
                        MedicalRooms=MedicalRooms.GetRange(40,12),//roomsCollections.ElementAt(3)

                        },
                    new Location()
                    {
                        City="Kraków",
                        StreetAndNumber="Podgórska 14",
                        Description="Ośrodek w Krakowie, w świetnie skomunikowanym Kazimierzu",
                        Facilities=new List<string>(){"15 gabinetów ogólno-konsultacyjnych", "Gabinet zabiegowy", "2 gabinety stomatologiczne", "Gabinet higieny jamy ustnej", "Gabinet diagnostyki obrazowej"},
                        Id=5,
                        Name="Ośrodek Kraków Pogórze",
                        Services=new List<MedicalService>(){ PrimaryMedicalServices[15],PrimaryMedicalServices[31],PrimaryMedicalServices[32],PrimaryMedicalServices[33],PrimaryMedicalServices[14],PrimaryMedicalServices[25],PrimaryMedicalServices[26],PrimaryMedicalServices[27],PrimaryMedicalServices[28] ,PrimaryMedicalServices[29],PrimaryMedicalServices[11] },

                        //Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia"},
                        //VoivodeshipType=Core.Enums.VoivodeshipType.malopolskie,
                                                Aglomeration=Core.Enums.Aglomeration.Cracow,

                        ImagePath="/img/Locations/loc5.jpg",
                        PhoneNumber="20 300 400 111",
                        PostalCode="80-078",
                        MedicalRooms=MedicalRooms.GetRange(52,12),//roomsCollections.ElementAt(4)

                        },
                    new Location()
                    {
                        City="Gdańsk",
                        StreetAndNumber="Chlebnicka 11",
                        Description="Ośrodek w centrum Gdańska na popularnej Wyspie Spichrzów",
                        Facilities=new List<string>(){"22 gabinety ogólno-konsultacyjne", "Gabinet zabiegowy", "2 gabinety stomatologiczne", "Gabinet higieny jamy ustnej", "Gabinet diagnostyki obrazowej"},
                        Id=6,
                        Name="Ośrodek Gdańsk Wyspa Spichrzów",
                        Services=new List<MedicalService>(){ PrimaryMedicalServices[18],PrimaryMedicalServices[19],PrimaryMedicalServices[20],PrimaryMedicalServices[21],PrimaryMedicalServices[22],PrimaryMedicalServices[25],PrimaryMedicalServices[26],PrimaryMedicalServices[27],PrimaryMedicalServices[28] ,PrimaryMedicalServices[29],PrimaryMedicalServices[11] },

                        //Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia"},
                        //VoivodeshipType=Core.Enums.VoivodeshipType.pomorskie,
                                                Aglomeration=Core.Enums.Aglomeration.Tricity,

                        ImagePath="/img/Locations/loc6.jpg",
                        PhoneNumber="30 500 500 241",
                        PostalCode="45-100",
                        MedicalRooms=MedicalRooms.GetRange(64,16),//roomsCollections.ElementAt(5)

                    },
                    new Location()
                    {
                        City="Poznań",
                        StreetAndNumber="Maltańska 1",
                        Description="Ośrodek położony na terenie Galerie Malta Poznań",
                        Facilities=new List<string>(){"20 gabinetów ogólno-konsultacyjnych", "Gabinet zabiegowy", "2 gabinety stomatologiczne", "Gabinet higieny jamy ustnej", "Gabinet diagnostyki obrazowej"},
                        Id=7,
                        Name="Ośrodek Poznań Malta",
                        Services=new List<MedicalService>(){ PrimaryMedicalServices[21],PrimaryMedicalServices[22],PrimaryMedicalServices[23],PrimaryMedicalServices[24],PrimaryMedicalServices[25],PrimaryMedicalServices[32],PrimaryMedicalServices[26],PrimaryMedicalServices[27],PrimaryMedicalServices[28] ,PrimaryMedicalServices[29],PrimaryMedicalServices[1] },

                        //Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia", "Okulistyka"},
                        //VoivodeshipType=Core.Enums.VoivodeshipType.pomorskie,
                        Aglomeration=Core.Enums.Aglomeration.Poznan,
                        ImagePath="/img/locations/loc7.jpg",
                        PhoneNumber="30 500 500 241",
                        PostalCode="60-102",
                        MedicalRooms=MedicalRooms.GetRange(80,10),//roomsCollections.ElementAt(1)

                    },
                    new Location()
                    {
                        City="Wrocław",
                        StreetAndNumber="Szczytnicka 11",
                        Description="Placówka położona nieco na wschód od ścisłego centrum. Łatwo do niej trafić, idąc prosto od strony placu Grunwaldzkiego.",
                        Facilities=new List<string>(){"15 gabinetów ogólno-konsultacyjnych", "Gabinet zabiegowy", "2 gabinety stomatologiczne", "Gabinet higieny jamy ustnej", "Gabinet diagnostyki obrazowej"},
                        Id=8,
                        Name="Ośrodek Wrocław Szczytnicka",
                                                Services=new List<MedicalService>(){ PrimaryMedicalServices[15],PrimaryMedicalServices[31],PrimaryMedicalServices[32],PrimaryMedicalServices[33],PrimaryMedicalServices[0],PrimaryMedicalServices[1],PrimaryMedicalServices[2],PrimaryMedicalServices[3] ,PrimaryMedicalServices[4],PrimaryMedicalServices[5] },

                        //Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia"},
                        //VoivodeshipType=Core.Enums.VoivodeshipType.pomorskie,
                        Aglomeration=Core.Enums.Aglomeration.Wroclaw,
                        ImagePath="/img/locations/loc8.jpg",
                        PhoneNumber="71 500 500 241",
                        PostalCode="50-031",
                        MedicalRooms=MedicalRooms.GetRange(90,14),//roomsCollections.ElementAt(3)
                        
                    },
                    new Location()
                    {
                        City="Katowice",
                        StreetAndNumber="Młyńska 23",
                        Description="Ośrodek położony w bliskiej okolicy dworca PKP oraz Placu Wolności",
                        Facilities=new List<string>(){"21 gabinetów ogólno-konsultacyjnych", "Gabinet zabiegowy", "2 gabinety stomatologiczne", "Gabinet higieny jamy ustnej", "Gabinet diagnostyki obrazowej"},
                        Id=9,
                        Name="Ośrodek Kopalnia Katowice",
                        Services=new List<MedicalService>(){ PrimaryMedicalServices[15],PrimaryMedicalServices[16],PrimaryMedicalServices[17],PrimaryMedicalServices[18],PrimaryMedicalServices[14],PrimaryMedicalServices[25],PrimaryMedicalServices[6],PrimaryMedicalServices[7],PrimaryMedicalServices[8] ,PrimaryMedicalServices[9],PrimaryMedicalServices[11] },
                        //Services=new List<string>(){"Interna", "Ginekologia", "Pediatria", "Diagnostyka obrazowa", "Stomatologia", "Higiena jamy ustnej", "Dermatologia", "Ortopedia", "Neurochirurgia", "Gastrologia"},
                        //VoivodeshipType=Core.Enums.VoivodeshipType.pomorskie,
                        Aglomeration=Core.Enums.Aglomeration.Silesia,
                        ImagePath="/img/locations/loc9.jpg",
                        PhoneNumber="32 500 500 241",
                        PostalCode="40-750",
                        MedicalRooms=MedicalRooms.GetRange(104,20),//roomsCollections.ElementAt(2)
                    },
            };
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

        private static List<Person> GetAllPersons()
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
                                                                                                                                                                                                                                                                                                                                                                                                                  //administrative workers

            //customer service workers
            //};
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
            int userId = 0;
            List<MedicalWorker> MedicalWorkers = new List<MedicalWorker>()
            {
                new Doctor(Persons[0],"IUHIDUASHDI545613216")
                {
                    Id=++id,
                    Education=UM_1,//new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/MW/m/1.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    User=Users[userId++],
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[0],PrimaryMedicalServices[1]
                    }

                },

                new Doctor(Persons[1], "ASGER51541213")
                {
                    Id=++id,
                    User=Users[userId++],
                    Education=UM_3,// new List<string>() {UM_3,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu praskim",
                    //ImagePath="/img/MW/m/2.jpg",
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
                    User=Users[userId++],
                    Education=UM_2,// new List<string>() {UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu MSWiA",
                    //ImagePath="/img/MW/m/3.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings3,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[33],                        PrimaryMedicalServices[31],                        PrimaryMedicalServices[32]
                    }

                },
                new Physiotherapist   (Persons[3],"IUJNKJN54321165")
                {Id=++id,
                    User=Users[userId++],
                    Education=UM_2,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu UMK",
                    //ImagePath="/img/MW/m/4.jpg",
                    HiredSince=new DateTime(2020,4,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[33],                        PrimaryMedicalServices[31],                        PrimaryMedicalServices[32]
                    }

                },
                new Doctor(Persons[4],"IUJKHJK546121646")
                {Id=++id,
                    User=Users[userId++],
                    Education=UM_1,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/MW/m/5.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[5]
                    }
                },
                new Doctor(Persons[5],"OPASDASP54156142313")
                {Id=++id,
                    User=Users[userId++],
                    Education=UM_1,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/MW/m/6.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings2,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[20],PrimaryMedicalServices[21]
                    }
                },
                new Doctor(Persons[6], "IAOSD5161231564")
                {Id=++id,
                    User=Users[userId++],
                    Education=UM_7,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu wrocławskim",
                    //ImagePath="/img/MW/m/7.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings3,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[8]
                    }
                },
                new Doctor(Persons[7], "UNCAJSDS51651323")
                {Id=++id,
                    User=Users[userId++],

                    Education=UM_3,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu podlaskim",
                    //ImagePath="/img/MW/m/8.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings2,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[7],PrimaryMedicalServices[2]
                    }

                },
                new Doctor(Persons[8], "DFSDFD4654213")
                {Id=++id,
                    User=Users[userId++],

                    Education=UM_4,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/MW/m/9.jpg",
                    HiredSince=new DateTime(2012,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[4],PrimaryMedicalServices[2]
                    }
                },
                new Doctor(Persons[9],"IOWNCAS5613245")
                {Id=++id,
                                    User=Users[userId++],

                    Education=UM_5,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu suwalskim",
                    //ImagePath="/img/MW/m/10.jpg",
                    HiredSince=new DateTime(2018,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[20],PrimaryMedicalServices[22]
                    }
                },
                new Doctor(Persons[10],"MNMCXISA561235")
                {Id=++id,
                                    User=Users[userId++],

                    Education=UM_9,// new List<string>() {UM_9},
                    Experience="W latach 2008-2019 praca w szpitalu podkarpackim",
                    //ImagePath="/img/MW/m/11.jpg",
                    HiredSince=new DateTime(2017,5,5),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[7],PrimaryMedicalServices[2]
                    }

                },
                new Doctor(Persons[11],"ASIUDAS5123463")
                {Id=++id,
                                    User=Users[userId++],

                    Education=UM_8,// new List<string>() {UM_8},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/MW/m/12.jpg",
                    HiredSince=new DateTime(2017,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[5]
                    }

                },
                new ElectroradiologyTechnician (Persons[12],"QPSCS5346448")
                {Id=++id,
                                    User=Users[userId++],

                    Education=UM_7,// new List<string>() {UM_7},
                    Experience="W latach 2005-2020 praca w szpitalu wojskowym",
                    //ImagePath="/img/MW/m/13.jpg",
                    HiredSince=new DateTime(2012,12,12),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[28],                        PrimaryMedicalServices[29]

                    }

                },
                new Doctor(Persons[13], "CXCXZS6543215")
                {Id=++id,
                                    User=Users[userId++],

                    Education=UM_6,// new List<string>() {UM_6},
                    Experience="W latach 2010-2019 praca w szpitalu matki i dziecka",
                    //ImagePath="/img/MW/m/14.jpg",
                    HiredSince=new DateTime(2019,4,4),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[7]}

                },
                new Doctor(Persons[14], "PASXCA516164")
                {Id=++id,
                                    User=Users[userId++],

                    Education=UM_5,// new List<string>() {UM_5},
                    Experience="W latach 2011-2021 praca w szpitalu zakaźnym",
                    //ImagePath="/img/MW/m/15.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[9]
                    }

                },
                new Doctor(Persons[15], "PSADNASJ1564613")
                {Id=++id,
                                    User=Users[userId++],

                    Education=UM_4,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2007-2021 praca w szpitalu kujawskim",
                    //ImagePath="/img/MW/m/16.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[8]
                    }
                },
                new Doctor(Persons[16], "AHUHIFDSD18564513")
                {Id=++id,
                                    User=Users[userId++],

                    Education=UM_4,// new List<string>() {UM_4},
                    Experience="W latach 2005-2020 praca w szpitalu łódzkim",
                    //ImagePath="/img/MW/m/17.jpg",
                    HiredSince=new DateTime(2013,3,3),
                    IsCurrentlyHired=true,VisitReviews=visitRatings3,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[11],PrimaryMedicalServices[12]
                    }

                },
                new Doctor(Persons[17],"UYGSDAS541321")
                {Id=++id,
                                    User=Users[userId++],
                    Education=UM_2,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/MW/m/2.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings2,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[11],PrimaryMedicalServices[13]
                    }

                },
                new Doctor(Persons[18],"JHGDAJSH516145")
                {Id=++id,
                                    User=Users[userId++],

                    Education=UM_3,// new List<string>() {UM_3},
                    Experience="W latach 2009-2020 praca w POZ Węgrów.",
                    //ImagePath="/img/MW/m/19.jpg",
                    HiredSince=new DateTime(2018,7,6),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[11],PrimaryMedicalServices[14]
                    }

                },
                new Doctor(Persons[19], "GSFEQWDXA515646")
                {Id=++id,
                                    User=Users[userId++],

                    Education=UM_1,// new List<string>() {UM_1},
                    Experience="W latach 2005-2020 praca w szpitalu miejskim w Krośnie",
                    //ImagePath="/img/MW/m/20.jpg",
                    HiredSince=new DateTime(2020,2,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[11],PrimaryMedicalServices[15]
                    }

                },
                new Doctor(Persons[20], "ISJAD4465132")
                {Id=++id,
                                    User=Users[userId++],

                    Education=UM_2,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu tarnowskim",
                    //ImagePath="/img/MW/m/21.jpg",
                    HiredSince=new DateTime(2017,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[16]
                    }

                },
                new Doctor(Persons[21], "UISDR216443")
                {Id=++id,
                                    User=Users[userId++],

                    Education=UM_3,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu miejskim w Zakopanem",
                    //ImagePath="/img/MW/m/22.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings2,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[17]
                    }

                },
                new Doctor(Persons[22], "VASDK5421324")
                {Id=++id,
                                    User=Users[userId++],

                    Education=UM_4,// new List<string>() {UM_7},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/MW/m/23.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[18]
                    }
                },
                new Doctor(Persons[23], "ASPDUI56321587")
                {Id=++id,
                                    User=Users[userId++],

                    Education=UM_5,// new List<string>() {UM_5},
                    Experience="W latach 2008-2014 praca w szpitalu kardiologicznym",
                    //ImagePath="/img/MW/m/2.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[19]
                    }
                },
                new Doctor(Persons[24], "BVNMXCA4623148")
                {Id=++id,
                                    User=Users[userId++],

                    Education=UM_6,// new List<string>() {UM_5},
                    Experience="W latach 2005-2020 praca w szpitalu w Dębicy",
                    //ImagePath="/img/MW/m/25.jpg",
                    //HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[20],PrimaryMedicalServices[24]
                    }

                },
                new Physiotherapist(Persons[25],"FAHDJ665413215")
                {Id=++id,
                                    User=Users[userId++],

                    Education=UM_7,// new List<string>() {UM_4},
                    Experience="W latach 2005-2020 praca w szpitalu powiatowym w Zamościu",
                    //ImagePath="/img/MW/m/26.jpg",
                    //HiredSince=new DateTime(2019,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[31],PrimaryMedicalServices[32],PrimaryMedicalServices[33]
                    }

                },
                new Doctor(Persons[26],"ALKJSD5461321")
                {Id=++id,
                                    User=Users[userId++],
                    Education=UM_8,// new List<string>() {UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu zakaźnym na Woli",
                    //ImagePath="/img/MW/m/27.jpg",
                    //HiredSince=new DateTime(2011,10,11),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[6],PrimaryMedicalServices[27]
                    }

                },
                new ElectroradiologyTechnician(Persons[27], "HGSDAS545641231")
                {Id=++id,
                                                    User=Users[userId++],

                    Education=UM_9,// new List<string>() {UM_6},
                    Experience="W latach 2006-2019 praca w szpitalu świętokrzyskim",
                    //ImagePath="/img/MW/m/28.jpg",
                    //HiredSince=new DateTime(2020,8,8),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[28],PrimaryMedicalServices[29]
                    }

                },
                new Doctor(Persons[28],"BHJASGDJAS54613254")
                {
                    Id=++id,
                                                        User=Users[userId++],

                    Education=UM_9,////new List<string>() {UM_8},
                    Experience="W latach 2005-2020 praca w szpitalu akademickim w Białymstoku",
                    //ImagePath="/img/MW/m/29.jpg",
                    //HiredSince=new DateTime(2018,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[6],PrimaryMedicalServices[27]
                    }
                },
                new Doctor(Persons[29],"OJIHJDAS543156")
                {
                Id=++id,
                                                    User=Users[userId++],

                    Education=UM_8,// new List<string>() {UM_6},
                    Experience="W latach 2005-2020 praca w szpitalu miejskim w Słupsku",
                    //ImagePath="/img/MW/m/30.jpg",
                    //HiredSince=new DateTime(2016,4,4),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[7]
                    }

                },
                new Doctor(Persons[30],"JHASKDAS65461321")
                {
                    Id = ++id,
                                                        User=Users[userId++],

                    Education =UM_7,// new List<string>() {UM_3},
                    Experience="W latach 2005-2012 praca w szpitalu klinicznym w Gnieźnie. Wcześniej pracował w Zielonej górze.",
                    //ImagePath="/img/MW/m/31.jpg",
                    //HiredSince=new DateTime(2011,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[11],PrimaryMedicalServices[12],PrimaryMedicalServices[13]
                    }
                },
                new Doctor(Persons[31],"JHKSDASD546123")
                {
                    Id = ++id,
                                                        User=Users[userId++],

                    Education =UM_6,// new List<string>() {UM_4},
                    Experience="W latach 2005-2020 praca w szpitalu akademickim w Krakowie",
                    //ImagePath="/img/MW/m/32.jpg",
                    //HiredSince=new DateTime(2019,8,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[8]
                    }

                },
                new Doctor(Persons[32],"JHASHJDGJA4516354")
                {
                    Id = ++id,
                                                        User=Users[userId++],

                    Education = UM_6,//new List<string>() {UM_6},
                    Experience="W latach 2009-2019 praca w szpitalu w Węgrowie",
                    //ImagePath="/img/MW/k/1.jpg",
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
                                                        User=Users[userId++],

                    Education =UM_1,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2015-2021 praca w szpitalu uniwersyteckim w Poznaniu",
                    //ImagePath="/img/MW/k/2.jpg",
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
                                                        User=Users[userId++],

                    Education =UM_3,// new List<string>() {UM_3},
                    Experience="W latach 2011-2021 praca w szpitalu miejskim w Łowiczu",
                    //ImagePath="/img/MW/k/3.jpg",
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
                                                        User=Users[userId++],

                    Education =UM_2,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2008-2020 praca w szpitalu zakaźnym w Krakowie",
                    //ImagePath="/img/mw/k/4.jpg",
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
                                                        User=Users[userId++],

                    Education =UM_4,// new List<string>() {UM_4},
                    Experience="W latach 2007-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/mw/k/5.jpg",
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
                                                        User=Users[userId++],

                    Education =UM_4,//  new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/mw/k/6.jpg",
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
                                                        User=Users[userId++],

                    Education =UM_5,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2012-2020 praca w szpitalu południowym w Warszawie",
                    //ImagePath="/img/mw/k/7.jpg",
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
                                                        User=Users[userId++],

                    Education =UM_2,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu chorób serca w Gdańsku",
                    //ImagePath="/img/mw/k/8.jpg",
                    HiredSince=new DateTime(2018,8,8),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[26],PrimaryMedicalServices[33]
                    }

                },
                new Nurse(Persons[40],"UGHSDS56134564")
                {
                    Id = ++id,
                                                        User=Users[userId++],

                    Education =UM_6,// new List<string>() {UM_6},
                    Experience="W latach 2007-2018 praca w szpitalu praskim w Warszawie",
                    //ImagePath="/img/mw/k/9.jpg",
                    HiredSince=new DateTime(2021,11,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[26],PrimaryMedicalServices[33]
                    }

                },
                new Doctor(Persons[41],"USHDKAS744561513")
                {
                    Id = ++id,
                                                        User=Users[userId++],

                    Education =UM_8,// new List<string>() {UM_8},
                    Experience="W latach 2009-2019 praca w szpitalu praskim w Warszawie",
                    //ImagePath="/img/mw/k/10.jpg",
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
                                                        User=Users[userId++],

                    Education =UM_5,// new List<string>() {UM_5},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/mw/k/11.jpg",
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
                                                        User=Users[userId++],

                    Education =UM_2,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2012-2019 praca w szpitalu MSWIA w Warszawie",
                    //ImagePath="/img/mw/k/12.jpg",
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
                                                        User=Users[userId++],

                    Education =UM_7,// new List<string>() {UM_7},
                    Experience="W latach 2005-2020 praca w szpitalu centralnym w Krakowie",
                    //ImagePath="/img/mw/k/13.jpg",
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
                                                        User=Users[userId++],

                    Education =UM_1,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2019-2021 praca w szpitalu u Koziołka Matołka w Poznaniu",
                    //ImagePath="/img/mw/k/14.jpg",
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
                                                        User=Users[userId++],

                    Education =UM_2 ,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu klinicznym we Wrocławiu",
                    //ImagePath="/img/mw/k/15.jpg",
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
                                                        User=Users[userId++],

                    Education =UM_3,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2018-2021 praca w szpitalu klinicznym we Wrocławiu",
                    //ImagePath="/img/mw/k/16.jpg",
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
                                                        User=Users[userId++],

                    Education =UM_8,// new List<string>() {UM_8},
                    Experience="W latach 2019-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/mw/k/17.jpg",
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
                                                        User=Users[userId++],

                    Education =UM_4,// new List<string>() {UM_4},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/mw/k/18.jpg",
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
                                                        User=Users[userId++],

                    Education =UM_7,// new List<string>() {UM_5,UM_7},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/mw/k/19.jpg",
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
                                                        User=Users[userId++],

                    Education =UM_9,// new List<string>() {UM_7,UM_9},
                    Experience="Staż odbyła w szpitalu Bródnowskim w Warszawie. Od 2016 roku pracuje w szpitalu Praskim w Warszawie.",
                    //ImagePath="/img/mw/k/20.jpg",
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
                                                        User=Users[userId++],

                    Education =UM_6,// new List<string>() {UM_6,UM_2},
                    Experience="Staż odbyty w szpitalu akademickim w Białymstoku. Od 2018 roku praca w szpitalu powiatowym w Węgrowie",
                    //ImagePath="/img/mw/k/21.jpg",
                    HiredSince=new DateTime(2018,8,8),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[26],PrimaryMedicalServices[33]
                    }

                },
                new Doctor(Persons[53], "RERDSDF2134969")
                {
                    Id = ++id,
                                                        User=Users[userId++],

                    Education = UM_4,// new List<string>() {UM_4,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/mw/k/22.jpg",
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
                                                        User=Users[userId++],

                    Education =UM_1,// new List<string>() {UM_5},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/mw/k/23.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[33],PrimaryMedicalServices[26]
                    }

                },
                new ElectroradiologyTechnician(Persons[55],"PODBASHJ4454321")
                {
                    Id = ++id,
                                                        User=Users[userId++],

                    Education =UM_3,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/mw/k/24.jpg",
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
                                                        User=Users[userId++],

                    Education =UM_1,// new List<string>() {UM_3},
                    Experience="W latach 2014-2021 praca w szpitalu zielonogórskim",
                    //ImagePath="/img/mw/k/25.jpg",
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
                                                        User=Users[userId++],

                    Education =UM_4,// new List<string>() {UM_5},
                    Experience="W latach 2005-2020 praca w szpitalu wojewódzkim w Olsztynie",
                    //ImagePath="/img/mw/k/26.jpg",
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
                                                        User=Users[userId++],

                    Education =UM_6,// new List<string>() {UM_8},
                    Experience="Od 2010 roku pracuje jako ordynator w szpitalu Matki i Dziecka w Warszawie",
                    //ImagePath="/img/mw/k/27.jpg",
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
                                                        User=Users[userId++],

                    Education =UM_7,// new List<string>() {UM_9},
                    Experience="W latach 2016-2020 praca w szpitalu miejskim w Grudziądzu",
                    //ImagePath="/img/mw/k/28.jpg",
                    HiredSince=new DateTime(2019,8,11),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                        MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[31],PrimaryMedicalServices[32],PrimaryMedicalServices[30]
                    }

                },
                new Doctor(Persons[60],"UIHDAS546516")
                {
                    Id = ++id,
                                                        User=Users[userId++],

                    Education =UM_9,    // new List<string>() {UM_1,UM_9},
                    Experience="W latach 2009-2020 praca w szpitalu miejskim w Suwałkach",
                    //ImagePath="/img/mw/k/29.jpg",
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
                                                        User=Users[userId++],

                    Education =UM_7,// new List<string>() {UM_7,UM_2},
                    Experience="W latach 2009-2020 praca w szpitalu wojewódzkim w Toruniu",
                    //ImagePath="/img/mw/k/30.jpg",
                    HiredSince=new DateTime(2019,5,4),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[26],PrimaryMedicalServices[33]
                    }

                },
                new ElectroradiologyTechnician(Persons[62],"YUGDSD56131")
                {
                    Id = ++id,
                                                        User=Users[userId++],

                    Education =UM_2,// new List<string>() {UM_2,UM_4},
                    Experience="Od 2016 pracuje w szpitalu Bródnowskim",
                    //ImagePath="/img/mw/k/31.jpg",
                    HiredSince=new DateTime(2015,5,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[28],PrimaryMedicalServices[29]
                    }
                },
                new Doctor(Persons[63],"YAJHD5461321")
                {
                    User=Users[userId++],
                    Id = ++id,
                    Education =UM_5,// new List<string>() {UM_5},
                    Experience="W latach 2009-2021 praca w szpitalu w Przemyślu",
                    //ImagePath="/img/mw/k/32.jpg",
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
                    User=Users[userId++],
                    Education =UM_3,// new List<string>() {UM_3},
                    Experience="W latach 2008-2020 praca w szpitalu w Lublinie",
                    //ImagePath="/img/mw/k/33.jpg",
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
                    User=Users[userId++],
                    Education =UM_2,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/mw/k/34.jpg",
                    HiredSince=new DateTime(2015,9,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[31],PrimaryMedicalServices[32],PrimaryMedicalServices[30]
                    }
                },
                new Doctor(Persons[66],"UHJKSAD51321")
                {
                    Id = ++id,
                    User=Users[userId++],
                    Education =UM_3,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/mw/k/35.jpg",
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
                    User=Users[userId++],
                    Education =UM_5,// new List<string>() {UM_5,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    //ImagePath="/img/mw/k/36.jpg",
                    HiredSince=new DateTime(2018,8,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[9]
                    }
                },
                new ElectroradiologyTechnician(Persons[68],"KLSAD546123")
                {
                    Id = ++id,
                    User=Users[userId++],
                    Education =UM_1,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2016-2020 praca w szpitalu lwowskim na Ukrainie",
                    //ImagePath="/img/mw/k/37.jpg",
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
                    User=Users[userId++],
                    Education =UM_3,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2005-2020 praca w szpitalu Bródnowskim",
                    ////ImagePath="/img/MW/k/38.jpg",
                    HiredSince=new DateTime(2015,2,4),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[25]
                    }
                },
                new DentalHygienist(Persons[70],"JHDAS4564231")
                {
                    Id = ++id,
                    User=Users[userId++],
                    Education =UM_5,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2016-2021 praca w szpitalu centralnym w Łodzi",
                    ////ImagePath="/img/MW/k/38.jpg",
                    HiredSince=new DateTime(2018,8,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings3,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[26],
                        PrimaryMedicalServices[33]
                    }
                },
                new DentalHygienist(Persons[71],"JHDAS4564231")
                {
                    Id = ++id,
                    User=Users[userId++],
                    Education =UM_4,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2013-2022 praca w Głównym Szpitalu Śląskim",
                    ////ImagePath="/img/MW/k/38.jpg",
                    HiredSince=new DateTime(2014,8,8),
                    IsCurrentlyHired=true,VisitReviews=visitRatings1,
                    MedicalServices=new List<MedicalService>()
                    {
                        PrimaryMedicalServices[26],
                        PrimaryMedicalServices[33]
                    }
                },
                new DentalHygienist(Persons[72],"HAISDAS465462")
                {
                    Id = ++id,
                    User=Users[userId++],
                    Education =UM_3,// new List<string>() {UM_1,UM_2},
                    Experience="W latach 2015-2020 praca w szpitalu Wolskim",
                    ////ImagePath="/img/MW/k/38.jpg",
                    HiredSince=new DateTime(2015,1,1),
                    IsCurrentlyHired=true,VisitReviews=visitRatings2,
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
                new NFZUnit() { Id = 1, Code = "DLŚ", Description = "Dolnośląski Fundusz Zdrowia" },
                new NFZUnit() { Id = 2, Code = "KPM", Description = "Kujawsko-Pomorski Fundusz Zdrowia" },
                new NFZUnit() { Id = 3, Code = "LBL", Description = "Lubelski Fundusz Zdrowia" },
                new NFZUnit() { Id = 4, Code = "LBS", Description = "Lubuski Fundusz Zdrowia" },
                new NFZUnit() { Id = 5, Code = "ŁDZ", Description = "Łódzki Fundusz Zdrowia" },
                new NFZUnit() { Id = 6, Code = "MŁP", Description = "Małopolski Fundusz Zdrowia" },
                new NFZUnit() { Id = 7, Code = "MAZ", Description = "Mazowiecki Fundusz Zdrowia" },
                new NFZUnit() { Id = 8, Code = "OPO", Description = "Opolski Fundusz Zdrowia" },
                new NFZUnit() { Id = 9, Code = "PDK", Description = "Podkarpacki Fundusz Zdrowia" },
                new NFZUnit() { Id = 10, Code = "PDL", Description = "Podlaski Fundusz Zdrowia" },
                new NFZUnit() { Id = 11, Code = "POM", Description = "Pomorski Fundusz Zdrowia" },
                new NFZUnit() { Id = 12, Code = "ŚLĄ", Description = "Śląski Fundusz Zdrowia" },
                new NFZUnit() { Id = 13, Code = "ŚWI", Description = "Świętokrzyski Fundusz Zdrowia" },
                new NFZUnit() { Id = 14, Code = "WAM", Description = "Warmińsko-Mazurski Fundusz Zdrowia" },
                new NFZUnit() { Id = 15, Code = "WLP", Description = "Wielkopolski Fundusz Zdrowia" },
                new NFZUnit() { Id = 16, Code = "ZAP", Description = "Zachodniopomorski Fundusz Zdrowia" }
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

            //List<Recommendation> recommendations = GetSomeRecommendations();
            List<MedicalReferral> referrals = GetDummyMedicalReferrals(patient, now);

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
                    Recommendations= new List<Recommendation>   (){ Recommendations[0] , Recommendations[4]},
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
                    Recommendations=new List<Recommendation>(){ Recommendations[1] },
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
                    MedicalResult=  MedicalTestResults[4] ,
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
                    Recommendations=new List<Recommendation>(){ Recommendations[4] },
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
                    Recommendations=new List<Recommendation>(){ Recommendations[3] }, //wybrać stomatologię zachowawczą
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
                    PrimaryService=PrimaryMedicalServices[33],
                    MinorMedicalServices=new List<MedicalService>(){ MedicalServices[7],MedicalServices[9]},
                    MedicalRoom=Locations.ElementAt(0).MedicalRooms.ElementAt(1),
                    DateTimeSince=dateTimeOffset.AddDays(-10),
                    DateTimeTill=dateTimeOffset.AddDays(-10).AddMinutes(15),
                    Location=Locations.ElementAt(3),
                    MedicalWorker=MedicalWorkers.ElementAt(39),
                    Patient=patient,
                    VisitCategory=VisitCategories.ElementAt(5),
                    MedicalResult=MedicalTestResults[1],

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

            referrals[0].VisitWhenIssued = patientHistoricalVisits.ElementAt(0);
            referrals[1].VisitWhenIssued = patientHistoricalVisits.ElementAt(0);
            referrals[2].VisitWhenIssued = patientHistoricalVisits.ElementAt(1);
            referrals[3].VisitWhenIssued = patientHistoricalVisits.ElementAt(1);
            referrals[4].VisitWhenIssued = patientHistoricalVisits.ElementAt(4);
            referrals[5].VisitWhenIssued = patientHistoricalVisits.ElementAt(6);
            referrals[6].VisitWhenIssued = patientHistoricalVisits.ElementAt(4);
            referrals[7].VisitWhenIssued = patientHistoricalVisits.ElementAt(8);
            referrals[8].VisitWhenIssued = patientHistoricalVisits.ElementAt(1);
            referrals[9].VisitWhenIssued = patientHistoricalVisits.ElementAt(8);
            //////wyniki badan krwi i moczu
            //     medicalTestResults[0].Visit = patientHistoricalVisits.ElementAt(0);
            //krwi
            MedicalTestResults[1].Visit = patientHistoricalVisits.ElementAt(7);
            //cholesterolu
            //medicalTestResults[2].Visit = patientHistoricalVisits.ElementAt(2);
            //ekg serca
            // medicalTestResults[3].Visit = patientHistoricalVisits.ElementAt(3);
            //rtg nadgarstka
            MedicalTestResults[4].Visit = patientHistoricalVisits.ElementAt(2);


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
                    PrimaryService=PrimaryMedicalServices[33],
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

        private static List<MedicalReferral> GetDummyMedicalReferrals(Patient patient, DateTimeOffset now)
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
                    PrimaryMedicalService=PrimaryMedicalServices[33],
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
                    PrimaryMedicalService=PrimaryMedicalServices[33],
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
                    PrimaryMedicalService=PrimaryMedicalServices[33],
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

        private static List<MedicalTestResult> GetSomeMedicalTestResults()
        {
            List<MedicalTestResult> medicalTestResults = new List<MedicalTestResult>()
            {
                new MedicalTestResult()
                {
                    Description="Wyniki rozszerzonych badań krwi",
                    MedicalService=MedicalServices[8],
                    Document= Properties.Resources.Badania_krwi_i_moczu                ,  //new PdfSharpCore.Pdf.PdfDocument( new MemoryStream( Properties.Resources.Badania_krwi_i_moczu                )),
                    Id=1,
                },
                new MedicalTestResult()
                {
                    Id=2,
                    Description="Wyniki podstawowych badań krwi",
                    MedicalService=MedicalServices[7],
                    Document= Properties.Resources.badania_krwi,// new PdfSharpCore.Pdf.PdfDocument( new MemoryStream( Properties.Resources.badania_krwi                ))
                },
                new MedicalTestResult()
                {
                    Id=3,
                    Description="Wyniki badań cholesterolu",
                    MedicalService=MedicalServices[50],
                    Document=Properties.Resources.cholesterol,//  new PdfSharpCore.Pdf.PdfDocument( new MemoryStream( Properties.Resources.cholesterol ))
                },
                new MedicalTestResult()
                {
                    Id=4,
                    Description="Wyniki ekg serca",
                    MedicalService=MedicalServices[0],
                    Document=Properties.Resources.ekg,//  new PdfSharpCore.Pdf.PdfDocument( new MemoryStream( Properties.Resources.ekg))
                },
                new MedicalTestResult()
                {
                    Id=5,
                    Description="RTG nadgarstka z trzech stron",
                    MedicalService=MedicalServices[86],//wybrać ekg serca
                    Document=Properties.Resources.ekg,//new PdfSharpCore.Pdf.PdfDocument(new MemoryStream(Properties.Resources.ekg)),
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

            };

            //chirurgia
            services.Where(d => d.Id == 48).FirstOrDefault().SubServices = new List<MedicalService>(services.GetRange(33, 6));
            //services[48].SubServices.Append(services[10]);
            services.Where(d => d.Id == 50).FirstOrDefault().SubServices = new List<MedicalService>() { services.Where(c => c.Id == 95).FirstOrDefault() };


            //ortopeda
            MedicalService service = services.Where(d => d.Id == 43).FirstOrDefault();
            MedicalService subService = services.Where(c => c.Id == 85).FirstOrDefault();
            service.SubServices = new List<MedicalService>();
            service.SubServices.Add(subService);
            service.SubServices.Add(services.Where(c => c.Id == 9).FirstOrDefault());

            //gastrologia

            services[0].SubServices = new List<MedicalService>() { services.Where(c => c.Id == 6).FirstOrDefault(), services.Where(c => c.Id == 8).FirstOrDefault() };

            //okulista
            services[45].SubServices = new List<MedicalService>(services.GetRange(80, 5));
            services[45].SubServices.Add(services[5]);


            //laryngologia
            service = services.Where(c => c.Id == 53).First();
            service.SubServices = new List<MedicalService>();
            service.SubServices.Add(services.Where(c => c.Id == 37).First());
            service.SubServices.Add(services.Where(c => c.Id == 7).First());


            //stomatologia
            services.Where(c => c.Id == 62).FirstOrDefault().SubServices = new List<MedicalService>(services.Where(c => c.Id >= 11 && c.Id < 13));
            services.Where(c => c.Id == 58).FirstOrDefault().SubServices = new List<MedicalService>(services.Where(c => c.Id >= 77 && c.Id < 80));
            services.Where(c => c.Id == 59).First().SubServices = new List<MedicalService>(services.Where(c => c.Id >= 13 && c.Id < 17));
            services.Where(c => c.Id == 60).First().SubServices = new List<MedicalService>(services.Where(c => c.Id >= 17 && c.Id < 19));
            services.Where(c => c.Id == 61).First().SubServices = new List<MedicalService>(services.Where(c => c.Id >= 63 && c.Id < 66));
            services.Where(c => c.Id == 57).First().SubServices = new List<MedicalService>(services.Where(c => c.Id >= 19 && c.Id < 24));

            //badania laboratoryjne oraz szczepienia
            services.Where(c => c.Id == 74).First().SubServices = new List<MedicalService>(services.Where(c => c.Id >= 24 && c.Id > 30));
            services.Where(c => c.Id == 76).First().SubServices = new List<MedicalService>(services.Where(c => c.Id >= 66 && c.Id < 73));

            //fizjoterapia
            services.Where(c => c.Id == 75).First().SubServices = new List<MedicalService>(services.Where(c => c.Id >= 32 && c.Id < 37));

            //szczepienia

            //proktolog

            service = services.Where(c => c.Id == 38).First();
            service.SubServices = new List<MedicalService>(services.Where(c => c.Id >= 96 && c.Id <= 98));


            return services;
        }

        public static IEnumerable<VisitCategory> GetVisitCategories()
        {
            List<VisitCategory> categories = new()
            {
                new VisitCategory() { Id = 1, CategoryName = "Konsultacje stacjonarne", PrimaryMedicalServices = new List<MedicalService>(PrimaryMedicalServices.GetRange(0, 20)) },
                new VisitCategory() { Id = 2, CategoryName = "E-konsultacje", PrimaryMedicalServices = new List<MedicalService>(PrimaryMedicalServices.GetRange(0, 20)) { } },
                new VisitCategory() { Id = 3, CategoryName = "Stomatologia", PrimaryMedicalServices = new List<MedicalService>(PrimaryMedicalServices.GetRange(20, 6)) { } },
                new VisitCategory() { Id = 4, CategoryName = "Diagnostyka obrazowa ", PrimaryMedicalServices = new List<MedicalService>(PrimaryMedicalServices.GetRange(27, 3)) { } },
                new VisitCategory() { Id = 5, CategoryName = "Fizjoterapia", PrimaryMedicalServices = new List<MedicalService>(PrimaryMedicalServices.GetRange(30, 3)) { } },
                new VisitCategory() { Id = 6, CategoryName = "Gabinet zabiegowy", PrimaryMedicalServices = new List<MedicalService>() { PrimaryMedicalServices[26], PrimaryMedicalServices[33] } },
            };
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

            List<Patient> patients = new List<Patient>()
            {
                new Patient(Persons[++id])
                {
                    Id=++patientId,
                    EmployerNIP="845465154654",
                    MedicalPackage=MedicalPackages[0],
                    NFZUnit=NfzUnits[0],
                    UserId=Persons[id].Id
                },
                new Patient (Persons[++id]                    )
                {
                    Id=++patientId,
                    EmployerNIP="7777742132152",
                    MedicalPackage=MedicalPackages[3],
                    NFZUnit=NfzUnits[15],
                    UserId=Persons[id].Id
                },

                new Patient (Persons[++id]                    )
                {
                    Id=++patientId,
                    EmployerNIP="549642132152",
                    MedicalPackage=MedicalPackages[0],
                    NFZUnit=NfzUnits[0],
                    UserId=Persons[id].Id

                },
                new Patient (Persons[++id]                    )
                {
                    Id=++patientId,
                    EmployerNIP="549642132152",
                    MedicalPackage=MedicalPackages[1],
                    NFZUnit=NfzUnits[1],
                    UserId=Persons[id].Id

                },
                new Patient (Persons[++id]                    )
                {
                    Id=++patientId,
                    EmployerNIP="7777742132152",
                    MedicalPackage=MedicalPackages[2],
                    NFZUnit=NfzUnits[2],
                    UserId=Persons[id].Id

                },
                new Patient (Persons[++id]                    )
                {
                    Id=++patientId,
                    EmployerNIP="7777742132152",
                    MedicalPackage=MedicalPackages[3],
                    NFZUnit=NfzUnits[3],
                    UserId=Persons[id].Id

                },
                new Patient (Persons[++id])
                {
                    Id=++patientId,
                    EmployerNIP="7777742132152",
                    MedicalPackage=MedicalPackages[1],
                    NFZUnit=NfzUnits[4],
                    UserId=Persons[id].Id

                },
                new Patient (Persons[++id]                    )
                {
                    Id=++patientId,
                    EmployerNIP="7777742132152",
                    MedicalPackage=MedicalPackages[0],
                    NFZUnit=NfzUnits[5],
                    UserId=Persons[id].Id

                },
                new Patient (Persons[++id]                    )
                {
                    Id=++patientId,
                    EmployerNIP="7777742132152",
                    MedicalPackage=MedicalPackages[2],
                    NFZUnit=NfzUnits[2],
                    UserId=Persons[id].Id

                },
                new Patient (Persons[++id]                    )
                {
                    Id=++patientId,
                    EmployerNIP="7777742132152",
                    MedicalPackage=MedicalPackages[3],
                    NFZUnit=NfzUnits[7],
                    UserId=Persons[id].Id

                },
                new Patient (Persons[++id]                    )
                {
                    Id=++patientId,
                    EmployerNIP="984891621654",
                    MedicalPackage=MedicalPackages[2],
                    NFZUnit=NfzUnits[8],
                    EmployerName="Styropmin",
                    UserId=Persons[id].Id

                },
                new Patient (Persons[++id]                    )
                {
                    Id=++patientId,
                    EmployerNIP="54646516465",
                    MedicalPackage=MedicalPackages[2],
                    NFZUnit=NfzUnits[8],
                    EmployerName="UM Ząbki",
                    UserId=Persons[id].Id

                },
                new Patient (Persons[++id])
                {
                    Id=++patientId,
                    EmployerNIP="7777742132152",
                    MedicalPackage=MedicalPackages[1],
                    NFZUnit=NfzUnits[7],
                    UserId=Persons[id].Id,
                    EmployerName="PKP Intercity"
                },
                new Patient (Persons[++id]                    )
                {
                    Id=++patientId,
                    EmployerNIP="54646516465",
                    MedicalPackage=MedicalPackages[1],
                    NFZUnit=NfzUnits[2],
                    EmployerName="Biedronka",
                    UserId=Persons[id].Id

                },
                new Patient (Persons[++id]                    )
                {
                    Id=++patientId,
                    EmployerName="McKinsey Polska Sp. z o.o.",
                    EmployerNIP="7777742132152",
                    MedicalPackage=MedicalPackages[0],
                    NFZUnit=NfzUnits[10],
                    UserId=Persons[id].Id

                },
                new Patient (Persons[++id]                    )
                {
                    Id=++patientId,
                    EmployerNIP="4657964654654",
                    EmployerName="Coca Cola",
                    MedicalPackage=MedicalPackages[2],
                    NFZUnit=NfzUnits[5],
                    UserId=Persons[id].Id

                },
                new Patient (Persons[++id]                    )
                {
                    Id=++patientId,
                    EmployerNIP="63123154654",
                    EmployerName="Apple Sp. z o.o.",
                    MedicalPackage=MedicalPackages[2],
                    NFZUnit=NfzUnits[4],
                    UserId=Persons[id].Id

                },
                new Patient (Persons[++id]                    )
                {
                    Id=++patientId,
                    EmployerNIP="1324564895413",
                    EmployerName="Samsung Polska",
                    MedicalPackage=MedicalPackages[3],
                    NFZUnit=NfzUnits[3],
                    UserId=Persons[id].Id

                },
                new Patient (Persons[++id]                    )
                {
                    Id=++patientId,
                    EmployerNIP="13648946542",
                    EmployerName="Dell Polska S.A.",
                    MedicalPackage=MedicalPackages[0],
                    NFZUnit=NfzUnits[2],
                    UserId=Persons[id].Id

                },
                new Patient (Persons[++id]                    )
                {
                    Id=++patientId,
                    EmployerNIP="161315648635",
                    EmployerName="CCC Sp. z o.o.",
                    MedicalPackage=MedicalPackages[1],
                    NFZUnit=NfzUnits[15],
                    UserId=Persons[id].Id

                },
                new Patient (Persons[++id]                    )
                {
                    Id=++patientId,
                    EmployerNIP="87841213654",
                    EmployerName="Henkel S.A.",
                    MedicalPackage=MedicalPackages[2],
                    NFZUnit=NfzUnits[11],
                    UserId=Persons[id].Id

                },
                new Patient (Persons[++id]                    )
                {
                    Id=++patientId,
                    EmployerNIP="123165498463",
                    EmployerName="LOT S.A.",
                    MedicalPackage=MedicalPackages[3],
                    NFZUnit=NfzUnits[10],
                    UserId=Persons[id].Id

                },
                new Patient (Persons[++id]                    )
                {
                    Id=++patientId,
                    EmployerNIP="94215748989",
                    EmployerName="Alior Bank S.A.",
                    MedicalPackage=MedicalPackages[0],
                    NFZUnit=NfzUnits[12],
                    UserId=Persons[id].Id

                },
                new Patient (Persons[++id]                    )
                {
                    Id=++patientId,
                    EmployerNIP="89461231651",
                    EmployerName="Nestle Polska",
                    MedicalPackage=MedicalPackages[1],
                    NFZUnit=NfzUnits[13],
                    UserId=Persons[id].Id

                },
                new Patient (Persons[++id]                    )
                {
                    Id=++patientId,
                    EmployerNIP="465465461231",
                    EmployerName="Wedel sp. z o.o.",
                    MedicalPackage=MedicalPackages[2],
                    NFZUnit=NfzUnits[14],
                    UserId=Persons[id].Id

                },
                //new Patient (Persons[++id]                    )
                //{
                //    Id=++patientId,
                //    EmployerNIP="67655634534",
                //    EmployerName="Compensa z o.o.",
                //    MedicalPackage=MedicalPackages[2],
                //    NFZUnit=NfzUnits[16],
                //    UserId=id

                //},
                //new Patient (Persons[++id]                    )
                //{
                //    Id=++patientId,
                //    EmployerNIP="4657964654654",
                //    EmployerName="Coca Cola",
                //    MedicalPackage=MedicalPackages[3],
                //    NFZUnit=NfzUnits[5],
                //    UserId=id

                //},
                //new Patient (Persons[++id]                    )
                //{
                //    Id=++patientId,
                //    EmployerNIP="4657964654654",
                //    EmployerName="Coca Cola",
                //    MedicalPackage=MedicalPackages[3],
                //    NFZUnit=NfzUnits[5],
                //    UserId=id

                //},
                //new Patient (Persons[++id]                    )
                //{
                //    Id=++patientId,
                //    EmployerNIP="4657964654654",
                //    EmployerName="Coca Cola",
                //    MedicalPackage=MedicalPackages[3],
                //    NFZUnit=NfzUnits[5],
                //    UserId=id

                //},
                //new Patient (Persons[++id]                    )
                //{
                //    Id=++patientId,
                //    EmployerNIP="4657964654654",
                //    EmployerName="Coca Cola",
                //    MedicalPackage=MedicalPackages[3],
                //    NFZUnit=NfzUnits[5],
                //    UserId=id

                //},
                //new Patient (Persons[++id]                    )
                //{
                //    Id=++patientId,
                //    EmployerNIP="4657964654654",
                //    EmployerName="Coca Cola",
                //    MedicalPackage=MedicalPackages[3],
                //    NFZUnit=NfzUnits[5],
                //    UserId=id

                //},
                //new Patient (Persons[++id]                    )
                //{
                //    Id=++patientId,
                //    EmployerNIP="4657964654654",
                //    EmployerName="Coca Cola",
                //    MedicalPackage=MedicalPackages[3],
                //    NFZUnit=NfzUnits[5],
                //    UserId=id

                //},

            };
            patients.ForEach(c => c.User = Users.Where(d => d.Id == c.UserId).FirstOrDefault());
            return patients;
        }

        public static Patient GetPatientById(long id)
        {

            Patient patient = AllPatients.Where(c => c.Id == id).FirstOrDefault();
            patient.BookedVisits = BookedVisits.Where(c => c.Patient.Id == patient.Id).ToList();
            patient.HistoricalVisits = HistoricalVisits.Where(c => c.Patient.Id == patient.Id).ToList();
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
