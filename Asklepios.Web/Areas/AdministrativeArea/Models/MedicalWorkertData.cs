using Asklepios.Core.Enums;
using Asklepios.Core.Models;
using Asklepios.Web.Enums;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Asklepios.Web.Areas.AdministrativeArea.Models
{
    public class MedicalWorkertData : IBaseViewModel
    {
        public long Id { get; set; }
        [Display(Name = "Czy zatrudniony obecnie")]
        public bool IsCurrentlyHired { get; set; }
        [Required(ErrorMessage = "Wprowadź typ pracownika medycznego!")]
        [Display(Name = "Typ pracownika medycznego")]

        public MedicalWorkerType? MedicalWorkerType { get; set; }
        //public List<string> Education { get; set; }
        [Required(ErrorMessage = "Wprowadź streszczenie odnośnie zdobytego wykształcenia, przebytych kursów itp.!")]
        [Display(Name = "Edukacja/wykształcenie")]

        public string Education { get; set; }
        [Required(ErrorMessage = "Wprowadź streszczenie odnośnie zdobytego doświadczenia!")]
        [Display(Name = "Doświadczenie")]

        public string Experience { get; set; }
        //public string ImagePath { get; set; }
        [Required(ErrorMessage = "Specjalizacja/świadczone usługi")]
        [Display(Name = "Usługi")]
        public long[] MedicalServiceIds { get; set; }

        //public List<MedicalService> MedicalServices { get; set; }
        public IFormFile Image { get; set; }
        //public string ImagePath { get; set; }
        [Required(ErrorMessage = "Wprowadź numer zawodowy!")]
        [Display(Name = "Numer zawodowy")]

        public string ProfessionalNumber { get; set; }
        public bool IsValid 
        { 
            get
            {
                if (!string.IsNullOrWhiteSpace(ProfessionalNumber))
                {
                    if (MedicalServiceIds.Length>0)
                    {
                        if (!string.IsNullOrWhiteSpace(Education))
                        {
                            if (!string.IsNullOrWhiteSpace(Experience))
                            {
                                if (MedicalWorkerType.HasValue)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
                return false;
            }
        }

        public string UserName { get; set; }
        public string Message { get; set; }
        public AlertMessageType AlertMessageType { get; set; }

        public MedicalWorkertData()
        {

        }
        public MedicalWorkertData(MedicalWorker medicalWorker)
        {
            if (medicalWorker is Nurse)
            {
                this.MedicalWorkerType = Core.Enums.MedicalWorkerType.Nurse;
            }
            if (medicalWorker is Doctor)
            {
                this.MedicalWorkerType = Core.Enums.MedicalWorkerType.Doctor;
            }
            if (medicalWorker is DentalHygienist)
            {
                this.MedicalWorkerType = Core.Enums.MedicalWorkerType.DentalHygienist;
            }
            if (medicalWorker is Physiotherapist)
            {
                this.MedicalWorkerType = Core.Enums.MedicalWorkerType.Physiotherapist;
            }
            if (medicalWorker is ElectroradiologyTechnician)
            {
                this.MedicalWorkerType = Core.Enums.MedicalWorkerType.ElectroriadologyTechnician;
            }
            this.Education = medicalWorker.Education;
            this.Experience = medicalWorker.Experience;
            this.IsCurrentlyHired = medicalWorker.IsCurrentlyHired;

            this.MedicalServiceIds = medicalWorker.MedicalServices.Select(c => c.Id).ToArray();
            this.ProfessionalNumber = medicalWorker.ProfessionalNumber;
           
        }

        public void UpdateWorkerWithData(MedicalWorker worker, List<MedicalService> primaryServices)
        {
            if (!string.IsNullOrWhiteSpace(this.Education))
            {
                worker.Education = this.Education;
            }
            if (!string.IsNullOrWhiteSpace(this.Experience))
            {
                worker.Experience = this.Experience;
            }
            if (!string.IsNullOrWhiteSpace(this.ProfessionalNumber))
            {
                worker.ProfessionalNumber = this.ProfessionalNumber;
            }
            if (this.MedicalServiceIds != null)
            {
                worker.MedicalServices = new List<MedicalService>();
                for (int i = 0; i < this.MedicalServiceIds.Length; i++)
                {
                    long id = this.MedicalServiceIds[i];
                    MedicalService medicalService = primaryServices.Where(c => c.Id == id).FirstOrDefault();
                    if (medicalService != null)
                    {
                        worker.MedicalServices.Add(medicalService);
                    }
                }
            }
        }
    }
}