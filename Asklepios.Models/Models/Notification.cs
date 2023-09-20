using Asklepios.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Core.Models
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public long Id { get; set; }
        public NotificationType NotificationType { get; set; }
        [NotMapped]
        public object EventObject { get; set; }
        public bool IsRead { get; set; }
        public long PatientId {get;set;}
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
        public DateTimeOffset DateTimeAdded { get; set; }
        public string Description
        {
            get
            {
                switch (NotificationType)
                {
                    case NotificationType.Prescription:
                        return "Nowa recepta!";
                    case NotificationType.TestResult:
                        return "Nowe wyniki badań!";
                    case NotificationType.MedicalReferral:
                        return "Nowe skierowanie!";
                    case NotificationType.VisitCancelled:
                        return "Nowa odwołana wizyta!";

                    default:
                        break;
                }
                return "";
            }
        }
        public string DescriptionOld
        {
            get
            {
                switch (NotificationType)
                {
                    case NotificationType.Prescription:
                        return "Recepta";
                    case NotificationType.TestResult:
                        return "Wyniki badań";
                    case NotificationType.MedicalReferral:
                        return "Skierowanie";
                    case NotificationType.VisitCancelled:
                        return "Odwołana wizyta!";
                    default:
                        break;
                }
                return "";
            }
        }
        public long VisitId { get; set; }
        [ForeignKey("VisitId")]
        public virtual Visit Visit
        {

            get;set;
        }
        public long EventObjectId 
        {
            get;set;

        }
    }
}
