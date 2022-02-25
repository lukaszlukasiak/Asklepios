using Asklepios.Core.Models;
using Asklepios.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.AdministrativeArea.Models
{
    public class PackageItemsManageViewModel
    {
        [Display(Name = "Id pakietu medycznego")]
        public long SelectedPackageId { get; set; }
        public MedicalPackage SelectedPackage { get; set; }
        //public MedicalRoom SelectedRoom { get; set; }
        public MedicalPackage NewPackage { get; set; }
        public List<MedicalPackage> MedicalPackages { get; set; }
        public List<MedicalPackage> SortedMedicalPackages { get; set; }
        public List<MedicalService> MedicalServices {get;set;}
        public List<MedicalService> SortedMedicalServices 
        { 
            get
            {
                if (MedicalServices!=null)
                {
                    return MedicalServices.OrderBy(c => c.Name).ToList();
                }
                else
                {
                    return null;
                }
            }
        }

        public List<MedicalServiceDiscount> MedicalServiceDiscounts
        {
            get
            {
                if (SortedMedicalServices==null)
                {
                    return null;
                }
                List<MedicalServiceDiscount> discounts = new List<MedicalServiceDiscount>();

                foreach (MedicalService ser in SortedMedicalServices)
                {
                    MedicalServiceDiscount discount = new MedicalServiceDiscount() { MedicalService = ser };
                    discounts.Add(discount);
                }

                return discounts;
            }


        }
        public List<MedicalService> SortedServices
        {
            get
            {
                if (MedicalServices != null)
                {
                    return MedicalServices.OrderBy(c => c.Name).ToList();
                }
                else
                {
                    return null;
                }
            }
        }
   
        public ViewMode ViewMode { get; set; }
        //public bool IsValid 
        //{ 
        //    get
        //    {
        //        if (!)
        //        {

        //        }
        //    }
        //}

        public List<int> Vals { get; set; } //= new List<long>() { 1, 2, 3, 4, 5, 6, 7 };

        internal List<MedicalServiceDiscount> UpdateDiscountsWithInputValues()
        {
            if (Vals.Count==MedicalServiceDiscounts.Count)
            {
                List<MedicalServiceDiscount> discounts = new List<MedicalServiceDiscount>();
                for (int i = 0; i < MedicalServiceDiscounts.Count; i++)
                {
                    MedicalServiceDiscount discount = MedicalServiceDiscounts[i];
                    discount.Discount =new decimal (Vals[i]/100.0);
                    discounts.Add(discount);
                    discount.MedicalPackage = SelectedPackage;
                    discount.MedicalPackageId = SelectedPackage.Id;
                    //discount.Id
                }
                return discounts;
            }
            else
            {
                return null;
            }
        }
    }
}
