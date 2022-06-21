//using PdfSharp.Pdf;

using PdfSharpCore.Pdf;
using System;
using System.ComponentModel.DataAnnotations;

namespace Asklepios.Core.Models
{
    public  class MedicalTestResult:ICloneable
    {
        public long Id { get; set; }
        [Display(Name = "Opis badania")]
        [Required(ErrorMessage = "Proszę wprowadzić opis/zakres badania")]
        [DataType(DataType.Text)]


        public string Description { get; set; }
        public MedicalService MedicalService { get; set; }
        //private byte[] _document;
        public byte[] Document 
        {
            get;
            set;
        }
        public string DocumentPath { get; set; }
        //public VisitSummary VisitSummary { get; set; }
        public MedicalWorker MedicalWorker { get; set; }
        public DateTimeOffset ExamDate { get; set; }
        public DateTimeOffset UploadDate { get; set; }
        public Patient Patient { get; set; }
        public long VisitId { get; set; }
        public Visit _visit;
        public Visit Visit
        {
            get
            {
                return _visit;
            }
            set
            {
                _visit = value;
                MedicalWorker = value.MedicalWorker;
                Patient = value.Patient;
            }
        }

        public MedicalTestResult()
        {

        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        //public MedicalTestResult(MedicalTestResult result, long id)
        //{
        //    this = result.MemberwiseClone();

        //}

    }
}