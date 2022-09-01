//using PdfSharp.Pdf;

using PdfSharpCore.Pdf;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asklepios.Core.Models
{
    public  class MedicalTestResult:ICloneable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public long Id { get; set; }
        [Display(Name = "Opis badania")]
        [Required(ErrorMessage = "Proszę wprowadzić opis/zakres badania")]
        [DataType(DataType.Text)]


        public string Description { get; set; }
        public long MedicalServiceId { get; set; }
        [ForeignKey("MedicalServiceId")]
        public virtual MedicalService MedicalService { get; set; }
        //private byte[] _document;
        public byte[] Document 
        {
            get;
            set;
        }
        public string DocumentPath { get; set; }
        //public VisitSummary VisitSummary { get; set; }
        public long MedicalWorkerId { get; set; }
        [ForeignKey("MedicalWorkerId")]
        public virtual MedicalWorker MedicalWorker { get; set; }
        public DateTimeOffset ExamDate { get; set; }
        public DateTimeOffset UploadDate { get; set; }
        public long PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }
        public long VisitId { get; set; }
        public Visit _visit;
        //[ForeignKey("VisitId")]
        public virtual Visit Visit
        {
            get;set;
            //get
            //{
            //    return _visit;
            //}
            //set
            //{
            //    _visit = value;
            //    MedicalWorker = value.MedicalWorker;
            //    Patient = value.Patient;
            //}
        }
        //public long MedicalTestResultId { get; set; }
        //[ForeignKey("MedicalTestResultId")]
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