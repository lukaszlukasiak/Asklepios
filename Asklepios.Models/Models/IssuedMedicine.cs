using System.ComponentModel.DataAnnotations;

namespace Asklepios.Core.Models
{
    public class IssuedMedicine
    {
        public long Id { get; set; }
        [Display(Name = "Nazwa leku")]
        [Required(ErrorMessage = "Proszę wprowadzić nazwę leku")]
        [DataType(DataType.Text)]

        public string MedicineName { get; set; }
        [Display(Name = "Wielkość opakowania")]
        [Required(ErrorMessage = "Proszę wprowadzić wielkość opakowania")]
        [DataType(DataType.Text)]

        public string PackageSize { get; set; }
        [Display(Name = "Odpłatność (w %)")]
        [Required(ErrorMessage = "Proszę wprowadzić wielkość odpłatności (w %)")]
        [Range(0,100, ErrorMessage = "Proszę wprowadzić wielkość odpłatności(w %, 0-100)")]

        //public string Dosage { get; set; }
        public float? PaymentDiscount { get; set; }
    }
}