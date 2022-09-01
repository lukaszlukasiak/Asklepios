using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Core.Models
{
    public class MedicalReferral
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]

        public long Id { get; set; }

        public MedicalService PrimaryMedicalService { get; set; }
        public MedicalService MinorMedicalService { get; set; }
        public DateTimeOffset IssueDate { get; set; }
        public DateTimeOffset ExpireDate { get; set; }
        public bool IsActive 
        {
            get
            {
                if (HasBeenUsed==false && DateTimeOffset.Now<ExpireDate)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool HasExpired
        {
            get
            {
                if (DateTimeOffset.Now>ExpireDate)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool HasBeenUsed { get; set; }

        public long IssuedById { get; set; }
        [ForeignKey("IssuedById")]
        public MedicalWorker IssuedBy { get; set; }
        [ForeignKey("IssuedToId")]

        public Patient IssuedTo { get; set; }
        public long IssuedToId { get; set; }

        [Display(Name = "Komentarz")]
        [DataType(DataType.Text)]
        public string Comment { get; set; }

        private Visit _visitUsed;
        [ForeignKey("VisitWhenUsedId")]
        public Visit VisitWhenUsed
        {
            get
            {
                return _visitUsed;
            }
            set
            {
                _visitUsed = value;
            }
        }
        public long VisitWhenUsedId { get; set; }

        public Visit _visitWhenIssued;

        [ForeignKey("VisitWhenIssuedId")]

        public Visit VisitWhenIssued 
        {
            get
            {
                return _visitWhenIssued;
            }
            set
            {
                _visitWhenIssued = value;
                //IssuedBy = value.MedicalWorker;
                //IssuedTo = value.Patient;
                //IssueDate = value.DateTimeSince;
            }
        }
        public long VisitWhenIssuedId { get; set; }

        public MedicalReferral MockClone(long id)
        {
            MedicalReferral referral=new MedicalReferral();
            referral.Id = id;
            referral.Comment = Comment;
            referral.ExpireDate = ExpireDate;
            referral.IssueDate= IssueDate;
            referral.PrimaryMedicalService = PrimaryMedicalService;
            referral.MinorMedicalService = MinorMedicalService;
            
            return referral;
        }
        //public VisitSummary VisitSummary { get; set; }

    }
}
