using System;
using System.ComponentModel.DataAnnotations;

namespace Asklepios.Core.Models
{
    public class MedicalServiceDiscount
    {
        public long Id { get; set; }
        [Display(Name = "Usługa")]
        public long MedicalServiceId { get; set; }
        public MedicalService MedicalService {get;set;}
        [Required(ErrorMessage = "Proszę podać wysokość rabatu (0-100%)")]
        [Display(Name = "Wysokość rabatu (0.2=20%)")]
        [Range(0,100)]
        public decimal Discount { get; set; }

        public MedicalPackage MedicalPackage { get; set; }
        public long MedicalPackageId { get; set; }
    }
}
