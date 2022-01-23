using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Asklepios.Core.Models
{
    public class MedicalPackage
    {
        public long Id { get; set; }
        [Required(ErrorMessage ="Proszę podać nazwę pakietu medycznego")]
        [Display(Name="Nazwa pakietu medycnzego")]
        public String Name { get; set; }
        [Required(ErrorMessage = "Proszę podać opis pakietu medycznego")]
        [Display(Name = "Opis pakietu medycnzego")]
        public string Description { get; set; }
        public Dictionary<MedicalService, decimal> ServicesDiscounts { get; set; }
    }
}
