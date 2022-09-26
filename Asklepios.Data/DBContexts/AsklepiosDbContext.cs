using Asklepios.Core.Enums;
using Asklepios.Core.Models;
using Asklepios.Data.InMemoryContexts;
using Asklepios.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Data.DBContexts
{

    public class AsklepiosDbContext : DbContext, IAdministrationModuleRepository, ICustomerServiceModuleRepository, IPatientModuleRepository, IMedicalWorkerModuleRepository, IHomeModuleRepository
    {
        //private const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=AsklepiosLocal;Trusted_Connection=True;";

        //public Patient CurrentPatient { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public AsklepiosDbContext(DbContextOptions<AsklepiosDbContext> options) : base(options)
        {






        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(connectionString);

            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<MedicalWorker>()
            .HasDiscriminator<string>("Discriminator")
            .HasValue<Doctor>("1")
            .HasValue<Nurse>("2")
            .HasValue<ElectroradiologyTechnician>("3")
            .HasValue<DentalHygienist>("4")
            .HasValue<Physiotherapist>("5");

            modelBuilder.Entity<MedicalService>()
                .Property(o => o.StandardPrice)
                .HasColumnType("decimal(8,2)");

            modelBuilder.Entity<MedicalService>()
                .HasMany<MedicalService>(c => c.SubServices)
                .WithOne(e => e.PrimaryService)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            modelBuilder.Entity<Visit>()
                .HasOne<MedicalService>(c => c.PrimaryService);
            //.WithMany(d => d.Visits);

            modelBuilder.Entity<MinorServiceToVisit>()
                //.HasKey(msv => new { msv.MedicalServiceId, msv.VisitId });
                .HasKey(c => c.Id);
            
            modelBuilder.Entity<MinorServiceToVisit>()
                .HasOne(msv => msv.Visit)
                .WithMany(v => v.MinorServicesToVisits)
                .HasForeignKey(msv => msv.VisitId);

            modelBuilder.Entity<MinorServiceToVisit>()
                .HasOne(msv => msv.MedicalService)
                .WithMany(v => v.MinorServicesToVisit)
                .HasForeignKey(msv => msv.MedicalServiceId);

            modelBuilder.Entity<Location>()
                .HasMany(a => a.Services)
                .WithMany(b => b.Locations);

            modelBuilder.Entity<VisitCategory>()
                .HasMany<MedicalService>(c => c.PrimaryMedicalServices)
                .WithOne(e => e.VisitCategory)
                .HasForeignKey(d => d.VisitCategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(true);


            modelBuilder.Entity<MedicalPackage>()
            .HasMany<MedicalServiceDiscount>(c => c.ServiceDiscounts)
            .WithOne(e => e.MedicalPackage)
            .HasForeignKey(d => d.MedicalPackageId);

            modelBuilder.Entity<MedicalServiceDiscount>()
                .Property(o => o.Discount)
                .HasColumnType("decimal(3,2)");


            modelBuilder.Entity<User>()
                .HasOne<Patient>(d => d.Patient)
                .WithOne(c => c.User);


            modelBuilder.Entity<User>()
                .HasOne<MedicalWorker>(d => d.MedicalWorker)
                .WithOne(c => c.User);


            modelBuilder.Entity<MedicalReferral>()
                .HasOne(r => r.VisitWhenIssued)
                .WithMany(d=>d.ExaminationReferrals);

            modelBuilder.Entity<MedicalReferral>()
                .HasOne<Visit>(r => r.VisitWhenUsed)
                .WithOne(d => d.UsedExaminationReferral)
                .OnDelete(DeleteBehavior.Restrict);
                //.HasForeignKey(e => e.UsedExaminationReferralId);

            modelBuilder.Entity<MedicalRoom>()
                .HasOne<Location>(c => c.Location)
                .WithMany();

            modelBuilder.Entity<Location>()
                .HasMany<MedicalRoom>(c => c.MedicalRooms)
                .WithOne(d => d.Location)
                .HasForeignKey(e => e.LocationId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<MedicalPackage>()
                .HasMany<Patient>()
                .WithOne(c => c.MedicalPackage)
                .HasForeignKey(d => d.MedicalPackageId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Prescription>()
                .HasMany<IssuedMedicine>(c => c.IssuedMedicines)
                .WithOne(d => d.Prescription)
                .HasForeignKey(e => e.PrescriptionId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(true);


            modelBuilder.Entity<MedicalTestResult>()
                .HasOne<Visit>(c => c.Visit)
                .WithOne(d => d.MedicalTestResult)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);


            PatientMockDB.SetData();


            var location2MedicalServiceData = new List<object>();

            foreach (Location item in PatientMockDB.Locations)
            {
                foreach (MedicalService ms in item.Services)
                {
                    location2MedicalServiceData.Add(new { LocationsId = item.Id, ServicesId = ms.Id });
                }
            }
            var medicalService2MedicalWorker = new List<object>();

            foreach (MedicalWorker item in PatientMockDB.MedicalWorkers)
            {
                foreach (MedicalService ms in item.MedicalServices)
                {
                    medicalService2MedicalWorker.Add(new {  MedicalServicesId = ms.Id  , MedicalWorkersId = item.Id });
                }
            }

            //Data cleaning

            // PatientMockDB.MedicalRooms.ForEach(c => c.Location = null);
            PatientMockDB.AllPatients.ForEach(c => c.MedicalPackage = null);
            PatientMockDB.AllPatients.ForEach(c => c.NFZUnit = null);
            PatientMockDB.AllPatients.ForEach(c => c.Person = null);
            PatientMockDB.AllPatients.ForEach(c => c.User = null);
            PatientMockDB.AllPatients.ForEach(c => c.AllVisits = null);

            PatientMockDB.AllVisits.ForEach(c => c.Location = null);
            PatientMockDB.AllVisits.ForEach(c => c.MinorMedicalServices = null);
            PatientMockDB.AllVisits.ForEach(c => c.ExaminationReferrals = null);
            PatientMockDB.AllVisits.ForEach(c => c.Recommendations = null);
            PatientMockDB.AllVisits.ForEach(c => c.MedicalRoom = null);
            PatientMockDB.AllVisits.ForEach(c => c.MedicalWorker = null);
            PatientMockDB.AllVisits.ForEach(c => c.PrimaryService = null);
            PatientMockDB.AllVisits.ForEach(c => c.VisitCategory = null);
            PatientMockDB.AllVisits.ForEach(c => c.Patient = null);

            PatientMockDB.Locations.ForEach(c => c.MedicalRooms = null);
            PatientMockDB.Locations.ForEach(c => c.Services = null);

            PatientMockDB.MedicalPackages.ForEach(c => c.ServiceDiscounts = null);

            PatientMockDB.MedicalReferrals.ForEach(c => c.IssuedBy = null);
            PatientMockDB.MedicalReferrals.ForEach(c => c.MinorMedicalService = null);
            PatientMockDB.MedicalReferrals.ForEach(c => c.PrimaryMedicalService = null);
            PatientMockDB.MedicalReferrals.ForEach(c => c.VisitWhenIssued = null);

            PatientMockDB.MedicalRooms.ForEach(c => c.Location = null);

            PatientMockDB.MedicalServices.ForEach(c => c.SubServices = null);

            PatientMockDB.MedicalTestResults.ForEach(c => c.MedicalService = null);
            PatientMockDB.MedicalTestResults.ForEach(c => c.MedicalWorker = null);

            PatientMockDB.MedicalServiceDiscounts.ForEach(c => c.MedicalPackage = null);
            PatientMockDB.MedicalServiceDiscounts.ForEach(c => c.MedicalService = null);

            PatientMockDB.MedicalWorkers.ForEach(c => c.User = null);
            PatientMockDB.MedicalWorkers.ForEach(c => c.MedicalServices = null);

            PatientMockDB.Prescriptions.ForEach(c => c.IssuedMedicines = null);

            PatientMockDB.Recommendations.ForEach(c => c.Visit = null);
            PatientMockDB.Recommendations.ForEach(c => c.Visit = null);
            PatientMockDB.Recommendations.ForEach(c => c.Visit = null);

            PatientMockDB.VisitCategories.ForEach(c => c.PrimaryMedicalServices = null);


            PatientMockDB.MinorServicesToVisits.ForEach(c => c.MedicalService = null);
            PatientMockDB.MinorServicesToVisits.ForEach(c => c.Visit = null);


            modelBuilder.Entity<MedicalService>().HasData(PatientMockDB.MedicalServices);
            modelBuilder.Entity<VisitCategory>().HasData(PatientMockDB.VisitCategories);

            modelBuilder.Entity<MedicalRoom>().HasData(PatientMockDB.MedicalRooms);
            modelBuilder.Entity<MedicalPackage>().HasData(PatientMockDB.MedicalPackages);
            //modelBuilder.Entity<VisitCategory>().HasData(PatientMockDB.VisitCategories);
            modelBuilder.Entity<NFZUnit>().HasData(PatientMockDB.NfzUnits);

            modelBuilder.Entity<Location>().HasData(PatientMockDB.Locations);
            modelBuilder.Entity<Patient>().HasData(PatientMockDB.AllPatients);

            modelBuilder.Entity<Person>().HasData(PatientMockDB.Persons);
            modelBuilder.Entity<Prescription>().HasData(PatientMockDB.Prescriptions);

            modelBuilder.Entity<MedicalReferral>().HasData(PatientMockDB.MedicalReferrals);

            modelBuilder.Entity<MedicalTestResult>().HasData(PatientMockDB.MedicalTestResults);
            modelBuilder.Entity<IssuedMedicine>().HasData(PatientMockDB.IssuedMedicines);


            modelBuilder.Entity<Doctor>().HasData(PatientMockDB.MedicalWorkers.OfType<Doctor>());
            modelBuilder.Entity<Nurse>().HasData(PatientMockDB.MedicalWorkers.OfType<Nurse>());
            modelBuilder.Entity<ElectroradiologyTechnician>().HasData(PatientMockDB.MedicalWorkers.OfType<ElectroradiologyTechnician>());
            modelBuilder.Entity<DentalHygienist>().HasData(PatientMockDB.MedicalWorkers.OfType<DentalHygienist>());
            modelBuilder.Entity<Physiotherapist>().HasData(PatientMockDB.MedicalWorkers.OfType<Physiotherapist>());


            modelBuilder.Entity<User>().HasData(PatientMockDB.Users);
            modelBuilder.Entity<VisitReview>().HasData(PatientMockDB.VisitReviews);
            modelBuilder.Entity<Visit>().HasData(PatientMockDB.AllVisits);

            modelBuilder.Entity<MedicalServiceDiscount>().HasData(PatientMockDB.MedicalServiceDiscounts);
            modelBuilder.Entity<Notification>().HasData(PatientMockDB.Notifications);
            modelBuilder.Entity<Recommendation>().HasData(PatientMockDB.Recommendations);

            modelBuilder.Entity("LocationMedicalService").HasData(location2MedicalServiceData);
            modelBuilder.Entity("MedicalServiceMedicalWorker").HasData(medicalService2MedicalWorker);
            modelBuilder.Entity<MinorServiceToVisit>().HasData(PatientMockDB.MinorServicesToVisits.Take(107));


        }

        public DbSet<User> Users { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<MedicalWorker> MedicalWorkers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<MedicalRoom> MedicalRooms { get; set; }
        public DbSet<MedicalPackage> MedicalPackages { get; set; }
        public DbSet<MedicalService> MedicalServices { get; set; }
        public DbSet<VisitCategory> VisitCategories { get; set; }
        public DbSet<MedicalReferral> MedicalReferrals { get; set; }
        public DbSet<MedicalTestResult> MedicalTestResults { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<NFZUnit> NFZUnits { get; set; }
        public DbSet<VisitReview> VisitReviews { get; set; }
        public DbSet<IssuedMedicine> IssuedMedicines { get; set; }
        public DbSet<MinorServiceToVisit> MinorServicesToVisits { get; set; }
        //public DbSet<MedicalServiceToMedicalWorker> MedicalServicesToMedicalWorkers { get; set; }
        //public DbSet<MedicalServiceToLocation> MedicalServicesToLocations { get; set; }


        public void AddLocation(Location location)
        {
            throw new NotImplementedException();
        }

        public void AddMedicalPackage(MedicalPackage newPackage)
        {
            throw new NotImplementedException();
        }

        public void AddMedicalReferral(MedicalReferral medicalReferral)
        {
            throw new NotImplementedException();
        }

        public void AddMedicalRoom(MedicalRoom room)
        {
            throw new NotImplementedException();
        }

        public void AddMedicalTestResult(MedicalTestResult medicalTestResult, IFormFile formFile, string hostPath)
        {
            throw new NotImplementedException();
        }

        public void AddMedicalWorkerObjects(User user, Person person, MedicalWorker medicalWorker)
        {
            throw new NotImplementedException();
        }

        public void AddMedicine(IssuedMedicine issuedMedicineToAdd)
        {
            throw new NotImplementedException();
        }

        public void AddNotification(long id1, NotificationType testResult, long id2, DateTimeOffset now, long visitId)
        {
            throw new NotImplementedException();
        }

        public void AddPatientObjects(User user, Person person, Patient patient)
        {
            throw new NotImplementedException();
        }

        public void AddPrescription(Prescription prescription)
        {
            throw new NotImplementedException();
        }

        public void AddRecommendation(Recommendation recommendationToAdd)
        {
            throw new NotImplementedException();
        }

        public void AddVisitsToSchedule(List<Visit> visitsToAdd)
        {
            throw new NotImplementedException();
        }

        public void BookVisit(Patient selectedPatient, Visit newVisit)
        {
            throw new NotImplementedException();
        }

        public void DeleteRecommendation(long id)
        {
            throw new NotImplementedException();
        }

        public Visit FutureVisitById(long id)
        {
            throw new NotImplementedException();
        }

        public List<Location> GetAllLocations()
        {
            throw new NotImplementedException();
        }

        public List<Patient> GetAllPatients()
        {
            throw new NotImplementedException();
        }

        public List<MedicalRoom> GetAllRooms()
        {
            throw new NotImplementedException();
        }

        public Visit GetAvailableVisitById(long id)
        {
            throw new NotImplementedException();
        }

        public List<Visit> GetAvailableVisits()
        {
            throw new NotImplementedException();
        }

        public Visit GetBookedVisitById(long currentVisitId)
        {
            throw new NotImplementedException();
        }

        public List<Visit> GetBookedVisitsByPatientId(long id)
        {
            throw new NotImplementedException();
        }

        public Patient GetCurrentPatient()
        {
            throw new NotImplementedException();
        }

        public byte[] GetDocument(string documentPath, string webRootPath)
        {
            throw new NotImplementedException();
        }

        public List<Visit> GetFutureVisits()
        {
            throw new NotImplementedException();
        }

        public List<Visit> GetFutureVisitsByMedicalWorkerId(long id)
        {
            throw new NotImplementedException();
        }

        public Visit GetHistoricalVisitById(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Visit> GetHistoricalVisits()
        {
            throw new NotImplementedException();
        }

        public List<Visit> GetHistoricalVisitsByMedicalWorkerId(long id)
        {
            throw new NotImplementedException();
        }

        public List<Visit> GetHistoricalVisitsByPatientId(long id)
        {
            throw new NotImplementedException();
        }

        public Location GetLocationById(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Location> GetLocations()
        {
            throw new NotImplementedException();
        }

        public MedicalPackage GetMedicalPackageById(long id)
        {
            throw new NotImplementedException();
        }

        public List<MedicalPackage> GetMedicalPackages()
        {
            throw new NotImplementedException();
        }

        public MedicalReferral GetMedicalReferralById(long medicalReferralIdToRemove)
        {
            throw new NotImplementedException();
        }

        public List<MedicalReferral> GetMedicalReferralsByPatientId(long id)
        {
            throw new NotImplementedException();
        }

        public MedicalService GetMedicalServiceById(long v)
        {
            throw new NotImplementedException();
        }

        public List<MedicalService> GetMedicalServices()
        {
            throw new NotImplementedException();
        }

        public MedicalWorker GetMedicalWorkerById(long id)
        {
            throw new NotImplementedException();
        }

        public MedicalWorker GetMedicalWorkerById(int id)
        {
            throw new NotImplementedException();
        }

        public MedicalWorker GetMedicalWorkerByPersonId(long personId)
        {
            MedicalWorker medicalWorker = MedicalWorkers.Where(c => c.Person.Id == personId).FirstOrDefault();
            return medicalWorker;
        }

        public List<MedicalWorker> GetMedicalWorkers()
        {
            throw new NotImplementedException();
        }

        public NFZUnit GetNFZUnitById(long id)
        {
            throw new NotImplementedException();
        }

        public NFZUnit GetNFZUnitById(string id)
        {
            throw new NotImplementedException();
        }

        public List<NFZUnit> GetNFZUnits()
        {
            throw new NotImplementedException();
        }

        public Notification GetNotificationById(long id)
        {
            throw new NotImplementedException();
        }

        public List<Notification> GetNotificationsByPatientId(long id)
        {
            throw new NotImplementedException();
        }

        public Patient GetPatientById(long id)
        {
            throw new NotImplementedException();
        }

        public Patient GetPatientById(int id)
        {
            throw new NotImplementedException();
        }

        public Patient GetPatientByUserId(long personId)
        {
            throw new NotImplementedException();
        }

        public Patient GetPatientData()
        {
            throw new NotImplementedException();
        }

        public Person GetPersonById(long personId)
        {
            return People.Where(c => c.Id == personId).FirstOrDefault();           
        }

        public Prescription GetPrescriptionById(long prescriptionIdToRemove)
        {
            throw new NotImplementedException();
        }

        public List<Prescription> GetPrescriptions()
        {
            throw new NotImplementedException();
        }

        public List<VisitReview> GetReviewsByMedicalWorkerId(long id)
        {
            throw new NotImplementedException();
        }

        public MedicalRoom GetRoomById(long id)
        {
            throw new NotImplementedException();
        }

        public List<MedicalRoom> GetRoomsByLocationId()
        {
            throw new NotImplementedException();
        }

        public List<MedicalRoom> GetUnasignedRooms()
        {
            throw new NotImplementedException();
        }

        public User GetUser(int parsedId)
        {
            throw new NotImplementedException();
        }

        public Patient GetUserById(string userId)
        {
            throw new NotImplementedException();
        }

        public List<VisitCategory> GetVisitCategories()
        {
            throw new NotImplementedException();
        }

        public VisitCategory GetVisitCategoryById(long id)
        {
            throw new NotImplementedException();
        }

        public User LogIn(User user)
        {
            List<User> users = PatientMockDB.Users;
            users = users.Where(c => c.UserType == user.UserType)?.Where(d => d.WorkerModuleType == user.WorkerModuleType).ToList();
            string emailAddressUpper = user.EmailAddress.ToUpper();
            User user1 = users.Where(c => c.EmailAddress.ToUpper() == emailAddressUpper).FirstOrDefault();
            if (user1 == null)
            {
                return null;
            }
            if (user.Password == user1.Password)
            {
                return user1;
            }
            else
            {
                return null;
            }
        }

        public void RemoveIssuedMedicineById(long medicineIdToRemove)
        {
            throw new NotImplementedException();
        }

        public void RemoveLocationById(long selectedLocationId)
        {
            throw new NotImplementedException();
        }

        public void RemoveMedicalPackageById(long selectedPackageId)
        {
            throw new NotImplementedException();
        }

        public void RemoveMedicalReferralById(long medicalReferralIdToRemove)
        {
            throw new NotImplementedException();
        }

        public void RemoveMedicalRoomById(long selectedRoomId)
        {
            throw new NotImplementedException();
        }

        public void RemoveMedicalWorkerById(long selectedWorkerId)
        {
            throw new NotImplementedException();
        }

        public void RemovePatientById(long id)
        {
            throw new NotImplementedException();
        }

        public void RemovePrescriptionById(long prescriptionIdToRemove)
        {
            throw new NotImplementedException();
        }

        public void RemoveTestResult(long id, long id1, string webRootPath)
        {
            throw new NotImplementedException();
        }

        public void RemoveVisitById(long id)
        {
            throw new NotImplementedException();
        }

        public void ResignFromVisit(Visit plannedVisit, Patient selectedPatient)
        {
            throw new NotImplementedException();
        }

        public void ResignFromVisit(long id)
        {
            throw new NotImplementedException();
        }

        public void UpdateLocation(Location selectedLocation, long selectedLocationId)
        {
            throw new NotImplementedException();
        }

        public void UpdateLocationImage(IFormFile imageFile, Location location, string webRootPath)
        {
            throw new NotImplementedException();
        }

        public void UpdateMedicalPackage(MedicalPackage newPackage)
        {
            throw new NotImplementedException();
        }

        public void UpdateMedicalWorker(MedicalWorker selectedWorker, long selectedWorkerId)
        {
            throw new NotImplementedException();
        }

        public void UpdatePatient(Patient patient)
        {
            throw new NotImplementedException();
        }

        public void UpdatePersonImage(IFormFile imageFile, Person person, string hostEnvironmentPath)
        {
            throw new NotImplementedException();
        }

        public void UpdatePrescription(Prescription prescription)
        {
            throw new NotImplementedException();
        }

        public void UpdateReferral(MedicalReferral referral)
        {
            throw new NotImplementedException();
        }

        public void UpdateRoom(MedicalRoom newRoom)
        {
            throw new NotImplementedException();
        }

        public void UpdateTestResultFile(IFormFile medicalTestFile, Visit visit, string webRootPath)
        {
            throw new NotImplementedException();
        }

        public void UpdateVisit(Visit visit)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Location> ICustomerServiceModuleRepository.GetAllLocations()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Location> IPatientModuleRepository.GetAllLocations()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Location> IHomeModuleRepository.GetAllLocations()
        {
            //List<Location> location = Locations;
            //location.ForEach(l =>GetLocationServices
            return Locations.Include(c=>c.Services).ToList();
        }

        IEnumerable<Patient> ICustomerServiceModuleRepository.GetAllPatients()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Patient> IPatientModuleRepository.GetAllPatients()
        {
            return Patients.ToList();
        }

        IEnumerable<Visit> ICustomerServiceModuleRepository.GetAvailableVisits()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Visit> IPatientModuleRepository.GetAvailableVisits()
        {
            throw new NotImplementedException();
        }

        List<Location> IMedicalWorkerModuleRepository.GetLocations()
        {
            throw new NotImplementedException();
        }

        IEnumerable<MedicalPackage> ICustomerServiceModuleRepository.GetMedicalPackages()
        {
            throw new NotImplementedException();
        }

        IEnumerable<MedicalPackage> IPatientModuleRepository.GetMedicalPackages()
        {
            throw new NotImplementedException();
        }

        IEnumerable<MedicalService> ICustomerServiceModuleRepository.GetMedicalServices()
        {
            throw new NotImplementedException();
        }

        IEnumerable<MedicalService> IPatientModuleRepository.GetMedicalServices()
        {
            throw new NotImplementedException();
        }

        IEnumerable<MedicalWorker> ICustomerServiceModuleRepository.GetMedicalWorkers()
        {
            throw new NotImplementedException();
        }

        IEnumerable<MedicalWorker> IPatientModuleRepository.GetMedicalWorkers()
        {
            throw new NotImplementedException();
        }

        IEnumerable<NFZUnit> ICustomerServiceModuleRepository.GetNFZUnits()
        {
            throw new NotImplementedException();
        }

        IEnumerable<NFZUnit> IPatientModuleRepository.GetNFZUnits()
        {
            throw new NotImplementedException();
        }

        IEnumerable<VisitCategory> ICustomerServiceModuleRepository.GetVisitCategories()
        {
            throw new NotImplementedException();
        }

        IEnumerable<VisitCategory> IPatientModuleRepository.GetVisitCategories()
        {
            throw new NotImplementedException();
        }

        public List<MedicalService> GetLocationServices(long id)
        {
            //Location location = Locations.Where(c => c.Id == id).FirstOrDefault();
            List<MedicalService> services = new List<MedicalService>();
          //  List<location> list = MedicalServicesToLocations.Where(c => c.LocationId == id).ToList();

            //if (list!=null)
            //{
            //    foreach (MedicalServiceToLocation item in list)
            //    {
            //        MedicalService service = GetMedicalServiceById(item.MedicalServiceId);
            //        if (service!=null)
            //        {
            //            services.Add(service);
            //        }
            //    }
            //}
            return services;
        }
   

    }
}
