using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asklepios.Core.Models
{
    public class MedicalServiceDiscount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]

        public long Id { get; set; }
        [Display(Name = "Usługa")]
        public long MedicalServiceId { get; set; }
        [ForeignKey("MedicalServiceId")]
        public virtual MedicalService MedicalService {get;set;}
        [Required(ErrorMessage = "Proszę podać wysokość rabatu (0-100%)")]
        [Display(Name = "Wysokość rabatu (0.2=20%)")]
        [Range(0,100)]
        [DisplayFormat(DataFormatString = "{0:P2}")]

        public decimal Discount { get; set; }
        public long MedicalPackageId { get; set; }
        [ForeignKey("MedicalPackageId")]
        public virtual MedicalPackage MedicalPackage { get; set; }
    }
}
