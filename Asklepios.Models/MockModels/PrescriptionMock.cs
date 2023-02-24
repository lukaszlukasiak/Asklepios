using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Core.MockModels
{
    public class PrescriptionMock
    {
        [Display(Name = "Kod dostępu")]
        [Required(ErrorMessage = "Proszę wprowadzić kod dostępu")]
        [DataType(DataType.Text)]
        [StringLength(4, ErrorMessage = "Kod dostępu powinien się składać z 4 cyfr")]
        [Range(0, 9999, ErrorMessage = "Kod dostępu powinien się składać z 4 cyfr")]

        public string AccessCode { get; set; }
        [Display(Name = "Numer identyfikacyjny")]
        [Required(ErrorMessage = "Proszę wprowadzić numer identyfikacyjny")]
        [DataType(DataType.Text)]
        [StringLength(20, ErrorMessage = "Numer identyfikacyjny powinien się składać z 20 znaków")]

        public string IdentificationCode { get; set; }
        [Display(Name = "Okres ważności")]
        [Range(7, 365, ErrorMessage = "Ważność recepty powinna wynosić między 7 a 365 dni.")]

        public int DaysToExpire { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public bool IsModelValid
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(AccessCode))
                {
                    if (DaysToExpire>=7 && DaysToExpire <=365)
                    {
                        if (!string.IsNullOrWhiteSpace(IdentificationCode))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        public Prescription PrescriptionFromMock(List<MedicineMock> medicineMocks)
        {
            Prescription prescription = new Prescription();
            prescription.AccessCode = AccessCode;
            prescription.IdentificationCode = IdentificationCode;
            prescription.ExpirationDate = DateTime.Now.AddDays(DaysToExpire);
            prescription.IssueDate = DateTime.Now;

            if (medicineMocks==null || medicineMocks.Count==0)
            {
                return prescription; 
            }
            for (int i = 0; i < medicineMocks.Count; i++)
            {
                IssuedMedicine issuedMedicine = new IssuedMedicine();
                issuedMedicine.MedicineName = medicineMocks[i].MedicineName;
                issuedMedicine.PaymentDiscount = medicineMocks[i].PaymentDiscount;
                issuedMedicine.PackageSize = medicineMocks[i].PackageSize;
                prescription.IssuedMedicines.Add(issuedMedicine);
            }

            return prescription;
        }

    }
}
