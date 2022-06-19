using Asklepios.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Core.Models
{
    public class Notification
    {
        public NotificationType NotificationType { get; set; }
        public long Id { get; set; }
        public object EventObject { get; set; }
        public bool IsRead { get; set; }
        public long PatientId {get;set;}
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
                    default:
                        break;
                }
                return "";
            }
        }
        public long VisitId { get; set; }
        public Visit Visit
        {
            get
            {
                switch (NotificationType)
                {
                    case NotificationType.Prescription:
                        Prescription prescription = EventObject as Prescription;
                        return prescription.Visit;
                    case NotificationType.TestResult:
                        MedicalTestResult testResult = EventObject as MedicalTestResult;
                        return testResult.Visit;
                    case NotificationType.MedicalReferral:
                        MedicalReferral referral  = EventObject as MedicalReferral;
                        return referral.VisitWhenIssued;
                    default:
                        break;
                }
                return null;
            }
        }
        public long EventObjectId 
        {
            get;set;
            //get
            //{
            //    if (EventObject is MedicalReferral medicalReferral)
            //    {
            //        return medicalReferral.Id;
            //    }
            //    else if         (EventObject is Prescription prescription)
            //    {
            //        return prescription.Id;
            //    }
            //    else if (EventObject is MedicalTestResult testResult)
            //    {
            //        return testResult.Id;
            //    }
            //    else
            //    {
            //        return -1;
            //    }
            //}
        }
    }
}
