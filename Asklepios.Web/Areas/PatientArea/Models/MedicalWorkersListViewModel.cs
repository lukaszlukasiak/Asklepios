using Asklepios.Core.Enums;
using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.PatientArea.Models
{
    public class MedicalWorkersListViewModel : IBaseViewModel
    {
        public IQueryable<MedicalWorker> MedicalWorkers { get; set; }
        public List<MedicalWorker> FilteredMedicalWorkers { get; set; }

        public string UserName { get; set; }
        public List<Notification> Notifications { get; set; }
        [DisplayName("Imię")]
        public string SelectedName { get; set; }
        [DisplayName("Nazwisko")]
        public string SelectedSurname { get; set; }
        public Aglomeration? SelectedAglomeration { get; set; }
        public string SelectedWorkerType { get; set; }
        public string SelectedServiceId { get; set; }


        public bool HasAnyFilters
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(SelectedServiceId))
                {
                    return true;
                }
                if (!string.IsNullOrWhiteSpace(SelectedWorkerType))
                {
                    return true;
                }
                if (!string.IsNullOrWhiteSpace(SelectedSurname))
                {
                    return true;
                }
                if (!string.IsNullOrWhiteSpace(SelectedName))
                {
                    return true;
                }
                if (SelectedAglomeration.HasValue)
                {
                    return true;
                }
                return false;
            }
        }



        public List<Aglomeration> Aglomerations
        {
            get
            {
                if (MedicalWorkers == null)
                {
                    return new List<Aglomeration>();
                }

                List< Aglomeration > aglomerations=FilteredMedicalWorkers?.Where(c => c.Person.DefaultAglomeration.HasValue == true).Select(c => c.Person.DefaultAglomeration.Value).Distinct().ToList();//     .Distinct().ToList();
                if (aglomerations==null)
                {
                    //return new  List<Aglomeration>();
                    return MedicalWorkers?.Where(c => c.Person.DefaultAglomeration.HasValue == true).Select(c => c.Person.DefaultAglomeration.Value).Distinct().ToList();
                }
                else
                {
                    return aglomerations;
                }
            }
        }
        public Dictionary<int, string> MedicalWorkerTypes
        {
            get
            {
                return new Dictionary<int, string>()
                {
                    {1,"Lekarz" },
                    {2, "Pielęgniarka"},
                    { 3,"Fizjoterapeuta"},
                    {4,"Technik elektroradiolog" },
                    {5, "Higienistka dentystyczna" }
                };
            }
        }
        public List<MedicalService> MedicalServices
        {
            get
            {
                if (MedicalWorkers==null)
                {
                    return new List<MedicalService>();
                }
                List<MedicalService> medicalServices= FilteredMedicalWorkers?.SelectMany(c => c.MedicalServices).Where(d => d.IsPrimaryService == true).Distinct().ToList();

                if (medicalServices == null)
                {
                    //return new List<MedicalService>();
                    return  MedicalWorkers.SelectMany(c => c.MedicalServices).Where(d => d.IsPrimaryService == true).Distinct().ToList();

                }
                else
                {
                    return medicalServices;
                }

            }
        }

        public void FilterWorkerItems()
        {
            if (HasAnyFilters)
            {
                //FilteredMedicalWorkers = MedicalWorkers;

                if (!string.IsNullOrEmpty(SelectedName))
                {
                    MedicalWorkers = MedicalWorkers.Where(c => c.Person.Name.Contains(SelectedName)).AsQueryable();
                }
                if (!string.IsNullOrWhiteSpace(SelectedSurname))
                {
                    MedicalWorkers = MedicalWorkers.Where(c => c.Person.Surname.Contains(SelectedSurname)).AsQueryable();
                }
                if (!string.IsNullOrWhiteSpace(SelectedServiceId))
                {
                    if (int.TryParse(SelectedServiceId, out var serviceId))
                    {
                        if (serviceId>0)
                        {
                            MedicalWorkers = MedicalWorkers.Where(c => c.MedicalServices.Any(d => d.Id == serviceId)).AsQueryable();
                        }
                    }
                }
                if (!string.IsNullOrWhiteSpace(SelectedWorkerType))
                {
                    if (SelectedWorkerType == "1")
                    {
                        MedicalWorkers = MedicalWorkers.Where(c => c is Doctor).AsQueryable();
                    }
                    else if (SelectedWorkerType == "2")
                    {
                        MedicalWorkers = MedicalWorkers.Where(c => c is Nurse).AsQueryable();
                    }
                    else if (SelectedWorkerType == "3")
                    {
                        MedicalWorkers = MedicalWorkers.Where(c => c is Physiotherapist).AsQueryable();
                    }
                    else if (SelectedWorkerType == "4")
                    {
                        MedicalWorkers = MedicalWorkers.Where(c => c is ElectroradiologyTechnician).AsQueryable();
                    }
                    else if (SelectedWorkerType == "5")
                    {
                        MedicalWorkers = MedicalWorkers.Where(c=>c is DentalHygienist).AsQueryable();
                    }
                }
                if (SelectedAglomeration.HasValue)
                {
                    MedicalWorkers = MedicalWorkers.Where(c => c.Person.DefaultAglomeration == SelectedAglomeration.Value).AsQueryable();
                }
                FilteredMedicalWorkers = MedicalWorkers.ToList();

            }
            else
            {
                FilteredMedicalWorkers = MedicalWorkers.ToList();
            }
        }
    }
}
