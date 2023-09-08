using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asklepios.Core.Models
{
    public class Prescription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]

        public long Id { get; set; }

        public DateTimeOffset IssueDate { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }
        public long? IssuedById { get; set; }
        [ForeignKey("IssuedById")]
        public MedicalWorker IssuedBy { get; set; }
        public long? IssuedToId { get; set; }
        [ForeignKey("IssuedToId")]
        public Patient IssuedTo { get; set; }
        
        public virtual List<IssuedMedicine> IssuedMedicines { get; set; }
        
        [Display(Name = "Kod dostępu")]
        [Required(ErrorMessage = "Proszę wprowadzić kod dostępu")]
        [DataType(DataType.Text)]
        [StringLength(4, ErrorMessage ="Kod dostępu powinien się składać z 4 cyfr")]
        [Range(0,9999, ErrorMessage = "Kod dostępu powinien się składać z 4 cyfr")]

        public string AccessCode { get; set; }
        [Display(Name = "Numer identyfikacyjny")]
        [Required(ErrorMessage = "Proszę wprowadzić numer identyfikacyjny")]
        [DataType(DataType.Text)]
        [StringLength(20, ErrorMessage ="Numer identyfikacyjny powinien się składać z 20 znaków")]

        public string IdentificationCode { get; set; }
        public long? VisitId { get; set; }

        public Visit Visit
        {
            get;set;
        }
        public Prescription()
        {
            IssuedMedicines = new List<IssuedMedicine>();
        }
        public Prescription MockClone( long pId, long iId )
        {
            Prescription prescription = new Prescription();
            prescription.Id = pId;
            prescription.AccessCode = AccessCode;
            prescription.IdentificationCode = IdentificationCode;
            prescription.ExpirationDate = ExpirationDate;
            prescription.IssueDate = IssueDate;

            for (int i = 0; i < IssuedMedicines.Count; i++)
            {
                IssuedMedicine issuedMedicine = new IssuedMedicine();
                issuedMedicine.Id = iId+i;
                issuedMedicine.PrescriptionId = pId;
                issuedMedicine.MedicineName = IssuedMedicines[i].MedicineName;
                issuedMedicine.PaymentDiscount= IssuedMedicines[i].PaymentDiscount;
                issuedMedicine.PackageSize= IssuedMedicines[i].PackageSize;
                prescription.IssuedMedicines.Add(issuedMedicine);
            }

            return prescription;
        }
    }
}