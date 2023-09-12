using Asklepios.Core.Enums;
using Asklepios.Core.Models;
using Asklepios.Data.InMemoryContexts;
using Asklepios.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Asklepios.Core.Extensions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Asklepios.Data.DBContexts
{
    public class AsklepiosDbContext : IdentityDbContext<User, IdentityRole<long>, long>, IAdministrationModuleRepository, ICustomerServiceModuleRepository, IPatientModuleRepository, IMedicalWorkerModuleRepository, IHomeModuleRepository
    {
        public AsklepiosDbContext(DbContextOptions<AsklepiosDbContext> options) : base(options)
        {


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            bool seedDatabase = true;

            base.OnModelCreating(modelBuilder);

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

                modelBuilder.Entity<Visit>()
                    .HasMany<MedicalService>(c => c.MinorMedicalServices);


                modelBuilder.Entity<MinorServiceToVisit>()
                    .HasKey(msv => new { msv.MedicalServiceId, msv.VisitId });

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
                    .HasMany<MedicalService>(c => c.MedicalServices)
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
                    .WithOne(c => c.User)
                    .HasPrincipalKey<User>(e => e.Id)
                    .HasForeignKey<Patient>(f => f.UserId);


                modelBuilder.Entity<User>()
                    .HasOne<MedicalWorker>(d => d.MedicalWorker)
                    .WithOne(c => c.User)
                    .HasPrincipalKey<User>(e => e.Id)
                    .HasForeignKey<MedicalWorker>(f => f.UserId);

                modelBuilder.Entity<MedicalReferral>()
                    .HasOne(r => r.VisitWhenIssued)
                    .WithMany(d => d.ExaminationReferrals);

                modelBuilder.Entity<MedicalReferral>()
                    .HasOne<Visit>(r => r.VisitWhenUsed)
                    
                    .WithOne(d => d.UsedExaminationReferral)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<MedicalReferral>()
                    .HasOne<Patient>(c => c.IssuedTo);

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


            if (seedDatabase)
            {


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

                PatientMockDB.VisitCategories.ForEach(c => c.MedicalServices = null);

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
                Console.WriteLine("Max visit id from visits: " + PatientMockDB.AllVisits.Max(c => c.Id));

                modelBuilder.Entity<Visit>().HasData(PatientMockDB.AllVisits);

                modelBuilder.Entity<MedicalServiceDiscount>().HasData(PatientMockDB.MedicalServiceDiscounts);
                modelBuilder.Entity<Notification>().HasData(PatientMockDB.Notifications);
                modelBuilder.Entity<Recommendation>().HasData(PatientMockDB.Recommendations);

                modelBuilder.Entity("LocationMedicalService").HasData(location2MedicalServiceData);
                modelBuilder.Entity("MedicalServiceMedicalWorker").HasData(medicalService2MedicalWorker);

                Console.WriteLine("All values: " + PatientMockDB.MinorServicesToVisits.Count().ToString());
                Console.WriteLine("Distinct values: " + PatientMockDB.MinorServicesToVisits.Select(c => c.VisitId.ToString() + c.MedicalServiceId.ToString()).Distinct().Count().ToString());
                Console.WriteLine("Max visit id from minortovisit: " + PatientMockDB.MinorServicesToVisits.Max(c => c.VisitId));
                Console.WriteLine("Max visit id from visits: " + PatientMockDB.AllVisits.Max(c => c.Id));

                modelBuilder.Entity<MinorServiceToVisit>().HasData(PatientMockDB.MinorServicesToVisits.DistinctBy(c => c.VisitId.ToString() + c.MedicalServiceId.ToString()));
                modelBuilder.Entity<IdentityRole<long>>().HasData(PatientMockDB.IdentityRoles);
                modelBuilder.Entity<IdentityUserRole<long>>().HasData(PatientMockDB.IdentityUserRoles);
                //modelBuilder.Entity("MedicalServiceToVisit").HasData(minorServiceToVisit);
                // modelBuilder.Entity<Recommendation>().HasData(PatientMockDB.Recommendations);
            }

        }

        public async Task Initialize(IServiceProvider serviceProvider,
                            List<User> userList)
        {
            var userManager = serviceProvider.GetService<UserManager<User>>();

            foreach (var user in userList)
            {
                var userPassword = user.PasswordHash;//GenerateSecurePassword();
                var userId = await EnsureUser(userManager, user);

                //NotifyUser(userName, userPassword);
            }
        }

        private async Task<long> EnsureUser(UserManager<User> userManager, User user)
        {
            var foundUser = await userManager.FindByNameAsync(user.UserName);

            if (foundUser == null)
            {
                foundUser = new User()
                {
                    UserName = user.UserName,

                    EmailConfirmed = true
                };
                await userManager.CreateAsync(foundUser, user.PasswordHash);
            }

            return foundUser.Id;
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

        //public DbSet<User> Users { get; set; }

        public DbSet<VisitCategory> VisitCategories { get; set; }

        public DbSet<VisitReview> VisitReviews { get; set; }

        public DbSet<Visit> Visits { get; set; }

        public void AddLocation(Location location, IFormFile formFile, string hostPath)
        {
            string filePath = SaveFile(formFile, StorageFolderType.Locations, hostPath);
            location.ImagePath = filePath;
            Locations.Add(location);

            SaveChanges();
        }

        //public DbSet<MedicalServiceToMedicalWorker> MedicalServicesToMedicalWorkers { get; set; }
        //public DbSet<MedicalServiceToLocation> MedicalServicesToLocations { get; set; }
        public void AddMedicalPackage(MedicalPackage newPackage)
        {
            MedicalPackages.Add(newPackage);
            SaveChanges();
        }

        public void AddMedicalReferral(MedicalReferral medicalReferral)
        {
            MedicalReferrals.Add(medicalReferral);
            SaveChanges();
        }

        public void AddMedicalRoom(MedicalRoom room)
        {
            MedicalRooms.Add(room);
            SaveChanges();
        }

        public void AddMedicalTestResult(MedicalTestResult medicalTestResult, IFormFile formFile, string hostPath)
        {
            string filePath = SaveFile(formFile, StorageFolderType.TestResult, hostPath);
            medicalTestResult.DocumentPath = filePath;
            medicalTestResult.UploadDate = DateTimeOffset.Now;

            MedicalTestResults.Add(medicalTestResult);
            SaveChanges();
            //throw new NotImplementedException();
        }

        public void AddMedicalWorkerObjects(MedicalWorker medicalWorker, string hostPath)
        {
            //Users.AddAsync(user);

            //People.AddAsync(person);
            //if (medicalWorker.Person.ImageFile != null)
            //{
            //    string filePath = SaveFile(medicalWorker.Person.ImageFile, StorageFolderType.Persons, hostPath);
            //    medicalWorker.Person.ImageFilePath = filePath;
            //}

            MedicalWorkers.Add(medicalWorker);
            SaveChanges();
        }

        public void AddMedicine(IssuedMedicine issuedMedicineToAdd)
        {
            IssuedMedicines.Add(issuedMedicineToAdd);
            SaveChanges();
        }

        public void AddNotification(long referencedItemId, NotificationType type, long patientId, DateTimeOffset now, long visitId)
        {
            Notification notification = new Notification();
            notification.NotificationType = type;
            notification.PatientId = patientId;
            notification.DateTimeAdded = now;
            notification.VisitId = visitId;
            notification.EventObjectId = referencedItemId;
            Notifications.Add(notification);
            SaveChanges();
        }
        public void AddPerson(Person person, string hostPath)
        {
            if (person.ImageFile != null)
            {
                string filePath = SaveFile(person.ImageFile, StorageFolderType.Persons, hostPath);
                person.ImageFilePath = filePath;
            }
            People.Add(person);
            SaveChanges();
        }

        public void AddPatientObjects(Patient patient, string hostPath)
        {
            //Users.AddAsync(user);
            //People.AddAsync(person);
            //if (patient.Person.ImageFile!=null)
            //{
            //    string filePath = SaveFile(patient.Person.ImageFile, StorageFolderType.Persons, hostPath);
            //    patient.Person.ImageFilePath = filePath;
            //}

            Patients.Add(patient);
            SaveChanges();
        }

        public void AddPrescription(Prescription prescription)
        {
            Prescriptions.AddAsync(prescription);
            SaveChanges();
        }

        public void AddRecommendation(Recommendation recommendationToAdd)
        {
            Recommendations.AddAsync(recommendationToAdd);
            SaveChanges();
        }

        public void AddVisitsToSchedule(List<Visit> visitsToAdd)
        {
            Visits.AddRange(visitsToAdd);
            SaveChanges();
        }

        public void BookVisit(long patientId, long visitId)
        {
            Visit visit = Visits.Find(visitId);

            visit.PatientId = patientId;
            visit.VisitStatus = VisitStatus.Booked;
            //visit.PrimaryService = null;
            Visits.Update(visit);

            ServiceClasses.EFDebugging.ListChanges(this);
            SaveChanges();
        }

        public void DeleteRecommendation(long id)
        {
            Recommendation recommendation = Recommendations.Find(id);
            long visitId = recommendation.VisitId.Value;
            Visit visit = Visits.Where(c => c.Id == visitId).FirstOrDefault();
            visit.Recommendations.Remove(recommendation);
            Recommendations.Remove(recommendation);
            SaveChanges();
        }

        public Visit FutureVisitById(long id)
        {
            Visit visit = Visits
                .Where(c => c.Id == id)
                .Include(a => a.VisitCategory)
                .Include(b => b.Location)
                .Include(c => c.PrimaryService)
                .Include(d => d.Patient)
                    .ThenInclude(e => e.Person)
                .FirstOrDefault();
            return visit;
        }

        public List<Location> GetAllLocations()
        {
            return Locations
                .Include(c => c.Services)
                .Include(d => d.MedicalRooms)
                .ToList();
        }
        public string SaveFile(IFormFile formFile, StorageFolderType type, string basePath)
        {
            string path = null;
            switch (type)
            {
                // _hostEnvironment
                case StorageFolderType.Persons:
                    path = Path.Combine("img", "Persons"); //Directory.GetCurrentDirectory() + "\\Persons";
                    break;
                case StorageFolderType.Locations:
                    path = Path.Combine("img", "Locations"); //Directory.GetCurrentDirectory() + "\\Locations";
                    break;
                case StorageFolderType.TestResult:
                    path = "Results"; //Directory.GetCurrentDirectory() + "\\Locations";
                    break;
                default:
                    break;
            }
            string extension = Path.GetExtension(formFile.FileName);
            //string resourcePath=Path.Combine(basePath,)
            string myUniqueFileName = null;//string.Format(@"{0}{1}" , Guid.NewGuid(), extension);
            string fullFileName = null;// Path.Combine(path, myUniqueFileName);

            do
            {
                myUniqueFileName = string.Format(@"{0}{1}", Guid.NewGuid(), extension);
                fullFileName = Path.Combine(basePath, path, myUniqueFileName);

            } while (System.IO.File.Exists(fullFileName));
            //if (System.IO.File.Exists(fullFileName))
            //{
            //    string myUniqueFileName = string.Format(@"{0}{1}", Guid.NewGuid(), extension);
            //    string fullFileName = Path.Combine(path, myUniqueFileName);

            //}
            string relativePath = Path.Combine("/", path, myUniqueFileName);
            using (var fileStream = new FileStream(fullFileName, FileMode.Create))
            {
                formFile.CopyTo(fileStream);
            }
            string serverFileName = relativePath;//Path.Combine("\\", path, myUniqueFileName);

            return serverFileName;

        }

        public void RemoveFile(string filePath, string webrootPath)
        {
            string fullFilePath = Path.Combine(webrootPath + filePath);

            if (File.Exists(fullFilePath))
            {
                File.Delete(fullFilePath);
            }
        }


        public IQueryable<Patient> GetAllPatientsQuery()
        {
            return Patients
                .Include(c => c.Person)
                .Include(d => d.User)
                .Include(n => n.NFZUnit)
                .Include(m => m.MedicalPackage)
                .AsQueryable();
        }


        public List<MedicalRoom> GetAllRooms()
        {
            return MedicalRooms.ToList();
        }

        public Visit GetFutureVisitById(long id)
        {
            Visit visit = Visits
                .Where(c => c.Id == id)
                .Include(d => d.MinorMedicalServices)
                .Include(e => e.MinorServicesToVisits)
                .Include(f => f.PrimaryService)
                .Include(i => i.Location)
                .Include(m => m.MedicalRoom)
                .Include(k => k.VisitCategory)
                .Include(g => g.MedicalWorker)
                    .ThenInclude(h => h.Person)
                .FirstOrDefault();
            return visit;
        }

        public IQueryable<Visit> GetAvailableVisitsQuery()
        {
            IQueryable<Visit> visits = Visits
                .Where(c => c.VisitStatus == VisitStatus.AvailableNotBooked)
                .Where(d => d.DateTimeSince > DateTime.Now)
                //.Include(d => d.MinorMedicalServices)
                //.Include(e => e.MinorServicesToVisits)
                .Include(p => p.PrimaryService)
                .Include(e => e.MedicalRoom)
                .Include(g => g.Location)
                .Include(v => v.VisitCategory)
                .Include(f => f.MedicalWorker)
                    .ThenInclude(g => g.Person)
                .AsQueryable();
            return visits;
        }

        public Visit GetBookedVisitById(long currentVisitId)
        {
            Visit visit = Visits
                .Where(c => c.Id == currentVisitId)
                .Include(d => d.MinorMedicalServices)
                .Include(e => e.MinorServicesToVisits)
                .FirstOrDefault();
            return visit;
        }


        public Visit GetBookedVisitByIdANT(long currentVisitId)
        {
            throw new NotImplementedException();
        }

        public List<Visit> GetBookedVisitsByPatientId(long id)
        {
            List<Visit> bookedVisits = Visits
                .Where(c => c.PatientId == id && c.VisitStatus == VisitStatus.Booked && c.DateTimeSince>DateTime.Now)
                .Include(d => d.MedicalWorker).ThenInclude(f => f.Person)
                .Include(e => e.Patient).ThenInclude(f => f.Person)
                .Include(g => g.Location)
                .Include(h => h.VisitCategory)
                .Include(k => k.PrimaryService)
                .Include(l => l.MinorMedicalServices)
                .ToList();
            return bookedVisits;
        }
        public IQueryable<Visit> GetBookedVisitsByPatientIdQuery(long id)
        {
            IQueryable<Visit> bookedVisits = Visits
                .Where(c => c.PatientId == id && c.VisitStatus == VisitStatus.Booked && c.DateTimeSince > DateTime.Now)
                .Include(d => d.MedicalWorker).ThenInclude(f => f.Person)
                .Include(e => e.Patient).ThenInclude(f => f.Person)
                .Include(g => g.Location)
                .Include(h => h.VisitCategory)
                .Include(k => k.PrimaryService)
                .Include(l => l.MinorMedicalServices)
                .AsQueryable();
            return bookedVisits;
        }

        public Patient GetCurrentPatient()
        {
            throw new NotImplementedException();
        }

        public byte[] GetDocument(string documentPath, string webRootPath)
        {
            string fullFilePath = Path.Combine(webRootPath + documentPath);

            byte[] file = File.ReadAllBytes(fullFilePath);

            return file;

        }

        public List<Visit> GetFutureVisits()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Visit> GetFutureVisitsByMedicalWorkerId(long id)
        {
            //List<Visit> visits=    Visits.Where(c => c.PatientId == id).Where(d => d.DateTimeSince > DateTimeOffset.Now).ToList();
            IQueryable<Visit> visits = Visits
                .Where(c => c.MedicalWorkerId == id)
                .Where(d => d.DateTimeSince.Date >= DateTimeOffset.Now.Date)// && (d.VisitStatus == VisitStatus.Booked || d.VisitStatus == VisitStatus.AvailableNotBooked))
                .Include(a => a.VisitCategory)
                .Include(b => b.Location)
                .Include(c => c.PrimaryService)
                .Include(d => d.Patient)
                    .ThenInclude(e => e.Person)
                .AsQueryable();
            return visits;
        }

        public List<Visit> GetFutureVisitsChunk(int currentPageNumId, int itemsPerPage)
        {
            //IEnumerable<Visit> visits = Visits.Where(c => c.DateTimeSince > DateTime.Now).Include(d=>d.MedicalWorker).Include();

            int numberOfVisits = Visits.Where(c => c.DateTimeSince > DateTime.Now).Count();

            int currentPageVisitsNumber = numberOfVisits % itemsPerPage;

            if (currentPageNumId * itemsPerPage < numberOfVisits)
            {
                return Visits
                .Where(c => c.DateTimeSince > DateTime.Now)
                .Skip((currentPageNumId - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Include(d => d.MedicalWorker)
                .ThenInclude(h => h.Person)
                .Include(e => e.Patient)
                .ThenInclude(i => i.Person)
                .Include(f => f.VisitCategory)
                .Include(g => g.PrimaryService)
                .AsNoTracking()
                .ToList();
            }
            else
            {
                return Visits
                .Where(c => c.DateTimeSince > DateTime.Now)
                .Skip((currentPageNumId - 1) * itemsPerPage)
                .Take(currentPageVisitsNumber)
                .Include(d => d.MedicalWorker)
                .ThenInclude(h => h.Person)
                .Include(e => e.Patient)
                .ThenInclude(i => i.Person)
                .Include(f => f.VisitCategory)
                .Include(g => g.PrimaryService)
                .AsNoTracking()
                .ToList();
            }
        }

        public Visit GetHistoricalVisitById(long id)
        {
            Visit visit = Visits
                .Where(c => c.Id == id)
                .Include(a => a.VisitCategory)
                .Include(b => b.Location)
                .Include(c => c.PrimaryService)
                .Include(d => d.Patient).ThenInclude(e => e.Person)
                .Include(e => e.MedicalWorker).ThenInclude(f => f.Person)
                .Include(k => k.MedicalRoom)
                .Include(p => p.Prescription).ThenInclude(m => m.IssuedMedicines)
                .Include(t => t.MedicalTestResult)
                .Include(v => v.VisitReview)
                .Include(r => r.Recommendations)
                .Include(l => l.ExaminationReferrals).ThenInclude(m => m.PrimaryMedicalService).Include(n => n.MinorMedicalServices)
                .FirstOrDefault();
            return visit;
        }

        public IQueryable<Visit> GetHistoricalVisitsQuery()
        {
            IQueryable<Visit> visits = Visits
                .Where(d => d.VisitStatus == VisitStatus.Finished)
                .Include(a => a.VisitCategory)
                .Include(b => b.Location)
                .Include(c => c.PrimaryService)
                .Include(d => d.Patient)
                    .ThenInclude(e => e.Person)
                .AsQueryable();
            return visits;
        }

        public IQueryable<Visit> GetHistoricalVisitsByMedicalWorkerId(long id)
        {
            IQueryable<Visit> visits = Visits
                .Where(c => c.MedicalWorkerId == id)
                .Where(d => d.VisitStatus == VisitStatus.Finished)
                .Include(a => a.VisitCategory)
                .Include(b => b.Location)
                .Include(c => c.PrimaryService)
                .Include(d => d.Patient)
                    .ThenInclude(e => e.Person)
                .AsQueryable();
            return visits;
        }

        public IQueryable<Visit> GetHistoricalVisitsByPatientIdQuery(long id)
        {
            IQueryable<Visit> visits = Visits
                .Where(c => c.PatientId == id)
                .Where(d => d.VisitStatus == VisitStatus.Finished)
                
                //.Include(k => k.ExaminationReferrals)
                .Include(b => b.Location)
                .Include(g => g.MedicalWorker)
                    .ThenInclude(h => h.Person)
                //.Include(s => s.MinorMedicalServices)

                .Include(d => d.Patient)
                    .ThenInclude(e => e.Person)
                //.Include(p => p.Prescription)
                .Include(c => c.PrimaryService)
                //.Include(w => w.Recommendations)
                //.Include(m => m.VisitReview)
                .Include(a => a.VisitCategory)
                .AsQueryable();
            return visits;
        }
        //public IQueryable<Visit> GetHistoricalVisitsByPatientIdQuery(long id)
        //{
        //    IQueryable<Visit> visits = Visits
        //        .Where(c => c.PatientId == id)
        //        .Where(d => d.VisitStatus == VisitStatus.Finished)
        //        .Include(a => a.VisitCategory)
        //        .Include(b => b.Location)
        //        .Include(c => c.PrimaryService)
        //        .Include(d => d.Patient).ThenInclude(e => e.Person)
        //        .AsQueryable();
        //    return visits;
        //}

        public Location GetLocationById(long id)
        {
            Location location = Locations
                .Where(c => c.Id == id)
                .Include(d => d.Services)
                .Include(e => e.MedicalRooms)
                .FirstOrDefault();
            return location;
        }

        public List<Location> GetLocations()
        {
            return Locations
                .Include(a => a.Services)
                .Include(b => b.MedicalRooms)
                .ToList();
        }

        //List<Location> IMedicalWorkerModuleRepository.GetLocations()
        //{
        //    return Locations.Include(a => a.Services).Include(b => b.MedicalRooms).ToList();
        //}

        List<Location> ICustomerServiceModuleRepository.GetLocations()
        {
            throw new NotImplementedException();
        }

        //public List<MedicalService> GetLocationServices(long id)
        //{
        //    //Location location = Locations.Where(c => c.Id == id).FirstOrDefault();
        //    List<MedicalService> services = new List<MedicalService>();
        //    //  List<location> list = MedicalServicesToLocations.Where(c => c.LocationId == id).ToList();

        //    //if (list!=null)
        //    //{
        //    //    foreach (MedicalServiceToLocation item in list)
        //    //    {
        //    //        MedicalService service = GetMedicalServiceById(item.MedicalServiceId);
        //    //        if (service!=null)
        //    //        {
        //    //            services.Add(service);
        //    //        }
        //    //    }
        //    //}
        //    return services;
        //}

        //public MedicalPackage GetMedicalPackageById(long id)
        //{
        //    MedicalPackage medicalPackage = MedicalPackages.Where(c => c.Id == id).FirstOrDefault();
        //    return medicalPackage;
        //}

        public MedicalPackage GetMedicalPackageById(long medicalPackageId)
        {
            MedicalPackage medicalPackage = MedicalPackages
                .Where(c => c.Id == medicalPackageId)
                .Include(d => d.ServiceDiscounts)
                .ThenInclude(e => e.MedicalService)
                .FirstOrDefault();
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
            MedicalWorker medicalWorker = MedicalWorkers
                .Where(c => c.Id == id)
                .Include(c => c.MedicalServices)
                .Include(d => d.Person)
                .FirstOrDefault();
            return medicalWorker;
        }
        public MedicalWorker GetMedicalWorkerDetailsById(long id)
        {
            MedicalWorker medicalWorker = MedicalWorkers
                .Where(c => c.Id == id)
                .Include(c => c.MedicalServices)
                .Include(d => d.Person)
                .Include(e => e.VisitReviews)
                    .ThenInclude(r => r.Reviewer)
                        .ThenInclude(p => p.Person)
                .Include(g => g.MedicalServices)
                .Include(k => k.User)
                .FirstOrDefault();
            return medicalWorker;
        }
        //public MedicalWorker GetMedicalWorkerDetailsById(long id)
        //{
        //    throw new NotImplementedException();
        //}

        public MedicalWorker GetMedicalWorkerByPersonId(long personId)
        {
            MedicalWorker medicalWorker = MedicalWorkers.Where(c => c.PersonId.Value == personId).FirstOrDefault();
            return medicalWorker;
        }
        public MedicalWorker GetMedicalWorkerByUserId(long id)
        {
            MedicalWorker medicalWorker = MedicalWorkers
                .Where(c => c.UserId.Value == id)
                .Include(p => p.Person)
                .FirstOrDefault();
            return medicalWorker;
        }

        public List<MedicalWorker> GetMedicalWorkers()
        {
            return MedicalWorkers.Include(c => c.Person).Include(d => d.MedicalServices).Include(e => e.VisitReviews).ToList();
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
            return NFZUnits.Find(id);
        }

        public List<NFZUnit> GetNFZUnits()
        {
            return NFZUnits.ToList();
            //throw new NotImplementedException();
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
            return Notifications.Find(id);
        }

        public List<Notification> GetNotificationsByPatientId(long id)
        {
            return Notifications.Where(c => c.PatientId == id).ToList();
        }

        public Patient GetPatientById(long id)
        {
            return Patients
                .Where(c => c.Id == id)
                .Include(c => c.MedicalPackage)
                .Include(d => d.NFZUnit)
                .Include(a => a.Person)
                .Include(b => b.User)
                //.Include(e => e.MedicalReferrals)
                //.Include(p => p.Prescriptions)
                //.Include(g => g.TestsResults)
                //.Include(h=>h.)
                .FirstOrDefault();
        }

        public Patient GetPatientByUserId(long userId)
        {
            Patient patient = Patients
                .Where(c => c.UserId == userId)
                .Include(d => d.Person)
                .Include(c => c.MedicalPackage)
                .Include(d => d.NFZUnit)
                .FirstOrDefault();
            return patient;
        }

        public Patient GetPatientData()
        {
            throw new NotImplementedException();
        }

        public Person GetPersonById(long personId)
        {
            return People.Where(c => c.Id == personId).FirstOrDefault();
        }

        public Prescription GetPrescriptionById(long id)
        {
            Prescription prescription = Prescriptions.Find(id);
            return prescription;
        }

        public List<Prescription> GetPrescriptions()
        {
            List<Prescription> prescriptions = Prescriptions.ToList();
            return prescriptions;
        }

        public List<VisitReview> GetReviewsByMedicalWorkerId(long id)
        {
            List<VisitReview> reviews = VisitReviews.Where(c => c.RevieweeId == id).Include(c => c.Reviewee).Include(d => d.Reviewer).ToList();
            return reviews;
        }

        public MedicalRoom GetRoomById(long id)
        {
            MedicalRoom room = MedicalRooms.Find(id);
            return room;
        }

        public List<MedicalRoom> GetRoomsByLocationId(long id)
        {
            List<MedicalRoom> rooms = MedicalRooms.Where(c => c.LocationId == id).ToList();
            return rooms;
        }

        //public List<MedicalRoom> GetUnasignedRooms()
        //{
        //    throw new NotImplementedException();
        //}

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
            Visit visit = Visits
                        .Where(c => c.Id == id)
                        .Include(d => d.MedicalWorker)
                            .ThenInclude(f => f.Person)
                        .Include(e => e.Patient)
                            .ThenInclude(g => g.Person)
                        .Include(k => k.Location)
                        .Include(mr => mr.MedicalRoom)
                        .Include(e => e.MinorMedicalServices)
                        .Include(f => f.MinorServicesToVisits)
                        .Include(p => p.PrimaryService)
                        .Include(r => r.Recommendations)
                        .Include(p => p.Prescription)
                            .ThenInclude(m => m.IssuedMedicines)
                        .Include(er => er.ExaminationReferrals)
                            .ThenInclude(ps => ps.PrimaryMedicalService)
                        .Include(t => t.MedicalTestResult)
                        .Include(vc => vc.VisitCategory)
                        .FirstOrDefault();
            return visit;
        }
        public Visit GetVisitByIdANT(long id)
        {
            Visit visit = Visits
                        .Where(c => c.Id == id)
                        .Include(d => d.MedicalWorker)
                            .ThenInclude(f => f.Person)
                        .Include(e => e.Patient)
                            .ThenInclude(g => g.Person)
                        .Include(k => k.Location)
                        .Include(mr => mr.MedicalRoom)
                        .Include(e => e.MinorMedicalServices)
                        .Include(f => f.MinorServicesToVisits)
                        .Include(p => p.PrimaryService)
                        .Include(r => r.Recommendations)
                        .Include(p => p.Prescription)
                            .ThenInclude(m => m.IssuedMedicines)
                        .Include(er => er.ExaminationReferrals)
                            .ThenInclude(ps => ps.PrimaryMedicalService)
                        .Include(t => t.MedicalTestResult)
                        .Include(vc => vc.VisitCategory)
                        .AsNoTrackingWithIdentityResolution()
                        .FirstOrDefault()
                        ;
            return visit;
        }

        public List<VisitCategory> GetVisitCategories()
        {
            return VisitCategories.ToList();
        }



        public VisitCategory GetVisitCategoryById(long visitCategoryId)
        {
            VisitCategory visitCategory = VisitCategories.Where(c => c.Id == visitCategoryId).FirstOrDefault();
            return visitCategory;
        }

        public IQueryable<Visit> GetVisitsByMedicalWorkerId(long id)
        {
            IQueryable<Visit> visits = Visits
                .Where(c => c.MedicalWorkerId == id)
                .Include(a => a.VisitCategory)
                .Include(b => b.Location)
                .Include(c => c.PrimaryService)
                .Include(d => d.Patient)
                    .ThenInclude(e => e.Person)
                .Include(k => k.Location)
                .AsQueryable();
            return visits;
        }

        public User CheckUserNameAndRole(string userName, WorkerModuleType workerModuleType, UserType userType)
        {
            System.Linq.IQueryable<User> users;//= Users;
            if (userType == UserType.Patient)
            {
                users = Users.Where(c => c.UserType == userType).AsQueryable();
            }
            else
            {
                users = Users.Where(c => c.UserType == userType)?.Where(d => d.WorkerModuleType == workerModuleType).AsQueryable();
            }
            //string emailAddressUpper = user.Email.ToUpper();

            //User user1 = users.Where(c => c.Email.ToUpper() == emailAddressUpper).FirstOrDefault();
            User user1 = users.Where(c => c.NormalizedUserName == userName.ToUpper()).FirstOrDefault();
            return user1;
        }

        public void RemoveIssuedMedicineById(long medicineIdToRemove)
        {
            IssuedMedicine medicine = IssuedMedicines.Find(medicineIdToRemove);
            IssuedMedicines.Remove(medicine);
            SaveChanges();
        }

        public void RemoveLocationById(long selectedLocationId)
        {
            Location location = Locations.Find(selectedLocationId);
            Locations.Remove(location);
            SaveChanges();
        }

        public void RemoveMedicalPackageById(long selectedPackageId)
        {
            MedicalPackage package = MedicalPackages.Find(selectedPackageId);
            MedicalPackages.Remove(package);
            SaveChanges();
        }

        public void RemoveMedicalReferralById(long medicalReferralIdToRemove)
        {
            MedicalReferral referral = MedicalReferrals.Find(medicalReferralIdToRemove);
            MedicalReferrals.Remove(referral);
            SaveChanges();
        }

        public void RemoveMedicalRoomById(long selectedRoomId)
        {
            MedicalRoom room = MedicalRooms.Find(selectedRoomId);
            MedicalRooms.Remove(room);
            SaveChanges();
        }

        public void RemoveMedicalWorkerById(long selectedWorkerId)
        {
            MedicalWorker worker = MedicalWorkers.Find(selectedWorkerId);
            MedicalWorkers.Remove(worker);
            SaveChanges();
        }

        public void RemovePatientById(long id)
        {
            Patient patient = Patients.Find(id);
            Patients.Remove(patient);
            SaveChanges();
        }

        public void RemovePrescriptionById(long prescriptionIdToRemove)
        {
            Prescription prescription = Prescriptions.Find(prescriptionIdToRemove);
            Prescriptions.Remove(prescription);
            SaveChanges();
        }

        public void RemoveTestResult(long testResultId, long visitId, string webRootPath)
        {
            Visit visit = PatientMockDB.FutureVisits.Where(c => c.Id == visitId).First();
            if (visit != null)
            {

                //if (visit.MedicalTestResult != null)
                //{
                //    if (visit.MedicalTestResult.Id == testResultId)
                //    {
                //        PatientMockDB.RemoveFile(visit.MedicalTestResult.DocumentPath);
                //        int index = PatientMockDB.MedicalTestResults.FindIndex(c => c.Id == testResultId);
                //        PatientMockDB.MedicalTestResults.RemoveAt(index);
                //        string fullPath = webRootPath + visit.MedicalTestResult.DocumentPath;// Path.Combine(webRootPath, documentPath);
                //        if (File.Exists(fullPath))
                //        {
                //            File.Delete(fullPath);
                //        }
                //        else
                //        {

                //        }
                //        visit.MedicalTestResult = null;

                //    }
                //}
            }


            //MedicalPackage package = MedicalPackages.Find(selectedPackageId);
            //MedicalPackages.Remove(package);
            //SaveChanges();
        }

        public void RemoveVisitById(long id)
        {
            Visit visit = Visits.Find(id);
            Visits.Remove(visit);
            SaveChanges();
        }

        public void ResignFromVisit(long id)
        {
            Visit visit = Visits.Find(id);
            visit.PatientId = null;
            visit.Patient = null;
            visit.VisitStatus = VisitStatus.AvailableNotBooked;
            if (visit.UsedExaminationReferralId!=null)
            {
                MedicalReferral medicalReferral = MedicalReferrals.FirstOrDefault(c => c.Id == visit.UsedExaminationReferralId.Value);
                if (medicalReferral!=null)
                {
                    medicalReferral.HasBeenUsed = false;
                    medicalReferral.VisitWhenUsedId = null;
                    MedicalReferrals.Update(medicalReferral);
                }
            }
            Visits.Update(visit);

            SaveChanges();
        }
        public void DeactivateVisit(long id)
        {
            Visit visit = Visits.Find(id);
            //visit.PatientId = null;
            //visit.Patient = null;
            visit.VisitStatus = VisitStatus.Cancelled;

            if (visit.UsedExaminationReferralId != null)
            {
                MedicalReferral medicalReferral = MedicalReferrals.FirstOrDefault(c => c.Id == visit.UsedExaminationReferralId.Value);
                if (medicalReferral != null)
                {
                    medicalReferral.HasBeenUsed = false;
                    medicalReferral.VisitWhenUsedId = null;
                    MedicalReferrals.Update(medicalReferral);
                }
            }
            Visits.Update(visit);

            SaveChanges();
        }

        public void UpdateLocation(Location selectedLocation, string webrootPath)
        {
            if (selectedLocation.ImageFile != null)
            {
                string fullImagePath = Path.Combine(webrootPath + selectedLocation.ImagePath);
                if (File.Exists(fullImagePath))
                {
                    File.Delete(fullImagePath);
                }
                string newLocation = SaveFile(selectedLocation.ImageFile, StorageFolderType.Locations, webrootPath);
                selectedLocation.ImagePath = newLocation;
            }

            var existingOrder = Locations.Local.SingleOrDefault(o => o.Id == selectedLocation.Id);
            existingOrder.UpdateWithAnotherLocation(selectedLocation);
            //if (existingOrder != null)
            //{
            //    Entry(existingOrder).State = EntityState.Detached;
            //}
            DisplayStates(ChangeTracker.Entries());

            //Locations.Update(selectedLocation);
            SaveChanges();

            //string filePath = SaveFile(formFile, StorageFolderType.Locations, hostPath);
            //location.ImagePath = filePath;

            //Locations.Add(location);

            //SaveChanges();

        }
        private static void DisplayStates(IEnumerable<EntityEntry> entries)
        {
            foreach (var entry in entries)
            {
                Console.WriteLine($"Entity: {entry.Entity.GetType().Name},State: { entry.State.ToString()}");
            }
        }

        //public void UpdateLocationImage(IFormFile imageFile, Location location, string webRootPath)
        //{
        //    string path = SaveFile(imageFile, StorageFolderType.Locations, webRootPath);              
        //    Path.Combine(webRootPath, webRootPath);
        //}

        public void UpdateMedicalPackage(MedicalPackage newPackage)
        {
            var existingOrder = MedicalPackages.Local.SingleOrDefault(o => o.Id == newPackage.Id);
            newPackage.ServiceDiscounts.ForEach(c => c.Discount = decimal.Multiply(c.Discount, (decimal)0.01));//  (decimal)(((double)c.Discount) * 0.01));

            if (existingOrder != null)
            {
                Entry(existingOrder).State = EntityState.Detached;
            }


            //Attach(newPackage);
            //Entry(newPackage).State = EntityState.Modified;

            MedicalPackages.Update(newPackage);
            SaveChanges();
        }

        public void UpdateMedicalWorker(MedicalWorker selectedWorker, string webrootPath)
        {
            if (selectedWorker.Person.ImageFile != null)
            {
                string fullImagePath = Path.Combine(webrootPath + selectedWorker.Person.ImageFilePath);
                if (File.Exists(fullImagePath))
                {
                    File.Delete(fullImagePath);
                }
                string newLocation = SaveFile(selectedWorker.Person.ImageFile, StorageFolderType.Persons, webrootPath);
                selectedWorker.Person.ImageFilePath = newLocation;
            }
            //MedicalWorker existingMW = MedicalWorkers.Local.SingleOrDefault(o => o.Id == selectedWorker.Id);

            //if (existingMW != null)
            //{
            //    Entry(existingMW).State = EntityState.Detached;
            //}

            MedicalWorkers.Update(selectedWorker);
            SaveChanges();
        }

        public void UpdatePatient(Patient patient, string webrootPath)
        {
            if (patient.Person.ImageFile != null)
            {
                //string fullImagePath = Path.Combine( patient.Person.ImageFilePath);
                if (File.Exists(patient.Person.ImageFilePath))
                {
                    File.Delete(patient.Person.ImageFilePath);
                }
                string newLocation = SaveFile(patient.Person.ImageFile, StorageFolderType.Persons, webrootPath);
                patient.Person.ImageFilePath = newLocation;
            }

            Patient existingPatient = Patients.Local.SingleOrDefault(o => o.Id == patient.Id);

            if (existingPatient != null)
            {
                Entry(existingPatient).State = EntityState.Detached;
            }
            Person existingPerson = People.Local.SingleOrDefault(o => o.Id == patient.PersonId.Value);

            if (existingPerson != null)
            {
                Entry(existingPerson).State = EntityState.Detached;
            }
            User existingUser = Users.Local.SingleOrDefault(o => o.Id == patient.UserId);

            if (existingUser != null)
            {
                Entry(existingUser).State = EntityState.Detached;
            }

            Patients.Update(patient);
            SaveChanges();
        }

        public void UpdatePersonImage(IFormFile imageFile, Person person, string hostEnvironmentPath)
        {
            throw new NotImplementedException();
        }

        public void UpdatePrescription(Prescription prescription)
        {
            Prescription existingPrescription = Prescriptions.Local.SingleOrDefault(o => o.Id == prescription.Id);

            if (existingPrescription != null)
            {
                Entry(existingPrescription).State = EntityState.Detached;
            }

            Prescriptions.Update(prescription);
            SaveChanges();
        }

        public void UpdateReferral(MedicalReferral referral)
        {
            MedicalReferral existingReferral = MedicalReferrals.Local.SingleOrDefault(o => o.Id == referral.Id);

            if (existingReferral != null)
            {
                Entry(existingReferral).State = EntityState.Detached;
            }

            MedicalReferrals.Update(referral);
            SaveChanges();
        }

        public void UpdateRoom(MedicalRoom newRoom)
        {
            var room = MedicalRooms.Local.SingleOrDefault(o => o.Id == newRoom.Id);

            if (room != null)
            {
                Entry(room).State = EntityState.Detached;
            }
            if (newRoom.LocationId == -1)
            {
                newRoom.LocationId = null;
            }
            MedicalRooms.Update(newRoom);
            SaveChanges();
        }

        public void UpdateTestResultFile(IFormFile medicalTestFile, Visit visit, string webRootPath)
        {
            throw new NotImplementedException();
        }

        public void UpdateVisit(Visit visitToUpdate)
        {
            Update(visitToUpdate);

            SaveChanges();
        }



        public bool HasMedicalWorkerVisits(long id)
        {
            return Visits.Any(x => x.MedicalWorkerId == id);
        }

        public bool HasPatientVisits(long id)
        {
            return Visits.Any(x => x.PatientId == id);
        }

        public IQueryable<Visit> GetFutureVisitsQuery()
        {
            return Visits
                .Where(a=>a.VisitStatus==VisitStatus.Booked || a.VisitStatus==VisitStatus.AvailableNotBooked)
                .Where(k => k.DateTimeSince > DateTimeOffset.Now)
                .Include(a => a.MedicalWorker).ThenInclude(b => b.Person)
                .Include(c => c.Patient).ThenInclude(d => d.Person)
                .AsQueryable<Visit>();
        }

        public IQueryable<Visit> GetFutureVisitsQueryPatient()
        {
            return Visits
                .Where(a => a.VisitStatus == VisitStatus.Booked || a.VisitStatus == VisitStatus.AvailableNotBooked)
                .Where(k => k.DateTimeSince > DateTimeOffset.Now)
                .Include(a => a.MedicalWorker).ThenInclude(b => b.Person)
                .Include(c => c.Patient).ThenInclude(d => d.Person)
                .Include(e=>e.MedicalRoom)
                .AsQueryable<Visit>();
        }

        public IQueryable<Visit> GetVisitsQuery()
        {
            return Visits
                .Include(a => a.MedicalWorker).ThenInclude(b => b.Person)
                .Include(c => c.Patient).ThenInclude(d => d.Person)
                .Include(e => e.Location)
                .AsQueryable<Visit>();
        }


        public IQueryable<Visit> GetAllVisitsByPatientIdQuery(long id)
        {
            return Visits
                .Where(c => c.PatientId == id)
                .Include(d => d.MedicalWorker)
                .ThenInclude(h => h.Person)
                .Include(e => e.Patient)
                .ThenInclude(i => i.Person)
                .Include(f => f.VisitCategory)
                .Include(g => g.PrimaryService)
                .Include(m => m.ExaminationReferrals)
                .Include(j => j.Location)
                .AsNoTracking()
                .AsQueryable();
        }
        //public List<MedicalReferral> GetMedicalReferralsByPatientIdQuery(long id)
        //{
        //    List<MedicalReferral> medicalReferrals = MedicalReferrals.Where(c => c.IssuedToId == id).ToList();
        //    return medicalReferrals;
        //}

        public IQueryable<MedicalReferral> GetMedicalReferralsByPatientIdQuery(long id)
        {
            return MedicalReferrals
                .Where(a => a.IssuedToId == id)
                .Include(b => b.IssuedTo).ThenInclude(c => c.Person)
                .Include(d => d.IssuedBy).ThenInclude(e => e.Person)
                .Include(m => m.MinorMedicalService)
                .Include(p => p.PrimaryMedicalService)
                .AsQueryable();
        }
        //List<MedicalReferral> GetMedicalReferralsByPatientIdQuery(long id)
        //{
        //    throw new NotImplementedException();
        //}

        public IQueryable<Prescription> GetPrescriptionsByPatientIdQuery(long id)
        {
            return Prescriptions
                .Where(a => a.IssuedToId == id)
                .Include(b => b.IssuedTo).ThenInclude(c => c.Person)
                .Include(d => d.IssuedBy).ThenInclude(e => e.Person)
                .Include(m => m.IssuedMedicines)
                .AsQueryable();
        }
        public IQueryable<MedicalTestResult> GetMedicalTestResultsByPatientIdQuery(long id)
        {
            return MedicalTestResults
                .Where(a => a.PatientId == id)
                .Include(b => b.Patient).ThenInclude(c => c.Person)
                .Include(d => d.MedicalWorker).ThenInclude(e => e.Person)
                .Include(m => m.MedicalService)
                .AsQueryable();
        }

        public MedicalTestResult GetMedicalTestResultById(long id)
        {
            return MedicalTestResults
                .Include(b => b.Patient)
                    .ThenInclude(c => c.Person)
                .Include(d => d.MedicalWorker)
                    .ThenInclude(e => e.Person)
                .Include(m => m.MedicalService)
                .FirstOrDefault(c => c.Id == id);

        }

        public void UpdateNotification(Notification notification)
        {
            Notifications.Update(notification);
            SaveChanges();
        }

        public IQueryable<Patient> GetAllPatients()
        {
            return Patients
                .Include(m => m.MedicalPackage)
                .Include(a => a.Person)
                .Include(n => n.NFZUnit)
                .AsQueryable();
        }

        public List<Prescription> GetPrescriptionsByPatientId(long id)
        {
            List<Prescription> prescriptions = Prescriptions
                .Where(c => c.IssuedToId == id)
                .Include(d => d.IssuedBy)
                    .ThenInclude(e => e.Person)
                .Include(f => f.IssuedMedicines)
                .ToList();
            return prescriptions;
        }
        //public List<Prescription> GetPrescriptionsByPatientId(long id)
        //{
        //    List<Prescription> prescriptions = Prescriptions
        //        .Where(c => c.IssuedToId == id)
        //        .Include(v => v.IssuedBy)
        //            .ThenInclude(b => b.Person)
        //        .ToList();
        //    return prescriptions;
        //}

        public List<MedicalTestResult> GetTestResultsByPatientId(long id)
        {
            var results = MedicalTestResults
                .Where(c => c.PatientId == id)
                .Include(d => d.MedicalWorker)
                    .ThenInclude(e => e.Person)
                .Include(f => f.MedicalService)
                .ToList();
            return results;
        }

        public void Save()
        {
            SaveChanges();
        }

        public void RemovePersonById(long personId)
        {
            Person person = People.FirstOrDefault(p => p.Id == personId);
            if (person != null)
            {
                People.Remove(person);
            }
        }

        public void RemoveUserById(long userId)
        {
            User user = Users.FirstOrDefault(p => p.Id == userId);
            if (user != null)
            {
                Users.Remove(user);
            }
        }

        public async Task<bool> AddIdenitytUserWithRole(User user, UserManager<User> userManager, RoleManager<IdentityRole<long>> roleManager)
        {
            IdentityResult identityResult = await userManager.CreateAsync(user, user.PasswordHash);
            if (!identityResult.Succeeded)
            {
                return false;
            }

            IdentityRoleTypes? roleType = GetRole(user);
            if (roleType == null)
            {
                throw new ArgumentNullException("Cannot define user role");
                return false;
            }

            //var role = await roleManager.FindByNameAsync(roleType.GetDescription());

            identityResult = await userManager.AddToRoleAsync(user, roleType.GetDescription());

            if (identityResult.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        private IdentityRoleTypes? GetRole(User user)
        {
            if (user.UserType == UserType.Patient)
            {
                return IdentityRoleTypes.Patient;
            }
            else
            {
                if (user.WorkerModuleType.Value == WorkerModuleType.MedicalWorkerModule)
                {
                    return IdentityRoleTypes.MedicalWorker;
                }
                else if (user.WorkerModuleType.Value == WorkerModuleType.AdministrativeWorkerModule)
                {
                    return IdentityRoleTypes.AdministrativeWorker;
                }
                else if (user.WorkerModuleType == WorkerModuleType.CustomerServiceModule)
                {
                    return IdentityRoleTypes.CustomerService;
                }
            }
            return null;
        }

        public List<MedicalReferral> GetMedicalReferralsByPatientId(long id)
        {
            List<MedicalReferral> medicalReferrals = MedicalReferrals
                .Where(c => c.IssuedToId == id)
                .Include(v=>v.VisitWhenIssued)
                    .ThenInclude(mw=>mw.MedicalWorker)
                        .ThenInclude(p=>p.Person)
                .Include(ms=>ms.PrimaryMedicalService)
                .Include(mms=>mms.MinorMedicalService)
                .ToList();
            return medicalReferrals;
        }

        public List<MedicalTestResult> GetMedicalTestResultsByPatientId(long id)
        {
            List<MedicalTestResult> medicalTestResults = MedicalTestResults
                .Where(c => c.PatientId == id)
                .Include(d=>d.MedicalWorker)
                    .ThenInclude(e=>e.Person)
                .Include(m=>m.MedicalService)
                .ToList();
            return medicalTestResults;
        }


        public List<Visit> GetHistoricalVisitsByPatientId(long id)
        {
            List<Visit> medicalReferrals = Visits
                .Where(c => c.PatientId == id)
                .Include(l=>l.Location)
                .Include(mw=>mw.MedicalWorker)
                    .ThenInclude(p=>p.Person)
                .Include(vc=>vc.VisitCategory)
                .Include(pm=>pm.PrimaryService)
                .ToList();
            return medicalReferrals;
        }

        public void UpdateVisitById(long id)
        {
            throw new NotImplementedException();
        }

        //public void UpdateVisitById(long id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}