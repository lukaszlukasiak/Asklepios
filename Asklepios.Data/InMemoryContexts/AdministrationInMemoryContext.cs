using Asklepios.Core.Models;
using Asklepios.Data.Interfaces;
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Asklepios.Core.Enums;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Asklepios.Data.InMemoryContexts
{
    public class AdministrationInMemoryContext : IAdministrationModuleRepository
    {
        readonly IEnumerable<Visit> availableVisits;
        readonly IEnumerable<Location> locations;
        private readonly IEnumerable<MedicalWorker> medicalWorkers;

        public AdministrationInMemoryContext()
        {
            if (!PatientMockDB.IsCreated)
            {
                PatientMockDB.SetData();
            }
            //nfzUnits = GetNFZUnits().ToList();
            medicalServices = GetMedicalServices().ToList();
            medicalPackages = GetMedicalPackages().ToList();
            allPatients = GetAllPatients().ToList();
            primaryMedicalServices = medicalServices.Where(c => c.IsPrimaryService == true).ToList();
            visitCategories = GetVisitCategories().ToList();
            medicalRooms = GetMedicalRooms().ToList();
            locations = GetAllLocations();
            medicalWorkers = GetMedicalWorkers();
            availableVisits = GetAvailableVisits().Where(c => c.Patient == null).ToList(); ;
        }

        private List<Patient> allPatients { get; set; }
        private List<MedicalPackage> medicalPackages { get; set; }
        private List<MedicalRoom> medicalRooms { get; set; }
        private List<MedicalService> medicalServices { get; set; }
        private List<NFZUnit> nfzUnits { get; set; }
        private List<MedicalService> primaryMedicalServices { get; set; }
        private List<VisitCategory> visitCategories { get; set; }

        public void AddLocation(Location location, IFormFile file, string path)
        {
            long id = PatientMockDB.Locations.Max(c => c.Id) + 1;
            location.Id = id;
            PatientMockDB.Locations.Add(location);
            //throw new NotImplementedException();
        }

        public void AddMedicalPackage(MedicalPackage newPackage)
        {
            long id = PatientMockDB.MedicalPackages.Max(c => c.Id) + 1;
            newPackage.Id = id;

            foreach (MedicalServiceDiscount item in newPackage.ServiceDiscounts)
            {
                item.MedicalPackageId = newPackage.Id;
                item.MedicalPackage = newPackage;
            }

            PatientMockDB.MedicalPackages.Add(newPackage);
            PatientMockDB.MedicalServiceDiscounts.AddRange(newPackage.ServiceDiscounts);
        }

        public void AddMedicalRoom(MedicalRoom room)
        {
            long id = PatientMockDB.MedicalRooms.Max(c => c.Id) + 1;
            room.Id = id;
            if (room.LocationId > 0 && room.Location == null)
            {
                room.Location = GetLocationById(room.LocationId.Value);
                //room.LocationId = room.Location.Id;
            }

            PatientMockDB.MedicalRooms.Add(room);
        }

        public void AddMedicalWorkerObjects( MedicalWorker medicalWorker, string webRootPath)
        {
            //if (medicalWorker.ser > 0)
            //{
            //    if (patient.NFZUnit == null)
            //    {
            //        patient.NFZUnit = GetNFZUnitById(patient.NFZUnitId);
            //    }
            //}
            //if (patient.MedicalPackageId > 0)
            //{
            //    if (patient.MedicalPackage == null)
            //    {
            //        patient.MedicalPackage = GetMedicalPackageById(patient.MedicalPackageId);
            //    }
            //}

            PatientMockDB.AddUser(medicalWorker.User);
            PatientMockDB.AddMedicalWorker(medicalWorker);
            PatientMockDB.AddPerson(medicalWorker.Person);

        }

        public void AddPatientObjects( Patient patient, string webRootPath)
        {
            if (patient.NFZUnitId > 0)
            {
                if (patient.NFZUnit == null)
                {
                    patient.NFZUnit = GetNFZUnitById(patient.NFZUnitId.Value);
                }
            }
            if (patient.MedicalPackageId > 0)
            {
                if (patient.MedicalPackage == null)
                {
                    patient.MedicalPackage = GetMedicalPackageById(patient.MedicalPackageId.Value);
                }
            }

            PatientMockDB.AddUser(patient.User);
            PatientMockDB.AddPatient(patient);
            PatientMockDB.AddPerson(patient.Person);
        }

        public void AddVisitsToSchedule(List<Visit> visitsToAdd)
        {
            foreach (Visit item in visitsToAdd)
            {
                PatientMockDB.AvailableVisits.Append(item);
            }
        }

        public Visit FutureVisitById(long id)
        {
            return PatientMockDB.FutureVisits.Where(c => c.Id == id).FirstOrDefault();
        }

        public List<Location> GetAllLocations()
        {
            return PatientMockDB.Locations.ToList();
        }

        public IQueryable<Patient> GetAllPatients()
        {
            return PatientMockDB.AllPatients.AsQueryable();
        }

        public List<MedicalRoom> GetAllRooms()
        {
            return PatientMockDB.MedicalRooms;
        }

        public Visit GetFutureVisitById(long id)
        {
            return PatientMockDB.AvailableVisits.Where(c => c.Id == id).FirstOrDefault();
        }

        public List<Visit> GetAvailableVisits()
        {
            return PatientMockDB.AvailableVisits.ToList();
        }

        public Patient GetCurrentPatient()
        {
            throw new NotImplementedException();
        }

        public List<Visit> GetFutureVisits()
        {
            return PatientMockDB.FutureVisits;
        }

        public List<Visit> GetFutureVisitsChunk(int currentPageNumId, int itemsPerPage)
        {
            return PatientMockDB.FutureVisits.ToList()
                .Skip((currentPageNumId - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToList();
        }

        public IQueryable<Visit> GetFutureVisitsQuery()
        {
            throw new NotImplementedException();
        }

        public Location GetLocationById(long id)
        {
            return PatientMockDB.Locations.Where(c => c.Id == id).FirstOrDefault();
        }

        public MedicalPackage GetMedicalPackageById(long id)
        {
            return PatientMockDB.MedicalPackages.Where(c => c.Id == id).FirstOrDefault();
        }

        public List<MedicalPackage> GetMedicalPackages()
        {
            return PatientMockDB.MedicalPackages;
        }

        public MedicalService GetMedicalServiceById(long id)
        {
            return PatientMockDB.GetMedicalServiceById(id);
        }

        public List<MedicalServiceDiscount> GetMedicalServiceDiscounts()
        {
            return PatientMockDB.MedicalServiceDiscounts;
        }

        public List<MedicalService> GetMedicalServices()
        {
            return PatientMockDB.MedicalServices;
        }

        public MedicalWorker GetMedicalWorkerById(long id)
        {
            return PatientMockDB.GetMedicalWorkerById(id);
        }

        public MedicalWorker GetMedicalWorkerDetailsById(long id)
        {
            throw new NotImplementedException();
        }

        public List<MedicalWorker> GetMedicalWorkers()
        {
            return PatientMockDB.MedicalWorkers.ToList();
        }

        public NFZUnit GetNFZUnitById(long id)
        {
            return PatientMockDB.GetNFZUnitById(id);
        }

        public List<NFZUnit> GetNFZUnits()
        {
            return PatientMockDB.NfzUnits;
        }

        public Patient GetPatientById(long id)
        {
            return PatientMockDB.AllPatients.Where(c => c.Id == id).FirstOrDefault();
        }

        public Person GetPersonById(long personId)
        {
            return PatientMockDB.Persons.Where(c => c.Id == personId).FirstOrDefault();
        }

        public MedicalRoom GetRoomById(long id)
        {
            return PatientMockDB.MedicalRooms.First(c => c.Id == id);
        }

        public List<MedicalRoom> GetRoomsByLocationId(long id)
        {
            return medicalRooms.FindAll(c => c.LocationId == id);
        }

        //public List<MedicalRoom> GetUnasignedRooms()
        //{
        //    List<MedicalRoom> rooms = GetMedicalRooms();
        //    List<long> usedIds = GetAllLocations().SelectMany(c => c.MedicalRooms.Select(d => d.Id).ToList()).ToList();
        //    List<long> allIds = rooms.Select(c => c.Id).ToList();
        //    List<long> unusedIds = allIds.Except(usedIds).ToList();

        //    return rooms.Where(c => unusedIds.Contains(c.Id))?.ToList();
        //}

        public User GetUser(int parsedId)
        {
            User user = PatientMockDB.GetUserById(parsedId);
            return user;
        }

        public User GetUserById(int parsedId)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(long parsedId)
        {
            throw new NotImplementedException();
        }

        public List<VisitCategory> GetVisitCategories()
        {
            return PatientMockDB.VisitCategories;
        }

        public VisitCategory GetVisitCategoryById(long id)
        {
            return PatientMockDB.VisitCategories.Where(c => c.Id == id).FirstOrDefault();
        }

        public bool HasMedicalWorkerVisits(long id)
        {
            throw new NotImplementedException();
        }

        public bool HasPatientVisits(long id)
        {
            throw new NotImplementedException();
        }

        public void RemoveLocationById(long selectedLocationId)
        {
            Location location = GetLocationById(selectedLocationId);
            if (location != null)
            {
                PatientMockDB.Locations.Remove(location);
            }

        }

        public void RemoveMedicalPackageById(long selectedPackageId)
        {
            MedicalPackage pack = PatientMockDB.MedicalPackages.First(c => c.Id == selectedPackageId);
            if (pack != null)
            {
                PatientMockDB.MedicalPackages.Remove(pack);
            }
        }

        public void RemoveMedicalRoomById(long selectedRoomId)
        {
            MedicalRoom room = PatientMockDB.MedicalRooms.First(c => c.Id == selectedRoomId);
            if (room != null)
            {
                PatientMockDB.MedicalRooms.Remove(room);
            }
        }

        public void RemoveMedicalWorkerById(long selectedWorkerId)
        {
            PatientMockDB.RemoveMedicalWorkerById(selectedWorkerId);
        }

        public void RemovePatientById(long id)
        {
            PatientMockDB.RemovePatientById(id);
        }

        public void RemoveVisitById(long id)
        {
            PatientMockDB.RemoveVisitById(id);
        }

        public void UpdateLocation(Location selectedLocation,  string path)
        {
            Location oldLocation = locations.Where(c => c.Id == selectedLocation.Id).FirstOrDefault();
            if (oldLocation != null)
            {
                PatientMockDB.UpdateLocation(selectedLocation, oldLocation);
            }

        }

        public void UpdateLocationImage(IFormFile imageFile, Location location, string hostEnvironmentPath)
        {
            string imagePath = SaveImage(location.ImageFile, StorageFolderType.Locations, hostEnvironmentPath);
            location.ImagePath = imagePath;
            location.ImageFile = null;
        }

        public void UpdateMedicalPackage(MedicalPackage newPackage)
        {
            MedicalPackage oldPackage = medicalPackages.Where(c => c.Id == newPackage.Id).FirstOrDefault();
            if (oldPackage != null)
            {
                PatientMockDB.UpdateMedicalPackage(newPackage, oldPackage);
            }
        }

        public void UpdateMedicalWorker(MedicalWorker selectedWorker,string webRoothPath)
        {
            MedicalWorker oldWorker = medicalWorkers.Where(c => c.Id == selectedWorker.Id).FirstOrDefault();
            if (oldWorker != null)
            {
                PatientMockDB.UpdateMedicalWorker(selectedWorker, oldWorker);
            }
        }

        public void UpdatePatient(Patient patient, string webRoothPath)
        {
            Patient oldPatient = allPatients.Where(c => c.Id == patient.Id).FirstOrDefault();
            if (patient.Person.ImageFile != null)
            {

            }
            if (oldPatient != null)
            {
                if (patient.MedicalPackage == null && patient.MedicalPackageId > 0)
                {
                    MedicalPackage medicalPackage = GetMedicalPackageById(patient.MedicalPackageId.Value);
                    if (medicalPackage != null)
                    {
                        patient.MedicalPackage = medicalPackage;
                    }
                }
                if (patient.NFZUnit == null && patient.NFZUnitId > 0)
                {
                    NFZUnit unit = GetNFZUnitById(patient.NFZUnitId.Value);
                    if (unit != null)
                    {
                        patient.NFZUnit = unit;
                    }
                }

                PatientMockDB.UpdatePatient(oldPatient, patient);
            }
        }

        public void UpdatePersonImage(IFormFile imageFile, Person person, string hostEnvironmentPath)
        {
            string imagePath = SaveImage(person.ImageFile, StorageFolderType.Persons, hostEnvironmentPath);
            person.ImageFilePath = imagePath;
        }

        //public void RemoveRoomById(long id)
        //{
        //    MedicalRoom room = PatientMockDB.Rooms.First(c=>c.Id==id);
        //    if (room!=null)
        //    {
        //        PatientMockDB.Rooms.Remove(room);
        //    }
        //}
        public void UpdateRoom(MedicalRoom newRoom)
        {
            MedicalRoom oldRoom = medicalRooms.Where(c => c.Id == newRoom.Id).FirstOrDefault();
            if (oldRoom != null)
            {
                PatientMockDB.UpdateRoom(newRoom, oldRoom);
            }
        }

        IQueryable<Visit> IAdministrationModuleRepository.GetAvailableVisitsQuery()
        {
            throw new NotImplementedException();
        }

        private List<MedicalRoom> GetMedicalRooms()
        {
            return PatientMockDB.MedicalRooms;
        }
        //public void UpdatePersonImage(IFormFile imageFile, Person person)
        //{
        //    string imagePath = SaveImage(person.ImageFile, ImageFolderType.Persons, _hostEnvironment.WebRootPath);
        //    person.ImageFilePath = imagePath;
        //}
        private string SaveImage(IFormFile formFile, StorageFolderType type, string basePath)
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
            using (var fileStream = new FileStream(fullFileName, FileMode.Create))
            {
                formFile.CopyTo(fileStream);
            }
            string serverFileName = Path.Combine("\\", path, myUniqueFileName);

            return serverFileName;

        }

        public void RemovePersonById(long personId)
        {
            throw new NotImplementedException();
        }

        public void RemoveUserById(long userId)
        {
            throw new NotImplementedException();
        }

        public void AddPerson(Person person, string webRootPath)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddIdenitytUserWithRole(User user, UserManager<User> userManager, RoleManager<IdentityRole<long>> roleManager)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Visit> GetVisitsQuery()
        {
            throw new NotImplementedException();
        }
    }
}
