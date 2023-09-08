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
        public long? MedicalServiceId { get; set; }
        [ForeignKey("MedicalServiceId")]
        public virtual MedicalService MedicalService { get; set; }
        [NotMapped]
        public byte[] Document 
        {
            get;
            set;
        }
        public string DocumentPath { get; set; }
        public long? MedicalWorkerId { get; set; }
        [ForeignKey("MedicalWorkerId")]
        public virtual MedicalWorker MedicalWorker { get; set; }

        public DateTimeOffset ExamDate { get; set; }
        public DateTimeOffset UploadDate { get; set; }

        public long? PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }

        public long? VisitId { get; set; }
        public virtual Visit Visit
        {
            get;set;
        }
        public MedicalTestResult()
        {

        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}