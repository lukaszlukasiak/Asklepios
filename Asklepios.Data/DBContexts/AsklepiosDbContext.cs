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
        private const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=AsklepiosLocal;Trusted_Connection=True;";

        //public Patient CurrentPatient { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public AsklepiosDbContext(DbContextOptions<AsklepiosDbContext> options) : base(options)
        {






        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            PatientMockDB.SetData();
            modelBuilder.Entity<MedicalService>().HasData(PatientMockDB.MedicalServices);

            modelBuilder.Entity<MedicalWorker>()
            .HasDiscriminator<string>("Discriminator")
            .HasValue<Doctor>("1")
            .HasValue<Nurse>("2")
            .HasValue<ElectroradiologyTechnician>("3")
            .HasValue<DentalHygienist>("4")
            .HasValue<Physiotherapist>("5");

            modelBuilder.Entity<MedicalWorker>()
                .HasMany<MedicalService>(a => a.MedicalServices);



            modelBuilder.Entity<User>()
                .HasOne<MedicalWorker>(d => d.MedicalWorker)
                .WithOne(c => c.User);
            //.HasForeignKey("UserId");

            modelBuilder.Entity<User>()
                .HasOne<Patient>(d => d.Patient)
                .WithOne(c => c.User);
            //.HasForeignKey("UserId");


            //modelBuilder.Entity<MedicalWorker>()
            //    .HasOne<User>(c => c.User)
            //    .WithOne(b => b.MedicalWorker);

            //modelBuilder.Entity<Patient>()
            //    .HasOne<User>(c => c.User)
            //    .WithOne(b => b.Patient);


            modelBuilder.Entity<MedicalReferral>()
                .HasOne(r => r.VisitWhenIssued)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MedicalReferral>()
                .HasOne(r => r.VisitWhenUsed)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MedicalRoom>()
                .HasOne<Location>(c => c.Location)
                .WithMany();

            modelBuilder.Entity<Location>()
                .HasMany<MedicalRoom>(c => c.MedicalRooms)
                .WithOne(d => d.Location)
                .HasForeignKey(e => e.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MedicalPackage>()
                .HasMany<MedicalServiceDiscount>(c => c.ServiceDiscounts)
                .WithOne(e => e.MedicalPackage)
                .HasForeignKey(d => d.MedicalPackageId);

            modelBuilder.Entity<MedicalService>()
                .Property(o => o.StandardPrice)
                .HasColumnType("decimal(8,2)");

            modelBuilder.Entity<MedicalService>()
                .HasOne<MedicalService>(c => c.PrimaryMedicalService)
                .WithMany(e => e.SubServices)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false)
;
            modelBuilder.Entity<VisitCategory>()
                .HasMany<MedicalService>(c => c.PrimaryMedicalServices)
                .WithOne(e => e.VisitCategory)
                .HasForeignKey(d => d.VisitCategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(true)
;
            modelBuilder.Entity<MedicalServiceDiscount>()
                .Property(o=>o.Discount)
                .HasColumnType("decimal(3,2)");

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

            modelBuilder.Entity<MedicalServiceMedicalWorker>()
                .HasKey(bc => new { bc.MedicalServiceId, bc.MedicalWorkerId});

            modelBuilder.Entity<MedicalServiceMedicalWorker>()
                .HasOne<MedicalService>(bc => bc.MedicalService)
                .WithMany(b => b.MedicalServiceMedicalWorker)
                .HasForeignKey(bc => bc.MedicalServiceId);

            modelBuilder.Entity<MedicalServiceMedicalWorker>()
                .HasOne<MedicalWorker>(bc => bc.MedicalWorker)
                .WithMany(c => c.MedicalServiceMedicalWorker)
                .HasForeignKey(bc => bc.MedicalWorkerId);

            //modelBuilder.Entity<Location>()
            //  .HasMany<MedicalRoom>(r => r.MedicalRooms);
            //.WithOne(m => m.Location)
            //.HasForeignKey(m => m.LocationId);

            //Data cleaning

            // PatientMockDB.MedicalRooms.ForEach(c => c.Location = null);
            PatientMockDB.Locations.ForEach(c => c.MedicalRooms = null);
            PatientMockDB.Locations.ForEach(c => c.Services = null);
            PatientMockDB.MedicalPackages.ForEach(c => c.ServiceDiscounts = null);
            PatientMockDB.MedicalServices.ForEach(c => c.SubServices = null);
            PatientMockDB.VisitCategories.ForEach(c => c.PrimaryMedicalServices = null);
            PatientMockDB.AllPatients.ForEach(c => c.MedicalPackage= null);
            PatientMockDB.AllPatients.ForEach(c => c.NFZUnit= null);
            PatientMockDB.AllPatients.ForEach(c => c.Person = null);
            PatientMockDB.AllPatients.ForEach(c => c.User = null);
            PatientMockDB.AllPatients.ForEach(c => c.AllVisits = null);

            PatientMockDB.Prescriptions.ForEach(c => c.IssuedMedicines= null);

            PatientMockDB.MedicalReferrals.ForEach(c => c.IssuedBy = null);
            PatientMockDB.MedicalReferrals.ForEach(c => c.MinorMedicalService = null);
            PatientMockDB.MedicalReferrals.ForEach(c => c.PrimaryMedicalService = null);
            PatientMockDB.MedicalReferrals.ForEach(c => c.VisitWhenIssued = null);
            PatientMockDB.MedicalTestResults.ForEach(c => c.MedicalService= null);
            PatientMockDB.MedicalTestResults.ForEach(c => c.MedicalWorker = null);
            PatientMockDB.MedicalWorkers.ForEach(c => c.MedicalServices = null);
            PatientMockDB.AllVisits.ForEach(c => c.Location = null);
            PatientMockDB.AllVisits.ForEach(c => c.MinorMedicalServices = null);
            PatientMockDB.AllVisits.ForEach(c => c.ExaminationReferrals = null);
            PatientMockDB.AllVisits.ForEach(c => c.Recommendations = null);
            PatientMockDB.AllVisits.ForEach(c => c.MedicalRoom = null);
            PatientMockDB.AllVisits.ForEach(c => c.MedicalWorker = null);
            PatientMockDB.AllVisits.ForEach(c => c.PrimaryService = null);
            PatientMockDB.AllVisits.ForEach(c => c.VisitCategory = null);
            PatientMockDB.AllVisits.ForEach(c => c.Patient = null);

            //PatientMockDB.Prescriptions.ForEach(c=>c.);
            //PatientMockDB.MedicalRooms.ForEach(c => c. = null);
            //PatientMockDB.MedicalRooms.ForEach(c => c.Location = null);
            //PatientMockDB.MedicalRooms.ForEach(c => c.Location = null);


            modelBuilder.Entity<MedicalRoom>().HasData(PatientMockDB.MedicalRooms);
            modelBuilder.Entity<MedicalPackage>().HasData(PatientMockDB.MedicalPackages);
            modelBuilder.Entity<VisitCategory>().HasData(PatientMockDB.VisitCategories);
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
            modelBuilder.Entity<Visit>().HasData(PatientMockDB.AllVisits);

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
        public DbSet<Asklepios.Core.Models.NFZUnit> NFZUnits { get; set; }
        public DbSet<Asklepios.Core.Models.VisitReview> VisitReviews { get; set; }
        public DbSet<Asklepios.Core.Models.IssuedMedicine> IssuedMedicines { get; set; }

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
            throw new NotImplementedException();
        }

        public MedicalWorker GetMedicalWorkerData()
        {
            throw new NotImplementedException();
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

        public Person GetPerson(long personId)
        {
            throw new NotImplementedException();
        }

        public Person GetPersonById(long personId)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            return Locations.ToList();
        }

        IEnumerable<Patient> ICustomerServiceModuleRepository.GetAllPatients()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Patient> IPatientModuleRepository.GetAllPatients()
        {
            throw new NotImplementedException();
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
    }
}
