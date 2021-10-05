using Asklepios.Data.Interfaces;
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
        readonly IEnumerable<Visit> visits;
        readonly IEnumerable<Location> locations;
        readonly Patient patient ;
        private readonly IEnumerable<MedicalWorker> medicalWorkers;

        public PatientInMemoryContext()
        {
            visits = GetAvailableVisits();
            locations = GetLocations();
            patient = GetPatientData();
            medicalWorkers = GetMedicalWorkers();
        }

        public IEnumerable<Visit> GetAvailableVisits()
        {
            DateTime today = DateTime.Today;
            return new List<Visit>()
            { new Visit() {Id=1,DateTimeSince=today+new TimeSpan(9,0,0),DateTimeTill=today+new TimeSpan(9,15,0),Location=locations.ElementAt(0)}};

        }

        public IEnumerable<Visit> GetHistoricalVisits()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Location> GetLocations()
        {
            return   new List<Location>()
            {
                new Location() {Id=1,Name="Warszawa Okopowa"        ,City="Warszawa",VoivodeshipType=Core.Enums.VoivodeshipType.mazowieckie},
                new Location() {Id=2,Name="Warszawa Jerozolimskie",City="Warszawa",VoivodeshipType=Core.Enums.VoivodeshipType.mazowieckie},
                new Location() {Id=3,Name="Warszawa Kabaty",City="Warszawa",VoivodeshipType=Core.Enums.VoivodeshipType.mazowieckie},
                new Location(){Id=4,Name="Ząbki Powstańców", City="Ząbki",VoivodeshipType=Core.Enums.VoivodeshipType.mazowieckie},
                new Location() {Id=5,Name="Poznań Stary Rynek", City="Poznań", VoivodeshipType=Core.Enums.VoivodeshipType.wielkopolskie},
                new Location() {Id=6,Name="Gniezno Rynek", City="Gniezno",VoivodeshipType=Core.Enums.VoivodeshipType.wielkopolskie},
                new Location() {Id=7,Name="Gdańsk Oliwa",City="Gdańsk",VoivodeshipType=Core.Enums.VoivodeshipType.pomorskie},
                new Location() {Id=8,Name="Gdynia Śródmieście",City="Gdynia", VoivodeshipType=Core.Enums.VoivodeshipType.pomorskie},
                new Location() {Id=9,Name="Sopot Molo", City="Sopot",VoivodeshipType=Core.Enums.VoivodeshipType.pomorskie}
            };
        }

        public IEnumerable<MedicalPackage> GetMedicalPackages()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MedicalService> GetMedicalServices()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MedicalWorker> GetMedicalWorkers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NFZUnit> GetNFZUnits()
        {
            throw new NotImplementedException();
        }

        public Patient GetPatientData()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VisitCategory> GetVisitCategories()
        {
            throw new NotImplementedException();
        }
    }
}
