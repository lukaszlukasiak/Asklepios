using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Core.MockModels
{
    public class MedicineMock
    {
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
        [Range(0, 100, ErrorMessage = "Proszę wprowadzić wielkość odpłatności(w %, 0-100)")]

        //public string Dosage { get; set; }
        public float PaymentDiscount { get; set; }

        public bool IsModelValid 
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(MedicineName))
                {
                    if (!string.IsNullOrWhiteSpace(PackageSize))
                    {
                        if (PaymentDiscount>=0)
                        {
                            return true;
                        }
                    }
                }
                return false;
            } 
        }
    }
}
