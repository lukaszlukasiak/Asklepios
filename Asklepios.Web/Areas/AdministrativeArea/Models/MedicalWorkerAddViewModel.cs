using Asklepios.Core.Enums;
using Asklepios.Core.Models;
using Asklepios.Web.Enums;
using Asklepios.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.AdministrativeArea.Models
{
    public class MedicalWorkerAddViewModel:IBaseViewModel
    {
        //public string ErrorMessage { get; set; }
        //public string SuccessMessage { get; set; }
        public MedicalWorker MedicalWorker { get; set; }
        public MedicalWorkertData MedicalWorkertData { get; set; }
        public User User { get; set; }
        public Person Person { get; set; }
        public List<MedicalService> PrimaryServices { get; set; }

        public bool IsValid
        {
            get
            {
                if (MedicalWorker == null)
                {
                    return false;
                }
                if (MedicalWorker.IsValid)
                {
                    if (Person.IsValid)
                    {
                        if (User.IsValid)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        public string UserName { get; set; }
        //public string Message { get; set; }
        //public AlertMessageType AlertMessageType { get; set; }
        public ViewMessage ViewMessage { get; set; } = new ViewMessage();

        internal bool CreateMedicalWorker()
        {
            if (MedicalWorkertData == null)
            {
                return false;
            }
            if (!Person.IsValid)
            {
                return false;
            }
            if (!User.IsValid)
            {
                return false;
            }
            if (MedicalWorkertData.IsValid)
            {
                switch (MedicalWorkertData.MedicalWorkerType)
                {
                    case MedicalWorkerType.Doctor:
                        MedicalWorker = new Doctor(Person.Id, MedicalWorkertData.ProfessionalNumber);
                        break;
                    case MedicalWorkerType.Nurse:
                        MedicalWorker = new Nurse(Person.Id, MedicalWorkertData.ProfessionalNumber);
                        break;
                    case MedicalWorkerType.Physiotherapist:
                        MedicalWorker = new Physiotherapist(Person.Id, MedicalWorkertData.ProfessionalNumber);
                        break;
                    case MedicalWorkerType.ElectroriadologyTechnician:
                        MedicalWorker = new ElectroradiologyTechnician(Person.Id, MedicalWorkertData.ProfessionalNumber);
                        break;
                    case MedicalWorkerType.DentalHygienist:
                        MedicalWorker = new DentalHygienist(Person.Id, MedicalWorkertData.ProfessionalNumber);
                        break;
                    default:
                        break;
                }
                MedicalWorker.User = User;
            }
            if (!string.IsNullOrWhiteSpace(MedicalWorkertData.Education))
            {
                MedicalWorker.Education = MedicalWorkertData.Education;
            }
            if (!string.IsNullOrWhiteSpace(MedicalWorkertData.Experience))
            {
                MedicalWorker.Experience= MedicalWorkertData.Experience;
            }
            if (!string.IsNullOrWhiteSpace(MedicalWorkertData.ProfessionalNumber))
            {
                MedicalWorker.ProfessionalNumber = MedicalWorkertData.ProfessionalNumber;
            }
            if (MedicalWorkertData.MedicalServiceIds!=null )
            {
                MedicalWorker.MedicalServices = new List<MedicalService>();
                for (int i = 0; i < MedicalWorkertData.MedicalServiceIds.Length; i++)
                {
                    long id = MedicalWorkertData.MedicalServiceIds[i];
                    MedicalService medicalService = PrimaryServices.Where(c => c.Id ==id).FirstOrDefault();
                    if (medicalService!=null)
                    {
                        MedicalWorker.MedicalServices.Add(medicalService);
                    }
                }
            }
            MedicalWorker.IsActive = MedicalWorkertData.IsActive;

            return true;
        }
    }
}
