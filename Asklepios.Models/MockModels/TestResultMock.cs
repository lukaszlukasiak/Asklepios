using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Core.MockModels
{
    public class TestResultMock
    {
       // public long Id { get; set; }
        [Display(Name = "Opis badania")]
        [Required(ErrorMessage = "Proszę wprowadzić opis/zakres badania")]
        [DataType(DataType.Text)]
        public string Description { get; set; }
        public string MedicalServiceName { get; set; }

        [Display(Name = "Wybierz typ badania")]
        public long? MedicalServiceId { get; set; }
        public DateTime ExamDate { get; set; }
        public string FilePath { get; set; }
        //private byte[] _document;
        //public byte[] Document
        //{
        //    get;
        //    set;
        //}

        //public long? MedicalWorkerId { get; set; }
        //public long? PatientId { get; set; }
        //public long? VisitId { get; set; }

        public bool IsModelValid
        {
            get
            {
                if (!string.IsNullOrWhiteSpace( FilePath))
                {
                    if (!string.IsNullOrEmpty(Description))
                    {
                        if (!string.IsNullOrWhiteSpace(MedicalServiceName))
                        {
                            if (MedicalServiceId.HasValue)
                            {
                                if (ExamDate.Ticks > 0)
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

        public MedicalTestResult TestResultFromMock()
        {
            MedicalTestResult result = new MedicalTestResult();
            result.MedicalServiceId = MedicalServiceId;
            //result.PatientId = PatientId;
            result.ExamDate = ExamDate;
            result.UploadDate=DateTime.Now;
            result.Description = Description;
            //result.MedicalWorkerId = MedicalWorkerId;
            //result.PatientId= PatientId;
            //result.VisitId= VisitId;
            result.DocumentPath = FilePath;
            return result;
        }
    }
}
