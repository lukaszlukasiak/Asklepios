using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Asklepios.Core.Models
{
    public class MedicalPackage
    {
        [Display(Name = "Id pakietu medycznego")]

        public long Id { get; set; }
        [Required(ErrorMessage ="Proszę podać nazwę pakietu medycznego")]
        [Display(Name="Nazwa pakietu medycznego")]
        public String Name { get; set; }
        [Required(ErrorMessage = "Proszę podać opis pakietu medycznego")]
        [Display(Name = "Opis pakietu medycznego")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        //public Dictionary<MedicalService, decimal> ServicesDiscounts { get; set; }
        public List<MedicalServiceDiscount> MedicalServiceDiscounts { get; set; }
        public bool IsValid
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Name))
                {
                    if (!string.IsNullOrWhiteSpace(Description))
                    {
                        if (MedicalServiceDiscounts==null)
                        {
                            return false;
                        }
                        foreach (MedicalServiceDiscount discount in MedicalServiceDiscounts)
                        {
                            if (discount.Discount>=0 && discount.Discount<=1)
                            {

                            }
                            else
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
