using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Core.MockModels
{
    public class ReferralMock
    {

        //public long Id { get; set; }
        [Display(Name = "Typ usługi")]

        public long? PrimaryMedicalServiceId { get; set; }
        public string PrimaryMedicalServiceName { get; set; }

        public long? MinorMedicalServiceId { get; set; }
        public string MinorMedicalServiceName { get; set;}

        public long? IssuedById { get; set; }

        public long? IssuedToId
        {
            get;
            set;
        }
        [Display(Name = "Okres ważności")]
        [Range(7, 365, ErrorMessage = "Ważność skierowania powinna wynosić między 7 a 365 dni.")]
        public long ServiceReferralDaysToExpire { get; set; }
        [Display(Name = "Komentarz")]
        [DataType(DataType.Text)]
        public string Comment { get; set; }

        public DateTime ExpireDate { get; set; }
        public DateTime IssueDate { get; set; }
        public bool IsModelvalid 
        {
            get
            {
                if (PrimaryMedicalServiceId.HasValue)
                {
                    if (ServiceReferralDaysToExpire>=7 && ServiceReferralDaysToExpire<=365)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        //public long? VisitWhenUsedId { get; set; }

        //public long? VisitWhenIssuedId { get; set; }


        public MedicalReferral ReferralFromMock()
        {
            MedicalReferral referral = new MedicalReferral();
            referral.Comment = Comment;
            referral.PrimaryMedicalServiceId = PrimaryMedicalServiceId;
            referral.MinorMedicalServiceId = MinorMedicalServiceId;
            referral.ExpireDate = DateTime.Now.AddDays(ServiceReferralDaysToExpire);
            referral.IssueDate = DateTime.Now;

            return referral;
        }
        //public VisitSummary VisitSummary { get; set; }


    }
}
