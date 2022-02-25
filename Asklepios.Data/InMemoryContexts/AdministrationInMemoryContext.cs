using Asklepios.Core.Models;
using Asklepios.Data.Interfaces;
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Asklepios.Core.Enums;

namespace Asklepios.Data.InMemoryContexts
{
    public class AdministrationInMemoryContext : IAdministrationModuleRepository
    {
        readonly IEnumerable<Visit> availableVisits;
        readonly IEnumerable<Location> locations;
        private readonly IEnumerable<MedicalWorker> medicalWorkers;
        private List<MedicalService> medicalServices { get; set; }
        private List<MedicalService> primaryMedicalServices { get; set; }
        private List<VisitCategory> visitCategories { get; set; }
        private List<MedicalPackage> medicalPackages { get; set; }
        private List<NFZUnit> nfzUnits { get; set; }
        private List<Patient> allPatients { get; set; }
        private List<MedicalRoom> medicalRooms { get; set; }

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

        private List<MedicalRoom> GetMedicalRooms()
        {
            return PatientMockDB.MedicalRooms;
        }

        public Patient GetCurrentPatient()
        {
            throw new NotImplementedException();
        }


        public Person GetPerson(long personId)
        {
            return PatientMockDB.Persons.Where(c => c.Id == personId).FirstOrDefault();
        }

        public User GetUser(int parsedId)
        {
            User user = PatientMockDB.GetUserById(parsedId);
            return user;
        }

        public List<MedicalService> GetMedicalServices()
        {
            return PatientMockDB.MedicalServices;
        }

        public List<MedicalPackage> GetMedicalPackages()
        {
            return PatientMockDB.MedicalPackages;
        }

        public List<Patient> GetAllPatients()
        {
            return PatientMockDB.AllPatients;
        }

        public List<VisitCategory> GetVisitCategories()
        {
            return PatientMockDB.VisitCategories;
        }
        public List<MedicalServiceDiscount> GetMedicalServiceDiscounts()
        {
            return PatientMockDB.MedicalServiceDiscounts;
        }

        public List<Location> GetAllLocations()
        {
            return PatientMockDB.Locations.ToList();
        }

        public List<MedicalWorker> GetMedicalWorkers()
        {
            return PatientMockDB.MedicalWorkers.ToList();
        }

        public List<Visit> GetAvailableVisits()
        {
            return PatientMockDB.AvailableVisits.ToList();
        }

        public MedicalPackage GetMedicalPackageById(long id)
        {
            return PatientMockDB.MedicalPackages.Where(c => c.Id == id).FirstOrDefault();
        }

        public MedicalWorker GetMedicalWorkerById(long id)
        {
            return PatientMockDB.GetMedicalWorkerById(id);
        }

        public Visit GetAvailableVisitById(long id)
        {
            return PatientMockDB.AvailableVisits.Where(c => c.Id == id).FirstOrDefault();
        }

        public Location GetLocationById(long id)
        {
            return PatientMockDB.Locations.Where(c => c.Id == id).FirstOrDefault();
        }

        public VisitCategory GetVisitCategoryById(long id)
        {
            return PatientMockDB.VisitCategories.Where(c => c.Id == id).FirstOrDefault();
        }

        public Patient GetPatientById(long id)
        {
            return PatientMockDB.AllPatients.Where(c => c.Id == id).FirstOrDefault();
        }

        public List<MedicalRoom> GetRoomsByLocationId()
        {
            throw new NotImplementedException();
        }

        public MedicalService GetMedicalServiceById(long id)
        {
            return PatientMockDB.GetMedicalServiceById(id);
        }

        public void AddVisitsToSchedule(List<Visit> visitsToAdd)
        {
            foreach (Visit item in visitsToAdd)
            {
                PatientMockDB.AvailableVisits.Append(item);
            }          
        }

        public void RemoveVisitById(long id)
        {
            PatientMockDB.RemoveVisitById(id);
        }

        public List<NFZUnit> GetNFZUnits()
        {
            return PatientMockDB.NfzUnits;
        }

        public NFZUnit GetNFZUnitById(long id)
        {
            return PatientMockDB.GetNFZUnitById(id);
        }

        public void AddPatientObjects(User user, Person person, Patient patient)
        {
            if (patient.NFZUnitId>0)
            {
                if (patient.NFZUnit==null)
                {
                    patient.NFZUnit = GetNFZUnitById(patient.NFZUnitId);
                }
            }
            if (patient.MedicalPackageId>0)
            {
                if (patient.MedicalPackage==null)
                {
                    patient.MedicalPackage = GetMedicalPackageById(patient.MedicalPackageId);
                }
            }
            
            PatientMockDB.AddUser(user);
            PatientMockDB.AddPatient(patient);
            PatientMockDB.AddPerson(person);
        }

        public void RemovePatientById(long id)
        {
            PatientMockDB.RemovePatientById(id);
        }

        public void UpdatePatient(Patient patient)
        {
            Patient oldPatient = allPatients.Where(c => c.Id == patient.Id).FirstOrDefault();
            if (patient.Person.ImageFile!=null)
            {
                
            }
            if (oldPatient!=null)
            {
                if (patient.MedicalPackage==null && patient.MedicalPackageId>0)
                {
                    MedicalPackage medicalPackage = GetMedicalPackageById(patient.MedicalPackageId);
                    if (medicalPackage!=null)
                    {
                        patient.MedicalPackage = medicalPackage;
                    }
                }
                if (patient.NFZUnit==null && patient.NFZUnitId>0)
                {
                    NFZUnit unit = GetNFZUnitById(patient.NFZUnitId);
                    if (unit!=null)
                    {
                        patient.NFZUnit = unit;
                    }
                }
                
                PatientMockDB.UpdatePatient(oldPatient, patient);
            }
        }

        public void AddMedicalWorkerObjects(User user, Person person, MedicalWorker medicalWorker)
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

            PatientMockDB.AddUser(user);
            PatientMockDB.AddMedicalWorker(medicalWorker);
            PatientMockDB.AddPerson(person);

        }

        public void RemoveMedicalWorkerById(long selectedWorkerId)
        {
            PatientMockDB.RemoveMedicalWorkerById(selectedWorkerId);
        }

        public void UpdateMedicalWorker(MedicalWorker selectedWorker, long selectedWorkerId)
        {
            MedicalWorker oldWorker = medicalWorkers.Where(c => c.Id == selectedWorkerId).FirstOrDefault();
            if (oldWorker != null)
            {
                PatientMockDB.UpdateMedicalWorker(selectedWorker, oldWorker);
            }
        }

        //public void UpdatePersonImage(IFormFile imageFile, Person person)
        //{
        //    string imagePath = SaveImage(person.ImageFile, ImageFolderType.Persons, _hostEnvironment.WebRootPath);
        //    person.ImageFilePath = imagePath;
        //}

        public void UpdatePersonImage(IFormFile imageFile, Person person, string hostEnvironmentPath)
        {
            string imagePath = SaveImage(person.ImageFile, ImageFolderType.Persons, hostEnvironmentPath);
            person.ImageFilePath = imagePath;
        }
        private string SaveImage(IFormFile formFile, ImageFolderType type, string basePath)
        {
            string path = null;
            switch (type)
            {
                // _hostEnvironment
                case ImageFolderType.Persons:
                    path = Path.Combine("img", "Persons"); //Directory.GetCurrentDirectory() + "\\Persons";
                    break;
                case ImageFolderType.Locations:
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

        public void UpdateLocationImage(IFormFile imageFile, Location location, string hostEnvironmentPath)
        {
            string imagePath = SaveImage(location.ImageFile, ImageFolderType.Locations, hostEnvironmentPath);
            location.ImagePath = imagePath;
            location.ImageFile = null;
        }

        public void AddLocation(Location location)
        {
            long id = PatientMockDB.Locations.Max(c => c.Id) + 1;
            location.Id = id;
            PatientMockDB.Locations.Add(location);
            //throw new NotImplementedException();
        }

        public void UpdateLocation(Location selectedLocation, long selectedLocationId)
        {
            Location oldLocation = locations.Where(c => c.Id == selectedLocationId).FirstOrDefault();
            if (oldLocation!= null)
            {
                PatientMockDB.UpdateLocation(selectedLocation, oldLocation);
            }

        }

        public List<MedicalRoom> GetUnasignedRooms()
        {
            List<MedicalRoom> rooms = GetMedicalRooms();
            List<long> usedIds = GetAllLocations().SelectMany(c => c.MedicalRooms.Select(d => d.Id).ToList()).ToList();
            List<long> allIds = rooms.Select(c => c.Id).ToList();
            List<long> unusedIds = allIds.Except(usedIds).ToList();
            
            return rooms.Where(c => unusedIds.Contains(c.Id))?.ToList();
        }

        public void RemoveLocationById(long selectedLocationId)
        {
            Location location = GetLocationById(selectedLocationId);
            if (location!=null)
            {
                PatientMockDB.Locations.Remove(location);
            }
            
        }

        public MedicalRoom GetRoomById(long id)
        {
            return PatientMockDB.MedicalRooms.First(c => c.Id == id);
        }

        public void AddMedicalRoom(MedicalRoom room)
        {
            long id = PatientMockDB.MedicalRooms.Max(c => c.Id) + 1;
            room.Id = id;
            if (room.LocationId>0 && room.Location==null)
            {
                room.Location = GetLocationById(room.LocationId);
            }

            PatientMockDB.MedicalRooms.Add(room);
        }

        public List<MedicalRoom> GetAllRooms()
        {
            return PatientMockDB.MedicalRooms;
        }

        //public void RemoveRoomById(long id)
        //{
        //    MedicalRoom room = PatientMockDB.Rooms.First(c=>c.Id==id);
        //    if (room!=null)
        //    {
        //        PatientMockDB.Rooms.Remove(room);
        //    }
        //}

        public void RemoveMedicalRoomById(long selectedRoomId)
        {
            MedicalRoom room = PatientMockDB.MedicalRooms.First(c => c.Id == selectedRoomId);
            if (room != null)
            {
                PatientMockDB.MedicalRooms.Remove(room);
            }
        }

        public void UpdateRoom(MedicalRoom newRoom)
        {
            MedicalRoom oldRoom = medicalRooms.Where(c => c.Id == newRoom.Id).FirstOrDefault();
            if (oldRoom != null)
            {
                PatientMockDB.UpdateRoom(newRoom, oldRoom);
            }
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

        public void RemoveMedicalPackageById(long selectedPackageId)
        {
            MedicalPackage pack = PatientMockDB.MedicalPackages.First(c => c.Id == selectedPackageId);
            if (pack != null)
            {
                PatientMockDB.MedicalPackages.Remove(pack);
            }
        }

        public void UpdateMedicalPackage(MedicalPackage newPackage)
        {
            MedicalPackage oldPackage = medicalPackages.Where(c => c.Id == newPackage.Id).FirstOrDefault();
            if (oldPackage != null)
            {
                PatientMockDB.UpdateMedicalPackage(newPackage, oldPackage);
            }
        }
    }
}
