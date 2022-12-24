using Asklepios.Core.Enums;
using Asklepios.Core.Models;
using Asklepios.Data.InMemoryContexts;
using Asklepios.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Asklepios.Data.DBContexts
{
    public class AsklepiosDbContext : DbContext, IAdministrationModuleRepository, ICustomerServiceModuleRepository, IPatientModuleRepository, IMedicalWorkerModuleRepository, IHomeModuleRepository
    {
        //private const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=AsklepiosLocal;Trusted_Connection=True;";

        //public Patient CurrentPatient { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public AsklepiosDbContext(DbContextOptions<AsklepiosDbContext> options) : base(options)
        {
        }

        public DbSet<IssuedMedicine> IssuedMedicines { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<MedicalPackage> MedicalPackages { get; set; }

        public DbSet<MedicalReferral> MedicalReferrals { get; set; }

        public DbSet<MedicalRoom> MedicalRooms { get; set; }

        public DbSet<MedicalService> MedicalServices { get; set; }

        public DbSet<MedicalTestResult> MedicalTestResults { get; set; }

        public DbSet<MedicalWorker> MedicalWorkers { get; set; }

        public DbSet<MinorServiceToVisit> MinorServicesToVisits { get; set; }

        public DbSet<NFZUnit> NFZUnits { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<Prescription> Prescriptions { get; set; }

        public DbSet<Recommendation> Recommendations { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<VisitCategory> VisitCategories { get; set; }

        public DbSet<VisitReview> VisitReviews { get; set; }

        public DbSet<Visit> Visits { get; set; }

        public void AddLocation(Location location)
        {
            Locations.Add(location);
            SaveChangesAsync();
        }

        //public DbSet<MedicalServiceToMedicalWorker> MedicalServicesToMedicalWorkers { get; set; }
        //public DbSet<MedicalServiceToLocation> MedicalServicesToLocations { get; set; }
        public void AddMedicalPackage(MedicalPackage newPackage)
        {
            MedicalPackages.AddAsync(newPackage);
            SaveChangesAsync();
        }

        public void AddMedicalReferral(MedicalReferral medicalReferral)
        {
            MedicalReferrals.Add(medicalReferral);
            SaveChangesAsync();
        }

        public void AddMedicalRoom(MedicalRoom room)
        {
            MedicalRooms.AddAsync(room);
            SaveChangesAsync();
        }

        public void AddMedicalTestResult(MedicalTestResult medicalTestResult, IFormFile formFile, string hostPath)
        {
            //MedicalTestResults.AddAsync()
            throw new NotImplementedException();
        }

        public void AddMedicalWorkerObjects(User user, Person person, MedicalWorker medicalWorker)
        {
            Users.AddAsync(user);
            People.AddAsync(person);
            MedicalWorkers.AddAsync(medicalWorker);
            SaveChangesAsync();
        }

        public void AddMedicine(IssuedMedicine issuedMedicineToAdd)
        {
            IssuedMedicines.AddAsync(issuedMedicineToAdd);
            SaveChangesAsync();
        }

        public void AddNotification(long referencedItemId, NotificationType type, long patientId, DateTimeOffset now, long visitId)
        {
            Notification notification = new Notification();
            notification.NotificationType = type;
            notification.PatientId = patientId;
            notification.DateTimeAdded = now;
            notification.VisitId = visitId;
            notification.EventObjectId = referencedItemId;
            Notifications.AddAsync(notification);
            SaveChangesAsync();
        }

        public void AddPatientObjects(User user, Person person, Patient patient)
        {
            Users.AddAsync(user);
            People.AddAsync(person);
            Patients.AddAsync(patient);
            SaveChangesAsync();
        }

        public void AddPrescription(Prescription prescription)
        {
            Prescriptions.AddAsync(prescription);
            SaveChangesAsync();
        }

        public void AddRecommendation(Recommendation recommendationToAdd)
        {
            Recommendations.AddAsync(recommendationToAdd);
            SaveChangesAsync();
        }

        public void AddVisitsToSchedule(List<Visit> visitsToAdd)
        {
            Visits.AddRange(visitsToAdd);
            SaveChangesAsync();
        }

        public void BookVisit(Patient selectedPatient, Visit newVisit)
        {
            newVisit.PatientId = selectedPatient.Id;
            newVisit.VisitStatus = VisitStatus.Booked;
            Visits.Update(newVisit);
            SaveChangesAsync();
        }

        public void DeleteRecommendation(long id)
        {
            Recommendation recommendation = Recommendations.Find(id);
            long visitId = recommendation.VisitId.Value;
            Visit visit = Visits.Where(c => c.Id == visitId).FirstOrDefault();
            visit.Recommendations.Remove(recommendation);
            Recommendations.Remove(recommendation);
            SaveChangesAsync();
        }

        public Visit FutureVisitById(long id)
        {
            Visit visit = Visits.Where(c => c.Id == id).Include(a => a.VisitCategory).Include(b => b.Location).Include(c => c.PrimaryService).Include(d => d.Patient).ThenInclude(e => e.Person).FirstOrDefault();
            return visit;
        }

        public List<Location> GetAllLocations()
        {
            return Locations.Include(c => c.Services).Include(d=>d.MedicalRooms).ToList();
        }

        //IEnumerable<Location> ICustomerServiceModuleRepository.GetAllLocations()
        //{
        //    throw new NotImplementedException();
        //}
        //List<Location> GetAllLocations()
        //{
        //    return Locations.Include(c => c.Services).ToList();
        //}

        //IEnumerable<Location> IPatientModuleRepository.GetAllLocations()
        //{
        //    throw new NotImplementedException();
        //}

        //IEnumerable<Location> IHomeModuleRepository.GetAllLocations()
        //{
        //    //List<Location> location = Locations;
        //    //location.ForEach(l =>GetLocationServices
        //    return Locations.Include(c => c.Services).ToList();
        //}

        public List<Patient> GetAllPatients()
        {
            return Patients.Include(c => c.Person).Include(d => d.User).ToList();
        }

        //IEnumerable<Patient> ICustomerServiceModuleRepository.GetAllPatients()
        //{
        //    return Patients.Include(c => c.Person).Include(d => d.User).ToList();
        //}

        //IEnumerable<Patient> IPatientModuleRepository.GetAllPatients()
        //{
        //    return Patients.ToList();
        //}

        public List<MedicalRoom> GetAllRooms()
        {
            return MedicalRooms.ToList();
        }

        public Visit GetAvailableVisitById(long id)
        {
            Visit visit = Visits.Where(c => c.Id == id).Include(d => d.MinorMedicalServices).Include(e => e.MinorServicesToVisits).FirstOrDefault();
            return visit;
        }

        public List<Visit> GetAvailableVisits()
        {
            List<Visit> visits = Visits.Where(c => c.VisitStatus == VisitStatus.AvailableNotBooked).Include(d => d.MinorMedicalServices).Include(e => e.MinorServicesToVisits).Include(e => e.MedicalRoom).ToList();
            return visits;
        }

        //IEnumerable<Visit> ICustomerServiceModuleRepository.GetAvailableVisits()
        //{
        //    throw new NotImplementedException();
        //}

        //IEnumerable<Visit> IPatientModuleRepository.GetAvailableVisits()
        //{
        //    throw new NotImplementedException();
        //}

        public Visit GetBookedVisitById(long currentVisitId)
        {
            Visit visit = Visits.Where(c => c.Id == currentVisitId).Include(d => d.MinorMedicalServices).Include(e => e.MinorServicesToVisits).FirstOrDefault();
            return visit;
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
            //List<Visit> visits=    Visits.Where(c => c.PatientId == id).Where(d => d.DateTimeSince > DateTimeOffset.Now).ToList();
            List<Visit> visits = Visits.Where(c => c.MedicalWorkerId == id).Where(d => d.DateTimeSince.Day >= DateTimeOffset.Now.Day && d.VisitStatus == VisitStatus.Booked && d.VisitStatus == VisitStatus.AvailableNotBooked).Include(a => a.VisitCategory).Include(b => b.Location).Include(c => c.PrimaryService).Include(d => d.Patient).ThenInclude(e => e.Person).ToList();
            return visits;
        }

        public List<Visit> GetFutureVisitsChunk(int currentPageNumId, int itemsPerPage)
        {
            IEnumerable<Visit> visits = Visits.Where(c => c.DateTimeSince > DateTime.Now);

            int numberOfVisits = visits.Count();

            int currentPageVisitsNumber = numberOfVisits % itemsPerPage;

            if (currentPageNumId * itemsPerPage < Visits.Count())
            {
                return Visits
                .Skip((currentPageNumId - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .AsNoTracking()
                .ToList();
            }
            else
            {
                return Visits
                .Skip((currentPageNumId - 1) * itemsPerPage)
                .Take(currentPageVisitsNumber)
                .AsNoTracking()
                .ToList();
            }
        }

        public Visit GetHistoricalVisitById(long id)
        {
            Visit visit = Visits.Where(c => c.Id == id).Include(a => a.VisitCategory).Include(b => b.Location).Include(c => c.PrimaryService).Include(d => d.Patient).ThenInclude(e => e.Person).FirstOrDefault();
            return visit;
        }

        public List<Visit> GetHistoricalVisits()
        {
            List<Visit> visits = Visits.Where(d => d.VisitStatus == VisitStatus.Finished).Include(a => a.VisitCategory).Include(b => b.Location).Include(c => c.PrimaryService).Include(d => d.Patient).ThenInclude(e => e.Person).ToList();
            return visits;
        }

        public List<Visit> GetHistoricalVisitsByMedicalWorkerId(long id)
        {
            List<Visit> visits = Visits.Where(c => c.MedicalWorkerId == id).Where(d => d.VisitStatus == VisitStatus.Finished).Include(a => a.VisitCategory).Include(b => b.Location).Include(c => c.PrimaryService).Include(d => d.Patient).ThenInclude(e => e.Person).ToList();
            return visits;
        }

        public List<Visit> GetHistoricalVisitsByPatientId(long id)
        {
            List<Visit> visits = Visits.Where(c => c.PatientId == id).Where(d => d.VisitStatus == VisitStatus.Finished).Include(a => a.VisitCategory).Include(b => b.Location).Include(c => c.PrimaryService).Include(d => d.Patient).ThenInclude(e => e.Person).ToList();
            return visits;
        }

        public Location GetLocationById(long id)
        {
            Location location = Locations.Where(c => c.Id == id).FirstOrDefault();
            return location;
        }

        public List<Location> GetLocations()
        {
            return Locations.Include(a => a.Services).Include(b => b.MedicalRooms).ToList();
        }

        //List<Location> IMedicalWorkerModuleRepository.GetLocations()
        //{
        //    return Locations.Include(a => a.Services).Include(b => b.MedicalRooms).ToList();
        //}

        List<Location> ICustomerServiceModuleRepository.GetLocations()
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

        //public MedicalPackage GetMedicalPackageById(long id)
        //{
        //    MedicalPackage medicalPackage = MedicalPackages.Where(c => c.Id == id).FirstOrDefault();
        //    return medicalPackage;
        //}

        public MedicalPackage GetMedicalPackageById(long medicalPackageId)
        {
            MedicalPackage medicalPackage = MedicalPackages.Where(c => c.Id == medicalPackageId).Include(d => d.ServiceDiscounts).FirstOrDefault();
            return medicalPackage;
        }

        public List<MedicalPackage> GetMedicalPackages()
        {
            return MedicalPackages.ToList();
        }

        List<MedicalPackage> ICustomerServiceModuleRepository.GetMedicalPackages()
        {
            throw new NotImplementedException();
        }

        List<MedicalPackage> IPatientModuleRepository.GetMedicalPackages()
        {
            throw new NotImplementedException();
        }

        public MedicalReferral GetMedicalReferralById(long id)
        {
            MedicalReferral medicalReferral = MedicalReferrals.Where(c => c.Id == id).FirstOrDefault();
            return medicalReferral;
        }

        public List<MedicalReferral> GetMedicalReferralsByPatientId(long id)
        {
            List<MedicalReferral> medicalReferrals = MedicalReferrals.Where(c => c.IssuedToId == id).ToList();
            return medicalReferrals;
        }

        public MedicalRoom GetMedicalRoomById(long medicalRoomId)
        {
            MedicalRoom medicalRoom = MedicalRooms.Where(c => c.Id == medicalRoomId).FirstOrDefault();
            return medicalRoom;
        }

        public MedicalService GetMedicalServiceById(long id)
        {
            MedicalService medicalService = MedicalServices.Where(c => c.Id == id).FirstOrDefault();
            return medicalService;
        }

        public List<MedicalService> GetMedicalServices()
        {
            return MedicalServices.ToList();
        }

        //IEnumerable<MedicalService> ICustomerServiceModuleRepository.GetMedicalServices()
        //{
        //    throw new NotImplementedException();
        //}

        //IEnumerable<MedicalService> IPatientModuleRepository.GetMedicalServices()
        //{
        //    throw new NotImplementedException();
        //}

        public MedicalWorker GetMedicalWorkerById(long id)
        {
            MedicalWorker medicalWorker = MedicalWorkers.Where(c => c.Id == id).Include(c => c.MedicalServices).Include(d => d.Person).FirstOrDefault();
            return medicalWorker;
        }
        public MedicalWorker GetMedicalWorkerDetailsById(long id)
        {
            MedicalWorker medicalWorker = MedicalWorkers.Where(c => c.Id == id).Include(c => c.MedicalServices).Include(d => d.Person).Include(e=>e.VisitReviews).FirstOrDefault();
            return medicalWorker;
        }
        //public MedicalWorker GetMedicalWorkerDetailsById(long id)
        //{
        //    throw new NotImplementedException();
        //}

        public MedicalWorker GetMedicalWorkerByPersonId(long personId)
        {
            MedicalWorker medicalWorker = MedicalWorkers.Where(c => c.Person.Id == personId).FirstOrDefault();
            return medicalWorker;
        }

        public List<MedicalWorker> GetMedicalWorkers()
        {
            return MedicalWorkers.Include(c => c.Person).ToList();
        }

        //IEnumerable<MedicalWorker> ICustomerServiceModuleRepository.GetMedicalWorkers()
        //{
        //    return MedicalWorkers.ToList();
        //}

        //IEnumerable<MedicalWorker> IPatientModuleRepository.GetMedicalWorkers()
        //{
        //    return MedicalWorkers.ToList();
        //}

        public NFZUnit GetNFZUnitById(long id)
        {
            throw new NotImplementedException();
        }

        public List<NFZUnit> GetNFZUnits()
        {
            throw new NotImplementedException();
        }

        //IEnumerable<NFZUnit> ICustomerServiceModuleRepository.GetNFZUnits()
        //{
        //    throw new NotImplementedException();
        //}

        //IEnumerable<NFZUnit> IPatientModuleRepository.GetNFZUnits()
        //{
        //    throw new NotImplementedException();
        //}

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
            return Patients.Where(c => c.Id == id).Include(a => a.Person).Include(b => b.User).FirstOrDefault();
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
            List<VisitReview> reviews = VisitReviews.Where(c => c.RevieweeId == id).Include(c => c.Reviewee).Include(d => d.Reviewer).ToList();
            return reviews;
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

        //public Patient GetUserById(string userId)
        //{
        //    throw new NotImplementedException();
        //}

        public User GetUserById(long userId)
        {
            User user = Users.Where(c => c.Id == userId).Include(d => d.Person).FirstOrDefault();
            return user;
        }

        public Visit GetVisitById(long id)
        {
            Visit visit = Visits.Where(c => c.Id == id).Include(d => d.MedicalWorker).ThenInclude(f => f.Person).Include(e => e.Patient).ThenInclude(g => g.Person).Include(k => k.Location).Include(e => e.MinorMedicalServices).Include(f => f.MinorServicesToVisits).FirstOrDefault();
            return visit;
        }

        //public User GetUser(int parsedId)
        //{
        //    throw new NotImplementedException();
        //}
        public List<VisitCategory> GetVisitCategories()
        {
            return VisitCategories.ToList();
        }

        //IEnumerable<VisitCategory> ICustomerServiceModuleRepository.GetVisitCategories()
        //{
        //    throw new NotImplementedException();
        //}

        //IEnumerable<VisitCategory> IPatientModuleRepository.GetVisitCategories()
        //{
        //    throw new NotImplementedException();
        //}

        //public VisitCategory GetVisitCategoryById(long id)
        //{
        //    throw new NotImplementedException();
        //}

        public VisitCategory GetVisitCategoryById(long visitCategoryId)
        {
            VisitCategory visitCategory = VisitCategories.Where(c => c.Id == visitCategoryId).FirstOrDefault();
            return visitCategory;
        }

        public List<Visit> GetVisitsByMedicalWorkerId(long id)
        {
            List<Visit> visits = Visits.Where(c => c.MedicalWorkerId == id).Include(a => a.VisitCategory).Include(b => b.Location).Include(c => c.PrimaryService).Include(d => d.Patient).ThenInclude(e => e.Person).Include(k => k.Location).ToList();
            return visits;
        }

        public User LogIn(User user)
        {
            List<User> users;//= Users;
            users = Users.Where(c => c.UserType == user.UserType)?.Where(d => d.WorkerModuleType == user.WorkerModuleType).ToList();
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

        public void UpdateVisit(Visit visitToUpdate)
        {
            Update(visitToUpdate);

            Visit visit = Visits.Find(visitToUpdate.Id);
            //if (visit != null)
            //{
            //    visit=visitToUpdate;
            //}
            SaveChangesAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer();

            optionsBuilder.EnableSensitiveDataLogging();
            // optionsBuilder.LogTo(Console.WriteLine);
            //optionsBuilder.UseSqlServer(options=>options.ExecutionStrategy)
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
                .WithMany(d => d.ExaminationReferrals);

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
                    medicalService2MedicalWorker.Add(new { MedicalServicesId = ms.Id, MedicalWorkersId = item.Id });
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
            // modelBuilder.Entity<Recommendation>().HasData(PatientMockDB.Recommendations);
        }

    }
}